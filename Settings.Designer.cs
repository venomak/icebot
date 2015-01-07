namespace IceBot
{
	partial class Settings
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
			this.cancelBtn = new System.Windows.Forms.Button();
			this.saveBtn = new System.Windows.Forms.Button();
			this.soundDirDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.musicDirDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btn_soundRescan = new System.Windows.Forms.Button();
			this.btn_musicRescan = new System.Windows.Forms.Button();
			this.label15 = new System.Windows.Forms.Label();
			this.shuffleBox = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.mbrowseBtn = new System.Windows.Forms.Button();
			this.musicDirBox = new System.Windows.Forms.ListBox();
			this.label11 = new System.Windows.Forms.Label();
			this.inputBox = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.outputBox = new System.Windows.Forms.ListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelBtn
			// 
			this.cancelBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.cancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cancelBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cancelBtn.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.cancelBtn.Location = new System.Drawing.Point(344, 385);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(87, 27);
			this.cancelBtn.TabIndex = 21;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = false;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// saveBtn
			// 
			this.saveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.saveBtn.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.saveBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveBtn.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.saveBtn.Location = new System.Drawing.Point(249, 385);
			this.saveBtn.Name = "saveBtn";
			this.saveBtn.Size = new System.Drawing.Size(87, 27);
			this.saveBtn.TabIndex = 22;
			this.saveBtn.Text = "Save";
			this.saveBtn.UseVisualStyleBackColor = false;
			this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.btn_soundRescan);
			this.panel3.Controls.Add(this.btn_musicRescan);
			this.panel3.Controls.Add(this.label15);
			this.panel3.Controls.Add(this.shuffleBox);
			this.panel3.Controls.Add(this.button1);
			this.panel3.Controls.Add(this.mbrowseBtn);
			this.panel3.Controls.Add(this.musicDirBox);
			this.panel3.Controls.Add(this.label11);
			this.panel3.Controls.Add(this.inputBox);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.outputBox);
			this.panel3.Controls.Add(this.label7);
			this.panel3.Location = new System.Drawing.Point(14, 12);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(417, 367);
			this.panel3.TabIndex = 27;
			// 
			// btn_soundRescan
			// 
			this.btn_soundRescan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_soundRescan.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_soundRescan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_soundRescan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_soundRescan.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_soundRescan.Location = new System.Drawing.Point(11, 133);
			this.btn_soundRescan.Name = "btn_soundRescan";
			this.btn_soundRescan.Size = new System.Drawing.Size(168, 27);
			this.btn_soundRescan.TabIndex = 43;
			this.btn_soundRescan.Text = "Rescan Sound Clips";
			this.btn_soundRescan.UseVisualStyleBackColor = false;
			this.btn_soundRescan.Click += new System.EventHandler(this.btn_soundRescan_Click);
			// 
			// btn_musicRescan
			// 
			this.btn_musicRescan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_musicRescan.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_musicRescan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_musicRescan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_musicRescan.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_musicRescan.Location = new System.Drawing.Point(11, 331);
			this.btn_musicRescan.Name = "btn_musicRescan";
			this.btn_musicRescan.Size = new System.Drawing.Size(87, 27);
			this.btn_musicRescan.TabIndex = 42;
			this.btn_musicRescan.Text = "Rescan";
			this.btn_musicRescan.UseVisualStyleBackColor = false;
			this.btn_musicRescan.Click += new System.EventHandler(this.btn_musicRescan_Click);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
			this.label15.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.label15.Location = new System.Drawing.Point(11, 9);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(51, 20);
			this.label15.TabIndex = 41;
			this.label15.Text = "Audio";
			// 
			// shuffleBox
			// 
			this.shuffleBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.shuffleBox.AutoSize = true;
			this.shuffleBox.BackColor = System.Drawing.Color.DarkCyan;
			this.shuffleBox.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.shuffleBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkTurquoise;
			this.shuffleBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.shuffleBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.shuffleBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.shuffleBox.Location = new System.Drawing.Point(104, 331);
			this.shuffleBox.Name = "shuffleBox";
			this.shuffleBox.Size = new System.Drawing.Size(102, 27);
			this.shuffleBox.TabIndex = 29;
			this.shuffleBox.Text = "Start Shuffled?";
			this.shuffleBox.UseVisualStyleBackColor = false;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.button1.Location = new System.Drawing.Point(316, 331);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(87, 27);
			this.button1.TabIndex = 40;
			this.button1.TabStop = false;
			this.button1.Text = "Delete";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// mbrowseBtn
			// 
			this.mbrowseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.mbrowseBtn.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.mbrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.mbrowseBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mbrowseBtn.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.mbrowseBtn.Location = new System.Drawing.Point(221, 331);
			this.mbrowseBtn.Name = "mbrowseBtn";
			this.mbrowseBtn.Size = new System.Drawing.Size(87, 27);
			this.mbrowseBtn.TabIndex = 39;
			this.mbrowseBtn.TabStop = false;
			this.mbrowseBtn.Text = "Add";
			this.mbrowseBtn.UseVisualStyleBackColor = false;
			this.mbrowseBtn.Click += new System.EventHandler(this.mbrowseBtn_Click);
			// 
			// musicDirBox
			// 
			this.musicDirBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.musicDirBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.musicDirBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.musicDirBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.musicDirBox.FormattingEnabled = true;
			this.musicDirBox.ItemHeight = 17;
			this.musicDirBox.Location = new System.Drawing.Point(11, 199);
			this.musicDirBox.Name = "musicDirBox";
			this.musicDirBox.Size = new System.Drawing.Size(391, 121);
			this.musicDirBox.TabIndex = 37;
			this.musicDirBox.TabStop = false;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.BackColor = System.Drawing.Color.Transparent;
			this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.ForeColor = System.Drawing.Color.Silver;
			this.label11.Location = new System.Drawing.Point(11, 181);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(109, 17);
			this.label11.TabIndex = 36;
			this.label11.Text = "Music Directories";
			// 
			// inputBox
			// 
			this.inputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.inputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.inputBox.Enabled = false;
			this.inputBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.inputBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.inputBox.FormattingEnabled = true;
			this.inputBox.ItemHeight = 17;
			this.inputBox.Location = new System.Drawing.Point(316, 58);
			this.inputBox.Name = "inputBox";
			this.inputBox.Size = new System.Drawing.Size(287, 53);
			this.inputBox.TabIndex = 33;
			this.inputBox.TabStop = false;
			this.inputBox.Visible = false;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Enabled = false;
			this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.ForeColor = System.Drawing.Color.Silver;
			this.label8.Location = new System.Drawing.Point(313, 38);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(75, 17);
			this.label8.TabIndex = 32;
			this.label8.Text = "Audio Input";
			this.label8.Visible = false;
			// 
			// outputBox
			// 
			this.outputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.outputBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.outputBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.outputBox.FormattingEnabled = true;
			this.outputBox.ItemHeight = 17;
			this.outputBox.Location = new System.Drawing.Point(11, 58);
			this.outputBox.Name = "outputBox";
			this.outputBox.Size = new System.Drawing.Size(280, 53);
			this.outputBox.TabIndex = 31;
			this.outputBox.TabStop = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.Silver;
			this.label7.Location = new System.Drawing.Point(11, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(86, 17);
			this.label7.TabIndex = 30;
			this.label7.Text = "Audio Output";
			// 
			// Settings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(445, 424);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.saveBtn);
			this.Controls.Add(this.cancelBtn);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Settings";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.Settings_Load);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button saveBtn;
		private System.Windows.Forms.FolderBrowserDialog soundDirDialog;
		private System.Windows.Forms.FolderBrowserDialog musicDirDialog;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.CheckBox shuffleBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button mbrowseBtn;
		private System.Windows.Forms.ListBox musicDirBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ListBox inputBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox outputBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button btn_musicRescan;
		private System.Windows.Forms.Button btn_soundRescan;
	}
}