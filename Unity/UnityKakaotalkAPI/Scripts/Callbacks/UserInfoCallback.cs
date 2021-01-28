using UnityEngine;
using Kakaotalk.Model;

namespace Kakaotalk.Callback {
    public class UserInfoCallback : AndroidJavaProxy {
        public delegate void SuccessAction(UserInfo user);
        public delegate void FailAction(string message);

        private SuccessAction OnSuccessCallback;
        private FailAction OnFailCallback;

        public UserInfoCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.UserInfoCallback") {
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