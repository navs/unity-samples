using System.IO;
using System.Linq;

namespace NativeSupports
{
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
    public static class MacAppChecker
    {
        private static readonly string[] applications =
        {
            "/Applications"
        };

        public static bool IsAppInstalled(string appName)
        {
            return applications.Any(app => Directory.Exists(Path.Combine(app, appName)));
        }
    }
#endif
}