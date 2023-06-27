namespace NativeSupports
{
    public static class AppChecker
    {
        public static bool IsDiscordInstalled() =>
            IsAppInstalled("discord", "Discord.app", "com.discord", "");
        
        private static bool IsAppInstalled(string winAppName, string macAppName, string androidApp, string iOsApp)
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            return WindowsAppChecker.GetInstallApps(winAppName).Count > 0;
#elif UNITY_ANDROID
            return AndroidAppChecker.IsInstalled(androidApp);
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            return MacAppChecker.IsAppInstalled(macAppName);
#elif UNITY_IPHONE
#endif
            // not supported
            return false;
        }
    }
}