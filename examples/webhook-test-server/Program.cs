using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SchematicHQ.Client.Webhooks.WebhookUtils;

namespace SchematicHQ.WebhookTestServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Parse command line arguments
            int port = 8080;
            string? secret = Environment.GetEnvironmentVariable("SCHEMATIC_WEBHOOK_SECRET");

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--port" when i + 1 < args.Length && int.TryParse(args[i + 1], out int parsedPort):
                        port = parsedPort;
                        i++;
                        break;
                    case "--secret" when i + 1 < args.Length:
                        secret = args[i + 1];
                        i++;
                        break;
                    case "--help":
                        PrintUsage();
                        return;
                }
            }

            // Start the server
            var server = new WebhookTestServer(port, secret);
            await server.Start();

            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            
            server.Stop();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Webhook Test Server for Schematic");
            Console.WriteLine();
            Console.WriteLine("Usage: dotnet run -- [OPTIONS]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --port PORT     Port to listen on (default: 8080)");
            Console.WriteLine("  --secret SECRET Webhook secret (default: reads from SCHEMATIC_WEBHOOK_SECRET env var)");
            Console.WriteLine("  --help          Print this help message");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  dotnet run -- --port 8080 --secret my_webhook_secret");
            Console.WriteLine();
            Console.WriteLine("  # Or using environment variables:");
            Console.WriteLine("  export SCHEMATIC_WEBHOOK_SECRET=my_webhook_secret");
            Console.WriteLine("  dotnet run");
            Console.WriteLine();
            Console.WriteLine("Notes:");
            Console.WriteLine("  For testing with Schematic, you can use a tool like ngrok to expose");
            Console.WriteLine("  your local server to the internet.");
        }
    }

    public class WebhookTestServer
    {
        private readonly int _port;
        private readonly string? _secret;
        private readonly HttpListener _listener;
        private bool _isRunning;

        public WebhookTestServer(int port, string? secret)
        {
            _port = port;
            _secret = secret;
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{_port}/");
        }

        public async Task Start()
        {
            if (_isRunning)
            {
                return;
            }

            _listener.Start();
            _isRunning = true;

            Console.WriteLine($"Webhook test server started on port {_port}");
            Console.WriteLine($"Ready to receive webhooks at http://localhost:{_port}/webhook");
            
            if (!string.IsNullOrEmpty(_secret))
            {
                Console.WriteLine($"Using webhook secret: {_secret}");
            }
            else
            {
                Console.WriteLine("WARNING: No webhook secret provided. Signature verification will be skipped.");
            }

            // Start processing requests
            _ = Task.Run(ProcessRequestsAsync);
        }

        public void Stop()
        {
            if (!_isRunning)
            {
                return;
            }

            _listener.Stop();
            _isRunning = false;
            Console.WriteLine("Server stopped");
        }

        private async Task ProcessRequestsAsync()
        {
            while (_isRunning)
            {
                try
                {
                    // Wait for a request
                    var context = await _listener.GetContextAsync();
                    _ = Task.Run(() => HandleRequestAsync(context));
                }
                catch (Exception ex) when (_isRunning)
                {
                    Console.WriteLine($"Error processing request: {ex.Message}");
                }
            }
        }

        private async Task HandleRequestAsync(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            // Only handle POST requests to /webhook
            if (request.HttpMethod != "POST" || !request.Url.AbsolutePath.Equals("/webhook", StringComparison.OrdinalIgnoreCase))
            {
                response.StatusCode = 404;
                response.Close();
                return;
            }

            try
            {
                // Read the request body
                string body;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    body = await reader.ReadToEndAsync();
                }

                // Log headers
                Console.WriteLine("\nHeaders:");
                foreach (string key in request.Headers.Keys)
                {
                    Console.WriteLine($"  {key}: {request.Headers[key]}");
                }

                // Extract headers into a dictionary for the verifier
                var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (string key in request.Headers.Keys)
                {
                    headers[key] = request.Headers[key];
                }

                // Verify signature if webhook secret is provided
                if (!string.IsNullOrEmpty(_secret))
                {
                    try
                    {
                        WebhookVerifier.VerifyWebhookSignature(body, headers, _secret);
                        Console.WriteLine("✅ Signature verification successful!");
                    }
                    catch (WebhookSignatureException ex)
                    {
                        Console.WriteLine($"❌ Signature verification failed: {ex.Message}");
                        await SendJsonResponseAsync(response, 401, new { error = ex.Message });
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ No webhook secret provided, skipping signature verification");
                }

                // Try to parse and pretty-print the JSON payload
                try
                {
                    var payload = JsonSerializer.Deserialize<object>(body);
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Converters = { new JsonStringEnumConverter() }
                    };
                    string prettyJson = JsonSerializer.Serialize(payload, options);
                    
                    Console.WriteLine("\nPayload:");
                    Console.WriteLine(prettyJson);
                }
                catch (JsonException)
                {
                    Console.WriteLine("\nRaw body (not JSON):");
                    Console.WriteLine(body);
                }

                // Send success response
                await SendJsonResponseAsync(response, 200, new { status = "success" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling request: {ex.Message}");
                await SendJsonResponseAsync(response, 500, new { error = "Internal server error" });
            }
        }

        private async Task SendJsonResponseAsync(HttpListenerResponse response, int statusCode, object data)
        {
            response.StatusCode = statusCode;
            response.ContentType = "application/json";

            string json = JsonSerializer.Serialize(data);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.Close();
        }
    }
}