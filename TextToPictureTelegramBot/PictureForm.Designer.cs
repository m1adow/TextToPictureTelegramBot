namespace TextToPictureTelegramBot
{
    partial class PictureForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelText = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.labelText.AutoSize = true;
            this.labelText.Font = new System.Drawing.Font("пиксели просто", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelText.Location = new System.Drawing.Point(98, 192);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(185, 34);
            this.labelText.TabIndex = 1;
            this.labelText.Text = "AHAHAAHAHA";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(207, 437);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(34, 15);
            this.labelUser.TabIndex = 2;
            this.labelUser.Text = "USER";
            // 
            // PictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.labelText);
            this.Name = "PictureForm";
            this.Text = "Picture";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label labelText;
        private Label labelUser;
    }
}