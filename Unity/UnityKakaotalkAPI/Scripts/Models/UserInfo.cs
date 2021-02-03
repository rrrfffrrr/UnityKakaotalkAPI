using System;
using System.Collections.Generic;

namespace Kakaotalk.Model {
    [Serializable]
    public class UserInfo {
        public long id;
        public string group_user_token;
        public Account kakao_account;
        public Dictionary<string, string> properties;
        public DateTime connected_at;
        public DateTime synched_at;
    }
}