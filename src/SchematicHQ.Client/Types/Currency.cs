using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(Currency.CurrencySerializer))]
[Serializable]
public readonly record struct Currency : IStringEnum
{
    public static readonly Currency Aed = new(Values.Aed);

    public static readonly Currency Afn = new(Values.Afn);

    public static readonly Currency All = new(Values.All);

    public static readonly Currency Amd = new(Values.Amd);

    public static readonly Currency Ang = new(Values.Ang);

    public static readonly Currency Aoa = new(Values.Aoa);

    public static readonly Currency Ars = new(Values.Ars);

    public static readonly Currency Aud = new(Values.Aud);

    public static readonly Currency Awg = new(Values.Awg);

    public static readonly Currency Azn = new(Values.Azn);

    public static readonly Currency Bam = new(Values.Bam);

    public static readonly Currency Bbd = new(Values.Bbd);

    public static readonly Currency Bdt = new(Values.Bdt);

    public static readonly Currency Bif = new(Values.Bif);

    public static readonly Currency Bmd = new(Values.Bmd);

    public static readonly Currency Bnd = new(Values.Bnd);

    public static readonly Currency Bob = new(Values.Bob);

    public static readonly Currency Brl = new(Values.Brl);

    public static readonly Currency Bsd = new(Values.Bsd);

    public static readonly Currency Bwp = new(Values.Bwp);

    public static readonly Currency Byn = new(Values.Byn);

    public static readonly Currency Bzd = new(Values.Bzd);

    public static readonly Currency Cad = new(Values.Cad);

    public static readonly Currency Cdf = new(Values.Cdf);

    public static readonly Currency Chf = new(Values.Chf);

    public static readonly Currency Clp = new(Values.Clp);

    public static readonly Currency Cny = new(Values.Cny);

    public static readonly Currency Cop = new(Values.Cop);

    public static readonly Currency Crc = new(Values.Crc);

    public static readonly Currency Cve = new(Values.Cve);

    public static readonly Currency Czk = new(Values.Czk);

    public static readonly Currency Djf = new(Values.Djf);

    public static readonly Currency Dkk = new(Values.Dkk);

    public static readonly Currency Dop = new(Values.Dop);

    public static readonly Currency Dzd = new(Values.Dzd);

    public static readonly Currency Egp = new(Values.Egp);

    public static readonly Currency Etb = new(Values.Etb);

    public static readonly Currency Eur = new(Values.Eur);

    public static readonly Currency Fjd = new(Values.Fjd);

    public static readonly Currency Fkp = new(Values.Fkp);

    public static readonly Currency Gbp = new(Values.Gbp);

    public static readonly Currency Gel = new(Values.Gel);

    public static readonly Currency Gip = new(Values.Gip);

    public static readonly Currency Gmd = new(Values.Gmd);

    public static readonly Currency Gnf = new(Values.Gnf);

    public static readonly Currency Gtq = new(Values.Gtq);

    public static readonly Currency Gyd = new(Values.Gyd);

    public static readonly Currency Hkd = new(Values.Hkd);

    public static readonly Currency Hnl = new(Values.Hnl);

    public static readonly Currency Htg = new(Values.Htg);

    public static readonly Currency Huf = new(Values.Huf);

    public static readonly Currency Idr = new(Values.Idr);

    public static readonly Currency Ils = new(Values.Ils);

    public static readonly Currency Inr = new(Values.Inr);

    public static readonly Currency Isk = new(Values.Isk);

    public static readonly Currency Jmd = new(Values.Jmd);

    public static readonly Currency Jpy = new(Values.Jpy);

    public static readonly Currency Kes = new(Values.Kes);

    public static readonly Currency Kgs = new(Values.Kgs);

    public static readonly Currency Khr = new(Values.Khr);

    public static readonly Currency Kmf = new(Values.Kmf);

    public static readonly Currency Krw = new(Values.Krw);

    public static readonly Currency Kyd = new(Values.Kyd);

    public static readonly Currency Kzt = new(Values.Kzt);

    public static readonly Currency Lak = new(Values.Lak);

    public static readonly Currency Lbp = new(Values.Lbp);

    public static readonly Currency Lkr = new(Values.Lkr);

    public static readonly Currency Lrd = new(Values.Lrd);

    public static readonly Currency Lsl = new(Values.Lsl);

    public static readonly Currency Mad = new(Values.Mad);

    public static readonly Currency Mdl = new(Values.Mdl);

    public static readonly Currency Mga = new(Values.Mga);

    public static readonly Currency Mkd = new(Values.Mkd);

    public static readonly Currency Mmk = new(Values.Mmk);

    public static readonly Currency Mnt = new(Values.Mnt);

    public static readonly Currency Mop = new(Values.Mop);

    public static readonly Currency Mur = new(Values.Mur);

    public static readonly Currency Mvr = new(Values.Mvr);

    public static readonly Currency Mwk = new(Values.Mwk);

    public static readonly Currency Mxn = new(Values.Mxn);

    public static readonly Currency Myr = new(Values.Myr);

    public static readonly Currency Mzn = new(Values.Mzn);

    public static readonly Currency Nad = new(Values.Nad);

    public static readonly Currency Ngn = new(Values.Ngn);

    public static readonly Currency Nio = new(Values.Nio);

    public static readonly Currency Nok = new(Values.Nok);

    public static readonly Currency Npr = new(Values.Npr);

    public static readonly Currency Nzd = new(Values.Nzd);

    public static readonly Currency Pab = new(Values.Pab);

    public static readonly Currency Pen = new(Values.Pen);

    public static readonly Currency Pgk = new(Values.Pgk);

    public static readonly Currency Php = new(Values.Php);

    public static readonly Currency Pkr = new(Values.Pkr);

    public static readonly Currency Pln = new(Values.Pln);

    public static readonly Currency Pyg = new(Values.Pyg);

    public static readonly Currency Qar = new(Values.Qar);

    public static readonly Currency Ron = new(Values.Ron);

    public static readonly Currency Rsd = new(Values.Rsd);

    public static readonly Currency Rub = new(Values.Rub);

    public static readonly Currency Rwf = new(Values.Rwf);

    public static readonly Currency Sar = new(Values.Sar);

    public static readonly Currency Sbd = new(Values.Sbd);

    public static readonly Currency Scr = new(Values.Scr);

    public static readonly Currency Sek = new(Values.Sek);

    public static readonly Currency Sgd = new(Values.Sgd);

    public static readonly Currency Shp = new(Values.Shp);

    public static readonly Currency Sle = new(Values.Sle);

    public static readonly Currency Sos = new(Values.Sos);

    public static readonly Currency Srd = new(Values.Srd);

    public static readonly Currency Std = new(Values.Std);

    public static readonly Currency Szl = new(Values.Szl);

    public static readonly Currency Thb = new(Values.Thb);

    public static readonly Currency Tjs = new(Values.Tjs);

    public static readonly Currency Top = new(Values.Top);

    public static readonly Currency Try = new(Values.Try);

    public static readonly Currency Ttd = new(Values.Ttd);

    public static readonly Currency Twd = new(Values.Twd);

    public static readonly Currency Tzs = new(Values.Tzs);

    public static readonly Currency Uah = new(Values.Uah);

    public static readonly Currency Ugx = new(Values.Ugx);

    public static readonly Currency Usd = new(Values.Usd);

    public static readonly Currency Uyu = new(Values.Uyu);

    public static readonly Currency Uzs = new(Values.Uzs);

    public static readonly Currency Vnd = new(Values.Vnd);

    public static readonly Currency Vuv = new(Values.Vuv);

    public static readonly Currency Wst = new(Values.Wst);

    public static readonly Currency Xaf = new(Values.Xaf);

    public static readonly Currency Xcd = new(Values.Xcd);

    public static readonly Currency Xcg = new(Values.Xcg);

    public static readonly Currency Xof = new(Values.Xof);

    public static readonly Currency Xpf = new(Values.Xpf);

    public static readonly Currency Yer = new(Values.Yer);

    public static readonly Currency Zar = new(Values.Zar);

    public static readonly Currency Zmw = new(Values.Zmw);

    public Currency(string value)
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
    public static Currency FromCustom(string value)
    {
        return new Currency(value);
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

    public static bool operator ==(Currency value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Currency value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Currency value) => value.Value;

    public static explicit operator Currency(string value) => new(value);

    internal class CurrencySerializer : JsonConverter<Currency>
    {
        public override Currency Read(
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
            return new Currency(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Currency value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override Currency ReadAsPropertyName(
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
            return new Currency(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            Currency value,
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
        public const string Aed = "aed";

        public const string Afn = "afn";

        public const string All = "all";

        public const string Amd = "amd";

        public const string Ang = "ang";

        public const string Aoa = "aoa";

        public const string Ars = "ars";

        public const string Aud = "aud";

        public const string Awg = "awg";

        public const string Azn = "azn";

        public const string Bam = "bam";

        public const string Bbd = "bbd";

        public const string Bdt = "bdt";

        public const string Bif = "bif";

        public const string Bmd = "bmd";

        public const string Bnd = "bnd";

        public const string Bob = "bob";

        public const string Brl = "brl";

        public const string Bsd = "bsd";

        public const string Bwp = "bwp";

        public const string Byn = "byn";

        public const string Bzd = "bzd";

        public const string Cad = "cad";

        public const string Cdf = "cdf";

        public const string Chf = "chf";

        public const string Clp = "clp";

        public const string Cny = "cny";

        public const string Cop = "cop";

        public const string Crc = "crc";

        public const string Cve = "cve";

        public const string Czk = "czk";

        public const string Djf = "djf";

        public const string Dkk = "dkk";

        public const string Dop = "dop";

        public const string Dzd = "dzd";

        public const string Egp = "egp";

        public const string Etb = "etb";

        public const string Eur = "eur";

        public const string Fjd = "fjd";

        public const string Fkp = "fkp";

        public const string Gbp = "gbp";

        public const string Gel = "gel";

        public const string Gip = "gip";

        public const string Gmd = "gmd";

        public const string Gnf = "gnf";

        public const string Gtq = "gtq";

        public const string Gyd = "gyd";

        public const string Hkd = "hkd";

        public const string Hnl = "hnl";

        public const string Htg = "htg";

        public const string Huf = "huf";

        public const string Idr = "idr";

        public const string Ils = "ils";

        public const string Inr = "inr";

        public const string Isk = "isk";

        public const string Jmd = "jmd";

        public const string Jpy = "jpy";

        public const string Kes = "kes";

        public const string Kgs = "kgs";

        public const string Khr = "khr";

        public const string Kmf = "kmf";

        public const string Krw = "krw";

        public const string Kyd = "kyd";

        public const string Kzt = "kzt";

        public const string Lak = "lak";

        public const string Lbp = "lbp";

        public const string Lkr = "lkr";

        public const string Lrd = "lrd";

        public const string Lsl = "lsl";

        public const string Mad = "mad";

        public const string Mdl = "mdl";

        public const string Mga = "mga";

        public const string Mkd = "mkd";

        public const string Mmk = "mmk";

        public const string Mnt = "mnt";

        public const string Mop = "mop";

        public const string Mur = "mur";

        public const string Mvr = "mvr";

        public const string Mwk = "mwk";

        public const string Mxn = "mxn";

        public const string Myr = "myr";

        public const string Mzn = "mzn";

        public const string Nad = "nad";

        public const string Ngn = "ngn";

        public const string Nio = "nio";

        public const string Nok = "nok";

        public const string Npr = "npr";

        public const string Nzd = "nzd";

        public const string Pab = "pab";

        public const string Pen = "pen";

        public const string Pgk = "pgk";

        public const string Php = "php";

        public const string Pkr = "pkr";

        public const string Pln = "pln";

        public const string Pyg = "pyg";

        public const string Qar = "qar";

        public const string Ron = "ron";

        public const string Rsd = "rsd";

        public const string Rub = "rub";

        public const string Rwf = "rwf";

        public const string Sar = "sar";

        public const string Sbd = "sbd";

        public const string Scr = "scr";

        public const string Sek = "sek";

        public const string Sgd = "sgd";

        public const string Shp = "shp";

        public const string Sle = "sle";

        public const string Sos = "sos";

        public const string Srd = "srd";

        public const string Std = "std";

        public const string Szl = "szl";

        public const string Thb = "thb";

        public const string Tjs = "tjs";

        public const string Top = "top";

        public const string Try = "try";

        public const string Ttd = "ttd";

        public const string Twd = "twd";

        public const string Tzs = "tzs";

        public const string Uah = "uah";

        public const string Ugx = "ugx";

        public const string Usd = "usd";

        public const string Uyu = "uyu";

        public const string Uzs = "uzs";

        public const string Vnd = "vnd";

        public const string Vuv = "vuv";

        public const string Wst = "wst";

        public const string Xaf = "xaf";

        public const string Xcd = "xcd";

        public const string Xcg = "xcg";

        public const string Xof = "xof";

        public const string Xpf = "xpf";

        public const string Yer = "yer";

        public const string Zar = "zar";

        public const string Zmw = "zmw";
    }
}
