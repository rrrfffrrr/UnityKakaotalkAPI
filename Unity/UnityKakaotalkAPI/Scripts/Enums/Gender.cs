using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;

namespace Kakaotalk {
    [JsonConverter(typeof(StringEnumConverter), converterParameters: typeof(CamelCaseNamingStrategy))]
    public enum Gender {
        [EnumMember(Value = "female")]
        FEMALE,
        [EnumMember(Value = "male")]
        MALE,
        UNKNOWN
    }
}