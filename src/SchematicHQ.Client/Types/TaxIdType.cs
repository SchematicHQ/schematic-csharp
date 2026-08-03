using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(TaxIdType.TaxIdTypeSerializer))]
[Serializable]
public readonly record struct TaxIdType : IStringEnum
{
    public static readonly TaxIdType AeTrn = new(Values.AeTrn);

    public static readonly TaxIdType AuAbn = new(Values.AuAbn);

    public static readonly TaxIdType AuArn = new(Values.AuArn);

    public static readonly TaxIdType BrCnpj = new(Values.BrCnpj);

    public static readonly TaxIdType BrCpf = new(Values.BrCpf);

    public static readonly TaxIdType CaBn = new(Values.CaBn);

    public static readonly TaxIdType CaGstHst = new(Values.CaGstHst);

    public static readonly TaxIdType CaPstBc = new(Values.CaPstBc);

    public static readonly TaxIdType CaPstMb = new(Values.CaPstMb);

    public static readonly TaxIdType CaPstSk = new(Values.CaPstSk);

    public static readonly TaxIdType CaQst = new(Values.CaQst);

    public static readonly TaxIdType ChUid = new(Values.ChUid);

    public static readonly TaxIdType ChVat = new(Values.ChVat);

    public static readonly TaxIdType EuVat = new(Values.EuVat);

    public static readonly TaxIdType GbVat = new(Values.GbVat);

    public static readonly TaxIdType HkBr = new(Values.HkBr);

    public static readonly TaxIdType IdNpwp = new(Values.IdNpwp);

    public static readonly TaxIdType IlVat = new(Values.IlVat);

    public static readonly TaxIdType InGst = new(Values.InGst);

    public static readonly TaxIdType JpCn = new(Values.JpCn);

    public static readonly TaxIdType JpRn = new(Values.JpRn);

    public static readonly TaxIdType JpTrn = new(Values.JpTrn);

    public static readonly TaxIdType KrBrn = new(Values.KrBrn);

    public static readonly TaxIdType MxRfc = new(Values.MxRfc);

    public static readonly TaxIdType MyFrp = new(Values.MyFrp);

    public static readonly TaxIdType MyItn = new(Values.MyItn);

    public static readonly TaxIdType MySst = new(Values.MySst);

    public static readonly TaxIdType NoVat = new(Values.NoVat);

    public static readonly TaxIdType NzGst = new(Values.NzGst);

    public static readonly TaxIdType PhTin = new(Values.PhTin);

    public static readonly TaxIdType SaVat = new(Values.SaVat);

    public static readonly TaxIdType SgGst = new(Values.SgGst);

    public static readonly TaxIdType SgUen = new(Values.SgUen);

    public static readonly TaxIdType ThVat = new(Values.ThVat);

    public static readonly TaxIdType TrTin = new(Values.TrTin);

    public static readonly TaxIdType TwVat = new(Values.TwVat);

    public static readonly TaxIdType UsEin = new(Values.UsEin);

    public static readonly TaxIdType ZaVat = new(Values.ZaVat);

    public TaxIdType(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static TaxIdType FromCustom(string value)
    {
        return new TaxIdType(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(TaxIdType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(TaxIdType value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(TaxIdType value) => value.Value;

    public static explicit operator TaxIdType(string value) => new(value);

    internal class TaxIdTypeSerializer : JsonConverter<TaxIdType>
    {
        public override TaxIdType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new TaxIdType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TaxIdType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TaxIdType ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new TaxIdType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TaxIdType value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string AeTrn = "ae_trn";

        public const string AuAbn = "au_abn";

        public const string AuArn = "au_arn";

        public const string BrCnpj = "br_cnpj";

        public const string BrCpf = "br_cpf";

        public const string CaBn = "ca_bn";

        public const string CaGstHst = "ca_gst_hst";

        public const string CaPstBc = "ca_pst_bc";

        public const string CaPstMb = "ca_pst_mb";

        public const string CaPstSk = "ca_pst_sk";

        public const string CaQst = "ca_qst";

        public const string ChUid = "ch_uid";

        public const string ChVat = "ch_vat";

        public const string EuVat = "eu_vat";

        public const string GbVat = "gb_vat";

        public const string HkBr = "hk_br";

        public const string IdNpwp = "id_npwp";

        public const string IlVat = "il_vat";

        public const string InGst = "in_gst";

        public const string JpCn = "jp_cn";

        public const string JpRn = "jp_rn";

        public const string JpTrn = "jp_trn";

        public const string KrBrn = "kr_brn";

        public const string MxRfc = "mx_rfc";

        public const string MyFrp = "my_frp";

        public const string MyItn = "my_itn";

        public const string MySst = "my_sst";

        public const string NoVat = "no_vat";

        public const string NzGst = "nz_gst";

        public const string PhTin = "ph_tin";

        public const string SaVat = "sa_vat";

        public const string SgGst = "sg_gst";

        public const string SgUen = "sg_uen";

        public const string ThVat = "th_vat";

        public const string TrTin = "tr_tin";

        public const string TwVat = "tw_vat";

        public const string UsEin = "us_ein";

        public const string ZaVat = "za_vat";
    }
}
