using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenFeature;
using OpenFeature.Model;
using SchematicHQ.Client;
using SchematicHQ.Client.OpenFeature;

namespace Examples.OpenFeature
{
    public class OpenFeatureExample
    {
        public static async Task Main(string[] args)
        {
            // Initialize the Schematic OpenFeature provider
            var apiKey = Environment.GetEnvironmentVariable("SCHEMATIC_API_KEY") ?? "your-api-key";
            var provider = new SchematicProvider(apiKey);
            
            // Set the provider with OpenFeature
            await Api.Instance.SetProviderAsync(provider);
            
            // Create evaluation context
            var context = EvaluationContext.Builder()
                .Set("company", new Structure(new Dictionary<string, Value>
                {
                    ["id"] = new Value("company-123"),
                    ["name"] = new Value("Acme Corp"),
                    ["plan"] = new Value("enterprise")
                }))
                .Set("user", new Structure(new Dictionary<string, Value>
                {
                    ["id"] = new Value("user-456"),
                    ["email"] = new Value("user@example.com"),
                    ["role"] = new Value("admin")
                }))
                .Build();
            
            // Get the OpenFeature client
            var client = Api.Instance.GetClient();
            
            // Evaluate a feature flag
            var isFeatureEnabled = await client.GetBooleanValue("new-feature", false, context);
            Console.WriteLine($"Feature 'new-feature' is {(isFeatureEnabled ? "enabled" : "disabled")}");
            
            // Get detailed flag evaluation
            var flagDetails = await client.GetBooleanDetails("new-feature", false, context);
            Console.WriteLine($"Flag evaluation reason: {flagDetails.Reason}");
            
            // Track an event using the provider
            await provider.TrackEventAsync("feature_viewed", context, new Dictionary<string, object>
            {
                ["feature"] = "new-feature",
                ["enabled"] = isFeatureEnabled
            });
            
            // Shutdown
            await Api.Instance.Shutdown();
        }
    }
}