using System;
using UnityEngine;

namespace Kakaotalk.Model
{
    [Serializable]
    public class OAuthToken
    {
        public string access_token;
        public string access_token_expires_at;
        public string refresh_token;
        public string refresh_token_expires_at;
        public string[] scopes;
    }
}