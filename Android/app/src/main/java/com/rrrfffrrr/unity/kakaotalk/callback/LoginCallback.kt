package com.rrrfffrrr.unity.kakaotalk.callback

interface LoginCallback {
    public fun onSuccess(tokenJson: String)
    public fun onFail(reason: String)
}