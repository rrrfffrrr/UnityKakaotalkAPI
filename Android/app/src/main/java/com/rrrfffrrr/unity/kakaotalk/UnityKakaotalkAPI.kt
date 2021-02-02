package com.rrrfffrrr.unity.kakaotalk

import android.content.Context
import android.content.pm.PackageManager
import android.os.Bundle
import android.util.Log
import com.kakao.sdk.auth.LoginClient
import com.kakao.sdk.auth.model.OAuthToken
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.common.util.KakaoJson
import com.kakao.sdk.common.util.Utility
import com.kakao.sdk.talk.TalkApiClient
import com.kakao.sdk.talk.model.Friend
import com.kakao.sdk.talk.model.Friends
import com.kakao.sdk.talk.model.Order
import com.kakao.sdk.user.UserApiClient
import com.rrrfffrrr.unity.kakaotalk.callback.*
import com.rrrfffrrr.unity.kakaotalk.model.*

@Suppress("unused")
class UnityKakaotalkAPI {
    private var context: Context? = null

    @JvmName("initialize")
    fun initialize(context: Context, callback: DefaultCallback) {
        Log.d(TAG, "initialize enter")
        if (this.context == null) {
            this.context = context
            try {
                val app = context.packageManager.getApplicationInfo(context.packageName, PackageManager.GET_META_DATA)
                val bundle: Bundle = app.metaData
                val key = bundle.getString("com.rrrfffrrr.unity.kakaotalk.KakaotalkAppKey")
                if (key != null) {
                    KakaoSdk.init(context, key)
                    callback.onSuccess()
                } else {
                    callback.onFail("AppKey not found")
                }
            } catch (e: Exception) {
                callback.onFail(e.message ?: ERROR_UNEXPECTED.format(this::initialize.name))
            }
        } else {
            callback.onFail("Already initialized")
        }
        Log.d(TAG, "initialize exit")
    }

