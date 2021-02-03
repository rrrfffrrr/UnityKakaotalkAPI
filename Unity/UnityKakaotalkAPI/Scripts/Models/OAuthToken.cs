using System;

namespace Kakaotalk.Model {
    [Serializable]
    public class OAuthToken {
        public string access_token;
        public DateTime access_token_expires_at;
        public string refresh_token;
        public DateTime refresh_token_expires_at;
        public string[] scopes;
    }
}