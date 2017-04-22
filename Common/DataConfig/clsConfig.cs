using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Xml;
using System.IO;
using System.Security.Cryptography;

namespace Common.DataConfig
{
    public class clsConfig
    {
        // public static string CONNECTIONSTRING = "Data Source=192.168.1.7;Initial Catalog=PMS_db;User ID=sa;Password=saoviet@123";
        //public static string CONNECTIONSTRING = "Data Source=113.160.93.142;Initial Catalog=PMS_db;User ID=sa;Password=saoviet@123";
        //public static string CONNECTIONSTRINGDATA = "";
        //public static string USERNAME = "";
        //public static string NAME = "";
        public static string CONNECTIONSTRING = "";
        public static string CONNECTIONSTRINGDATA = "";
        public static string USERNAME = "";
        public static string NAME = "";
        protected static string CKEY = "iLock_Kobe";
        public clsConfig()
        {
            ReadConnectionString();
        }
        #region Doc Chuoi KetNoi
        public static string ReadConnectionString()
        {
            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(Application.StartupPath + "\\appData.config");
                foreach (XmlElement xElement in XmlDoc.DocumentElement)
                {
                    if (xElement.Name == "connectionStrings")
                    {
                        CONNECTIONSTRING = TextHelper.Decrypt2(xElement.FirstChild.Attributes["connectionString"].Value.Remove(2, 3), CKEY, true);
                        CONNECTIONSTRING = CONNECTIONSTRING.Remove(CONNECTIONSTRING.Length - 66);
                        // CONNECTIONSTRINGDATA = xElement.FirstChild.Attributes["connectionString"].Value;
                    }
                }
                //  MessageBox.Show(CONNECTIONSTRING);
            }
            catch
            {
                MessageBox.Show("Không tìm thấy file appData.config .");
            }
            return CONNECTIONSTRING;
        }
        #endregion

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch
            {

            }
            return pingable;
        }
        public static bool IsValidIP(string address)
        {
            if (!Regex.IsMatch(address, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
                return false;
            IPAddress dummy;
            return IPAddress.TryParse(address, out dummy);
        }
        public static string GetMacAddress()
        {
            try
            {
                string macAddresses = "";

                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    macAddresses = nic.GetPhysicalAddress().ToString();
                    break;
                }
                return macAddresses;
            }
            catch
            {
                return "sv3772";
            }
        }
    }
}
