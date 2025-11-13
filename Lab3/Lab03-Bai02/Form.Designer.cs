namespace Lab03_Bai02
{
    partial class Form
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
            this.button_Listen = new System.Windows.Forms.Button();
            this.listView_Message = new System.Windows.Forms.ListView();
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // button_Listen
            // 
            this.button_Listen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Listen.Location = new System.Drawing.Point(612, 22);
            this.button_Listen.Name = "button_Listen";
            this.button_Listen.Size = new System.Drawing.Size(150, 55);
            this.button_Listen.TabIndex = 0;
            this.button_Listen.Text = "Listen";
            this.button_Listen.UseVisualStyleBackColor = true;
            this.button_Listen.Click += new System.EventHandler(this.button_Listen_Click);
            // 
            // listView_Message
            // 
            this.listView_Message.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Message});
            this.listView_Message.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_Message.HideSelection = false;
            this.listView_Message.Location = new System.Drawing.Point(55, 95);
            this.listView_Message.Name = "listView_Message";
            this.listView_Message.Size = new System.Drawing.Size(707, 315);
            this.listView_Message.TabIndex = 2;
            this.listView_Message.UseCompatibleStateImageBehavior = false;
            this.listView_Message.View = System.Windows.Forms.View.Details;
            // 
            // Message
            // 
            this.Message.Width = 500;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView_Message);
            this.Controls.Add(this.button_Listen);
            this.Name = "Form";
            this.Text = "Bài 2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Listen;
        private System.Windows.Forms.ListView listView_Message;
        private System.Windows.Forms.ColumnHeader Message;
    }
}

