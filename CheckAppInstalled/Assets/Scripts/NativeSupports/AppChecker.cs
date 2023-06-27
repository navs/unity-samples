namespace NativeSupports
{
    public static class AppChecker
    {
        public static bool IsDiscordInstalled() =>
            IsAppInstalled("discord", "Discord.app", "com.discord", "");
        
        private static bool IsAppInstalled(string winAppName, string mapAppName, string androidApp, string iOsApp)
        {
#if UNITY_STANDALONE_WIN
            return WindowsAppChecker.GetInstallApps(winAppName).Count > 0;
#elif UNITY_ANDROID
            return AndroidAppChecker.IsInstalled(androidApp);
#endif
        }
    }
}