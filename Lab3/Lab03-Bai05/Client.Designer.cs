namespace Lab03_Bai05
{
    partial class Lab305
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
            this.lblAddFood = new System.Windows.Forms.Label();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.btnSelectFood = new System.Windows.Forms.Button();
            this.lstFoodList = new System.Windows.Forms.ListBox();
            this.txtFood = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPicture = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtBrowseImage = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAddFood
            // 
            this.lblAddFood.AutoSize = true;
            this.lblAddFood.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddFood.Location = new System.Drawing.Point(23, 25);
            this.lblAddFood.Name = "lblAddFood";
            this.lblAddFood.Size = new System.Drawing.Size(118, 23);
            this.lblAddFood.TabIndex = 0;
            this.lblAddFood.Text = "Nhập món ăn";
            // 
            // btnAddFood
            // 
            this.btnAddFood.Location = new System.Drawing.Point(380, 429);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(168, 45);
            this.btnAddFood.TabIndex = 2;
            this.btnAddFood.Text = "Thêm";
            this.btnAddFood.UseVisualStyleBackColor = true;
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // btnSelectFood
            // 
            this.btnSelectFood.Location = new System.Drawing.Point(774, 350);
            this.btnSelectFood.Name = "btnSelectFood";
            this.btnSelectFood.Size = new System.Drawing.Size(168, 45);
            this.btnSelectFood.TabIndex = 4;
            this.btnSelectFood.Text = "Chọn";
            this.btnSelectFood.UseVisualStyleBackColor = true;
            this.btnSelectFood.Click += new System.EventHandler(this.btnSelectFood_Click);
            // 
            // lstFoodList
            // 
            this.lstFoodList.FormattingEnabled = true;
            this.lstFoodList.ItemHeight = 17;
            this.lstFoodList.Location = new System.Drawing.Point(642, 29);
            this.lstFoodList.Name = "lstFoodList";
            this.lstFoodList.Size = new System.Drawing.Size(300, 293);
            this.lstFoodList.TabIndex = 5;
            // 
            // txtFood
            // 
            this.txtFood.Location = new System.Drawing.Point(248, 26);
            this.txtFood.Name = "txtFood";
            this.txtFood.Size = new System.Drawing.Size(300, 25);
            this.txtFood.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(248, 86);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(300, 25);
            this.txtName.TabIndex = 9;
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(248, 187);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(300, 198);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 10;
            this.picImage.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(27, 86);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(136, 23);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "Tên người đăng";
            // 
            // lblPicture
            // 
            this.lblPicture.AutoSize = true;
            this.lblPicture.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPicture.Location = new System.Drawing.Point(27, 143);
            this.lblPicture.Name = "lblPicture";
            this.lblPicture.Size = new System.Drawing.Size(82, 23);
            this.lblPicture.TabIndex = 12;
            this.lblPicture.Text = "Hình ảnh";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(31, 191);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(139, 34);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "Chọn Ảnh";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtBrowseImage
            // 
            this.txtBrowseImage.Location = new System.Drawing.Point(248, 143);
            this.txtBrowseImage.Name = "txtBrowseImage";
            this.txtBrowseImage.ReadOnly = true;
            this.txtBrowseImage.Size = new System.Drawing.Size(300, 25);
            this.txtBrowseImage.TabIndex = 14;
            // 
            // Lab305
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 503);
            this.Controls.Add(this.txtBrowseImage);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblPicture);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtFood);
            this.Controls.Add(this.lstFoodList);
            this.Controls.Add(this.btnSelectFood);
            this.Controls.Add(this.btnAddFood);
            this.Controls.Add(this.lblAddFood);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Lab305";
            this.Text = "Lab3-05";
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddFood;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.Button btnSelectFood;
        private System.Windows.Forms.ListBox lstFoodList;
        private System.Windows.Forms.TextBox txtFood;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPicture;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtBrowseImage;
    }
}

