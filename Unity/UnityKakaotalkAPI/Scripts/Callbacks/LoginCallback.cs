using UnityEngine;
using Kakaotalk.Model;

namespace Kakaotalk.Callback {
    public class LoginCallback : AndroidJavaProxy {
        private readonly LoginSuccessAction OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public LoginCallback(LoginSuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.LoginCallback") {
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