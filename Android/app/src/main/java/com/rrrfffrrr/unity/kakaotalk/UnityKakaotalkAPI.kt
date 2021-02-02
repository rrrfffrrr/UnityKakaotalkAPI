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


class UnityKakaotalkAPI {
    private var context: Context? = null
    private val TAG: String = "UnityKakaotalkAPI"

    @JvmName("Initialize")
    fun Initialize(context: Context, callback: InitializeCallback) {
        Log.d(TAG, "Initialize enter")
        if (this.context == null) {
            this.context = context
            try {
                val app = context.packageManager.getApplicationInfo(context.packageName, PackageManager.GET_META_DATA)
                val bundle: Bundle = app.metaData
                val key = bundle.getString("com.rrrfffrrr.unity.kakaotalk.KakaotalkAppKey")
                if (key != null) {
                    KakaoSdk.init(context, key)
                    callback.onSuccess();
                } else {
                    callback.onFail("AppKey not found");
                }
            } catch (e: Exception) {
                callback.onFail(e.message ?: ERROR_UNEXPECTED.format(this::Initialize.name));
            }
        } else {
            callback.onFail("Already initialized");
        }
        Log.d(TAG, "Initialize exit")
    }

    private fun BuildLoginCallback(callback: LoginCallback): (OAuthToken?, Throwable?) -> Unit  {
        return { token, error ->
            Log.d(TAG, "Login callback entered")
            if (error != null) {
                // fail to login
                callback.onFail(error.message ?: ERROR_UNEXPECTED.format("Login"))
            } else if (token != null) {
                // success to login
                callback.onSuccess(KakaoJson.toJson(token))
            }
            Log.d(TAG, "Login callback exit")
        }
    }
    @JvmName("LoginWithKakaotalk")
    fun LoginWithKakaotalk(callback: LoginCallback) {
        Log.d(TAG, "LoginWithKakaotalk enter")
        if (context == null) {
            callback.onFail(ERROR_NOTINITIALIZED)
        } else if (LoginClient.instance.isKakaoTalkLoginAvailable(context!!)) {
            LoginClient.instance.loginWithKakaoTalk(context!!, callback = BuildLoginCallback(callback))
        } else { // When kakaotalk not installed
            callback.onFail(ERROR_NOTINSTALLED)
        }
        Log.d(TAG, "LoginWithKakaotalk exit")
    }
    @JvmName("LoginWithKakaoAccount")
    fun LoginWithKakaoAccount(callback: LoginCallback) {
        Log.d(TAG, "LoginWithKakaoAccount enter")
        if (context == null) {
            callback.onFail(ERROR_NOTINITIALIZED)
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = BuildLoginCallback(callback))
        }
        Log.d(TAG, "LoginWithKakaoAccount exit")
    }
    @JvmName("LoginWithKakao")
    fun LoginWithKakao(callback: LoginCallback) {
        Log.d(TAG, "LoginWithKakao enter")
        if (context == null) {
            callback.onFail(ERROR_NOTINITIALIZED)
        } else if (LoginClient.instance.isKakaoTalkLoginAvailable(context!!)) {
            LoginClient.instance.loginWithKakaoTalk(context!!, callback = BuildLoginCallback(callback))
        } else {
            LoginClient.instance.loginWithKakaoAccount(context!!, callback = BuildLoginCallback(callback))
        }
        Log.d(TAG, "LoginWithKakao exit")
    }
    @JvmName("LoginWithNewScopes")
    fun LoginWithNewScopes(scopes: Array<String>, callback: LoginCallback) {
        Log.d(TAG, "LoginWithKakao enter")
        if (context == null) {
            callback.onFail(ERROR_NOTINITIALIZED)
        } else {
            LoginClient.instance.loginWithNewScopes(context!!, scopes.toList(), callback = BuildLoginCallback(callback))
        }
        Log.d(TAG, "LoginWithKakao exit")
    }

    @JvmName("Logout")
    fun Logout(callback: LogoutCallback) {
        Log.d(TAG, "Logout enter")
        UserApiClient.instance.logout { error ->
            if (error != null) {
                callback.onFail(error.message ?: ERROR_UNEXPECTED.format(this::Logout.name))
            } else {
                callback.onSuccess()
            }
        }
        Log.d(TAG, "Logout exit")
    }

    @JvmName("Unlink")
    fun Unlink(callback: DefaultCallback) {
        Log.d(TAG, "Unlink enter")
        UserApiClient.instance.unlink { error ->
            if (error != null) {
                callback.onFail( error.message ?: ERROR_UNEXPECTED.format(this::Unlink.name))
            } else {
                callback.onSuccess()
            }
        }
        Log.d(TAG, "Unlink exit")
    }

    @JvmName("GetUserInformation")
    fun GetUserInformation(callback: UserInfoCallback) {
        Log.d(TAG, "GetUserInformation entered")
        UserApiClient.instance.me { user, error ->
            if (error != null) {
                callback.onFail(error.message ?: ERROR_UNEXPECTED.format(this::GetUserInformation.name))
            } else if (user != null) {
                callback.onSuccess(KakaoJson.toJson(user))
            } else {
                callback.onFail(ERROR_NORESULT.format(this::GetUserInformation.name))
            }
        }
        Log.d(TAG, "GetUserInformation exit")
    }
    @JvmName("GetProfile")
    fun GetProfile(callback: ProfileCallback) {
        Log.d(TAG, "GetProfile entered")
        TalkApiClient.instance.profile { profile, error ->
            if (error != null) {
                callback.onFail(error.message ?: ERROR_UNEXPECTED.format(this::GetProfile.name))
            } else if (profile != null) {
                callback.onSuccess(KakaoJson.toJson(TalkProfile(profile.nickname, profile.profileImageUrl, profile.thumbnailUrl, profile.countryISO)))
            } else {
                callback.onFail(ERROR_NORESULT.format(this::GetProfile.name))
            }
        }
        Log.d(TAG, "GetProfile exit")
    }
    @JvmName("GetFriends")
    fun GetFriends(offset: Int, count: Int, order: String, callback: FriendsCallback) {
        Log.d(TAG, "GetFriends entered")
        var forder = Order.ASC
        if (order == "desc")
            forder = Order.DESC
        TalkApiClient.instance.friends(offset, count, forder, callback = { friends: Friends<Friend>?, error: Throwable? ->
            if (error != null) {
                callback.onFail(error.message ?: ERROR_UNEXPECTED.format(this::GetFriends.name))
            } else if (friends != null) {
                callback.onSuccess(KakaoJson.toJson(friends))
            } else {
                callback.onFail(ERROR_NORESULT.format(this::GetFriends.name))
            }
        })
        Log.d(TAG, "GetFriends exit")
    }

    @JvmName("GetKeyHash")
    fun GetKeyHash() : String {
        Log.d(TAG, "GetKeyHash")
        if (context == null) return ""
        return Utility.getKeyHash(context!!)
    }

    companion object {
        private var sdk : UnityKakaotalkAPI? = null
        private val TAG: String = "UnityKakaotalkAPI"

        const val ERROR_NOTINITIALIZED = "Plugin not initialized"
        const val ERROR_NOTINSTALLED = "Kakaotalk not installed on this device"
        const val ERROR_UNEXPECTED = "Something went wrong while %s"
        const val ERROR_NORESULT = "No result while %s"

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