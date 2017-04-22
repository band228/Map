using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Security.Cryptography;
using System.Xml;
using System.Net;
using System.Web;
using ControllerBox.Properties;

namespace Common.DataConfig
{
    public class Provider
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        //DataSet ds;
        //SqlParameter Sqlpar;
        //SqlCommandBuilder Sqlbui; 
        //public string ConnectionString = "Data Source=113.160.93.142;Initial Catalog=PMS_db;User ID=sa;Password=saoviet@123";
        public bool TestConnect(ref string address, ref string strConnect)
        {
            strConnect = clsConfig.ReadConnectionString();
            try
            {
                //strConnect = clsConfig.ReadConnectionString();
                con = new SqlConnection(strConnect);
                con.Open();
                con.Close();
                address = strConnect.Split(';')[0].Split('=')[1] + " - " + strConnect.Split(';')[1].Split('=')[1];
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Not connection to SQL Server ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool Connect()
        {
            try
            {
                
                string strConnect =  Settings.Default.strConnecting;
                if (string.IsNullOrEmpty(strConnect))
                {
                    MessageBox.Show("Not connection to SQL Server ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //strConnect = clsConfig.ReadConnectionString();
                con = new SqlConnection(strConnect);
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not connection to SQL Server" ,"Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public void Disconnect()
        {
            if ((con != null) && (con.State == ConnectionState.Open))
                con.Close();
        }
        public string LoadInforConfigProxy()
        {
            string proxyAdress = "";
            string proxyPort = "";
            string userName = "";
            string password = "";
            string isProxy = "";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + @"\Config.xml");
            var itemNodes1 = xmlDoc.SelectNodes("/Config");
            foreach (XmlNode node in itemNodes1)
            {
                proxyAdress = node.SelectSingleNode("proxyAdress").InnerText;
                proxyPort = node.SelectSingleNode("proxyPort").InnerText;
                userName = node.SelectSingleNode("userName").InnerText;
                password = node.SelectSingleNode("passWord").InnerText;
                isProxy = node.SelectSingleNode("isProxy").InnerText;
            }
            string connectionstr = proxyAdress + ";" + proxyPort + ";" + userName + ";" + password + ";" + isProxy;
            return connectionstr;
        }
        public string load_HostServer(ref bool bStatus)
        {
            try
            {
                string hostServer = "";
                List<SqlParameter> sqlpa = new List<SqlParameter>();
                DataTable dt = new DataTable();
                dt = ExecuteProc("InforDate_GetInfor", sqlpa);
                if (dt.Rows.Count > 0)
                {
                    hostServer = dt.Rows[0]["AppIP"].ToString();
                }
                bStatus = true;
                return hostServer;
            }
            catch (Exception ex) { bStatus = false; return ex.ToString(); }
        }
        public void LoadInforServer(ref string ipServer, ref int portServer)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.StartupPath + @"\Config.xml");
            var itemNodes1 = xmlDoc.SelectNodes("/Config");
            foreach (XmlNode node in itemNodes1)
            {
                ipServer = node.SelectSingleNode("ipServer").InnerText;
                portServer = int.Parse(node.SelectSingleNode("portServer").InnerText);
            }

        }
        public DataTable ExecuteProc(String spName, List<SqlParameter> sqlParams)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!Connect())
                {
                    return dt;
                }
                SqlCommand command = con.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                if (sqlParams != null)
                {
                    foreach (SqlParameter param in sqlParams)
                    {
                        command.Parameters.Add(param);
                    }
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                    Disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Disconnect();
            }
            return dt;
        }
        public bool UpdateDataAdapter(DataTable table)
        {
            try
            {
                da.Update(table);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool RunProc(String spName, List<SqlParameter> sqlParams)
        {
            try
            {
                if (!Connect())
                {
                    return false;
                }
                SqlCommand command = con.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                if (sqlParams != null)
                {
                    foreach (SqlParameter param in sqlParams)
                    {
                        command.Parameters.Add(param);
                    }
                }
                command.ExecuteNonQuery();
                Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }
        public bool CheckExists(String spName, List<SqlParameter> sqlParams)
        {
            bool kt = false;
            DataTable dt = new DataTable();
            try
            {
                if (!Connect())
                {
                    return false;
                }
                SqlCommand command = con.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = spName;
                if (sqlParams != null)
                {
                    foreach (SqlParameter param in sqlParams)
                    {
                        command.Parameters.Add(param);
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                Disconnect();
                if (dt.Rows.Count > 0)
                    kt = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Disconnect();
            }

            return kt;
        }
        public DataSet GetDataSet(CommandType cmdType, string strName, string[] arrName, Object[] arrValue, DbType[] arrType)
        {
            DataSet DtSet = new DataSet();
            try
            {
                if (!Connect())
                {
                    return DtSet;
                }
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = cmdType;
                cmd.CommandText = strName;
                if (arrName != null)
                {
                    for (int i = 0; i < arrName.Length; i++)
                    {
                        SqlParameter sqlpar = new SqlParameter();
                        sqlpar.ParameterName = arrName[i];
                        sqlpar.Value = arrValue[i];
                        sqlpar.DbType = arrType[i];
                        cmd.Parameters.Add(sqlpar);
                    }
                }
                da = new SqlDataAdapter(cmd);
                da.Fill(DtSet);
            }
            catch
            { }
            return DtSet;
        }
        public bool ExecuteNonQuery(string sql)
        {
            try
            {
                if (!Connect())
                {
                    return false;
                }
                if (con.State == ConnectionState.Closed)
                    if (!this.Connect())
                        return false;
                cmd = new SqlCommand(sql, con);
                if (cmd.ExecuteNonQuery() <= 0)
                    return false;
                Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }




        private string CKEY = "iLock_Kobe";
        public static string Mask(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

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

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public string Encode(string input)
        {
            string values = "";
            input = Mask(input, CKEY, true).Replace("2", "a").Replace("u", "3").Replace("m", "n");
            string[] temp;
            temp = values.Split('3');
            foreach (string tmp in temp)
            {
                values += Mask(tmp, CKEY, true);
            }
            return values;
        }
        public string HttpPost(string url, string[] paramName, string[] paramVal)
        {
            try
            {
                HttpWebRequest req = WebRequest.Create(new Uri(url))
                                     as HttpWebRequest;
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                // Build a string with all the params, properly encoded.
                // We assume that the arrays paramName and paramVal are
                // of equal length:
                StringBuilder paramz = new StringBuilder();
                for (int i = 0; i < paramName.Length; i++)
                {
                    paramz.Append(paramName[i]);
                    paramz.Append("=");
                    paramz.Append(HttpUtility.UrlEncode(paramVal[i]));
                    paramz.Append("&");
                }

                // Encode the parameters as form data:
                byte[] formData =
                    UTF8Encoding.UTF8.GetBytes(paramz.ToString());
                req.ContentLength = formData.Length;

                // Send the request:
                using (Stream post = req.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }

                // Pick up the response:
                string result = null;
                using (HttpWebResponse resp = req.GetResponse()
                                              as HttpWebResponse)
                {
                    StreamReader reader =
                        new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }

                return result;
            }
            catch
            {
                return "Server is not running";
            }
        }

        public static string HttpGet(string url)
        {
            HttpWebRequest req = WebRequest.Create(url)
                                 as HttpWebRequest;
            string result = null;
            using (HttpWebResponse resp = req.GetResponse()
                                          as HttpWebResponse)
            {
                StreamReader reader =
                    new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
