﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Core
{
    public static class GlobalVariables
    {
        public static bool ServerIsOnline { get; set; }
        public static string BackendServer_IP { get; set; }
        public static string BackendServer_URL { get; set; }
        public static int? BackendServer_Port { get; set; }
    }
}
