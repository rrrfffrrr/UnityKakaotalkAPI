package com.rrrfffrrr.unity.kakaotalk

interface FriendsCallback {
    public fun onSuccess(friendsJson: String)
    public fun onFail(reason: String)
}