using UnityEngine;
using Kakaotalk.Model;

namespace Kakaotalk.Callback {
    public class ProfileCallback : AndroidJavaProxy {
        public delegate void SuccessAction(TalkProfile profile);
        public delegate void FailAction(string message);

        private SuccessAction OnSuccessCallback;
        private FailAction OnFailCallback;

        public ProfileCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.ProfileCallback") {
            OnSuccessCallback = success;
            OnFailCallback = fail;
        }

        public void onSuccess(string profileJson) {
            try {
                var profile = JsonUtility.FromJson<TalkProfile>(profileJson);
                OnSuccessCallback?.Invoke(profile);
            } catch {
                onFail("Error occurred while deserialize profile");
            }
        }
        public void onFail(string message) {
            OnFailCallback?.Invoke(message);
        }
    }
}