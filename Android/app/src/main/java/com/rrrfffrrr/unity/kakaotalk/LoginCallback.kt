package com.rrrfffrrr.unity.kakaotalk

interface LoginCallback {
    public fun onSuccess(tokenJson: String)
    public fun onFail(reason: String)
}