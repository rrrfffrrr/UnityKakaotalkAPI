using System;
using Newtonsoft.Json;

namespace Kakaotalk.Model {
    [Serializable]
    public class TalkProfile {
        [JsonProperty("nickName")]
        public string nickname;
        [JsonProperty("profileImageURL")]
        public string profile_image_url;
        [JsonProperty("thumbnailURL")]
        public string thumbnail_url;
        [JsonProperty("countryISO")]
        public string country_iso;
    }
}