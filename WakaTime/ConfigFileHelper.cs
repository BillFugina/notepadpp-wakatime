﻿using System;
using System.Text;
using System.Runtime.InteropServices;

namespace WakaTime
{
    class ConfigFileHelper
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// Retrive ApiKey from config file.
        /// </summary>
        /// <returns></returns>
        public static string getApiKey()
        {
            StringBuilder keyValue = new StringBuilder(255);
            string configFilepath = getConfigFilePath();
            if (string.IsNullOrWhiteSpace(configFilepath) == false)
            {
                if (GetPrivateProfileString("settings", "api_key", "", keyValue, 255, configFilepath) > 0)
                {
                    return keyValue.ToString();
                }
            }

            return null;
        }

        /// <summary>
        /// Update ApiKey file in config file
        /// </summary>
        /// <returns></returns>
        public static void updateApiKey(string apiKey)
        {
            string configFilepath = getConfigFilePath();
            if (string.IsNullOrWhiteSpace(apiKey) == false)
            {
                WritePrivateProfileString("settings", "api_key", apiKey, configFilepath);
            }
        }

        public static string getConfigFilePath()
        {
            string userHomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (string.IsNullOrWhiteSpace(userHomeDir) == false)
            {
                return userHomeDir + "\\.wakatime.cfg";
            }

            return null;
        }
    }
}
