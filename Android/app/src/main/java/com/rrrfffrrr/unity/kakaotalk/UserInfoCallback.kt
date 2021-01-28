package com.rrrfffrrr.unity.kakaotalk

interface UserInfoCallback {
    public fun onSuccess(userJson: String)
    public fun onFail(reason: String)
}