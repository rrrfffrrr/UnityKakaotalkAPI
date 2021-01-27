package com.example.unitykakaotalkapi

import android.content.Context
import android.util.Log
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
    private var context: Context? = null
    private lateinit var receiverObject: String
    private val TAG: String = "UnityKakaotalkAPI"

    private val LoginCallback: (OAuthToken?, Throwable?) -> Unit = { token, error ->
        Log.d(TAG, "Login callback entered")
        if (error != null) {
            // fail to login
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", error.message)
        } else if (token != null) {
            // success to login
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginSuccess", KakaoJson.toJson(token))
        }
        Log.d(TAG, "Login callback exit")
    }

    @JvmName("Initialize")
    fun Initialize(context: Context, receiverObject: String) {
        Log.d(TAG, "Initialize enter")
        if (this.context == null) {
            this.context = context
            this.receiverObject = receiverObject
            var index = context.resources.getIdentifier("KakaoTalkAppKey", "string", context.packageName)
            if (index == 0) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnInitializeFail", "Cannot find AppKey.")
            } else {
                KakaoSdk.init(context, context.resources.getString(index))
                UnityPlayer.UnitySendMessage(receiverObject, "OnInitializeSuccess", "success")
            }
        } else {
            UnityPlayer.UnitySendMessage(receiverObject, "OnInitializeFail", "Already initialized")
        }
        Log.d(TAG, "Initialize exit")
    }
    @JvmName("LoginWithKakaotalk")
    fun LoginWithKakaotalk() {
        Log.d(TAG, "LoginWithKakaotalk enter")
        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.")
        } else if (LoginClient.instance.isKakaoTalkLoginAvailable(context!!)) {
            LoginClient.instance.loginWithKakaoTalk(context!!, callback = LoginCallback)
        } else { // When kakaotalk not installed
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "Kakaotalk not installed on this device.")
        }
        Log.d(TAG, "LoginWithKakaotalk exit")
    }
    @JvmName("LoginWithKakaoAccount")
    fun LoginWithKakaoAccount() {
        Log.d(TAG, "LoginWithKakaoAccount enter")
        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.")
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = LoginCallback)
        }
        Log.d(TAG, "LoginWithKakaoAccount exit")
    }
    @JvmName("LoginWithKakao")
    fun LoginWithKakao() {
        Log.d(TAG, "LoginWithKakao enter")

        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.")
        } else if (LoginClient.instance.isKakaoTalkLoginAvailable(context!!)) {
            LoginClient.instance.loginWithKakaoTalk(context!!, callback = LoginCallback)
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = LoginCallback)
        }
        Log.d(TAG, "LoginWithKakao exit")
    }
    @JvmName("Logout")
    fun Logout() {
        UserApiClient.instance.logout { error ->
            if (error != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnLogout", error.message)
            } else {
                UnityPlayer.UnitySendMessage(receiverObject, "OnLogout", "success")
            }
        }
    }
    @JvmName("GetKeyHash")
    fun GetKeyHash() : String {
        Log.d(TAG, "GetKeyHash")
        if (context == null) return ""
        return Utility.getKeyHash(context!!)
    }
    @JvmName("GetUserInformation")
    fun GetUserInformation() {
        Log.d(TAG, "GetUserInformation entered")
        UserApiClient.instance.me { user, error ->
            if (error != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnUserInformationFail", error.message)
            } else if (user != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnUserInformationSuccess", KakaoJson.toJson(user))
            } else {
                UnityPlayer.UnitySendMessage(receiverObject, "OnUserInformationFail", "Somthing went wrong")
            }
        }
        Log.d(TAG, "GetUserInformation exit")
    }
    @JvmName("GetProfile")
    fun GetProfile() {
        Log.d(TAG, "GetProfile entered")
        TalkApiClient.instance.profile { profile, error ->
            if (error != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnProfileFail", error.message)
            }
            else if (profile != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnProfileSuccess", KakaoJson.toJson(profile))
            } else {
                UnityPlayer.UnitySendMessage(receiverObject, "OnProfileFail", "Somthing went wrong")
            }
        }
        Log.d(TAG, "GetProfile exit")
    }
    @JvmName("GetFriends")
    fun GetFriends(offset: Int, count: Int, order: String) {
        var forder = Order.ASC
        if (order == "desc")
            forder = Order.DESC
        TalkApiClient.instance.friends(offset, count, forder, callback = { friends: Friends<Friend>?, error: Throwable? ->
            if (error != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnFriendsFail", error.message)
            } else if (friends != null) {
                UnityPlayer.UnitySendMessage(receiverObject, "OnFriendsSuccess", KakaoJson.toJson(friends))
            }
        })
    }

    companion object {
        private var sdk : UnityKakaotalkAPI? = null
        private val TAG: String = "UnityKakaotalkAPI"

        @JvmStatic
        fun GetInstance() : UnityKakaotalkAPI {
            Log.d(TAG, "GetInstance")
            if (sdk == null) {
                Log.d(TAG, "Create new instance")
                sdk = UnityKakaotalkAPI()
            }
            return sdk!!
        }
    }

}