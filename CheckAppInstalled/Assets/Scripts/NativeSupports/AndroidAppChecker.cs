using System;
using UnityEngine;

namespace NativeSupports
{
#if UNITY_ANDROID
    // Manifest 에 <queries> 추가 필요
    //
    //     <queries>
    //       <package android:name="com.discord"/>
    //     </queries>
    public static class AndroidAppChecker
    {
        public static bool IsInstalled(string packageId)
        {
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var packageManager = activity.Call<AndroidJavaObject>("getPackageManager");
            try
            {
                var _ = packageManager.Call<AndroidJavaObject>("getPackageInfo", packageId, 0);
                return true;
            }
            catch (Exception _)
            {
                return false;
            }
        }
    }
#endif
}