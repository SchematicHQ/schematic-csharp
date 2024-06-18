using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CreateCrmDealLineItemAssociationRequestBody
{
    [JsonPropertyName("deal_external_id")]
    public string DealExternalId { get; init; }

    [JsonPropertyName("line_item_external_id")]
    public string LineItemExternalId { get; init; }
}
