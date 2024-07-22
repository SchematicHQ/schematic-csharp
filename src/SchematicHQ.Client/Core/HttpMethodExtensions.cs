using System.Net.Http;

namespace SchematicHQ.Client.Core;

public static class HttpMethodExtensions
{
    public static readonly HttpMethod Patch = new("PATCH");
}
