using UnityEngine;
using Kakaotalk.Callback;
using Kakaotalk.Model;

namespace Kakaotalk
{
    public delegate void SuccessAction();
    public delegate void FailAction(string message);

    public delegate void LoginSuccessAction(OAuthToken token);
    public delegate void GetProfileSuccessAction(TalkProfile profile);
    public delegate void GetUserInfoSuccessAction(UserInfo user);
    public delegate void GetFriendsSuccessAction(Friends friends);

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

        public const string FAIL_RESULT_NOT_SUPPORTED_DEVICE = "UnityKakaotalkAPI not supported on this device";
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
        public static void Initialize(SuccessAction onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext")) {
                        KakaoSdkObject.Call("Initialize", activity, new InitializeCallback(onSuccess, onFail));
                    }
                }
            }
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }

        public static void Login(LOGIN_METHOD method, LoginSuccessAction onSuccess, FailAction onFail) {
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
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void Logout(SuccessAction onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("Logout", new LogoutCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void GetUserInformation(GetUserInfoSuccessAction onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetUserInformation", new UserInfoCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void GetProfile(GetProfileSuccessAction onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetProfile", new ProfileCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void GetFriends(int offset, int count, string order, GetFriendsSuccessAction onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetFriends", offset, count, order, new FriendsCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
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