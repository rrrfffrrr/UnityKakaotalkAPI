package com.rrrfffrrr.unity.kakaotalk.model

import com.google.gson.annotations.SerializedName

data class TalkProfile(
    @SerializedName("nickname") val nickname: String,
    @SerializedName("profile_image_url") val profileImageUrl: String,
    @SerializedName("thumbnail_url") val thumbnailUrl: String,
    @SerializedName("country_iso") val countryISO: String
)