package com.rrrfffrrr.unity.kakaotalk.callback

interface FriendsCallback {
    public fun onSuccess(friendsJson: String)
    public fun onFail(reason: String)
}