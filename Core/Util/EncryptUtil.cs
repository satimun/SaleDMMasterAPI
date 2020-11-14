using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Util
{
    public static class EncryptUtil
    {
        

        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static string NewID(this string secret)
        {
            string hash = "";
            using (var sha256 = SHA256.Create())
            {
                var key = $"{Guid.NewGuid().ToString()}+{secret}";
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return hash;
        }

        public static string Hash(this string key)
        {
            string hash = "";
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return hash;
        }

        public static string MD5(this string s)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();
            }
        }

        public static string ENDCodeNEW(string d)
        {            
            string strOutput = "";

            if (!String.IsNullOrEmpty(d))
            {
                string strInput = d.ToUpper();

                char[] charInput = strInput.ToCharArray();
                char[] charOutput = new char[strInput.Length];

                for (int i = 0; i < charInput.Length; i++)
                {

                    charOutput[i] = Convert.ToChar((int)charInput[i] + 5);
                }


                strOutput = new string(charOutput);
            }

            

            return strOutput;
        }

        public static string DECodeNEW(string d)
        {
            string strOutput = "";

            if (!String.IsNullOrEmpty(d))
            {
                string strInput = d.ToUpper();

                char[] charInput = strInput.ToCharArray();
                char[] charOutput = new char[strInput.Length];

                for (int i = 0; i < charInput.Length; i++)
                {

                    charOutput[i] = Convert.ToChar((int)charInput[i] - 5);
                }


                strOutput = new string(charOutput);
            }
                

            return strOutput;
        }
    }
}
