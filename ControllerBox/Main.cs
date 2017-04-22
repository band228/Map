using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerBox.DataConfig;
using Microsoft.Win32;
using System.Xml;
using ControllerBox.GUI;
using Common.BLL;
using Common.DataConfig;
using ControllerBox.Properties;


namespace ControllerBox
{
    public partial class Main : Form
    {
      
         public Main()
        {

            InitializeComponent();
            
        }

        
         private void configDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
         {
             frmConfigDatabase frm = new frmConfigDatabase();
             frm.ShowDialog();
         }

        
         private void Main_Load(object sender, EventArgs e)
         {
             //mSystem.Visible = false;             
             mConfigDatabase.Visible = true;
             mQuanlychinhanhATM.Visible = false;
             mQuanlytaikhoan.Visible = false;
             mQuanlymail.Visible = false;
             //mDoimatkhau.Visible = false;
             mQuanlynhanvien.Visible = false;
             //mMenu.Visible = false;
             //foreach (MenuStrip.ControlCollection ctr in this.msMenuStrip.ContextMenu)
             //{
             //    MessageBox.Show(ctr.Name);
             //}

             DataTable dtRoleMenu = new DataTable();
             dtRoleMenu = new bllUserRole().RoleMenuByUserID(Settings.Default.UserID);
             foreach (ToolStripMenuItem dropDownItem in msMenuStrip.Items)
             {
                 
                     foreach (ToolStripMenuItem subItem in dropDownItem.DropDownItems)
                     {
                         foreach(DataRow dtr in dtRoleMenu.Rows)
                         {
                             if(subItem.Text == dtr["MenuButtonName"].ToString())
                             {
                                 subItem.Visible = true;
                                 break;
                             } 
                         }
                         
                     }

                 
                 
             }
         }

         private void Main_FormClosed(object sender, FormClosedEventArgs e)
         {
             Application.Exit();
         }

    

         private void createKeyToolStripMenuItem_Click(object sender, EventArgs e)
         {

         }

        
    }
}