    private fun buildLoginCallback(callback: JsonCallback): (OAuthToken?, Throwable?) -> Unit  {
        return { token, error ->
            Log.d(TAG, "login callback entered")
            if (error != null) {
                // fail to login
                callback.onFail(error.message ?: ERROR_UNEXPECTED.format("Login"))
            } else if (token != null) {
                // success to login
                callback.onSuccess(KakaoJson.toJson(token))
            }
            Log.d(TAG, "login callback exit")
        }
    }
    @JvmName("loginWithKakaotalk")
    fun loginWithKakaotalk(callback: JsonCallback) {
        Log.d(TAG, "loginWithKakaotalk enter")
        when {
            context == null -> {
                callback.onFail(ERROR_NOTINITIALIZED)
            }
            LoginClient.instance.isKakaoTalkLoginAvailable(context!!) -> {
                LoginClient.instance.loginWithKakaoTalk(context!!, callback = buildLoginCallback(callback))
            }
            else -> { // When kakaotalk not installed
                callback.onFail(ERROR_NOTINSTALLED)
            }
        }
        Log.d(TAG, "loginWithKakaotalk exit")
    }
    @JvmName("loginWithKakaoAccount")
    fun loginWithKakaoAccount(callback: JsonCallback) {
        Log.d(TAG, "loginWithKakaoAccount enter")
        if (context == null) {
            callback.onFail(ERROR_NOTINITIALIZED)
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = buildLoginCallback(callback))
        }
        Log.d(TAG, "loginWithKakaoAccount exit")
    }
    @JvmName("loginWithKakao")
    fun loginWithKakao(callback: JsonCallback) {
        Log.d(TAG, "loginWithKakao enter")
        when {
            context == null -> {
                callback.onFail(ERROR_NOTINITIALIZED)
            }
            LoginClient.instance.isKakaoTalkLoginAvailable(context!!) -> {
                LoginClient.instance.loginWithKakaoTalk(context!!, callback = buildLoginCallback(callback))
            }
            else -> {
                LoginClient.instance.loginWithKakaoAccount(context!!, callback = buildLoginCallback(callback))
            }
        }
        Log.d(TAG, "loginWithKakao exit")
    }
    @JvmName("loginWithNewScopes")
    fun loginWithNewScopes(scopes: Array<String>, callback: JsonCallback) {
        Log.d(TAG, "loginWithKakao enter")
        if (context == null) {
            callback.onFail(ERROR_NOTINITIALIZED)
        } else {
            LoginClient.instance.loginWithNewScopes(context!!, scopes.toList(), callback = buildLoginCallback(callback))
        }
        Log.d(TAG, "loginWithKakao exit")
    }

    @JvmName("logout")
    fun logout(callback: DefaultCallback) {
        Log.d(TAG, "logout enter")
        UserApiClient.instance.logout { error ->
            if (error != null) {
                callback.onFail(error.message ?: ERROR_UNEXPECTED.format(this::logout.name))
            } else {
                callback.onSuccess()
            }
        }
        Log.d(TAG, "logout exit")
    }

    @JvmName("unlink")
    fun unlink(callback: DefaultCallback) {
        Log.d(TAG, "unlink enter")
        UserApiClient.instance.unlink { error ->
            if (error != null) {
                callback.onFail( error.message ?: ERROR_UNEXPECTED.format(this::unlink.name))
            } else {
                callback.onSuccess()
            }
        }
        Log.d(TAG, "unlink exit")
    }

    @JvmName("getUserInformation")
    fun getUserInformation(callback: JsonCallback) {
        Log.d(TAG, "getUserInformation entered")
        UserApiClient.instance.me { user, error ->
            when {
                error != null -> {
                    callback.onFail(error.message ?: ERROR_UNEXPECTED.format(this::getUserInformation.name))
                }
                user != null -> {
                    callback.onSuccess(KakaoJson.toJson(User(user)))
                }
                else -> {
                    callback.onFail(ERROR_NORESULT.format(this::getUserInformation.name))
                }
            }
        }
        Log.d(TAG, "getUserInformation exit")
    }
    @JvmName("getProfile")
    fun getProfile(callback: JsonCallback) {
        Log.d(TAG, "getProfile entered")
        TalkApiClient.instance.profile { profile, error ->
            when {
                error != null -> {
                    callback.onFail(error.message ?: ERROR_UNEXPECTED.format(this::getProfile.name))
                }
                profile != null -> {
                    callback.onSuccess(KakaoJson.toJson(TalkProfile(profile)))
                }
                else -> {
                    callback.onFail(ERROR_NORESULT.format(this::getProfile.name))
                }
            }
        }
        Log.d(TAG, "getProfile exit")
    }
    @JvmName("getFriends")
    fun getFriends(offset: Int, count: Int, order: String, callback: JsonCallback) {
        Log.d(TAG, "getFriends entered")
        var forder = Order.ASC
        if (order == "desc")
            forder = Order.DESC
        TalkApiClient.instance.friends(offset, count, forder, callback = { friends: Friends<Friend>?, error: Throwable? ->
            when {
                error != null -> {
                    callback.onFail(error.message ?: ERROR_UNEXPECTED.format(this::getFriends.name))
                }
                friends != null -> {
                    callback.onSuccess(KakaoJson.toJson(friends))
                }
                else -> {
                    callback.onFail(ERROR_NORESULT.format(this::getFriends.name))
                }
            }
        })
        Log.d(TAG, "getFriends exit")
    }

    @JvmName("getKeyHash")
    fun getKeyHash() : String {
        Log.d(TAG, "getKeyHash")
        if (context == null) return ""
        return Utility.getKeyHash(context!!)
    }

    companion object {
        private const val TAG: String = "UnityKakaotalkAPI"

        const val ERROR_NOTINITIALIZED = "Plugin not initialized"
        const val ERROR_NOTINSTALLED = "Kakaotalk not installed on this device"
        const val ERROR_UNEXPECTED = "Something went wrong while %s"
        const val ERROR_NORESULT = "No result while %s"

        private var sdk : UnityKakaotalkAPI? = null

        @JvmStatic
        fun getInstance() : UnityKakaotalkAPI {
            Log.d(TAG, "getInstance")
            if (sdk == null) {
                Log.d(TAG, "Create new instance")
                sdk = UnityKakaotalkAPI()
            }
            return sdk!!
        }
    }

}