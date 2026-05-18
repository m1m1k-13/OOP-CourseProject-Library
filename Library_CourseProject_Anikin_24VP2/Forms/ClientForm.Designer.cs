namespace Library_CourseProject_Anikin_24VP2.Forms
{
    partial class ClientForm
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
            CancelBtn = new Button();
            SaveBtn = new Button();
            NameTextBox = new TextBox();
            label1 = new Label();
            PhoneTextBox = new TextBox();
            label2 = new Label();
            AddressTextBox = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // CancelBtn
            // 
            CancelBtn.BackColor = Color.FromArgb(190, 107, 122);
            CancelBtn.FlatStyle = FlatStyle.Flat;
            CancelBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CancelBtn.ForeColor = Color.White;
            CancelBtn.Location = new Point(310, 332);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(160, 50);
            CancelBtn.TabIndex = 7;
            CancelBtn.Text = "Отмена";
            CancelBtn.UseVisualStyleBackColor = false;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // SaveBtn
            // 
            SaveBtn.BackColor = Color.FromArgb(90, 107, 122);
            SaveBtn.FlatStyle = FlatStyle.Flat;
            SaveBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            SaveBtn.ForeColor = Color.White;
            SaveBtn.Location = new Point(20, 332);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(160, 50);
            SaveBtn.TabIndex = 6;
            SaveBtn.Text = "Добавить";
            SaveBtn.UseVisualStyleBackColor = false;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // NameTextBox
            // 
            NameTextBox.BackColor = Color.White;
            NameTextBox.BorderStyle = BorderStyle.FixedSingle;
            NameTextBox.ForeColor = Color.FromArgb(47, 47, 47);
            NameTextBox.Location = new Point(20, 60);
            NameTextBox.MaxLength = 64;
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(450, 31);
            NameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(15, 15);
            label1.Name = "label1";
            label1.Size = new Size(75, 32);
            label1.TabIndex = 0;
            label1.Text = "ФИО:";
            // 
            // PhoneTextBox
            // 
            PhoneTextBox.BackColor = Color.White;
            PhoneTextBox.BorderStyle = BorderStyle.FixedSingle;
            PhoneTextBox.ForeColor = Color.FromArgb(47, 47, 47);
            PhoneTextBox.Location = new Point(20, 159);
            PhoneTextBox.MaxLength = 64;
            PhoneTextBox.Name = "PhoneTextBox";
            PhoneTextBox.Size = new Size(450, 31);
            PhoneTextBox.TabIndex = 3;
            PhoneTextBox.KeyPress += ClientPhoneTextBox_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(15, 115);
            label2.Name = "label2";
            label2.Size = new Size(116, 32);
            label2.TabIndex = 2;
            label2.Text = "Телефон:";
            // 
            // AddressTextBox
            // 
            AddressTextBox.BackColor = Color.White;
            AddressTextBox.BorderStyle = BorderStyle.FixedSingle;
            AddressTextBox.ForeColor = Color.FromArgb(47, 47, 47);
            AddressTextBox.Location = new Point(20, 260);
            AddressTextBox.MaxLength = 64;
            AddressTextBox.Name = "AddressTextBox";
            AddressTextBox.Size = new Size(450, 31);
            AddressTextBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.Location = new Point(15, 215);
            label3.Name = "label3";
            label3.Size = new Size(89, 32);
            label3.TabIndex = 4;
            label3.Text = "Адрес:";
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 243, 238);
            ClientSize = new Size(488, 394);
            Controls.Add(AddressTextBox);
            Controls.Add(label3);
            Controls.Add(PhoneTextBox);
            Controls.Add(label2);
            Controls.Add(CancelBtn);
            Controls.Add(SaveBtn);
            Controls.Add(NameTextBox);
            Controls.Add(label1);
            ForeColor = Color.FromArgb(47, 47, 47);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(510, 450);
            MinimizeBox = false;
            MinimumSize = new Size(510, 450);
            Name = "ClientForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Регистрация читателя";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CancelBtn;
        private Button SaveBtn;
        private TextBox NameTextBox;
        private Label label1;
        private TextBox PhoneTextBox;
        private Label label2;
        private TextBox AddressTextBox;
        private Label label3;
    }
}