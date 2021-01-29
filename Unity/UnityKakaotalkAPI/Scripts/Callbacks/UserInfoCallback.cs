using UnityEngine;
using Kakaotalk.Model;
using System.Collections.Generic;

namespace Kakaotalk.Callback
{
    public class UserInfoCallback : AndroidJavaProxy {
        private readonly GetUserInfoSuccessAction OnSuccessCallback;
        private readonly FailAction OnFailCallback;

        public UserInfoCallback(GetUserInfoSuccessAction success, FailAction fail) : base("com.rrrfffrrr.unity.kakaotalk.callback.UserInfoCallback") {
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