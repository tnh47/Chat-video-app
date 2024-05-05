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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ShutdownServer = new System.Windows.Forms.Button();
            this.NewServerPortBox = new System.Windows.Forms.TextBox();
            this.CreateNewServerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.clientsDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.identifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kick = new System.Windows.Forms.DataGridViewButtonColumn();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.clientsDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(17, 32);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(34, 16);
            label3.TabIndex = 42;
            label3.Text = "Port:";
            // 
            // ShutdownServer
            // 
            this.ShutdownServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ShutdownServer.Location = new System.Drawing.Point(206, 56);
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
            this.NewServerPortBox.Location = new System.Drawing.Point(73, 26);
            this.NewServerPortBox.Margin = new System.Windows.Forms.Padding(4);
            this.NewServerPortBox.Name = "NewServerPortBox";
            this.NewServerPortBox.Size = new System.Drawing.Size(132, 22);
            this.NewServerPortBox.TabIndex = 41;
            // 
            // CreateNewServerButton
            // 
            this.CreateNewServerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CreateNewServerButton.Location = new System.Drawing.Point(20, 56);
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
            this.label1.Location = new System.Drawing.Point(17, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 37;
            this.label1.Text = "Host Server";
            // 
            // MuteButton
            // 
            this.MuteButton.Location = new System.Drawing.Point(405, 391);
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
            this.ClientNameList.Location = new System.Drawing.Point(33, 366);
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
            this.DisconnectButton.Location = new System.Drawing.Point(146, 142);
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
            this.ConnectButton.Location = new System.Drawing.Point(29, 142);
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
            this.StatusLabel.Location = new System.Drawing.Point(33, 0);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(65, 24);
            this.StatusLabel.TabIndex = 29;
            this.StatusLabel.Text = "Status:";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(388, 16);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(76, 27);
            this.RefreshButton.TabIndex = 28;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // serversFoundLabel
            // 
            this.serversFoundLabel.AutoSize = true;
            this.serversFoundLabel.Location = new System.Drawing.Point(13, 4);
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
            this.ServerList.Location = new System.Drawing.Point(16, 51);
            this.ServerList.Margin = new System.Windows.Forms.Padding(4);
            this.ServerList.Name = "ServerList";
            this.ServerList.ScrollAlwaysVisible = true;
            this.ServerList.Size = new System.Drawing.Size(469, 84);
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
            // clientsDataGridView
            // 
            this.clientsDataGridView.AllowUserToAddRows = false;
            this.clientsDataGridView.AllowUserToDeleteRows = false;
            this.clientsDataGridView.AllowUserToResizeColumns = false;
            this.clientsDataGridView.AllowUserToResizeRows = false;
            this.clientsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.clientsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.clientsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.clientsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.clientsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.clientsDataGridView.ColumnHeadersHeight = 24;
            this.clientsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.clientsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.identifier,
            this.name,
            this.Kick});
            this.clientsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.clientsDataGridView.EnableHeadersVisualStyles = false;
            this.clientsDataGridView.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.clientsDataGridView.Location = new System.Drawing.Point(599, 30);
            this.clientsDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.clientsDataGridView.MultiSelect = false;
            this.clientsDataGridView.Name = "clientsDataGridView";
            this.clientsDataGridView.ReadOnly = true;
            this.clientsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.clientsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.clientsDataGridView.RowHeadersVisible = false;
            this.clientsDataGridView.RowHeadersWidth = 40;
            this.clientsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.clientsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.clientsDataGridView.RowTemplate.Height = 24;
            this.clientsDataGridView.RowTemplate.ReadOnly = true;
            this.clientsDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clientsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.clientsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientsDataGridView.ShowCellErrors = false;
            this.clientsDataGridView.ShowCellToolTips = false;
            this.clientsDataGridView.ShowEditingIcon = false;
            this.clientsDataGridView.ShowRowErrors = false;
            this.clientsDataGridView.Size = new System.Drawing.Size(234, 464);
            this.clientsDataGridView.TabIndex = 44;
            this.clientsDataGridView.TabStop = false;
            this.clientsDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.clientsDataGridView_CellMouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(label3);
            this.panel1.Controls.Add(this.CreateNewServerButton);
            this.panel1.Controls.Add(this.ShutdownServer);
            this.panel1.Controls.Add(this.NewServerPortBox);
            this.panel1.Location = new System.Drawing.Point(37, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 214);
            this.panel1.TabIndex = 45;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.serversFoundLabel);
            this.panel2.Controls.Add(this.ServerList);
            this.panel2.Controls.Add(this.RefreshButton);
            this.panel2.Controls.Add(this.ConnectButton);
            this.panel2.Controls.Add(this.DisconnectButton);
            this.panel2.Location = new System.Drawing.Point(37, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 214);
            this.panel2.TabIndex = 46;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(305, 113);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(187, 30);
            this.textBox3.TabIndex = 44;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Green;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(305, 173);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 29);
            this.button3.TabIndex = 42;
            this.button3.TabStop = false;
            this.button3.Text = "Invite";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.label5.Location = new System.Drawing.Point(231, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 43;
            this.label5.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.label2.Location = new System.Drawing.Point(16, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 20);
            this.label2.TabIndex = 45;
            this.label2.Text = "Invite a user into room";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(750, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(83, 465);
            this.panel3.TabIndex = 47;
            // 
            // identifier
            // 
            this.identifier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.identifier.DefaultCellStyle = dataGridViewCellStyle2;
            this.identifier.HeaderText = "ID";
            this.identifier.MaxInputLength = 20;
            this.identifier.MinimumWidth = 20;
            this.identifier.Name = "identifier";
            this.identifier.ReadOnly = true;
            this.identifier.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.identifier.Width = 75;
            // 
            // name
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.name.DefaultCellStyle = dataGridViewCellStyle3;
            this.name.HeaderText = "Name";
            this.name.MaxInputLength = 20;
            this.name.MinimumWidth = 50;
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.name.Width = 75;
            // 
            // Kick
            // 
            this.Kick.HeaderText = "OutRoom";
            this.Kick.MinimumWidth = 6;
            this.Kick.Name = "Kick";
            this.Kick.ReadOnly = true;
            this.Kick.Text = "Kick";
            this.Kick.Width = 75;
            // 
            // Room2_host
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 507);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.clientsDataGridView);
            this.Controls.Add(this.MuteButton);
            this.Controls.Add(this.ClientNameList);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.confirmNameButton);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.NameInputBox);
            this.Name = "Room2_host";
            this.Text = "Room2_host";
            ((System.ComponentModel.ISupportInitialize)(this.clientsDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button ShutdownServer;
        public System.Windows.Forms.TextBox NewServerPortBox;
        public System.Windows.Forms.Button CreateNewServerButton;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.DataGridView clientsDataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn identifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewButtonColumn Kick;
    }
}