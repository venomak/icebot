namespace IceBot
{
	public partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.chatWorker1 = new System.ComponentModel.BackgroundWorker();
			this.btn_steamConnect = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btn_steamSettings = new System.Windows.Forms.Button();
			this.lbl_steamStatus = new System.Windows.Forms.Label();
			this.lbl_steamFriends = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btn_tsSettings = new System.Windows.Forms.Button();
			this.lbl_ts3Status = new System.Windows.Forms.Label();
			this.lbl_ts3Users = new System.Windows.Forms.Label();
			this.btn_ts3Connect = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btn_audioSettings = new System.Windows.Forms.Button();
			this.lbl_songs = new System.Windows.Forms.Label();
			this.lbl_soundClips = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btn_logToggle = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.logBox = new System.Windows.Forms.TextBox();
			this.checkLogs = new System.Windows.Forms.Timer(this.components);
			this.btn_Close = new System.Windows.Forms.Button();
			this.btn_Minimize = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.btn_admSettings = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btn_settings = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_steamConnect
			// 
			this.btn_steamConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_steamConnect.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_steamConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_steamConnect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_steamConnect.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_steamConnect.Location = new System.Drawing.Point(8, 56);
			this.btn_steamConnect.Name = "btn_steamConnect";
			this.btn_steamConnect.Size = new System.Drawing.Size(87, 27);
			this.btn_steamConnect.TabIndex = 29;
			this.btn_steamConnect.Text = "Connect";
			this.btn_steamConnect.UseVisualStyleBackColor = false;
			this.btn_steamConnect.Click += new System.EventHandler(this.btn_steamConnect_Click);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.btn_steamSettings);
			this.panel1.Controls.Add(this.lbl_steamStatus);
			this.panel1.Controls.Add(this.lbl_steamFriends);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.btn_steamConnect);
			this.panel1.Location = new System.Drawing.Point(296, 38);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(264, 95);
			this.panel1.TabIndex = 30;
			// 
			// btn_steamSettings
			// 
			this.btn_steamSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_steamSettings.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_steamSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_steamSettings.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_steamSettings.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_steamSettings.Location = new System.Drawing.Point(101, 56);
			this.btn_steamSettings.Name = "btn_steamSettings";
			this.btn_steamSettings.Size = new System.Drawing.Size(87, 27);
			this.btn_steamSettings.TabIndex = 35;
			this.btn_steamSettings.Text = "Settings";
			this.btn_steamSettings.UseVisualStyleBackColor = false;
			this.btn_steamSettings.Click += new System.EventHandler(this.btn_steamSettings_Click);
			// 
			// lbl_steamStatus
			// 
			this.lbl_steamStatus.AutoSize = true;
			this.lbl_steamStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_steamStatus.Location = new System.Drawing.Point(5, 7);
			this.lbl_steamStatus.Name = "lbl_steamStatus";
			this.lbl_steamStatus.Size = new System.Drawing.Size(73, 16);
			this.lbl_steamStatus.TabIndex = 31;
			this.lbl_steamStatus.Text = "Connected";
			// 
			// lbl_steamFriends
			// 
			this.lbl_steamFriends.AutoSize = true;
			this.lbl_steamFriends.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_steamFriends.Location = new System.Drawing.Point(18, 26);
			this.lbl_steamFriends.Name = "lbl_steamFriends";
			this.lbl_steamFriends.Size = new System.Drawing.Size(121, 16);
			this.lbl_steamFriends.TabIndex = 30;
			this.lbl_steamFriends.Text = "0 / 0 Friends Online";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label1.Location = new System.Drawing.Point(215, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 16);
			this.label1.TabIndex = 31;
			this.label1.Text = "Steam";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label9.Location = new System.Drawing.Point(158, 7);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(92, 16);
			this.label9.TabIndex = 30;
			this.label9.Text = "Teamspeak 3";
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.btn_tsSettings);
			this.panel2.Controls.Add(this.lbl_ts3Status);
			this.panel2.Controls.Add(this.lbl_ts3Users);
			this.panel2.Controls.Add(this.btn_ts3Connect);
			this.panel2.Controls.Add(this.label9);
			this.panel2.Location = new System.Drawing.Point(12, 38);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(252, 95);
			this.panel2.TabIndex = 32;
			// 
			// btn_tsSettings
			// 
			this.btn_tsSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_tsSettings.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_tsSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_tsSettings.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_tsSettings.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_tsSettings.Location = new System.Drawing.Point(101, 56);
			this.btn_tsSettings.Name = "btn_tsSettings";
			this.btn_tsSettings.Size = new System.Drawing.Size(87, 27);
			this.btn_tsSettings.TabIndex = 34;
			this.btn_tsSettings.Text = "Settings";
			this.btn_tsSettings.UseVisualStyleBackColor = false;
			this.btn_tsSettings.Click += new System.EventHandler(this.btn_tsSettings_Click);
			// 
			// lbl_ts3Status
			// 
			this.lbl_ts3Status.AutoSize = true;
			this.lbl_ts3Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_ts3Status.Location = new System.Drawing.Point(5, 7);
			this.lbl_ts3Status.Name = "lbl_ts3Status";
			this.lbl_ts3Status.Size = new System.Drawing.Size(73, 16);
			this.lbl_ts3Status.TabIndex = 33;
			this.lbl_ts3Status.Text = "Connected";
			// 
			// lbl_ts3Users
			// 
			this.lbl_ts3Users.AutoSize = true;
			this.lbl_ts3Users.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_ts3Users.Location = new System.Drawing.Point(18, 26);
			this.lbl_ts3Users.Name = "lbl_ts3Users";
			this.lbl_ts3Users.Size = new System.Drawing.Size(112, 16);
			this.lbl_ts3Users.TabIndex = 32;
			this.lbl_ts3Users.Text = "0 / 0 Users Online";
			// 
			// btn_ts3Connect
			// 
			this.btn_ts3Connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_ts3Connect.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_ts3Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_ts3Connect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_ts3Connect.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_ts3Connect.Location = new System.Drawing.Point(8, 56);
			this.btn_ts3Connect.Name = "btn_ts3Connect";
			this.btn_ts3Connect.Size = new System.Drawing.Size(87, 27);
			this.btn_ts3Connect.TabIndex = 29;
			this.btn_ts3Connect.Text = "Connect";
			this.btn_ts3Connect.UseVisualStyleBackColor = false;
			this.btn_ts3Connect.Click += new System.EventHandler(this.btn_ts3Connect_Click);
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.btn_audioSettings);
			this.panel3.Controls.Add(this.lbl_songs);
			this.panel3.Controls.Add(this.lbl_soundClips);
			this.panel3.Controls.Add(this.label6);
			this.panel3.Location = new System.Drawing.Point(12, 139);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(252, 92);
			this.panel3.TabIndex = 33;
			// 
			// btn_audioSettings
			// 
			this.btn_audioSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_audioSettings.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_audioSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_audioSettings.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_audioSettings.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_audioSettings.Location = new System.Drawing.Point(101, 53);
			this.btn_audioSettings.Name = "btn_audioSettings";
			this.btn_audioSettings.Size = new System.Drawing.Size(87, 27);
			this.btn_audioSettings.TabIndex = 35;
			this.btn_audioSettings.Text = "Settings";
			this.btn_audioSettings.UseVisualStyleBackColor = false;
			this.btn_audioSettings.Click += new System.EventHandler(this.button2_Click);
			// 
			// lbl_songs
			// 
			this.lbl_songs.AutoSize = true;
			this.lbl_songs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_songs.Location = new System.Drawing.Point(5, 24);
			this.lbl_songs.Name = "lbl_songs";
			this.lbl_songs.Size = new System.Drawing.Size(57, 16);
			this.lbl_songs.TabIndex = 33;
			this.lbl_songs.Text = "0 Songs";
			// 
			// lbl_soundClips
			// 
			this.lbl_soundClips.AutoSize = true;
			this.lbl_soundClips.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_soundClips.Location = new System.Drawing.Point(5, 8);
			this.lbl_soundClips.Name = "lbl_soundClips";
			this.lbl_soundClips.Size = new System.Drawing.Size(90, 16);
			this.lbl_soundClips.TabIndex = 32;
			this.lbl_soundClips.Text = "0 Sound Clips";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label6.Location = new System.Drawing.Point(198, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(43, 16);
			this.label6.TabIndex = 31;
			this.label6.Text = "Audio";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
			// 
			// settingsToolStripMenuItem1
			// 
			this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
			this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 1000;
			this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.toolTip1.ForeColor = System.Drawing.Color.Silver;
			this.toolTip1.InitialDelay = 1250;
			this.toolTip1.ReshowDelay = 100;
			// 
			// btn_logToggle
			// 
			this.btn_logToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_logToggle.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_logToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_logToggle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_logToggle.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_logToggle.Location = new System.Drawing.Point(237, 260);
			this.btn_logToggle.Name = "btn_logToggle";
			this.btn_logToggle.Size = new System.Drawing.Size(87, 27);
			this.btn_logToggle.TabIndex = 34;
			this.btn_logToggle.Text = "Show Logs";
			this.btn_logToggle.UseVisualStyleBackColor = false;
			this.btn_logToggle.Click += new System.EventHandler(this.btn_logToggle_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.button1.Location = new System.Drawing.Point(12, 260);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(146, 27);
			this.button1.TabIndex = 35;
			this.button1.Text = "Soundpack Manager";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// logBox
			// 
			this.logBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
			this.logBox.Font = new System.Drawing.Font("ProggyCleanTT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.logBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.logBox.Location = new System.Drawing.Point(12, 309);
			this.logBox.MaxLength = 0;
			this.logBox.Multiline = true;
			this.logBox.Name = "logBox";
			this.logBox.ReadOnly = true;
			this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.logBox.Size = new System.Drawing.Size(548, 200);
			this.logBox.TabIndex = 36;
			// 
			// checkLogs
			// 
			this.checkLogs.Interval = 250;
			// 
			// btn_Close
			// 
			this.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.PaleTurquoise;
			this.btn_Close.FlatAppearance.BorderSize = 0;
			this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Close.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Close.Location = new System.Drawing.Point(543, 2);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Size = new System.Drawing.Size(25, 25);
			this.btn_Close.TabIndex = 37;
			this.btn_Close.TabStop = false;
			this.btn_Close.Text = "¤";
			this.btn_Close.UseVisualStyleBackColor = true;
			this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// btn_Minimize
			// 
			this.btn_Minimize.FlatAppearance.BorderColor = System.Drawing.Color.PaleTurquoise;
			this.btn_Minimize.FlatAppearance.BorderSize = 0;
			this.btn_Minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.btn_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_Minimize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Minimize.Location = new System.Drawing.Point(509, 2);
			this.btn_Minimize.Name = "btn_Minimize";
			this.btn_Minimize.Size = new System.Drawing.Size(25, 25);
			this.btn_Minimize.TabIndex = 38;
			this.btn_Minimize.TabStop = false;
			this.btn_Minimize.Text = "➖";
			this.btn_Minimize.UseVisualStyleBackColor = true;
			this.btn_Minimize.Click += new System.EventHandler(this.btn_Minimize_Click);
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.Add(this.btn_admSettings);
			this.panel4.Controls.Add(this.label3);
			this.panel4.Controls.Add(this.label4);
			this.panel4.Location = new System.Drawing.Point(296, 139);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(263, 92);
			this.panel4.TabIndex = 39;
			// 
			// btn_admSettings
			// 
			this.btn_admSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_admSettings.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_admSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_admSettings.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_admSettings.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_admSettings.Location = new System.Drawing.Point(101, 53);
			this.btn_admSettings.Name = "btn_admSettings";
			this.btn_admSettings.Size = new System.Drawing.Size(87, 27);
			this.btn_admSettings.TabIndex = 35;
			this.btn_admSettings.Text = "Settings";
			this.btn_admSettings.UseVisualStyleBackColor = false;
			this.btn_admSettings.Click += new System.EventHandler(this.btn_admSettings_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(5, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 16);
			this.label3.TabIndex = 32;
			this.label3.Text = "0 Sound Clips";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label4.Location = new System.Drawing.Point(215, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(46, 16);
			this.label4.TabIndex = 31;
			this.label4.Text = "Admin";
			// 
			// btn_settings
			// 
			this.btn_settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_settings.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_settings.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_settings.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_settings.Location = new System.Drawing.Point(471, 260);
			this.btn_settings.Name = "btn_settings";
			this.btn_settings.Size = new System.Drawing.Size(87, 27);
			this.btn_settings.TabIndex = 40;
			this.btn_settings.Text = "Settings";
			this.btn_settings.UseVisualStyleBackColor = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(572, 299);
			this.Controls.Add(this.btn_settings);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.btn_Minimize);
			this.Controls.Add(this.btn_Close);
			this.Controls.Add(this.logBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btn_logToggle);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "IceBot";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		#endregion

		private System.ComponentModel.BackgroundWorker chatWorker1;
		private System.Windows.Forms.Button btn_steamConnect;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btn_ts3Connect;
		public System.Windows.Forms.Label lbl_steamFriends;
		public System.Windows.Forms.Label lbl_steamStatus;
		private System.Windows.Forms.Panel panel3;
		public System.Windows.Forms.Label lbl_ts3Status;
		public System.Windows.Forms.Label lbl_ts3Users;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.Label lbl_songs;
		public System.Windows.Forms.Label lbl_soundClips;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button btn_logToggle;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox logBox;
		private System.Windows.Forms.Timer checkLogs;
		private System.Windows.Forms.Button btn_steamSettings;
		private System.Windows.Forms.Button btn_tsSettings;
		private System.Windows.Forms.Button btn_audioSettings;
		private System.Windows.Forms.Button btn_Close;
		private System.Windows.Forms.Button btn_Minimize;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button btn_admSettings;
		public System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btn_settings;

	}
}

