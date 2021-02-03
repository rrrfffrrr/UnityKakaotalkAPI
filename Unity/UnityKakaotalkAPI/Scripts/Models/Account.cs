using System;

namespace Kakaotalk.Model
{
    [Serializable]
    public class Account {
        public AgeRange? age_range;
        public bool? age_range_needs_agreement;
        public string birthday;
        public bool? birthday_needs_agreement;
        public string birthyear;
        public bool? birthyear_needs_agreement;
        public string ci;
        public DateTime? ci_authenticated_at;
        public bool? ci_needs_agreement;
        public string email;
        public bool? email_needs_agreement;
        public Gender? gender;
        public bool? gender_needs_agreement;
        public bool? is_email_valid;
        public bool? is_email_verified;
        public string legal_birth_date;
        public bool? legal_birth_date_needs_agreement;
        public Gender? legal_gender;
        public bool? legal_gender_needs_agreement;
        public string legal_name;
        public bool? legal_name_needs_agreement;
        public string phone_number;
        public bool? phone_number_needs_agreement;
        public Profile profile;
        public bool? profile_needs_agreement;
    }
}