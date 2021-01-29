using UnityEngine;

namespace Kakaotalk.Callback {
    public class InitializeCallback : AndroidJavaProxy {
        private readonly SuccessAction OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public InitializeCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.InitializeCallback") {
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