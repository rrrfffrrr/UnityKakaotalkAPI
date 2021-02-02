package com.rrrfffrrr.unity.kakaotalk.model

import com.google.gson.annotations.SerializedName
import com.kakao.sdk.user.model.User

data class User (
    @SerializedName("id") val id: Long,
    @SerializedName("properties") val properties: List<Pair<String, String>>?,
    @SerializedName("kakao_account") val kakaoAccount: Account?,
    @SerializedName("group_user_token") val groupUserToken: String?,
    @SerializedName("connected_at") val connectedAt: Long,
    @SerializedName("synched_at") val synchedAt: Long
) {
    constructor(user: User) : this(
            user.id,
            when (user.properties) {
                null -> null
                else -> user.properties!!.toList()
            },
            when (user.kakaoAccount) {
                null -> null
                else -> Account(user.kakaoAccount!!)
            },
            user.groupUserToken,
            user.connectedAt?.time ?: -1,
            user.synchedAt?.time ?: -1
    )
}