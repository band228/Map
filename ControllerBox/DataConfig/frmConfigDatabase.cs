using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

using Common.DataLayer;//class dataservice

namespace ControllerBox.DataConfig
{
    public partial class frmConfigDatabase : Form
    {
        public frmConfigDatabase()
        {
            InitializeComponent();
        }
        #region Khai Bao
        SqlConnection con;
        SqlCommand com;
        SqlDataReader rd;
        private string CKEY = "iLock_Kobe";
        private string chuoimahoa;
        public string chuoi = "";
        bool exitApplication = false;
        public bool loginConfig = false;
        #endregion
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboSever.Items.Clear();
            ArrayList servers = GetSQLServers();
            for (int i = 0; i < servers.Count; i++)
            {
                cboSever.Items.Add(servers[i]);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public ArrayList GetSQLServers()
        {
            ArrayList col = new ArrayList();
            SqlDataSourceEnumerator listServer = SqlDataSourceEnumerator.Instance;
            DataTable dTable = listServer.GetDataSources();
            foreach (DataRow row in dTable.Rows)
            {
                col.Add(row["ServerName"]);
            }
            return col;
        }
        public void load_CboType()
        {
            cboType.Items.Add("Windows Authentication");
            cboType.Items.Add("SQL Server Authentication");
            cboType.SelectedIndex = 1;
        }
        public static string Mahoa(string toEncrypt, string key, bool useHashing)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex == 0)
            {
                chuoi = "Data source=" + cboSever.Text + ";Initial Catalog=" + cboDatabase.Text + ";Integrated Security=true;Pooling=true;Min Pool Size=5;Max Pool Size=100;Connect Timeout=60;";
                chuoimahoa = Mahoa(chuoi, CKEY, true).Insert(2, "pql");
                EditXml(chuoimahoa);
                DataService getConntr = new DataService();
                getConntr.docchuoiketnoi();
                exitApplication = true;
                MessageBox.Show("Cập nhật thành công! Hãy tắt chương trình và khởi động lại.");
                this.Close();
            }
            if (cboType.SelectedIndex == 1)
            {
                chuoi = "Data Source=" + cboSever.Text + ";Initial Catalog=" + cboDatabase.Text + ";User id=" + txtUser.Text + ";password=" + txtPass.Text + ";Pooling=true;Min Pool Size=5;Max Pool Size=100;Connect Timeout=60;";
                chuoimahoa = Mahoa(chuoi, CKEY, true).Insert(2, "pql");
                EditXml(chuoimahoa);
                DataService getConntr = new DataService();
                getConntr.docchuoiketnoi();
                exitApplication = true;
                MessageBox.Show("Cập nhật thành công! Hãy tắt chương trình và khởi động lại.");
                this.Close();
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex == 0)
            {
                txtUser.Enabled = false;
                txtPass.Enabled = false;
            }
            else
            {
                txtUser.Enabled = true;
                txtPass.Enabled = true;
                btnTess.Enabled = false;
            }
        }

        private void btnTess_Click(object sender, EventArgs e)
        {
            //Quyen Windowns
            if (cboType.SelectedIndex == 0)
            {
                cboDatabase.Items.Clear();

                con = new SqlConnection("Data source=" + cboSever.Text + ";Initial Catalog=master;Integrated Security=true;Pooling=true;Min Pool Size=5;Max Pool Size=100;Connect Timeout=60;");
                com = new SqlCommand("SP_DATABASES", con);
                try
                {
                    con.Open();
                    rd = com.ExecuteReader();
                    cboDatabase.Items.Clear();
                    while (rd.Read())
                    {
                        cboDatabase.Items.Add(rd[0].ToString());
                    }
                    cboDatabase.Enabled = true;
                    MessageBox.Show("Kết nối thành công!");

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Kết nối không thành công. " + ex.Message);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                    com.Dispose();
                }

            }
            //Quyen SQL Server
            if (cboType.SelectedIndex == 1)
            {
                if ((txtUser.Text != "") && (txtPass.Text != ""))
                {
                    cboDatabase.Items.Clear();

                    con = new SqlConnection("Data Source=" + cboSever.Text + ";Initial Catalog=master;User id=" + txtUser.Text + ";password=" + txtPass.Text + ";Pooling=true;Min Pool Size=5;Max Pool Size=100;Connect Timeout=60;");
                    com = new SqlCommand("SP_DATABASES", con);
                    try
                    {
                        con.Open();
                        rd = com.ExecuteReader();
                        while (rd.Read())
                        {
                            cboDatabase.Items.Add(rd[0].ToString());
                        }

                        MessageBox.Show("Kết Nối Thành Công!!!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Thất bại!!! " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                        com.Dispose();

                    }
                }
                else
                    //if((txtuse.Text=="")||(txtpass.Text==""))
                    MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!");

            }
        }

        #region EditXml
        void EditXml(string con)
        {
            //updating config file
            XmlDocument XmlDoc = new XmlDocument();
            //Loading the Config file
            XmlDoc.Load("AppData.config");
            foreach (XmlElement xElement in XmlDoc.DocumentElement)
            {
                if (xElement.Name == "connectionStrings")
                {
                    xElement.FirstChild.Attributes["connectionString"].Value = con;

                }
            }
            XmlDoc.Save("AppData.config");
        }
        #endregion

        private void cboSever_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboType.Enabled = true;
            btnTess.Enabled = true;
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            if (txtUser.TextLength > 0)
            {
                btnTess.Enabled = true;
            }
            else
            {
                btnTess.Enabled = false;
            }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtUser.TextLength > 0 && txtPass.TextLength > 0)
            {
                btnTess.Enabled = true;
            }
            else
            {
                btnTess.Enabled = false;
            }
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDatabase.SelectedIndex > -1)
            {
                btnSave.Enabled = true;
            }
        }

        private void frmConfigDatabase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (exitApplication)
            {
                Application.Exit();
            }
            else
            {
                if (loginConfig)
                {
                    Application.Exit();
                }

            }
        }

        private void frmConfigDatabase_Load(object sender, EventArgs e)
        {
            load_CboType();
        }
    }
}
