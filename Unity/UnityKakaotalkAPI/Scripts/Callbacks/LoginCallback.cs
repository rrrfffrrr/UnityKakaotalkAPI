using UnityEngine;
using Kakaotalk.Model;

namespace Kakaotalk.Callback {
    public class LoginCallback : AndroidJavaProxy {
        public delegate void SuccessAction(OAuthToken token);
        public delegate void FailAction(string message);

        private SuccessAction OnSuccessCallback;
        private FailAction OnFailCallback;

        public LoginCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.LoginCallback") {
            OnSuccessCallback = success;
            OnFailCallback = fail;
        }

        public void onSuccess(string tokenJson) {
            try {
                var token = JsonUtility.FromJson<OAuthToken>(tokenJson);
                OnSuccessCallback?.Invoke(token);
            } catch {
                onFail("Error occurred while deserialize token");
            }
        }
        public void onFail(string message) {
            OnFailCallback?.Invoke(message);
        }
    }
}