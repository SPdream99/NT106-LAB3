namespace Lab03_Bai01
{
    partial class MainForm
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
            this.Button_OpenServer = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Button_Client = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button_OpenServer
            // 
            this.Button_OpenServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_OpenServer.Location = new System.Drawing.Point(89, 78);
            this.Button_OpenServer.Name = "Button_OpenServer";
            this.Button_OpenServer.Size = new System.Drawing.Size(211, 112);
            this.Button_OpenServer.TabIndex = 0;
            this.Button_OpenServer.Text = "Server";
            this.Button_OpenServer.UseVisualStyleBackColor = true;
            this.Button_OpenServer.Click += new System.EventHandler(this.Button_OpenServer_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(47, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(8, 8);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Button_Client
            // 
            this.Button_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Client.Location = new System.Drawing.Point(494, 78);
            this.Button_Client.Name = "Button_Client";
            this.Button_Client.Size = new System.Drawing.Size(211, 112);
            this.Button_Client.TabIndex = 2;
            this.Button_Client.Text = "Client";
            this.Button_Client.UseVisualStyleBackColor = true;
            this.Button_Client.Click += new System.EventHandler(this.Button_Client_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 274);
            this.Controls.Add(this.Button_Client);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Button_OpenServer);
            this.Name = "Form1";
            this.Text = "Bài 1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_OpenServer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Button_Client;
    }
}

