namespace Library_CourseProject_Anikin_24VP2.Forms
{
    partial class WelcomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            StartBtn = new Button();
            label6 = new Label();
            label7 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(230, 225, 216);
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.FromArgb(47, 62, 78);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(453, 95);
            label1.TabIndex = 0;
            label1.Text = "Программа управления \r\nИС «Библиотека»";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.ForeColor = Color.FromArgb(47, 62, 78);
            label2.Location = new Point(15, 110);
            label2.Name = "label2";
            label2.Size = new Size(173, 32);
            label2.TabIndex = 0;
            label2.Text = "Тема проекта:";
            // 
            // label3
            // 
            label3.Location = new Point(15, 150);
            label3.Name = "label3";
            label3.Size = new Size(419, 90);
            label3.TabIndex = 0;
            label3.Text = "Разработка программы с использованием объектно-ориентированного подхода. \r\nИС «Библиотека»";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(47, 62, 78);
            label4.Location = new Point(15, 245);
            label4.Name = "label4";
            label4.Size = new Size(185, 32);
            label4.TabIndex = 0;
            label4.Text = "Автор проекта:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 285);
            label5.Name = "label5";
            label5.Size = new Size(190, 28);
            label5.TabIndex = 0;
            label5.Text = "Аникин А. А. 24ВП2";
            // 
            // StartBtn
            // 
            StartBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            StartBtn.BackColor = Color.FromArgb(90, 107, 122);
            StartBtn.FlatStyle = FlatStyle.Flat;
            StartBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            StartBtn.ForeColor = Color.White;
            StartBtn.Location = new Point(122, 432);
            StartBtn.Name = "StartBtn";
            StartBtn.Size = new Size(200, 50);
            StartBtn.TabIndex = 1;
            StartBtn.Text = "Начать работу";
            StartBtn.UseVisualStyleBackColor = false;
            StartBtn.Click += StartBtn_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 370);
            label6.Name = "label6";
            label6.Size = new Size(322, 28);
            label6.TabIndex = 0;
            label6.Text = "C# • WinForms • SQLite • QuestPDF";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(47, 62, 78);
            label7.Location = new Point(15, 330);
            label7.Name = "label7";
            label7.Size = new Size(148, 32);
            label7.TabIndex = 0;
            label7.Text = "Технологии:";
            // 
            // WelcomeForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 243, 238);
            ClientSize = new Size(453, 494);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(StartBtn);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(47, 47, 47);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(475, 550);
            MinimizeBox = false;
            MinimumSize = new Size(475, 550);
            Name = "WelcomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Информация о проекте";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button StartBtn;
        private Label label6;
        private Label label7;
    }
}