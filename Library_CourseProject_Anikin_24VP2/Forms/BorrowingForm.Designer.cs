namespace Library_CourseProject_Anikin_24VP2.Forms
{
    partial class BorrowingForm
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
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            ClientComboBox = new ComboBox();
            BookComboBox = new ComboBox();
            DueDateTimePicker = new DateTimePicker();
            IsReturnedCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // CancelBtn
            // 
            CancelBtn.Anchor = AnchorStyles.Bottom;
            CancelBtn.BackColor = Color.FromArgb(190, 107, 122);
            CancelBtn.FlatStyle = FlatStyle.Flat;
            CancelBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CancelBtn.ForeColor = Color.White;
            CancelBtn.Location = new Point(310, 332);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(160, 50);
            CancelBtn.TabIndex = 8;
            CancelBtn.Text = "Отмена";
            CancelBtn.UseVisualStyleBackColor = false;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // SaveBtn
            // 
            SaveBtn.Anchor = AnchorStyles.Bottom;
            SaveBtn.BackColor = Color.FromArgb(90, 107, 122);
            SaveBtn.FlatStyle = FlatStyle.Flat;
            SaveBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            SaveBtn.ForeColor = Color.White;
            SaveBtn.Location = new Point(20, 332);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(160, 50);
            SaveBtn.TabIndex = 7;
            SaveBtn.Text = "Добавить";
            SaveBtn.UseVisualStyleBackColor = false;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(15, 15);
            label1.Name = "label1";
            label1.Size = new Size(123, 32);
            label1.TabIndex = 0;
            label1.Text = "Читатель:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(15, 115);
            label2.Name = "label2";
            label2.Size = new Size(84, 32);
            label2.TabIndex = 2;
            label2.Text = "Книга:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label4.Location = new Point(15, 215);
            label4.Name = "label4";
            label4.Size = new Size(77, 32);
            label4.TabIndex = 4;
            label4.Text = "Срок:";
            // 
            // ClientComboBox
            // 
            ClientComboBox.BackColor = Color.White;
            ClientComboBox.ForeColor = Color.FromArgb(47, 47, 47);
            ClientComboBox.FormattingEnabled = true;
            ClientComboBox.Location = new Point(20, 60);
            ClientComboBox.Name = "ClientComboBox";
            ClientComboBox.Size = new Size(450, 33);
            ClientComboBox.TabIndex = 1;
            // 
            // BookComboBox
            // 
            BookComboBox.BackColor = Color.White;
            BookComboBox.ForeColor = Color.FromArgb(47, 47, 47);
            BookComboBox.FormattingEnabled = true;
            BookComboBox.Location = new Point(20, 160);
            BookComboBox.Name = "BookComboBox";
            BookComboBox.Size = new Size(450, 33);
            BookComboBox.TabIndex = 3;
            // 
            // DueDateTimePicker
            // 
            DueDateTimePicker.CalendarForeColor = Color.FromArgb(47, 47, 47);
            DueDateTimePicker.CalendarMonthBackground = Color.White;
            DueDateTimePicker.CalendarTitleForeColor = Color.FromArgb(47, 47, 47);
            DueDateTimePicker.Format = DateTimePickerFormat.Short;
            DueDateTimePicker.Location = new Point(20, 260);
            DueDateTimePicker.Name = "DueDateTimePicker";
            DueDateTimePicker.Size = new Size(225, 31);
            DueDateTimePicker.TabIndex = 5;
            // 
            // IsReturnedCheckBox
            // 
            IsReturnedCheckBox.AutoSize = true;
            IsReturnedCheckBox.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            IsReturnedCheckBox.Location = new Point(289, 258);
            IsReturnedCheckBox.Name = "IsReturnedCheckBox";
            IsReturnedCheckBox.Size = new Size(181, 36);
            IsReturnedCheckBox.TabIndex = 6;
            IsReturnedCheckBox.Text = "Возвращена";
            IsReturnedCheckBox.UseVisualStyleBackColor = true;
            // 
            // BorrowingForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 243, 238);
            ClientSize = new Size(488, 394);
            Controls.Add(IsReturnedCheckBox);
            Controls.Add(DueDateTimePicker);
            Controls.Add(BookComboBox);
            Controls.Add(ClientComboBox);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(CancelBtn);
            Controls.Add(SaveBtn);
            Controls.Add(label1);
            ForeColor = Color.FromArgb(47, 47, 47);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(510, 450);
            MinimizeBox = false;
            MinimumSize = new Size(510, 450);
            Name = "BorrowingForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Выдача книги";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CancelBtn;
        private Button SaveBtn;
        private Label label1;
        private Label label2;
        private Label label4;
        private ComboBox ClientComboBox;
        private ComboBox BookComboBox;
        private DateTimePicker DueDateTimePicker;
        private CheckBox IsReturnedCheckBox;
    }
}