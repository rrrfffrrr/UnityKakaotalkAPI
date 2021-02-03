using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;

namespace Kakaotalk {
    [JsonConverter(typeof(StringEnumConverter), converterParameters:typeof(CamelCaseNamingStrategy))]
    public enum AgeRange {
        [EnumMember(Value = "0~9")]
        AGE_0_9,
        [EnumMember(Value = "10~14")]
        AGE_10_14,
        [EnumMember(Value = "15~19")]
        AGE_15_19,
        [EnumMember(Value = "20~29")]
        AGE_20_29,
        [EnumMember(Value = "30~39")]
        AGE_30_39,
        [EnumMember(Value = "40~49")]
        AGE_40_49,
        [EnumMember(Value = "50~59")]
        AGE_50_59,
        [EnumMember(Value = "60~69")]
        AGE_60_69,
        [EnumMember(Value = "70~79")]
        AGE_70_79,
        [EnumMember(Value = "80~89")]
        AGE_80_89,
        [EnumMember(Value = "90~")]
        AGE_90_ABOVE,
        UNKNOWN
    }
}