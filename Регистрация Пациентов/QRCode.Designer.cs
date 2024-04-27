namespace Регистрация_Пациентов
{
    partial class QRCodeGeneratorWindow
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
            QRCodeImage = new PictureBox();
            QRButton = new Button();
            ((System.ComponentModel.ISupportInitialize)QRCodeImage).BeginInit();
            SuspendLayout();
            // 
            // QRCodeImage
            // 
            QRCodeImage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            QRCodeImage.Location = new Point(54, 29);
            QRCodeImage.Name = "QRCodeImage";
            QRCodeImage.Size = new Size(236, 238);
            QRCodeImage.SizeMode = PictureBoxSizeMode.CenterImage;
            QRCodeImage.TabIndex = 0;
            QRCodeImage.TabStop = false;
            // 
            // QRButton
            // 
            QRButton.BackColor = SystemColors.ButtonShadow;
            QRButton.Font = new Font("Segoe UI", 11F);
            QRButton.Location = new Point(93, 313);
            QRButton.Name = "QRButton";
            QRButton.Size = new Size(163, 42);
            QRButton.TabIndex = 1;
            QRButton.Text = "Считать QR код";
            QRButton.UseVisualStyleBackColor = false;
            QRButton.Click += QRButton_Click;
            // 
            // QRCodeGeneratorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(359, 399);
            Controls.Add(QRButton);
            Controls.Add(QRCodeImage);
            Name = "QRCodeGeneratorWindow";
            Text = "QRCode";
            Load += QRCode_Load;
            ((System.ComponentModel.ISupportInitialize)QRCodeImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox QRCodeImage;
        private Button QRButton;
    }
}