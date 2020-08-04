using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stash_Search.API
{
    class IniManagerAPI
    {
        private static string filePath;

        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(byte[] section, byte[] key, byte[] val, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] def, byte[] retVal, int size, string filePath);

        public IniManagerAPI(string iniPath)
        {
            filePath = iniPath;
        }

        //与ini交互必须统一编码格式
        private static byte[] getBytes(string s, string encodingName)
        {
            return null == s ? null : Encoding.GetEncoding(encodingName).GetBytes(s);
        }
        public string ReadIniFile(string section, string key, string def = null, string encodingName = "utf-8", int size = 1024)
        {
            string fileName = filePath;
            byte[] buffer = new byte[size];
            int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(key, encodingName), getBytes(def, encodingName), buffer, size, fileName);
            return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        }
        public bool WriteIniFile(string section, string key, string value, string encodingName = "utf-8")
        {
            string fileName = filePath;
            return WritePrivateProfileString(getBytes(section, encodingName), getBytes(key, encodingName), getBytes(value, encodingName), fileName);
        }
    }
}
