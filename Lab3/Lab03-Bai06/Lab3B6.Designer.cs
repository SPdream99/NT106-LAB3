namespace Lab03_Bai06
{
    partial class Lab3B6
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
            this.Server = new System.Windows.Forms.Button();
            this.Client = new System.Windows.Forms.Button();
            this.Setting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Server
            // 
            this.Server.Location = new System.Drawing.Point(204, 65);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(125, 47);
            this.Server.TabIndex = 0;
            this.Server.Text = "Server";
            this.Server.UseVisualStyleBackColor = true;
            this.Server.Click += new System.EventHandler(this.Server_Click);
            // 
            // Client
            // 
            this.Client.Location = new System.Drawing.Point(45, 65);
            this.Client.Name = "Client";
            this.Client.Size = new System.Drawing.Size(125, 47);
            this.Client.TabIndex = 1;
            this.Client.Text = "Client";
            this.Client.UseVisualStyleBackColor = true;
            this.Client.Click += new System.EventHandler(this.Client_Click);
            // 
            // Setting
            // 
            this.Setting.Location = new System.Drawing.Point(365, 65);
            this.Setting.Name = "Setting";
            this.Setting.Size = new System.Drawing.Size(125, 47);
            this.Setting.TabIndex = 2;
            this.Setting.Text = "Setting";
            this.Setting.UseVisualStyleBackColor = true;
            this.Setting.Click += new System.EventHandler(this.Setting_Click);
            // 
            // Lab3B6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 175);
            this.Controls.Add(this.Setting);
            this.Controls.Add(this.Client);
            this.Controls.Add(this.Server);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Lab3B6";
            this.Text = "Lab3 Bài 6";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Server;
        private System.Windows.Forms.Button Client;
        private System.Windows.Forms.Button Setting;
    }
}