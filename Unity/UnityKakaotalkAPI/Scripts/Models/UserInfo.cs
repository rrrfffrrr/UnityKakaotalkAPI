using System;

namespace Kakaotalk.Model
{
    [Serializable]
    public class UserInfo
    {
        public long id;
        public string group_user_token;
        public Account kakao_account;
        public string properties; // Map<string, string>
        public string connected_at;
        public string synched_at;
    }
}