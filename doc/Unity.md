# C# interface

## KakaoSdk

A wrapper class to communicate between user code and plugin.  
Return all results by calling ResultEvent. (Except dev feature like KeyHash)  
`string[] { TAG, [type], [whether success], [result data or fail reason] }`  

### Methods
```
void Initialize()
void Login(LOGIN_METHOD)
void Logout()
void GetUserInformation()
void GetProfile()
string GetKeyHash()
```

#### Sub classes
```
ResultEvent: UnityEvent<string[]>
KakaoPluginReceiver: MonoVihaviour
```

#### Enums
```
LOGIN_METHOD { Error, Kakaotalk, KakaoAccount, Both }
```

#### Strings
```
RECEIVER_OBJECT_NAME    = "Kakaotalk plugin result receiver";
TAG                     = "kakaotalk plugin";

RESULT_SUCCESS          = "success";
RESULT_FAIL             = "fail";

RESULT_TYPE_INITIALIZE  = "initialize";
RESULT_TYPE_LOGIN       = "login";
RESULT_TYPE_LOGOUT      = "logout";
RESULT_TYPE_USERINFO    = "user information";
RESULT_TYPE_PROFILE     = "profile";

FAIL_RESULT_NOT_AN_ANDROID_DEVICE = "Not an android device";
```

### Model
Template of kakaotalk results to deserialize it

| Class      | Result of          | Contains              |
| :--------- | :----------------- | :-------------------- |
| OAuthToken | Login              | Token and scopes      |
| UserInfo   | GetUserInformation | Account data          |
| Profile    | GetProfile         | Nickname and icon URL |


## Plugins/Android/UnityKakaotalkAPI.androidlib

A directory to store aar file and others for android build.  
