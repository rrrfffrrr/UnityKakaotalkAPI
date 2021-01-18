package com.example.unitykakaotalkapi

import android.content.Context
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.auth.AuthCodeHandlerActivity;
import com.kakao.sdk.auth.LoginClient
import com.unity3d.player.UnityPlayer;

class UnityKakaotalkAPI {
    companion object {
        private lateinit var context: Context;
        fun Initialize(context: Context, key: String) {
            this.context = context;
            KakaoSdk.init(context, key);
        }

        fun TryLogin(gameObject: String) {
            if (LoginClient.instance.isKakaoTalkLoginAvailable(context)) {
                LoginClient.instance.loginWithKakaoTalk(context) { token, error ->
                    if (error != null) {
                        // login fail
                        UnityPlayer.UnitySendMessage(gameObject, "OnLoginFail", error.message);
                    } else if (token != null) {
                        // login success token.accessToken
                        UnityPlayer.UnitySendMessage(gameObject, "OnLoginSuccess", token.accessToken);
                    }
                }
            } else {
                // login fail cause no kakaotalk
                UnityPlayer.UnitySendMessage(gameObject, "OnLoginFail", "No kakaotalk installed");
            }
        }
    }
}