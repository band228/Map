using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Common.DataConfig
{
    public class TextHelper
    {
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "muoisaukytu12345";
        //private const string initVector = "tu89geji340t89u2";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        public static string Encrypt(string plainText)
        {
            string passPhrase = "pq2324";
            try
            {
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch
            {
                return null;
            }
        }
        #region Giai Ma Pass Connect
        public static string Decrypt2(string toDecrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            int a = toDecrypt.Length;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
       
        #endregion

        public static string Decrypt(string cipherText)
        {
            string passPhrase = "pq2324";
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch
            {
                return null;
            }
        }

        //public static void WriteLogAttendance(string input, string filename)
        //{
        //    if (string.IsNullOrEmpty(input) == false)
        //    {
        //        try
        //        {
        //            string path = Application.StartupPath + @"\\Logs\\" + filename + ".txt";
        //            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true);
        //            sw.WriteLine(input);
        //            sw.Close();
        //        }
        //        catch
        //        { }
        //    }
        //}

        #region ma hoa Pass
        public string EncodePass(string strInput)
        {
            string strTempOut = "";
            string strOutput = "";
            for (int i = 0; i < 4; i++)
            {
                strTempOut = EncodeSHA1(i + strInput + "iSEAS");
                strTempOut = EncodeSHA1(strTempOut.Replace('1', '8').Replace('2', '1').Replace('3', '7').Replace('4', '6').Replace('5', '4').Replace('7', '5').Replace('8', '3').Replace('9', '2').Replace('0', '9').Replace('a', 'f').ToString());
                strOutput += strTempOut;
            }
            if (strOutput.Length > 250)
                return strOutput.Substring(0, 250);
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
    public class Converter
    {
        private static char[] tcvnchars = {
            'µ', '¸', '¶', '·', '¹',
    '¨', '»', '¾', '¼', '½', 'Æ',
    '©', 'Ç', 'Ê', 'È', 'É', 'Ë',
    '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ',
    'ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö',
    '×', 'Ý', 'Ø', 'Ü', 'Þ',
    'ß', 'ã', 'á', 'â', 'ä',
    '«', 'å', 'è', 'æ', 'ç', 'é',
    '¬', 'ê', 'í', 'ë', 'ì', 'î',
    'ï', 'ó', 'ñ', 'ò', 'ô',
    '­', 'õ', 'ø', 'ö', '÷', 'ù',
    'ú', 'ý', 'û', 'ü', 'þ',
    '¡', '¢', '§', '£', '¤', '¥', '¦'
            };
        private static char[] unichars = {
            'à', 'á', 'ả', 'ã', 'ạ',
    'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ',
    'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ',
    'đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ',
    'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ',
    'ì', 'í', 'ỉ', 'ĩ', 'ị',
    'ò', 'ó', 'ỏ', 'õ', 'ọ',
    'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ',
    'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ',
    'ù', 'ú', 'ủ', 'ũ', 'ụ',
    'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự',
    'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ',
    'Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'
            };

        private static char[] convertTable;

        static Converter()
        {
            convertTable = new char[256];

            for (int i = 0; i < 256; i++)
                convertTable[i] = (char)i;

            for (int i = 0; i < tcvnchars.Length; i++)
                convertTable[tcvnchars[i]] = unichars[i];
        }

        public static string TCVN3ToUnicode(string value)
        {
            if (string.IsNullOrEmpty(value) != true)
            {
                char[] chars = value.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                    if (chars[i] < (char)256)
                        chars[i] = convertTable[chars[i]];
                string text = new string(chars);
                return text;
            }
            else
                return null;
        }
    }
}
