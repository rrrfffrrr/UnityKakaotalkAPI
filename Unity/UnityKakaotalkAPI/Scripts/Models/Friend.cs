using System;

namespace Kakaotalk.Model
{
    [Serializable]
    public class Friend
    {
        public bool favorite;
        public long id;
        public string profileNickname;
        public string profileThumbnailImage;
        public string uuid;
    }
}