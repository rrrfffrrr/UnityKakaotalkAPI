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
enum LOGIN_METHOD { Error, Kakaotalk, KakaoAccount, Both }
static class ORDER { ASC="asc", DESC="desc" }
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

