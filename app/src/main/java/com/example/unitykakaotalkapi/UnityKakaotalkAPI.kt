package com.example.unitykakaotalkapi

import android.content.Context
import android.content.res.Resources
import android.util.Log
import com.kakao.sdk.auth.LoginClient
import com.kakao.sdk.auth.model.OAuthToken
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.common.util.Utility;
import com.unity3d.player.UnityPlayer;

class UnityKakaotalkAPI {
    private var context: Context? = null;

    @JvmName("Initialize")
    fun Initialize(context: Context, appKey: String, receiverObject: String) {
        Log.d("UnityKakaotalkAPI", "Initialize enter");
        if (this.context == null) {
            this.context = context;

            KakaoSdk.init(context, appKey);
            UnityPlayer.UnitySendMessage(receiverObject, "OnInitializeSuccess", "success");
        } else {
            UnityPlayer.UnitySendMessage(receiverObject, "OnInitializeFail", "Already initialized");
        }
        Log.d("UnityKakaotalkAPI", "Initialize exit");
    }
    @JvmName("LoginWithKakaotalk")
    fun LoginWithKakaotalk(receiverObject: String) {
        Log.d("UnityKakaotalkAPI", "LoginWithKakaotalk enter");
        val callback: (OAuthToken?, Throwable?) -> Unit = { token, error ->
            if (error != null) {
                // fail to login
                UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", error.localizedMessage);
            }
            else if (token != null) {
                // success to login
                UnityPlayer.UnitySendMessage(receiverObject, "OnLoginSuccess", token.accessToken);
            }
        }

        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.");
        } else if (LoginClient.instance.isKakaoTalkLoginAvailable(context!!)) {
            LoginClient.instance.loginWithKakaoTalk(context!!, callback = callback);
        } else { // When kakaotalk not installed
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "Kakaotalk not installed on this device.");
        }
        Log.d("UnityKakaotalkAPI", "LoginWithKakaotalk exit");
    }

    @JvmName("LoginWithKakaoAccount")
    fun LoginWithKakaoAccount(receiverObject: String) {
        Log.d("UnityKakaotalkAPI", "LoginWithKakaoAccount enter");
        val callback: (OAuthToken?, Throwable?) -> Unit = { token, error ->
            if (error != null) {
                // fail to login
                UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", error.localizedMessage);
            }
            else if (token != null) {
                // success to login
                UnityPlayer.UnitySendMessage(receiverObject, "OnLoginSuccess", token.accessToken);
            }
        }

        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.");
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = callback);
        }
        Log.d("UnityKakaotalkAPI", "LoginWithKakaoAccount exit");
    }

    @JvmName("LoginWithKakao")
    fun LoginWithKakao(receiverObject: String) {
        Log.d("UnityKakaotalkAPI", "LoginWithKakao enter");
        val callback: (OAuthToken?, Throwable?) -> Unit = { token, error ->
            if (error != null) {
                // fail to login
                UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", error.localizedMessage);
            }
            else if (token != null) {
                // success to login
                UnityPlayer.UnitySendMessage(receiverObject, "OnLoginSuccess", token.accessToken);
            }
        }

        if (context == null) {
            UnityPlayer.UnitySendMessage(receiverObject, "OnLoginFail", "KakaoSdk not initialized.");
        } else if (LoginClient.instance.isKakaoTalkLoginAvailable(context!!)) {
            LoginClient.instance.loginWithKakaoTalk(context!!, callback = callback);
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = callback);
        }
        Log.d("UnityKakaotalkAPI", "LoginWithKakao exit");
    }

    @JvmName("GetKeyHash")
    fun GetKeyHash() : String {
        Log.d("UnityKakaotalkAPI", "GetKeyHash");
        if (context == null) return "";
        return Utility.getKeyHash(context!!);
    }

    companion object {
        private var sdk : UnityKakaotalkAPI? = null;

        @JvmStatic
        fun GetInstance() : UnityKakaotalkAPI {
            if (sdk == null)
                sdk = UnityKakaotalkAPI();
            return sdk!!;
        }
    }
}