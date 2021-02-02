using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace KakaotalkEditor {
    [InitializeOnLoad]
    public class UPMDependencyManager {
        const string GraphicsSettingsAssetPath = "ProjectSettings/PackageManagerSettings.asset";
        static ListRequest Request;

        static UPMDependencyManager() {
            Request = Client.List();
            EditorApplication.update += Progress;
        }

        static void Progress() {
            if (Request.IsCompleted) {
                EditorApplication.update -= Progress;

                if (Request.Status == StatusCode.Success) {
                    bool isInstalled = false;
                    foreach (var package in Request.Result) {
                        if (package.name == @"jillejr.newtonsoft.json-for-unity") {
                            isInstalled = true;
                        }
                    }
                    if (!isInstalled) {
                        Client.Add(@"https://github.com/jilleJr/Newtonsoft.Json-for-Unity.git#upm");
                    }

                } else if (Request.Status >= StatusCode.Failure) {
                    Debug.LogError(Request.Error);
                }
            }
        }
    }
}