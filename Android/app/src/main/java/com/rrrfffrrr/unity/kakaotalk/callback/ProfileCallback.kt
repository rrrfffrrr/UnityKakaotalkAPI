package com.rrrfffrrr.unity.kakaotalk.callback

interface ProfileCallback {
    public fun onSuccess(profileJson: String)
    public fun onFail(reason: String)
}