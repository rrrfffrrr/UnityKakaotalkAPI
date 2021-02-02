using UnityEngine;

namespace Kakaotalk.Callback {
    public class DefaultCallback : AndroidJavaProxy {
        private readonly SuccessAction OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public DefaultCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.DefaultCallback") {
            OnSuccessCallback = success;
            OnFailCallback = fail;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006", Justification = "<Kotlin code convention>")]
        public void onSuccess() {
            OnSuccessCallback?.Invoke();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006", Justification = "<Kotlin code convention>")]
        public void onFail(string message) {
            OnFailCallback?.Invoke(message);
        }
    }
}