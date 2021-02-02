using UnityEngine;
using Newtonsoft.Json;

namespace Kakaotalk.Callback
{
    public class JsonCallback<T> : AndroidJavaProxy {
        private readonly JsonSuccessAction<T> OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public JsonCallback(JsonSuccessAction<T> success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.JsonCallback") {
            OnSuccessCallback = success;
            OnFailCallback = fail;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006", Justification = "<Kotlin code convention>")]
        public void onSuccess(string result) {
            try {
                var data = JsonConvert.DeserializeObject<T>(result);
                OnSuccessCallback?.Invoke(data);
            } catch (System.Exception e) {
                OnFailCallback?.Invoke(e.Message);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006", Justification = "<Kotlin code convention>")]
        public void onFail(string message) {
            OnFailCallback?.Invoke(message);
        }
    }
}