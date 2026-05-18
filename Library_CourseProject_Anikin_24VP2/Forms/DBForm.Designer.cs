namespace Library_CourseProject_Anikin_24VP2.Forms
{
    partial class DBForm
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
            label1 = new Label();
            DBNameTextBox = new TextBox();
            SaveBtn = new Button();
            CancelBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(15, 15);
            label1.Name = "label1";
            label1.Size = new Size(168, 32);
            label1.TabIndex = 0;
            label1.Text = "Название БД:";
            // 
            // DBNameTextBox
            // 
            DBNameTextBox.BackColor = Color.White;
            DBNameTextBox.BorderStyle = BorderStyle.FixedSingle;
            DBNameTextBox.ForeColor = Color.FromArgb(47, 47, 47);
            DBNameTextBox.Location = new Point(20, 60);
            DBNameTextBox.MaxLength = 64;
            DBNameTextBox.Name = "DBNameTextBox";
            DBNameTextBox.Size = new Size(450, 34);
            DBNameTextBox.TabIndex = 1;
            // 
            // SaveBtn
            // 
            SaveBtn.BackColor = Color.FromArgb(90, 107, 122);
            SaveBtn.Cursor = Cursors.Hand;
            SaveBtn.FlatStyle = FlatStyle.Flat;
            SaveBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            SaveBtn.ForeColor = Color.White;
            SaveBtn.Location = new Point(20, 132);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(160, 50);
            SaveBtn.TabIndex = 2;
            SaveBtn.Text = "Создать";
            SaveBtn.UseVisualStyleBackColor = false;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // CancelBtn
            // 
            CancelBtn.BackColor = Color.FromArgb(190, 107, 122);
            CancelBtn.Cursor = Cursors.Hand;
            CancelBtn.FlatStyle = FlatStyle.Flat;
            CancelBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CancelBtn.ForeColor = Color.White;
            CancelBtn.Location = new Point(310, 132);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(160, 50);
            CancelBtn.TabIndex = 3;
            CancelBtn.Text = "Отмена";
            CancelBtn.UseVisualStyleBackColor = false;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // DBForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 243, 238);
            ClientSize = new Size(488, 194);
            Controls.Add(CancelBtn);
            Controls.Add(SaveBtn);
            Controls.Add(DBNameTextBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(47, 47, 47);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(510, 250);
            MinimizeBox = false;
            MinimumSize = new Size(510, 250);
            Name = "DBForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание БД";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox DBNameTextBox;
        private Button SaveBtn;
        private Button CancelBtn;
    }
}