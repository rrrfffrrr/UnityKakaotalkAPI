using System;
using System.Collections.Generic;

namespace Kakaotalk.Model
{
    [Serializable]
    public class UserInfo {
        public long id;
        public string group_user_token;
        public Account kakao_account;
        public Map properties; // Map<string, string> example){"key":"value","key":"value",...}
        public string connected_at;
        public string synched_at;

        [Serializable]
        public class Map : Dictionary<string, string> { }
    }
}