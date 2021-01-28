package com.rrrfffrrr.unity.kakaotalk

interface ProfileCallback {
    public fun onSuccess(profileJson: String)
    public fun onFail(reason: String)
}