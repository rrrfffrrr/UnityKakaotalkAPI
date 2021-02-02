using UnityEngine;

namespace Kakaotalk.Callback {
    public class DefaultCallback : AndroidJavaProxy {
        private readonly SuccessAction OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public DefaultCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.DefaultCallback") {
            OnSuccessCallback = success;
            OnFailCallback = fail;
        }

        public void onSuccess() {
            OnSuccessCallback?.Invoke();
        }
        public void onFail(string message) {
            OnFailCallback?.Invoke(message);
        }
    }
}