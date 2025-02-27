using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreateBillingSubscriptionsRequestBodyTrialEndSetting>))]
public enum CreateBillingSubscriptionsRequestBodyTrialEndSetting
{
    [EnumMember(Value = "subscribe")]
    Subscribe,

    [EnumMember(Value = "cancel")]
    Cancel,
}
