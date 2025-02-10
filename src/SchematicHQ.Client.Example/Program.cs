using SchematicHQ.Client;
using SchematicHQ.Client.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchematicHQ.Example;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Schematic Event Buffer Test App");
        Console.WriteLine("------------------------------");

        string apiKey = Environment.GetEnvironmentVariable("SCHEMATIC_API_KEY") ??
            throw new Exception("Please set SCHEMATIC_API_KEY environment variable");

        string companyId = Environment.GetEnvironmentVariable("SCHEMATIC_COMPANY_ID") ??
            throw new Exception("Please set SCHEMATIC_COMPANY_ID environment variable");

        Schematic schematic = new Schematic(apiKey);

        try
        {
            Console.WriteLine("Sending test event...");

            schematic.Track(
                eventName: "test-event",
                company: new Dictionary<string, string> {
                    { "id", companyId }
                }
            );

            Console.WriteLine("Event queued in buffer.");
            Console.WriteLine($"Current buffer size: {schematic.GetBufferWaitingEventCount()}");

            await Task.Delay(1000);
            Console.WriteLine($"Buffer size after 1 second: {schematic.GetBufferWaitingEventCount()}");

            Console.WriteLine("Shutting down Schematic (this will flush the buffer)...");
            await schematic.Shutdown();

            Console.WriteLine("Shutdown complete. Check your Schematic dashboard for the event.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Environment.Exit(1);
        }
    }
}
