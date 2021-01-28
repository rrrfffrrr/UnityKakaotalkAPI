using UnityEngine;

namespace Kakaotalk.Callback {
    public class LogoutCallback : AndroidJavaProxy {
        public delegate void SuccessAction();
        public delegate void FailAction(string message);

        private SuccessAction OnSuccessCallback;
        private FailAction OnFailCallback;

        public LoginCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.LogoutCallback") {
            OnSuccessCallback = success;
            OnFailCallback = fail;
        }

        public void onSuccess(string tokenJson) {
            OnSuccessCallback?.Invoke();
        }
        public void onFail(string message) {
            OnFailCallback?.Invoke(message);
        }
    }
}