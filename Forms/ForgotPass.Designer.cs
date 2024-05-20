namespace Chat_video_app.Forms
{
    partial class ForgotPass
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
            this.SendCodeBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.VerifyBtn = new System.Windows.Forms.Button();
            this.CodeBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.errmsg2 = new System.Windows.Forms.Label();
            this.errmsg1 = new System.Windows.Forms.Label();
            this.notmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SendCodeBtn
            // 
            this.SendCodeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.SendCodeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SendCodeBtn.FlatAppearance.BorderSize = 0;
            this.SendCodeBtn.ForeColor = System.Drawing.Color.White;
            this.SendCodeBtn.Location = new System.Drawing.Point(453, 185);
            this.SendCodeBtn.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.SendCodeBtn.Name = "SendCodeBtn";
            this.SendCodeBtn.Size = new System.Drawing.Size(103, 44);
            this.SendCodeBtn.TabIndex = 50;
            this.SendCodeBtn.Text = "Send Code";
            this.SendCodeBtn.UseVisualStyleBackColor = false;
            this.SendCodeBtn.Click += new System.EventHandler(this.SendCodeBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.label7.Location = new System.Drawing.Point(278, 364);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 23);
            this.label7.TabIndex = 49;
            this.label7.Text = "< Back to Login";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.label5.Location = new System.Drawing.Point(276, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 33);
            this.label5.TabIndex = 48;
            this.label5.Text = "Zeem";
            // 
            // VerifyBtn
            // 
            this.VerifyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.VerifyBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VerifyBtn.FlatAppearance.BorderSize = 0;
            this.VerifyBtn.ForeColor = System.Drawing.Color.White;
            this.VerifyBtn.Location = new System.Drawing.Point(453, 302);
            this.VerifyBtn.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.VerifyBtn.Name = "VerifyBtn";
            this.VerifyBtn.Size = new System.Drawing.Size(103, 44);
            this.VerifyBtn.TabIndex = 47;
            this.VerifyBtn.Text = "Verify";
            this.VerifyBtn.UseVisualStyleBackColor = false;
            this.VerifyBtn.Click += new System.EventHandler(this.VerifyBtn_Click);
            // 
            // CodeBox
            // 
            this.CodeBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.CodeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CodeBox.Font = new System.Drawing.Font("MS UI Gothic", 16.2F);
            this.CodeBox.Location = new System.Drawing.Point(220, 263);
            this.CodeBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.CodeBox.Name = "CodeBox";
            this.CodeBox.Size = new System.Drawing.Size(305, 27);
            this.CodeBox.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(107, 263);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 23);
            this.label2.TabIndex = 45;
            this.label2.Text = "Enter Code:";
            // 
            // EmailBox
            // 
            this.EmailBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(233)))));
            this.EmailBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EmailBox.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailBox.Location = new System.Drawing.Point(220, 146);
            this.EmailBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(305, 27);
            this.EmailBox.TabIndex = 44;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(107, 150);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 23);
            this.label9.TabIndex = 43;
            this.label9.Text = "Enter Email:";
            // 
            // errmsg2
            // 
            this.errmsg2.AutoSize = true;
            this.errmsg2.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errmsg2.ForeColor = System.Drawing.Color.Red;
            this.errmsg2.Location = new System.Drawing.Point(216, 223);
            this.errmsg2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.errmsg2.Name = "errmsg2";
            this.errmsg2.Size = new System.Drawing.Size(67, 23);
            this.errmsg2.TabIndex = 51;
            this.errmsg2.Text = "errmsg";
            this.errmsg2.Visible = false;
            // 
            // errmsg1
            // 
            this.errmsg1.AutoSize = true;
            this.errmsg1.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errmsg1.ForeColor = System.Drawing.Color.Red;
            this.errmsg1.Location = new System.Drawing.Point(216, 106);
            this.errmsg1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.errmsg1.Name = "errmsg1";
            this.errmsg1.Size = new System.Drawing.Size(67, 23);
            this.errmsg1.TabIndex = 52;
            this.errmsg1.Text = "errmsg";
            this.errmsg1.Visible = false;
            // 
            // notmsg
            // 
            this.notmsg.AutoSize = true;
            this.notmsg.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notmsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.notmsg.Location = new System.Drawing.Point(216, 185);
            this.notmsg.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.notmsg.Name = "notmsg";
            this.notmsg.Size = new System.Drawing.Size(106, 23);
            this.notmsg.TabIndex = 53;
            this.notmsg.Text = "Notification";
            this.notmsg.Visible = false;
            // 
            // ForgotPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(699, 465);
            this.Controls.Add(this.notmsg);
            this.Controls.Add(this.errmsg1);
            this.Controls.Add(this.errmsg2);
            this.Controls.Add(this.SendCodeBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.VerifyBtn);
            this.Controls.Add(this.CodeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.label9);
            this.Name = "ForgotPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ForgotPass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendCodeBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button VerifyBtn;
        private System.Windows.Forms.TextBox CodeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label errmsg2;
        private System.Windows.Forms.Label errmsg1;
        private System.Windows.Forms.Label notmsg;
    }
}