# How to install on android with unity jar resolver

## Copy Unity/* to Assets/  

## Goto Edit/Project Settings/Player/Android/Publishing Settings  

## Check and enable custom gradle setting  
Custom Launcher Gradle Template  
Custom Base Gradle Template  
Custom Gradle Properties Template  

## Add Android AppKey to Assets/Plugins/Android/launcherTemplate.gradle
```Gradle
android {
    defaultConfig {
        manifestPlaceholders = [ "KakaoTalkAndroidAppKey": "User AppKey here" ]
    }
}
```  

## Install [unity-jar-resolver](https://github.com/googlesamples/unity-jar-resolver)  

## Just run android resolver once!  
  
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