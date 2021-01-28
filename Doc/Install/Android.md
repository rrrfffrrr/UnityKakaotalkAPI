# How to install on android

## Copy Unity/* to Assets/  

## Goto Edit/Project Settings/Player/Android/Publishing Settings  

## Check and enable custom gradle setting  
Custom Launcher Gradle Template  
Custom Base Gradle Template  
Custom Gradle Properties Template  

## Enable AndroidX on Assets/Plugins/Android/gradleTemplate.properties
```js
android.useAndroidX=true
android.enableJetifier=true
```  
## Add maven url to Assets/Plugins/Android/baseProjectTemplate.gradle
```Gradle
allprojects {
    repositories {
        maven { url 'https://devrepo.kakao.com/nexus/content/groups/public/' }
    }
}
```  
## Add dependencies to Assets/Plugins/Android/launcherTemplate.gradle
```Gradle
dependencies {
    implementation "org.jetbrains.kotlin:kotlin-stdlib:1.3.72"
    implementation "com.kakao.sdk:v2-user:2.2.0" // 카카오 로그인
    implementation "com.kakao.sdk:v2-talk:2.2.0" // 친구, 메시지(카카오톡)
    implementation "com.kakao.sdk:v2-story:2.2.0" // 카카오스토리
    implementation "com.kakao.sdk:v2-link:2.2.0" // 메시지(카카오링크)
    implementation "com.kakao.sdk:v2-navi:2.2.0" // 카카오내비
}
```  
## Add Android AppKey to Assets/Plugins/Android/launcherTemplate.gradle
```Gradle
android {
    defaultConfig {
        manifestPlaceholders = [ "KakaoTalkAndroidAppKey": "User AppKey here" ]
    }
}
```  
  
## That's it! Now we can call Kakaotalk.KakaoSdk.Initialize()!
```C#
using UnityEngine;
using Kakaotalk;

public class TestKakaotalkAPI : MonoBehaviour
{
    void Awake()
    {
        KakaoSdk.Initialize(() => {
            KakaoSdk.Login(KakaoSdk.LOGIN_METHOD.Both, (token) => {
            Debug.Log(JsonUtility.ToJson(token));
            KakaoSdk.GetProfile((profile) => {
                Debug.Log(JsonUtility.ToJson(profile));
            }, e => Debug.Log(e) );
            }, e => Debug.Log(e) );
        }, e => Debug.Log(e) );
    }
}
```