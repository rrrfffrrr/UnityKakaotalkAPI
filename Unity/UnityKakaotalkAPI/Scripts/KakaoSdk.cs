using UnityEngine;
using Kakaotalk.Callback;
using Kakaotalk.Model;

namespace Kakaotalk
{
    public delegate void SuccessAction();
    public delegate void JsonSuccessAction<T>(T data);
    public delegate void FailAction(string message);

    public static class KakaoSdk
    {
        public const string FAIL_RESULT_NOT_SUPPORTED_DEVICE = "UnityKakaotalkAPI not supported on this device";
        public const string FAIL_RESULT_UNEXPECTED_LOGIN_METHOD = "Unexpected login method";

#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaObject _kakaoSdkObject;
        private static AndroidJavaObject KakaoSdkObject {
            get {
                if (_kakaoSdkObject == null) {
                    using(var _static = new AndroidJavaClass("com.rrrfffrrr.unity.kakaotalk.UnityKakaotalkAPI")) {
                        _kakaoSdkObject = _static.CallStatic<AndroidJavaObject>("getInstance");
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
                        KakaoSdkObject.Call("initialize", activity, new DefaultCallback(onSuccess, onFail));
                    }
                }
            }
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }

        public static void Login(LoginMethod method, JsonSuccessAction<OAuthToken> onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            switch (method) {
                case LoginMethod.Both:
                    KakaoSdkObject.Call("loginWithKakao", new JsonCallback<OAuthToken>(onSuccess, onFail));
                    break;
                case LoginMethod.Kakaotalk:
                    KakaoSdkObject.Call("loginWithKakaotalk", new JsonCallback<OAuthToken>(onSuccess, onFail));
                    break;
                case LoginMethod.KakaoAccount:
                    KakaoSdkObject.Call("loginWithKakaoAccount", new JsonCallback<OAuthToken>(onSuccess, onFail));
                    break;
                default:
                    onFail(FAIL_RESULT_UNEXPECTED_LOGIN_METHOD);
                    break;
            }
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void LoginWithNewScopes(string[] scopes, JsonSuccessAction<OAuthToken> onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("loginWithNewScopes", scopes, new JsonCallback<OAuthToken>(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void Logout(SuccessAction onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("logout", new DefaultCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void Unlink(SuccessAction onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("unlink", new DefaultCallback(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void GetUserInformation(JsonSuccessAction<UserInfo> onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("getUserInformation", new JsonCallback<UserInfo>(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void GetProfile(JsonSuccessAction<TalkProfile> onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("getProfile", new JsonCallback<TalkProfile>(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }
        public static void GetFriends(int offset, int count, string order, JsonSuccessAction<Friends> onSuccess, FailAction onFail) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("getFriends", offset, count, order, new JsonCallback<Friends>(onSuccess, onFail));
#else
            onFail(FAIL_RESULT_NOT_SUPPORTED_DEVICE);
#endif
        }

        public static string GetKeyHash() {
#if UNITY_ANDROID && !UNITY_EDITOR
            return KakaoSdkObject.Call<string>("getKeyHash");
#else
            return null;
#endif
        }
    }
}