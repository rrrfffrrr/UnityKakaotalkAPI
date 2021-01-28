using UnityEngine;

namespace Kakaotalk.Callback {
    public class UserInfoCallback : AndroidJavaProxy {
        public delegate void SuccessAction(string userJson);
        public delegate void FailAction(string message);

        private SuccessAction OnSuccessCallback;
        private FailAction OnFailCallback;

        public LoginCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.UserInfoCallback") {
            OnSuccessCallback = success;
            OnFailCallback = fail;
        }

        public void onSuccess(string userJson) {
            try {
                var user = JsonUtility.FromJson<UserInfo>(userJson);
                OnSuccessCallback?.Invoke(user);
            } catch {
                onFail("Error occurred while deserialize user");
            }
        }
        public void onFail(string message) {
            OnFailCallback?.Invoke(message);
        }
    }
}