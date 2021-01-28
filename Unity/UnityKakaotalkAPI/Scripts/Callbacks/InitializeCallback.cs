using UnityEngine;

namespace Kakaotalk.Callback {
    public class InitializeCallback : AndroidJavaProxy {
        public delegate void SuccessAction();
        public delegate void FailAction(string message);

        private SuccessAction OnSuccessCallback;
        private FailAction OnFailCallback;

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