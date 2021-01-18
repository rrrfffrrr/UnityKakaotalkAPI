package com.example.unitykakaotalkapi

import android.content.Context
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.auth.AuthCodeHandlerActivity;
import com.unity3d.player.UnityPlayer;

class UnityKakaotalkAPI {
    companion object {
        private lateinit var context: Context;
        fun Initialize(context: Context, key: String) {
            this.context = context;



            KakaoSdk.init(context, key);
        }


    }
}