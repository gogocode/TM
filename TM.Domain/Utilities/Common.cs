using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Utilities
{
    public static class Common
    {
        public static string Encrypt(string encrypt)
        {
            string ret = string.Empty;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encrypt);

            using (MD5CryptoServiceProvider csp = new MD5CryptoServiceProvider())
            {
                ret = BitConverter.ToString(csp.ComputeHash(inputByteArray)).Replace("-", string.Empty);
            }

            return ret;
        }
    }
}
