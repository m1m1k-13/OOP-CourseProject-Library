namespace Library_CourseProject_Anikin_24VP2.Forms
{
    partial class BookForm
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
            TitleTextBox = new TextBox();
            label1 = new Label();
            AuthorTextBox = new TextBox();
            label2 = new Label();
            GenreTextBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            PublishYearUpDown = new NumericUpDown();
            CountUpDown = new NumericUpDown();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)PublishYearUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CountUpDown).BeginInit();
            SuspendLayout();
            // 
            // CancelBtn
            // 
            CancelBtn.BackColor = Color.FromArgb(190, 107, 122);
            CancelBtn.FlatStyle = FlatStyle.Flat;
            CancelBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CancelBtn.ForeColor = Color.White;
            CancelBtn.Location = new Point(310, 532);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(160, 50);
            CancelBtn.TabIndex = 11;
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
            SaveBtn.Location = new Point(20, 532);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(160, 50);
            SaveBtn.TabIndex = 10;
            SaveBtn.Text = "Добавить";
            SaveBtn.UseVisualStyleBackColor = false;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // TitleTextBox
            // 
            TitleTextBox.BackColor = Color.White;
            TitleTextBox.BorderStyle = BorderStyle.FixedSingle;
            TitleTextBox.ForeColor = Color.FromArgb(47, 47, 47);
            TitleTextBox.Location = new Point(20, 60);
            TitleTextBox.MaxLength = 64;
            TitleTextBox.Name = "TitleTextBox";
            TitleTextBox.Size = new Size(450, 31);
            TitleTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(15, 15);
            label1.Name = "label1";
            label1.Size = new Size(200, 32);
            label1.TabIndex = 0;
            label1.Text = "Название книги:";
            // 
            // AuthorTextBox
            // 
            AuthorTextBox.BackColor = Color.White;
            AuthorTextBox.BorderStyle = BorderStyle.FixedSingle;
            AuthorTextBox.ForeColor = Color.FromArgb(47, 47, 47);
            AuthorTextBox.Location = new Point(20, 160);
            AuthorTextBox.MaxLength = 64;
            AuthorTextBox.Name = "AuthorTextBox";
            AuthorTextBox.Size = new Size(450, 31);
            AuthorTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(15, 115);
            label2.Name = "label2";
            label2.Size = new Size(87, 32);
            label2.TabIndex = 2;
            label2.Text = "Автор:";
            // 
            // GenreTextBox
            // 
            GenreTextBox.BackColor = Color.White;
            GenreTextBox.BorderStyle = BorderStyle.FixedSingle;
            GenreTextBox.ForeColor = Color.FromArgb(47, 47, 47);
            GenreTextBox.Location = new Point(20, 360);
            GenreTextBox.MaxLength = 64;
            GenreTextBox.Name = "GenreTextBox";
            GenreTextBox.Size = new Size(450, 31);
            GenreTextBox.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.Location = new Point(15, 315);
            label3.Name = "label3";
            label3.Size = new Size(83, 32);
            label3.TabIndex = 6;
            label3.Text = "Жанр:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label4.Location = new Point(15, 215);
            label4.Name = "label4";
            label4.Size = new Size(158, 32);
            label4.TabIndex = 4;
            label4.Text = "Год издания:";
            // 
            // PublishYearUpDown
            // 
            PublishYearUpDown.BackColor = Color.White;
            PublishYearUpDown.BorderStyle = BorderStyle.FixedSingle;
            PublishYearUpDown.ForeColor = Color.FromArgb(47, 47, 47);
            PublishYearUpDown.Location = new Point(20, 260);
            PublishYearUpDown.Maximum = new decimal(new int[] { 2026, 0, 0, 0 });
            PublishYearUpDown.Minimum = new decimal(new int[] { 1450, 0, 0, 0 });
            PublishYearUpDown.Name = "PublishYearUpDown";
            PublishYearUpDown.Size = new Size(450, 31);
            PublishYearUpDown.TabIndex = 5;
            PublishYearUpDown.Value = new decimal(new int[] { 1900, 0, 0, 0 });
            // 
            // CountUpDown
            // 
            CountUpDown.BackColor = Color.White;
            CountUpDown.BorderStyle = BorderStyle.FixedSingle;
            CountUpDown.ForeColor = Color.FromArgb(47, 47, 47);
            CountUpDown.Location = new Point(20, 460);
            CountUpDown.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            CountUpDown.Name = "CountUpDown";
            CountUpDown.Size = new Size(450, 31);
            CountUpDown.TabIndex = 9;
            CountUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label5.Location = new Point(15, 415);
            label5.Name = "label5";
            label5.Size = new Size(151, 32);
            label5.TabIndex = 8;
            label5.Text = "Количество:";
            // 
            // BookForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 243, 238);
            ClientSize = new Size(488, 594);
            Controls.Add(CountUpDown);
            Controls.Add(label5);
            Controls.Add(PublishYearUpDown);
            Controls.Add(label4);
            Controls.Add(GenreTextBox);
            Controls.Add(label3);
            Controls.Add(AuthorTextBox);
            Controls.Add(label2);
            Controls.Add(CancelBtn);
            Controls.Add(SaveBtn);
            Controls.Add(TitleTextBox);
            Controls.Add(label1);
            ForeColor = Color.FromArgb(47, 47, 47);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(510, 650);
            MinimizeBox = false;
            MinimumSize = new Size(510, 650);
            Name = "BookForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Добавление книги";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)PublishYearUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)CountUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CancelBtn;
        private Button SaveBtn;
        private TextBox TitleTextBox;
        private Label label1;
        private TextBox AuthorTextBox;
        private Label label2;
        private TextBox GenreTextBox;
        private Label label3;
        private Label label4;
        private NumericUpDown PublishYearUpDown;
        private NumericUpDown CountUpDown;
        private Label label5;
    }
}