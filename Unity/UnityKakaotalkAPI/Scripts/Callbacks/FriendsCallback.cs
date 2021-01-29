using UnityEngine;
using Kakaotalk.Model;

namespace Kakaotalk.Callback
{
    public class FriendsCallback : AndroidJavaProxy {

        private readonly GetFriendsSuccessAction OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public FriendsCallback(GetFriendsSuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.FriendsCallback") {
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