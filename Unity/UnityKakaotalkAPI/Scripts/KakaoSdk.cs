using UnityEngine;
using UnityEngine.Events;
using Kakaotalk.Callback;

namespace Kakaotalk
{
    public static class KakaoSdk
    {
        [System.Flags]
        public enum LOGIN_METHOD {
            Error = 0,
            Kakaotalk = 1,
            KakaoAccount = 2,
            Both = 3,
        }
        public static class ORDER {
            public const string ASC = "asc";
            public const string DESC = "desc";
        }

        public const string FAIL_RESULT_NOT_AN_ANDROID_DEVICE = "Not an android device";
        public const string FAIL_RESULT_UNEXPECTED_LOGIN_METHOD = "Unexpected login method";

#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaObject _kakaoSdkObject;
        private static AndroidJavaObject KakaoSdkObject {
            get {
                if (_kakaoSdkObject == null) {
                    using(var _static = new AndroidJavaClass("com.rrrfffrrr.unity.kakaotalk.UnityKakaotalkAPI")) {
                        _kakaoSdkObject = _static.CallStatic<AndroidJavaObject>("GetInstance");
                    }
                }
                return _kakaoSdkObject;
            }
        }
#endif
        public static void Initialize(InitializeCallback.SuccessAction onSuccess, InitializeCallback.FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext")) {
                        KakaoSdkObject.Call("Initialize", activity, new InitializeCallback(onSuccess, onFail));
                    }
                }
            }
#else
            onFail(FAIL_RESULT_NOT_AN_ANDROID_DEVICE);
#endif
        }

        public static void Login(LOGIN_METHOD method, LoginCallback.SuccessAction onSuccess, LoginCallback.FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJNI.AttachCurrentThread();
            switch (method) {
                case LOGIN_METHOD.Both:
                    KakaoSdkObject.Call("LoginWithKakao", new LoginCallback(onSuccess, onFail));
                    break;
                case LOGIN_METHOD.Kakaotalk:
                    KakaoSdkObject.Call("LoginWithKakaotalk", new LoginCallback(onSuccess, onFail));
                    break;
                case LOGIN_METHOD.KakaoAccount:
                    KakaoSdkObject.Call("LoginWithKakaoAccount", new LoginCallback(onSuccess, onFail));
                    break;
                default:
                    onFail(FAIL_RESULT_UNEXPECTED_LOGIN_METHOD);
                    break;
            }
#else
            onFail(FAIL_RESULT_NOT_AN_ANDROID_DEVICE);
#endif
        }
        public static void Logout(LogoutCallback.SuccessAction onSuccess, LogoutCallback.FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("Logout", new LogoutCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_AN_ANDROID_DEVICE);
#endif
        }
        public static void GetUserInformation(UserInfoCallback.SuccessAction onSuccess, UserInfoCallback.FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetUserInformation", new UserInfoCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_AN_ANDROID_DEVICE);
#endif
        }
        public static void GetProfile(ProfileCallback.SuccessAction onSuccess, ProfileCallback.FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetProfile", new ProfileCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_AN_ANDROID_DEVICE);
#endif
        }
        public static void GetFriends(int offset, int count, string order, FriendsCallback.SuccessAction onSuccess, FriendsCallback.FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetFriends", offset, count, order, new FriendsCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_AN_ANDROID_DEVICE);
#endif
        }

        public static string GetKeyHash() {
#if UNITY_ANDROID && !UNITY_EDITOR
            Debug.Log("Call GetKeyHash");
            return KakaoSdkObject.Call<string>("GetKeyHash");
#else
            return null;
#endif
        }
    }
}