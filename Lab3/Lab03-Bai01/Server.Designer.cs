namespace Lab03_Bai01
{
    partial class Server
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
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.listbox_message = new System.Windows.Forms.ListBox();
            this.button1_Listen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(104, 37);
            this.textBox_Port.Multiline = true;
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(221, 45);
            this.textBox_Port.TabIndex = 0;
            // 
            // listbox_message
            // 
            this.listbox_message.FormattingEnabled = true;
            this.listbox_message.ItemHeight = 25;
            this.listbox_message.Location = new System.Drawing.Point(36, 169);
            this.listbox_message.Name = "listbox_message";
            this.listbox_message.Size = new System.Drawing.Size(729, 179);
            this.listbox_message.TabIndex = 1;
            // 
            // button1_Listen
            // 
            this.button1_Listen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1_Listen.Location = new System.Drawing.Point(607, 37);
            this.button1_Listen.Name = "button1_Listen";
            this.button1_Listen.Size = new System.Drawing.Size(158, 45);
            this.button1_Listen.TabIndex = 2;
            this.button1_Listen.Text = "Listen";
            this.button1_Listen.UseVisualStyleBackColor = true;
            this.button1_Listen.Click += new System.EventHandler(this.button1_Listen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 33);
            this.label2.TabIndex = 4;
            this.label2.Text = "Recieve message";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 404);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1_Listen);
            this.Controls.Add(this.listbox_message);
            this.Controls.Add(this.textBox_Port);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.ListBox listbox_message;
        private System.Windows.Forms.Button button1_Listen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}