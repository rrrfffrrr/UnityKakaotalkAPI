package com.rrrfffrrr.unity.kakaotalk.model

import com.kakao.sdk.user.model.Account
import com.kakao.sdk.user.model.AgeRange
import com.kakao.sdk.user.model.Gender
import com.kakao.sdk.user.model.Profile
import java.util.*

data class Account (
        val profileNeedsAgreement: Boolean?,
        val profile: Profile?,

        val emailNeedsAgreement: Boolean?,
        val isEmailValid: Boolean?,
        val isEmailVerified: Boolean?,
        val email: String?,

        val ageRangeNeedsAgreement: Boolean?,
        val ageRange: AgeRange?,

        val birthyearNeedsAgreement: Boolean?,
        val birthyear: String?,

        val birthdayNeedsAgreement: Boolean?,
        val birthday: String?,

        val genderNeedsAgreement: Boolean?,
        val gender: Gender?,

        val ciNeedsAgreement: Boolean?,
        val ci: String?,
        val ciAuthenticatedAt: Date?,

        val legalNameNeedsAgreement: Boolean?,
        val legalName: String?,

        val legalBirthDateNeedsAgreement: Boolean?,
        val legalBirthDate: String?,

        val legalGenderNeedsAgreement: Boolean?,
        val legalGender: Gender?,

        val phoneNumberNeedsAgreement: Boolean?,
        val phoneNumber: String?
    ) {
    constructor(account: Account) : this()
}