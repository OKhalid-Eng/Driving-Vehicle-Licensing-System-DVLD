namespace DVLD
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.msMainFrom = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDrivingLinsencesServices = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLocalLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmManageAppType = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLocalDrivingLincenseApp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmManageTestType = new System.Windows.Forms.ToolStripMenuItem();
            this.msPeople = new System.Windows.Forms.ToolStripMenuItem();
            this.driversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCurrentUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.currentUserInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePsswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msMainFrom.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMainFrom
            // 
            this.msMainFrom.BackColor = System.Drawing.Color.Indigo;
            this.msMainFrom.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMainFrom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem,
            this.msPeople,
            this.driversToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.accountSettingsToolStripMenuItem});
            this.msMainFrom.Location = new System.Drawing.Point(0, 0);
            this.msMainFrom.Name = "msMainFrom";
            this.msMainFrom.Size = new System.Drawing.Size(1228, 49);
            this.msMainFrom.TabIndex = 3;
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDrivingLinsencesServices,
            this.tsmManageAppType,
            this.tsmManageTestType});
            this.applicationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applicationToolStripMenuItem.ForeColor = System.Drawing.Color.Lavender;
            this.applicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("applicationToolStripMenuItem.Image")));
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(229, 45);
            this.applicationToolStripMenuItem.Text = "Applications";
            // 
            // tsmDrivingLinsencesServices
            // 
            this.tsmDrivingLinsencesServices.BackColor = System.Drawing.Color.Indigo;
            this.tsmDrivingLinsencesServices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.tsmDrivingLinsencesServices.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmDrivingLinsencesServices.ForeColor = System.Drawing.Color.LavenderBlush;
            this.tsmDrivingLinsencesServices.Image = global::DVLD.Properties.Resources.export_license__1_;
            this.tsmDrivingLinsencesServices.Name = "tsmDrivingLinsencesServices";
            this.tsmDrivingLinsencesServices.Size = new System.Drawing.Size(346, 32);
            this.tsmDrivingLinsencesServices.Text = "Driving Licenses Services";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.Indigo;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLocalLicense});
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.LavenderBlush;
            this.toolStripMenuItem1.Image = global::DVLD.Properties.Resources.licensing;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(292, 32);
            this.toolStripMenuItem1.Text = "New Driving License";
            // 
            // tsmLocalLicense
            // 
            this.tsmLocalLicense.BackColor = System.Drawing.Color.Indigo;
            this.tsmLocalLicense.ForeColor = System.Drawing.Color.LavenderBlush;
            this.tsmLocalLicense.Image = global::DVLD.Properties.Resources.driver_license__1_;
            this.tsmLocalLicense.Name = "tsmLocalLicense";
            this.tsmLocalLicense.Size = new System.Drawing.Size(222, 32);
            this.tsmLocalLicense.Text = "Local License";
            this.tsmLocalLicense.Click += new System.EventHandler(this.tsmLocalLicense_Click);
            // 
            // tsmManageAppType
            // 
            this.tsmManageAppType.BackColor = System.Drawing.Color.Indigo;
            this.tsmManageAppType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLocalDrivingLincenseApp});
            this.tsmManageAppType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmManageAppType.ForeColor = System.Drawing.Color.LavenderBlush;
            this.tsmManageAppType.Image = global::DVLD.Properties.Resources.text__1_;
            this.tsmManageAppType.Name = "tsmManageAppType";
            this.tsmManageAppType.Size = new System.Drawing.Size(346, 32);
            this.tsmManageAppType.Text = "Manage Application Type";
            this.tsmManageAppType.Click += new System.EventHandler(this.tsmManageAppType_Click);
            // 
            // tsmLocalDrivingLincenseApp
            // 
            this.tsmLocalDrivingLincenseApp.BackColor = System.Drawing.Color.Indigo;
            this.tsmLocalDrivingLincenseApp.ForeColor = System.Drawing.Color.LavenderBlush;
            this.tsmLocalDrivingLincenseApp.Image = global::DVLD.Properties.Resources.application__3_;
            this.tsmLocalDrivingLincenseApp.Name = "tsmLocalDrivingLincenseApp";
            this.tsmLocalDrivingLincenseApp.Size = new System.Drawing.Size(413, 32);
            this.tsmLocalDrivingLincenseApp.Text = "Local Driving License Application";
            this.tsmLocalDrivingLincenseApp.Click += new System.EventHandler(this.tsmLocalDrivingLincenseApp_Click);
            // 
            // tsmManageTestType
            // 
            this.tsmManageTestType.BackColor = System.Drawing.Color.Indigo;
            this.tsmManageTestType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmManageTestType.ForeColor = System.Drawing.Color.LavenderBlush;
            this.tsmManageTestType.Image = global::DVLD.Properties.Resources.type_1;
            this.tsmManageTestType.Name = "tsmManageTestType";
            this.tsmManageTestType.Size = new System.Drawing.Size(346, 32);
            this.tsmManageTestType.Text = "Manage Test Types";
            this.tsmManageTestType.Click += new System.EventHandler(this.tsmManageTestType_Click);
            // 
            // msPeople
            // 
            this.msPeople.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msPeople.ForeColor = System.Drawing.Color.Lavender;
            this.msPeople.Image = ((System.Drawing.Image)(resources.GetObject("msPeople.Image")));
            this.msPeople.Name = "msPeople";
            this.msPeople.Size = new System.Drawing.Size(147, 45);
            this.msPeople.Text = "People";
            this.msPeople.Click += new System.EventHandler(this.msPeople_Click);
            // 
            // driversToolStripMenuItem
            // 
            this.driversToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.driversToolStripMenuItem.ForeColor = System.Drawing.Color.Lavender;
            this.driversToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("driversToolStripMenuItem.Image")));
            this.driversToolStripMenuItem.Name = "driversToolStripMenuItem";
            this.driversToolStripMenuItem.Size = new System.Drawing.Size(152, 45);
            this.driversToolStripMenuItem.Text = "Drivers";
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usersToolStripMenuItem.ForeColor = System.Drawing.Color.Lavender;
            this.usersToolStripMenuItem.Image = global::DVLD.Properties.Resources.user;
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(128, 45);
            this.usersToolStripMenuItem.Text = "Users";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // accountSettingsToolStripMenuItem
            // 
            this.accountSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCurrentUserInfo,
            this.tsmChangePassword,
            this.toolStripMenuItem2,
            this.tsmSignOut});
            this.accountSettingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountSettingsToolStripMenuItem.ForeColor = System.Drawing.Color.Lavender;
            this.accountSettingsToolStripMenuItem.Image = global::DVLD.Properties.Resources.setting;
            this.accountSettingsToolStripMenuItem.Name = "accountSettingsToolStripMenuItem";
            this.accountSettingsToolStripMenuItem.Size = new System.Drawing.Size(291, 45);
            this.accountSettingsToolStripMenuItem.Text = "Account Settings";
            // 
            // tsmCurrentUserInfo
            // 
            this.tsmCurrentUserInfo.BackColor = System.Drawing.Color.Indigo;
            this.tsmCurrentUserInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmCurrentUserInfo.ForeColor = System.Drawing.Color.LavenderBlush;
            this.tsmCurrentUserInfo.Image = global::DVLD.Properties.Resources.user__8_;
            this.tsmCurrentUserInfo.Name = "tsmCurrentUserInfo";
            this.tsmCurrentUserInfo.Size = new System.Drawing.Size(292, 36);
            this.tsmCurrentUserInfo.Text = "Current User Info";
            this.tsmCurrentUserInfo.Click += new System.EventHandler(this.tsmCurrentUserInfo_Click);
            // 
            // tsmChangePassword
            // 
            this.tsmChangePassword.BackColor = System.Drawing.Color.Indigo;
            this.tsmChangePassword.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmChangePassword.ForeColor = System.Drawing.Color.LavenderBlush;
            this.tsmChangePassword.Image = global::DVLD.Properties.Resources.password__3_;
            this.tsmChangePassword.Name = "tsmChangePassword";
            this.tsmChangePassword.Size = new System.Drawing.Size(292, 36);
            this.tsmChangePassword.Text = "Change Password";
            this.tsmChangePassword.Click += new System.EventHandler(this.tsmChangePassword_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.Indigo;
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.Indigo;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(289, 6);
            this.toolStripMenuItem2.Visible = false;
            // 
            // tsmSignOut
            // 
            this.tsmSignOut.BackColor = System.Drawing.Color.Indigo;
            this.tsmSignOut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmSignOut.ForeColor = System.Drawing.Color.LavenderBlush;
            this.tsmSignOut.Image = global::DVLD.Properties.Resources.logout__2_;
            this.tsmSignOut.Name = "tsmSignOut";
            this.tsmSignOut.Size = new System.Drawing.Size(292, 36);
            this.tsmSignOut.Text = "Sign Out";
            this.tsmSignOut.Click += new System.EventHandler(this.tsmSignOut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Headline R", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(41, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "DVLD SYSTEM";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(63, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(354, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "Driver & Vehicle Licensing System - Welcome";
            // 
            // currentUserInfoToolStripMenuItem
            // 
            this.currentUserInfoToolStripMenuItem.Name = "currentUserInfoToolStripMenuItem";
            this.currentUserInfoToolStripMenuItem.Size = new System.Drawing.Size(354, 46);
            this.currentUserInfoToolStripMenuItem.Text = "Current User Info";
            // 
            // changePsswordToolStripMenuItem
            // 
            this.changePsswordToolStripMenuItem.Name = "changePsswordToolStripMenuItem";
            this.changePsswordToolStripMenuItem.Size = new System.Drawing.Size(354, 46);
            this.changePsswordToolStripMenuItem.Text = "Change Pssword";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImage = global::DVLD.Properties.Resources.Screenshot_2026_07_23_122503;
            this.ClientSize = new System.Drawing.Size(1228, 591);
            this.Controls.Add(this.msMainFrom);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DVLD-Driving and Vehicle Licensing Department";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.msMainFrom.ResumeLayout(false);
            this.msMainFrom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMainFrom;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem msPeople;
        private System.Windows.Forms.ToolStripMenuItem driversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmManageAppType;
        private System.Windows.Forms.ToolStripMenuItem accountSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmCurrentUserInfo;
        private System.Windows.Forms.ToolStripMenuItem currentUserInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePsswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmChangePassword;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmSignOut;
        private System.Windows.Forms.ToolStripMenuItem tsmManageTestType;
        private System.Windows.Forms.ToolStripMenuItem tsmDrivingLinsencesServices;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmLocalLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmLocalDrivingLincenseApp;
    }
}

