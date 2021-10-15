using System;

namespace IP2Email.Helpers
{
    internal static class TextHelper
    {
        internal static readonly string EmailBody = $"PC \"{Environment.MachineName.ToUpper()}\" has ip address";
        internal static readonly string EmailSubject = $"PC \"{Environment.MachineName.ToUpper()}\" ip address";
        internal static readonly string FailedGetInternetIP = "Failed to obtain public IP address";
        internal static readonly string FailedGetLocalIP = "Failed to obtain local IP address";
        internal static readonly string InternetIpSite = "https://ifconfig.me/ip";
        internal static readonly string RegistryAppName = "IP2Email";
<<<<<<< HEAD
        internal static readonly string RegistryKeyAuthor = "Inestic";
        internal static readonly string RegistryKeySoftware = "Software";
        internal static readonly string SecurityKey = "";
        internal static readonly string SenderDisplayName = "IP2Email";
    }
}
=======
        internal static readonly string RegistryAppPath = $@"{RegistryKeySoftware}\{RegistryKeyAuthor}\{RegistryAppName}";
        internal static readonly string RegistryKeyAuthor = "Inestic";
        internal static readonly string RegistryKeySoftware = "Software";
        internal static readonly string SecurityKey = "1234567812345678";
        internal static readonly string SenderDisplayName = "IP2Email";
    }
}
>>>>>>> 6c0a5d048f41f68a5de63dee15f0d81e87e6ad0f
