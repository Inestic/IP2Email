﻿using IP2Email.Helpers;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IP2Email.Classes
{
    public class AppConfig
    {
        internal AppConfig()
        {
            isConfigured(EmailBody, EmailServer, EmailServerPort, RecipientEmail, SenderEmail, SenderPassword);
        }

        internal string EmailBody { get => DecodeFromRegistry("EBO"); set => EncodeToRegistry("EBO", value); }
        internal string EmailServer { get => DecodeFromRegistry("ESE"); set => EncodeToRegistry("ESE", value); }
        internal string EmailServerPort { get => DecodeFromRegistry("ESP"); set => EncodeToRegistry("ESP", value); }
        internal bool IsConfigured { get; set; }
        internal string RecipientEmail { get => DecodeFromRegistry("REM"); set => EncodeToRegistry("REM", value); }
        internal string SenderEmail { get => DecodeFromRegistry("SEM"); set => EncodeToRegistry("SEM", value); }
        internal string SenderPassword { get => DecodeFromRegistry("SEP"); set => EncodeToRegistry("SEP", value); }

        private string DecodeFromRegistry(string regKeyName)
        {
            string plaintext = null;
            byte[] regKeyValue = Registry.CurrentUser.OpenSubKey(TextHelper.RegistryAppPath)?.GetValue(regKeyName) as byte[];

            if (regKeyValue != null)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(TextHelper.SecurityKey);
                    aes.IV = Encoding.UTF8.GetBytes(TextHelper.SecurityKey);

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(regKeyValue))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }

            return plaintext;
        }

        private void EncodeToRegistry(string regKeyName, string regKeyValue)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(TextHelper.SecurityKey);
                aes.IV = Encoding.UTF8.GetBytes(TextHelper.SecurityKey);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(regKeyValue);
                        }
                    }

                    Registry.CurrentUser.OpenSubKey(TextHelper.RegistryKeySoftware, true)
                                        .CreateSubKey(TextHelper.RegistryKeyAuthor, true)
                                        .CreateSubKey(TextHelper.RegistryAppName, true)
                                        .SetValue(regKeyName, msEncrypt.ToArray());
                }
            }
        }

        private void isConfigured(params string[] properties)
        {
            foreach (var property in properties)
            {
                if (property is null)
                {
                    IsConfigured = false;
                    break;
                }

                IsConfigured = true;
            }
        }
    }
}