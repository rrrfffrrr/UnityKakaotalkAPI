using System;

namespace Kakaotalk.Model {
    [Serializable]
    public class Friend {
        public bool favorite;
        public long id;
        public string profile_nickname;
        public string profile_thumbnail_image;
        public string uuid;
    }
}