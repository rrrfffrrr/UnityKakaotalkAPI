using UnityEngine;
using Kakaotalk.Model;

namespace Kakaotalk.Callback {
    public class FriendsCallback : AndroidJavaProxy {
        public delegate void SuccessAction(Friends friends);
        public delegate void FailAction(string message);

        private SuccessAction OnSuccessCallback;
        private FailAction OnFailCallback;

        public FriendsCallback(SuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.FriendsCallback") {
            OnSuccessCallback = success;
            OnFailCallback = fail;
        }

        public void onSuccess(string frendsJson) {
            try {
                var friends = JsonUtility.FromJson<Friends>(frendsJson);
                OnSuccessCallback?.Invoke(friends);
            } catch {
                onFail("Error occurred while deserialize friends");
            }
        }
        public void onFail(string message) {
            OnFailCallback?.Invoke(message);
        }
    }
}