#if UNITY_STANDALONE_WIN

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace NativeSupports
{
    public static class WindowsAppChecker
    {
        private static readonly List<string> keys = new()
        {
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
            @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
        };
        private static List<string> installedApps;

        public static List<string> GetInstallApps(string appName, bool useCache = false)
        {
            installedApps ??= new();
            if (!useCache || installedApps == null || installedApps.Count == 0)
            {
                FindInstalls(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64), keys, installedApps);
                FindInstalls(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64), keys, installedApps);
                installedApps = installedApps.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                installedApps.Sort();
            }

            var appNameLower = appName.ToLower();
            return installedApps.Where(app => app.ToLower().Contains(appName)).ToList();
        }
        
        private static void FindInstalls(RegistryKey regKey, List<string> keys, List<string> installed)
        {
            foreach (string key in keys)
            {
                using (RegistryKey rk = regKey.OpenSubKey(key))
                {
                    if (rk == null)
                    {
                        continue;
                    }
                    foreach (string skName in rk.GetSubKeyNames())
                    {
                        using (RegistryKey sk = rk.OpenSubKey(skName))
                        {
                            try
                            {
                                installed.Add(Convert.ToString(sk.GetValue("DisplayName")));
                            }
                            catch (Exception ex)
                            { }
                        }
                    }
                }
            }
        }
    }
}

#endif