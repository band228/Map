using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.BLL;
using Common.DataConfig;
using ControllerBox.Properties;
using ControllerBox.DataConfig;
using System.Threading;

namespace ControllerBox.GUI
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
        }
        string UserID;
        bllUserRole UR = new bllUserRole();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {

                lblAlert.Text = "Bạn chưa nhập tên tài khoản";
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblAlert.Text = "Bạn chưa nhập mật khẩu";
                txtPassword.Focus();
                return;
            }
            if (chkRemember.Checked ==true)
            {
                Settings.Default.Remember = "true";
                Settings.Default.UserName = txtUserName.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Settings.Default.Remember = "false";
                Settings.Default.UserName = txtUserName.Text;
                Properties.Settings.Default.Save();
            }
            if(UR.Login(txtUserName.Text, txtPassword.Text,ref UserID))
            {
                bool bStatus = false;
                string host = new Provider().load_HostServer(ref bStatus);
                Settings.Default.HostServer =  host ;
                if (bStatus)
                    Properties.Settings.Default.Save();
                else
                {
                    MessageBox.Show(host);
                }
                Settings.Default.UserID = UserID;               
                Settings.Default.Save();
                Main frm = new Main();
                frm.Show();
                this.Hide();
            }
            else
            {
                lblAlert.Text = "Sai thông tin đăng nhập";
                txtUserName.Focus();
                return;
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            lblAlert.Text = "";
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            lblAlert.Text = "";
        }

        public void CheckDB()
        {
            string serverSql = ""; string strConnnect = "";
           if( new Provider().TestConnect(ref serverSql,ref strConnnect))
           {
               Settings.Default.strConnecting = strConnnect;
               Settings.Default.Save();
               this.Invoke(new MethodInvoker(delegate()
                    {
                        txtServer.Text = "Database Using: " + serverSql;
                    }));
           }
           else
           {
               Settings.Default.strConnecting = "";
               Settings.Default.Save();
               try
               {
                   this.Invoke(new MethodInvoker(delegate()
                        {
                            txtServer.Text = "No connection Database!";
                        }));
               }
               catch { }
           }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            Thread objThread = new Thread( CheckDB);
            objThread.Start();
            if (Settings.Default.Remember == "true")
            {
                txtUserName.Text = Settings.Default.UserName;
                txtPassword.Focus();
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtUserName.Text))
            {
                btnLogin_Click(sender, e);
            }
        }


        private void linkChangeDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmConfigDatabase frm = new frmConfigDatabase();
            frm.Show();
        }

        
    }
}
