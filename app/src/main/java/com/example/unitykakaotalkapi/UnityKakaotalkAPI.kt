package com.example.unitykakaotalkapi

import android.content.Context
import android.util.Log
import com.google.gson.GsonBuilder
import com.kakao.sdk.auth.LoginClient
import com.kakao.sdk.talk.TalkApiClient
import com.kakao.sdk.auth.model.OAuthToken
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.common.util.Utility
import com.kakao.sdk.common.util.KakaoJson
import com.kakao.sdk.talk.model.Friend
import com.kakao.sdk.talk.model.Friends
import com.kakao.sdk.talk.model.Order
import com.kakao.sdk.user.UserApiClient
import com.unity3d.player.UnityPlayer

class UnityKakaotalkAPI {
    private var context: Context? = null;
    private val TAG: String = "UnityKakaotalkAPI";

    private fun GetLoginCallback(receiverObject: String): (OAuthToken?, Throwable?) -> Unit {
        return fun(token: OAuthToken?, error: Throwable?) {
            Log.d(TAG, "Login callback entered");
            if (error != null) {
                // fail to login
                UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", error.localizedMessage);
            } else if (token != null) {
                // success to login
                UnityPlayer.UnitySendMessage(receiverObject, "OnLoginSuccess", token.accessToken);
            }
            Log.d(TAG, "Login callback exit");
        };
    }
    @JvmName("Initialize")
    fun Initialize(context: Context, receiverObject: String) {
        Log.d(TAG, "Initialize enter");
        if (this.context == null) {
            this.context = context;
            var index = context.resources.getIdentifier("KakaoTalkAppKey", "string", context.packageName);
            if (index == 0) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnInitializeFail", "Cannot find AppKey.");
            } else {
                KakaoSdk.init(context, context.resources.getString(index));
                UnityPlayer.UnitySendMessage(receiverObject, "OnInitializeSuccess", "success");
            }
        } else {
            UnityPlayer.UnitySendMessage(receiverObject, "OnInitializeFail", "Already initialized");
        }
        Log.d(TAG, "Initialize exit");
    }
    @JvmName("LoginWithKakaotalk")
    fun LoginWithKakaotalk(receiverObject: String) {
        Log.d(TAG, "LoginWithKakaotalk enter");
        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.");
        } else if (LoginClient.instance.isKakaoTalkLoginAvailable(context!!)) {
            LoginClient.instance.loginWithKakaoTalk(context!!, callback = GetLoginCallback(receiverObject));
        } else { // When kakaotalk not installed
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "Kakaotalk not installed on this device.");
        }
        Log.d(TAG, "LoginWithKakaotalk exit");
    }
    @JvmName("LoginWithKakaoAccount")
    fun LoginWithKakaoAccount(receiverObject: String) {
        Log.d(TAG, "LoginWithKakaoAccount enter");
        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.");
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = GetLoginCallback(receiverObject));
        }
        Log.d(TAG, "LoginWithKakaoAccount exit");
    }
    @JvmName("LoginWithKakao")
    fun LoginWithKakao(receiverObject: String) {
        Log.d(TAG, "LoginWithKakao enter");

        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.");
        } else if (LoginClient.instance.isKakaoTalkLoginAvailable(context!!)) {
            LoginClient.instance.loginWithKakaoTalk(context!!, callback = GetLoginCallback(receiverObject));
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = GetLoginCallback(receiverObject));
        }
        Log.d(TAG, "LoginWithKakao exit");
    }
    @JvmName("Logout")
    fun Logout(receiverObject: String) {
        UserApiClient.instance.logout { error ->
            if (error != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnLogout", error.localizedMessage);
            } else {
                UnityPlayer.UnitySendMessage(receiverObject, "OnLogout", "success");
            }
        }
    }
    @JvmName("GetKeyHash")
    fun GetKeyHash() : String {
        Log.d(TAG, "GetKeyHash");
        if (context == null) return "";
        return Utility.getKeyHash(context!!);
    }
    @JvmName("GetUserInformation")
    fun GetUserInformation(receiverObject: String) {
        Log.d(TAG, "GetUserInformation entered");
        UserApiClient.instance.me { user, error ->
            if (error != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnUserInformationFail", error.localizedMessage);
            } else if (user != null) {
                var gson = GsonBuilder().create();
                var json = gson.toJson(user);
                UnityPlayer.UnitySendMessage(receiverObject, "OnUserInformationSuccess", json);
            } else {
                UnityPlayer.UnitySendMessage(receiverObject, "OnUserInformationFail", "Somthing went wrong");
            }
        }
        Log.d(TAG, "GetUserInformation exit");
    }
    @JvmName("GetProfile")
    fun GetProfile(receiverObject: String) {
        Log.d(TAG, "GetProfile entered");
        TalkApiClient.instance.profile { profile, error ->
            if (error != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnProfileFail", error.localizedMessage);
            }
            else if (profile != null) {
                var gson = GsonBuilder().create();
                var json = gson.toJson(profile);
                UnityPlayer.UnitySendMessage(receiverObject, "OnProfileSuccess", json);
            } else {
                UnityPlayer.UnitySendMessage(receiverObject, "OnProfileFail", "Somthing went wrong");
            }
        }
        Log.d(TAG, "GetProfile exit");
    }
    @JvmName("GetFriends")
    fun GetFriends(receiverObject: String, offset: Int, count: Int, order: String) {
        var forder = Order.ASC;
        if (order == "desc")
            forder = Order.DESC;
        TalkApiClient.instance.friends(offset, count, forder, callback = { friends: Friends<Friend>?, error: Throwable? ->
            if (error != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnFriendsFail", error.localizedMessage);
            } else if (friends != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnFriendsSuccess", KakaoJson.toJson(friends));
            }
        });
    }

    companion object {
        private var sdk : UnityKakaotalkAPI? = null;
        private val TAG: String = "UnityKakaotalkAPI";

        @JvmStatic
        fun GetInstance() : UnityKakaotalkAPI {
            Log.d(TAG, "GetInstance");
            if (sdk == null) {
                Log.d(TAG, "Create new instance");
                sdk = UnityKakaotalkAPI();
            }
            return sdk!!;
        }
    }
}