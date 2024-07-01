using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore
{
    public static class Configuration
    {
        public const int DefaultStatusCode = 200;
        public static string ConnectionString { get; set; } = string.Empty;
        public static string BackendUrl { get; set; } = "https://localhost:7176";
        public static string FrontendUrl { get; set; } = "https://localhost:7180";
    }
}
