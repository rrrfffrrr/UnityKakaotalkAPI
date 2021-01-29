using System;

namespace Kakaotalk.Model
{
    [Serializable]
    public class Account
    {
        public string age_range;
        public string age_range_needs_agreement;
        public string birthday;
        public string birthday_needs_agreement;
        public string birthyear;
        public string birthyear_needs_agreement;
        public string ci;
        public string ci_authenticated_at;
        public string ci_needs_agreement;
        public string email;
        public string email_needs_agreement;
        public string gender;
        public string gender_needs_agreement;
        public string is_email_valid;
        public string is_email_verified;
        public string legal_birth_date;
        public string legal_birth_date_needs_agreement;
        public string legal_gender;
        public string legal_gender_needs_agreement;
        public string legal_name;
        public string legal_name_needs_agreement;
        public string phone_number;
        public string phone_number_needs_agreement;
        public Profile profile;
        public string profile_needs_agreement;
    }
}