package com.rrrfffrrr.unity.kakaotalk.callback

interface JsonCallback {
    fun onSuccess(result: String)
    fun onFail(reason: String)
}