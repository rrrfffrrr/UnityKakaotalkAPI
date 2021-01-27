using UnityEngine;
using UnityEngine.Events;

namespace Kakaotalk
{
    public static class KakaoSdk
    {
        public const string RECEIVER_OBJECT_NAME = "Kakaotalk plugin result receiver";
        public const string TAG = "kakaotalk plugin";

        [System.Flags]
        public enum LOGIN_METHOD {
            Error = 0,
            Kakaotalk = 1,
            KakaoAccount = 2,
            Both = 3,
        }
        public static class Order {
            public const string ASC = "asc";
            public const string DESC = "desc";
        }
        public static class Type {
            public const string INITIALIZE = "initialize";
            public const string LOGIN = "login";
            public const string LOGOUT = "logout";
            public const string USERINFO = "user information";
            public const string PROFILE = "profile";
            public const string FRIENDS = "friends";
        }
        public const string RESULT_SUCCESS = "success";
        public const string RESULT_FAIL = "fail";


        public const string FAIL_RESULT_NOT_AN_ANDROID_DEVICE = "Not an android device";
        public static ResultEvent OnResult = new ResultEvent();
        private static KakaoPluginReceiver ResultReceiver;

#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaObject _kakaoSdkObject;
        private static AndroidJavaObject KakaoSdkObject {
            get {
                if (_kakaoSdkObject == null) {
                    using(var _static = new AndroidJavaClass("com.example.unitykakaotalkapi.UnityKakaotalkAPI")) {
                        _kakaoSdkObject = _static.CallStatic<AndroidJavaObject>("GetInstance");
                    }
                }
                return _kakaoSdkObject;
            }
        }
#endif
        public static void Initialize() {
            if (ResultReceiver == null) {
                ResultReceiver = new GameObject(RECEIVER_OBJECT_NAME).AddComponent<KakaoPluginReceiver>();
                Object.DontDestroyOnLoad(ResultReceiver);
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext")) {
                        KakaoSdkObject.Call("Initialize", activity, RECEIVER_OBJECT_NAME);
                    }
                }
            }
#else
            OnResult?.Invoke(new string[] { TAG, Type.INITIALIZE, RESULT_FAIL ,FAIL_RESULT_NOT_AN_ANDROID_DEVICE });
#endif
        }

        public static void Login(LOGIN_METHOD method) {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJNI.AttachCurrentThread();
            switch (method) {
                case LOGIN_METHOD.Both:
                    KakaoSdkObject.Call("LoginWithKakao");
                    break;
                case LOGIN_METHOD.Kakaotalk:
                    KakaoSdkObject.Call("LoginWithKakaotalk");
                    break;
                case LOGIN_METHOD.KakaoAccount:
                    KakaoSdkObject.Call("LoginWithKakaoAccount");
                    break;
                default:
                    throw new System.Exception();
        }
#else
            OnResult?.Invoke(new string[] { TAG, Type.LOGIN, RESULT_FAIL, FAIL_RESULT_NOT_AN_ANDROID_DEVICE });
#endif
        }
        public static void Logout() {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("Logout");
#else
            OnResult?.Invoke(new string[] { TAG, Type.LOGOUT, RESULT_FAIL, FAIL_RESULT_NOT_AN_ANDROID_DEVICE });
#endif
        }
        public static void GetUserInformation() {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetUserInformation");
#else
            OnResult?.Invoke(new string[] { TAG, Type.USERINFO, RESULT_FAIL, FAIL_RESULT_NOT_AN_ANDROID_DEVICE });
#endif
        }
        public static void GetProfile() {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetProfile");
#else
            OnResult?.Invoke(new string[] { TAG, Type.PROFILE, RESULT_FAIL, FAIL_RESULT_NOT_AN_ANDROID_DEVICE });
#endif
        }
        public static void GetFriends(int offset, int count, string order) {
#if UNITY_ANDROID && !UNITY_EDITOR
            KakaoSdkObject.Call("GetFriends", offset, count, order);
#else
            OnResult?.Invoke(new string[] { TAG, Type.FRIENDS, RESULT_FAIL, FAIL_RESULT_NOT_AN_ANDROID_DEVICE });
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

        [global::System.Serializable]
        public class ResultEvent : UnityEvent<string[]> { }
        private sealed class KakaoPluginReceiver: MonoBehaviour
        {
            public void OnInitializeSuccess(string reason) {
                OnResult?.Invoke(new string[] { TAG, Type.INITIALIZE, RESULT_SUCCESS, reason});
            }
            public void OnInitializeFail(string reason) {
                OnResult?.Invoke(new string[] { TAG, Type.INITIALIZE, RESULT_FAIL, reason });
            }
            public void OnLoginSuccess(string token) {
                OnResult?.Invoke(new string[] { TAG, Type.LOGIN, RESULT_SUCCESS, token });
            }
            public void OnLoginFail(string reason) {
                OnResult?.Invoke(new string[] { TAG, Type.LOGIN, RESULT_FAIL, reason });
            }
            public void OnProfileSuccess(string json) {
                OnResult?.Invoke(new string[] { TAG, Type.PROFILE, RESULT_SUCCESS, json });
            }
            public void OnProfileFail(string reason) {
                OnResult?.Invoke(new string[] { TAG, Type.PROFILE, RESULT_FAIL, reason });
            }
            public void OnUserInformationSuccess(string json) {
                OnResult?.Invoke(new string[] { TAG, Type.USERINFO, RESULT_SUCCESS, json });
            }
            public void OnUserInformationFail(string reason) {
                OnResult?.Invoke(new string[] { TAG, Type.USERINFO, RESULT_FAIL, reason });
            }
            public void OnFriendsSuccess(string json) {
                OnResult?.Invoke(new string[] { TAG, Type.FRIENDS, RESULT_SUCCESS, json });
            }
            public void OnFriendsFail(string reason) {
                OnResult?.Invoke(new string[] { TAG, Type.FRIENDS, RESULT_FAIL, reason });
            }
        }
    }
}