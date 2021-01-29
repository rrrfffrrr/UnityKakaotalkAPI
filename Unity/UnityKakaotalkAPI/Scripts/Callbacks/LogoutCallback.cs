using UnityEngine;

namespace Kakaotalk.Callback {
    public class LogoutCallback : AndroidJavaProxy {
        private readonly SuccessAction OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public LogoutCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.LogoutCallback") {
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