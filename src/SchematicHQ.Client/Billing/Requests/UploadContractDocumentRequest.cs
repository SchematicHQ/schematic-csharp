using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UploadContractDocumentRequest
{
    public required FileParameter File { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
