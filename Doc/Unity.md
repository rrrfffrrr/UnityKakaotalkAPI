# C# interface

## KakaoSdk

A wrapper class to communicate between user code and plugin.  

### Methods
```js
void Initialize(void(), void(string))
void Login(LOGIN_METHOD, void(OAuthToken), void(string))
void LoginWithNewScopes(string[], void(OAuthToken), void(string))
void Logout(void(), void(string))
void Unlink(void(), void(string))
void GetUserInformation(void(UserInfo), void(string))
void GetProfile(void(TalkProfile), void(string))
void GetFriends(int offset, int count, int order, void(Friends), void(string))
```
```C#
string GetKeyHash()
```

#### Enums
```C#
enum LoginMethod { Error, Kakaotalk, KakaoAccount, Both }
enum Gender { FEMALE, MALE, UNKNOWN }
enum AgeRange { AGE_0_9, AGE_10_14, AGE_15_19, AGE_20_29, AGE_30_39, AGE_40_49, AGE_50_59, AGE_60_69, AGE_70_79, AGE_80_89, AGE_90_ABOVE, UNKNOWN }
static class Order { ASC="asc", DESC="desc" }
```

#### Strings
```C#
FAIL_RESULT_NOT_SUPPORTED_DEVICE = "UnityKakaotalkAPI not supported on this device";
FAIL_RESULT_UNEXPECTED_LOGIN_METHOD = "Unexpected login method";
```

### Model
Template of kakaotalk results to deserialize it

| Class       | Result of          | Contains                           |
| :---------- | :----------------- | :--------------------------------- |
| OAuthToken  | Login              | Token and scopes                   |
| UserInfo    | GetUserInformation | Account data                       |
| Account     | GetUserInformation | The data which need agreement      |
| Profile     | GetUserInformation | Nickname and icon URL              |
| TalkProfile | GetProfile         | Nickname and icon URL              |
| Friend      | GetFriends         | Friend data and favorite           |
| Friends     | GetFriends         | Friends list and number of friends |

