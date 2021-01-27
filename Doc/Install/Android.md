# How to install on android

## Copy Unity/* to Assets/  
## Goto Edit/Project Settings/Player/Android/Publishing Settings  
## Check Custom Launcher Gradle Template, Custom Base Gradle Template, Custom Gradle Properties Template  
## Enable AndroidX on Assets/Plugins/Android/gradleTemplate.properties
```
android.useAndroidX=true
android.enableJetifier=true
```  
## Add maven url to Assets/Plugins/Android/baseProjectTemplate.gradle
```
allprojects {
    repositories {
        maven { url 'https://devrepo.kakao.com/nexus/content/groups/public/' }
    }
}
```  
## Add dependencies to Assets/Plugins/Android/launcherTemplate.gradle
```
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
```
android {
    defaultConfig {
        manifestPlaceholders = [ "KakaoTalkAndroidAppKey": User AppKey here ]
    }
}
```  
  
## That's it! Now we can call Kakaotalk.KakaoSdk.Initialize()!