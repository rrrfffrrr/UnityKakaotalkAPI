# C# interface

## KakaoSdk

A wrapper class to communicate between user code and plugin.  

### Methods
```
void Initialize(onSuccess, onFail)
void Login(LOGIN_METHOD, onSuccess, onFail)
void LoginWithNewScopes(scopes, onSucces, onFail)
void Logout(onSuccess, onFail)
void GetUserInformation(onSuccess, onFail)
void GetProfile(onSuccess, onFail)
void GetFriends(offset, count, order, onSuccess, onFail)
```
```
string GetKeyHash()
```

#### Enums
```
LOGIN_METHOD { Error, Kakaotalk, KakaoAccount, Both }
ORDER { ASC, DESC }
```

#### Strings
```
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

