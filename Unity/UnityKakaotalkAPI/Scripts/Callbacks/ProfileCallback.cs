using UnityEngine;
using Kakaotalk.Model;

namespace Kakaotalk.Callback {
    public class ProfileCallback : AndroidJavaProxy {
        private readonly GetProfileSuccessAction OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public ProfileCallback(GetProfileSuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.ProfileCallback") {
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