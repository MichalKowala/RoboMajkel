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
    }
}
