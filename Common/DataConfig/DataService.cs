using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace Common.DataLayer
{
    public class DataService
    {
        #region Khai Bao
        protected SqlConnection con;
        protected string ddan = "";
        protected string CKEY = "iLock_Kobe";
        protected DataTable dt;
        public static string chuoiketnoi = "";
        protected SqlDataAdapter ad;
        protected SqlCommand com;

        #endregion

        #region Doc Chuoi KetNoi
        public void docchuoiketnoi()
        {
            try
            {
                //updating config file
                XmlDocument XmlDoc = new XmlDocument();
                //Loading the Config file
                XmlDoc.Load(Application.StartupPath + "\\AppData.config");
                foreach (XmlElement xElement in XmlDoc.DocumentElement)
                {
                    if (xElement.Name == "connectionStrings")
                    {
                        chuoiketnoi = xElement.FirstChild.Attributes["connectionString"].Value.Remove(2, 3);
                        chuoiketnoi = Giaima(chuoiketnoi, CKEY, true);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Không tìm thấy file AppData.config");
            }

        }
        #endregion

        #region Giai Ma Pass Connect
        public static string Giaima(string toDecrypt, string key, bool useHashing)
        {
            byte[] keyArray;
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

        #region Open Ket Noi
        public void moketnoi()
        {
            docchuoiketnoi();
            con = new SqlConnection(chuoiketnoi);
            try
            {
                con.Open();

            }
            catch
            {
                MessageBox.Show("Lỗi kết nối CSDL.Đang Kiểm Tra Lỗi Xin Vui Lòng Chờ!!!");
            }


        }
        #endregion

        #region Thuoc tinh connec
        public SqlConnection Con
        {
            get { return con; }
        }
        #endregion

        #region Dong ket noi CSDL
        public void dongketnoi()
        {
            if (con.State == ConnectionState.Open)
                con.Dispose();
        }
        #endregion

        #region kiem tra trang thai ket noi
        public bool kiemtraketnoi()
        {
            if (con.State == ConnectionState.Open)
                return true;
            else
                return false;
        }
        #endregion

        #region Phuong thuc dien DL vao DataTable
        public DataTable filldatatable(string s)
        {

            try
            {
                moketnoi();
                dt = new DataTable();
                ad = new SqlDataAdapter(s, con);
                dt.Clear();
                ad.Fill(dt);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Có lỗi trong phương thức fillDataSet " + ex.Message);
            }
            return dt;

        }
        #endregion

        #region Phuong thuc update truc tiep du lieu tu DatatGridView vao CSDL
        public void SaveCSDL()
        {
            SqlCommandBuilder ComBuilder = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }
        #endregion

        #region Them dong moi
        public DataTable Dt
        {
            get { return dt; }
        }
        #endregion

        #region ExecuteNonQuery
        public void thuchiencaulenhSQL(string lenh)
        {
            moketnoi();
            com = new SqlCommand(lenh, con);
            com.ExecuteNonQuery();
            dongketnoi();
            com.Dispose();
        }
        #endregion
    }
}
