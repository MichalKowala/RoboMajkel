using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboMajkel.Utilities
{
    public static class EnvVars
    {
        public static string RoboMajkelToken => Environment.GetEnvironmentVariable("RoboMajkelToken");
        public static string RoboMajkelYTApiKey => Environment.GetEnvironmentVariable("RoboMajkelYTApiKey");
        public static string RoboMajkelYTApiKeyBackup => Environment.GetEnvironmentVariable("RoboMajkelYTApiKeyBackup");
        public static string RoboMajkelYTApiKeyBackup2 => Environment.GetEnvironmentVariable("RoboMajkelYTApiKeyBackup2");
        public static string RoboMajkelYTApiKeyBackup3 => Environment.GetEnvironmentVariable("RoboMajkelYTApiKeyBackup3");
        public static string RoboMajkelTtsAudioFileLocation => Environment.GetEnvironmentVariable("RoboMajkelTtsAudioFileLocation");
    }
}
