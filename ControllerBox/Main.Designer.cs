namespace ControllerBox
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.msMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.mConfigDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mQuanlychinhanhATM = new System.Windows.Forms.ToolStripMenuItem();
            this.mQuanlytaikhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.mQuanlynhanvien = new System.Windows.Forms.ToolStripMenuItem();
            this.mDoimatkhau = new System.Windows.Forms.ToolStripMenuItem();
            this.mQuanlymail = new System.Windows.Forms.ToolStripMenuItem();
            this.mThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.createKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msMenuStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMenuStrip
            // 
            this.msMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSystem,
            this.createKeyToolStripMenuItem});
            this.msMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.msMenuStrip.Name = "msMenuStrip";
            this.msMenuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.msMenuStrip.Size = new System.Drawing.Size(599, 28);
            this.msMenuStrip.TabIndex = 0;
            this.msMenuStrip.Text = "msMenuStrip";
            // 
            // mSystem
            // 
            this.mSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mConfigDatabase,
            this.mQuanlychinhanhATM,
            this.mQuanlytaikhoan,
            this.mQuanlynhanvien,
            this.mDoimatkhau,
            this.mQuanlymail,
            this.mThoat});
            this.mSystem.Name = "mSystem";
            this.mSystem.Size = new System.Drawing.Size(68, 24);
            this.mSystem.Text = "System";
            // 
            // mConfigDatabase
            // 
            this.mConfigDatabase.Name = "mConfigDatabase";
            this.mConfigDatabase.Size = new System.Drawing.Size(270, 26);
            this.mConfigDatabase.Text = "Config Database SQL Server";
            this.mConfigDatabase.Click += new System.EventHandler(this.configDatabaseToolStripMenuItem_Click);
            // 
            // mQuanlychinhanhATM
         
            // 
            this.createKeyToolStripMenuItem.Name = "createKeyToolStripMenuItem";
            this.createKeyToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
            this.createKeyToolStripMenuItem.Text = "create key";
            this.createKeyToolStripMenuItem.Click += new System.EventHandler(this.createKeyToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Data";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 30);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 359);
            this.Controls.Add(this.msMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SeaMatrix Technology";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.msMenuStrip.ResumeLayout(false);
            this.msMenuStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mSystem;
        private System.Windows.Forms.ToolStripMenuItem mConfigDatabase;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mQuanlytaikhoan;
        private System.Windows.Forms.ToolStripMenuItem mDoimatkhau;
        private System.Windows.Forms.ToolStripMenuItem mThoat;
        private System.Windows.Forms.ToolStripMenuItem mQuanlymail;
        private System.Windows.Forms.ToolStripMenuItem mQuanlychinhanhATM;
        private System.Windows.Forms.ToolStripMenuItem mQuanlynhanvien;
        private System.Windows.Forms.ToolStripMenuItem createKeyToolStripMenuItem;
    }
}