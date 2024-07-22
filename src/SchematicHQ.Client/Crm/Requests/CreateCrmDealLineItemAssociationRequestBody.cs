using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateCrmDealLineItemAssociationRequestBody
{
    [JsonPropertyName("deal_external_id")]
    public required string DealExternalId { get; init; }

    [JsonPropertyName("line_item_external_id")]
    public required string LineItemExternalId { get; init; }
}
