using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.DataConfig
{
    public class SqlHelper
    {
        #region ma hoa
        public string EncodeLicense(string strInput)
        {
            string strTempOut = "";
            string strOutput = "";
            for (int i = 0; i < 8; i++)
            {
                strTempOut = EncodeSHA1(i + strInput + "DO");
                strTempOut = EncodeSHA1(strTempOut.Replace('1', '8').Replace('2', '1').Replace('3', '7').Replace('4', '6').Replace('5', '4').Replace('7', '5').Replace('8', '3').Replace('9', '2').Replace('0', '9').Replace('a', 'f').ToString());
                strOutput += strTempOut;
            }
            if (strOutput.Length > 255)
                return strOutput.Substring(0, 255);
            else
                return strOutput;

        }
        private string EncodeSHA1(string pass)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
            bs = sha1.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x1").ToLower());
            }
            pass = s.ToString();
            return pass;
        }
        #endregion
    }
}
