namespace Chat_video_app.Forms
{
    partial class Room2_host
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
            System.Windows.Forms.Label label3;
            this.ShutdownServer = new System.Windows.Forms.Button();
            this.NewServerPortBox = new System.Windows.Forms.TextBox();
            this.CreateNewServerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.KickButton = new System.Windows.Forms.Button();
            this.MuteButton = new System.Windows.Forms.Button();
            this.ClientNameList = new System.Windows.Forms.ListBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.serversFoundLabel = new System.Windows.Forms.Label();
            this.ServerList = new System.Windows.Forms.ListBox();
            this.confirmNameButton = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.NameInputBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(41, 406);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(34, 16);
            label3.TabIndex = 42;
            label3.Text = "Port:";
            // 
            // ShutdownServer
            // 
            this.ShutdownServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ShutdownServer.Location = new System.Drawing.Point(193, 433);
            this.ShutdownServer.Margin = new System.Windows.Forms.Padding(4);
            this.ShutdownServer.Name = "ShutdownServer";
            this.ShutdownServer.Size = new System.Drawing.Size(140, 28);
            this.ShutdownServer.TabIndex = 43;
            this.ShutdownServer.Text = "Shutdown Server";
            this.ShutdownServer.UseVisualStyleBackColor = false;
            this.ShutdownServer.Click += new System.EventHandler(this.ShutdownServer_Click);
            // 
            // NewServerPortBox
            // 
            this.NewServerPortBox.Location = new System.Drawing.Point(83, 403);
            this.NewServerPortBox.Margin = new System.Windows.Forms.Padding(4);
            this.NewServerPortBox.Name = "NewServerPortBox";
            this.NewServerPortBox.Size = new System.Drawing.Size(132, 22);
            this.NewServerPortBox.TabIndex = 41;
            // 
            // CreateNewServerButton
            // 
            this.CreateNewServerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CreateNewServerButton.Location = new System.Drawing.Point(33, 433);
            this.CreateNewServerButton.Margin = new System.Windows.Forms.Padding(4);
            this.CreateNewServerButton.Name = "CreateNewServerButton";
            this.CreateNewServerButton.Size = new System.Drawing.Size(152, 28);
            this.CreateNewServerButton.TabIndex = 40;
            this.CreateNewServerButton.Text = "Create New Server";
            this.CreateNewServerButton.UseVisualStyleBackColor = false;
            this.CreateNewServerButton.Click += new System.EventHandler(this.CreateNewServerButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 368);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 37;
            this.label1.Text = "Host Server";
            // 
            // KickButton
            // 
            this.KickButton.Location = new System.Drawing.Point(392, 521);
            this.KickButton.Margin = new System.Windows.Forms.Padding(4);
            this.KickButton.Name = "KickButton";
            this.KickButton.Size = new System.Drawing.Size(147, 28);
            this.KickButton.TabIndex = 34;
            this.KickButton.Text = "Kick Person";
            this.KickButton.UseVisualStyleBackColor = true;
            this.KickButton.Click += new System.EventHandler(this.KickButton_Click);
            // 
            // MuteButton
            // 
            this.MuteButton.Location = new System.Drawing.Point(392, 485);
            this.MuteButton.Margin = new System.Windows.Forms.Padding(4);
            this.MuteButton.Name = "MuteButton";
            this.MuteButton.Size = new System.Drawing.Size(147, 28);
            this.MuteButton.TabIndex = 33;
            this.MuteButton.Text = "Mute Person";
            this.MuteButton.UseVisualStyleBackColor = true;
            this.MuteButton.Click += new System.EventHandler(this.MuteButton_Click);
            // 
            // ClientNameList
            // 
            this.ClientNameList.FormattingEnabled = true;
            this.ClientNameList.ItemHeight = 16;
            this.ClientNameList.Location = new System.Drawing.Point(33, 485);
            this.ClientNameList.Margin = new System.Windows.Forms.Padding(4);
            this.ClientNameList.Name = "ClientNameList";
            this.ClientNameList.Size = new System.Drawing.Size(349, 100);
            this.ClientNameList.Sorted = true;
            this.ClientNameList.TabIndex = 32;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(141, 293);
            this.DisconnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(100, 28);
            this.DisconnectButton.TabIndex = 31;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ConnectButton.Location = new System.Drawing.Point(33, 293);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(100, 28);
            this.ConnectButton.TabIndex = 30;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(33, 14);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(65, 24);
            this.StatusLabel.TabIndex = 29;
            this.StatusLabel.Text = "Status:";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(438, 117);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(100, 27);
            this.RefreshButton.TabIndex = 28;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // serversFoundLabel
            // 
            this.serversFoundLabel.AutoSize = true;
            this.serversFoundLabel.Location = new System.Drawing.Point(33, 129);
            this.serversFoundLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.serversFoundLabel.Name = "serversFoundLabel";
            this.serversFoundLabel.Size = new System.Drawing.Size(98, 16);
            this.serversFoundLabel.TabIndex = 26;
            this.serversFoundLabel.Text = "Servers Found:";
            // 
            // ServerList
            // 
            this.ServerList.FormattingEnabled = true;
            this.ServerList.ItemHeight = 16;
            this.ServerList.Location = new System.Drawing.Point(33, 153);
            this.ServerList.Margin = new System.Windows.Forms.Padding(4);
            this.ServerList.Name = "ServerList";
            this.ServerList.ScrollAlwaysVisible = true;
            this.ServerList.Size = new System.Drawing.Size(504, 132);
            this.ServerList.TabIndex = 25;
            // 
            // confirmNameButton
            // 
            this.confirmNameButton.Location = new System.Drawing.Point(206, 79);
            this.confirmNameButton.Margin = new System.Windows.Forms.Padding(4);
            this.confirmNameButton.Name = "confirmNameButton";
            this.confirmNameButton.Size = new System.Drawing.Size(123, 25);
            this.confirmNameButton.TabIndex = 24;
            this.confirmNameButton.Text = "Confirm Name";
            this.confirmNameButton.UseVisualStyleBackColor = true;
            this.confirmNameButton.Click += new System.EventHandler(this.confirmNameButton_Click);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(33, 55);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(70, 16);
            this.usernameLabel.TabIndex = 23;
            this.usernameLabel.Text = "Username";
            // 
            // NameInputBox
            // 
            this.NameInputBox.Location = new System.Drawing.Point(33, 79);
            this.NameInputBox.Margin = new System.Windows.Forms.Padding(4);
            this.NameInputBox.MaxLength = 16;
            this.NameInputBox.Name = "NameInputBox";
            this.NameInputBox.Size = new System.Drawing.Size(164, 22);
            this.NameInputBox.TabIndex = 22;
            // 
            // Room2_host
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 599);
            this.Controls.Add(this.ShutdownServer);
            this.Controls.Add(label3);
            this.Controls.Add(this.NewServerPortBox);
            this.Controls.Add(this.CreateNewServerButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KickButton);
            this.Controls.Add(this.MuteButton);
            this.Controls.Add(this.ClientNameList);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.serversFoundLabel);
            this.Controls.Add(this.ServerList);
            this.Controls.Add(this.confirmNameButton);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.NameInputBox);
            this.Name = "Room2_host";
            this.Text = "Room2_host";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button ShutdownServer;
        public System.Windows.Forms.TextBox NewServerPortBox;
        public System.Windows.Forms.Button CreateNewServerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button KickButton;
        public System.Windows.Forms.Button MuteButton;
        public System.Windows.Forms.ListBox ClientNameList;
        public System.Windows.Forms.Button DisconnectButton;
        public System.Windows.Forms.Button ConnectButton;
        public System.Windows.Forms.Label StatusLabel;
        public System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Label serversFoundLabel;
        public System.Windows.Forms.ListBox ServerList;
        private System.Windows.Forms.Button confirmNameButton;
        private System.Windows.Forms.Label usernameLabel;
        public System.Windows.Forms.TextBox NameInputBox;
    }
}