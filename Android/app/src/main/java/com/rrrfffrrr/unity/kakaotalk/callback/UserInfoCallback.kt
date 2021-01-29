package com.rrrfffrrr.unity.kakaotalk.callback

interface UserInfoCallback {
    public fun onSuccess(userJson: String)
    public fun onFail(reason: String)
}