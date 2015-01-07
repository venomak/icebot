namespace IceBot
{
	partial class SteamSettings
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.label14 = new System.Windows.Forms.Label();
			this.steamAutoConnectCheck = new System.Windows.Forms.CheckBox();
			this.label13 = new System.Windows.Forms.Label();
			this.steamPWBox = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.steamUNBox = new System.Windows.Forms.TextBox();
			this.btn_ok = new System.Windows.Forms.Button();
			this.btn_cancel = new System.Windows.Forms.Button();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.label14);
			this.panel2.Controls.Add(this.steamAutoConnectCheck);
			this.panel2.Controls.Add(this.label13);
			this.panel2.Controls.Add(this.steamPWBox);
			this.panel2.Controls.Add(this.label12);
			this.panel2.Controls.Add(this.steamUNBox);
			this.panel2.Location = new System.Drawing.Point(12, 12);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(294, 174);
			this.panel2.TabIndex = 27;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
			this.label14.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.label14.Location = new System.Drawing.Point(6, 6);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(57, 20);
			this.label14.TabIndex = 29;
			this.label14.Text = "Steam";
			// 
			// steamAutoConnectCheck
			// 
			this.steamAutoConnectCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.steamAutoConnectCheck.AutoSize = true;
			this.steamAutoConnectCheck.BackColor = System.Drawing.Color.DarkCyan;
			this.steamAutoConnectCheck.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.steamAutoConnectCheck.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkTurquoise;
			this.steamAutoConnectCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.steamAutoConnectCheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.steamAutoConnectCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.steamAutoConnectCheck.Location = new System.Drawing.Point(185, 133);
			this.steamAutoConnectCheck.Name = "steamAutoConnectCheck";
			this.steamAutoConnectCheck.Size = new System.Drawing.Size(97, 27);
			this.steamAutoConnectCheck.TabIndex = 26;
			this.steamAutoConnectCheck.Text = "Auto-Connect";
			this.steamAutoConnectCheck.UseVisualStyleBackColor = false;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.BackColor = System.Drawing.Color.Transparent;
			this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.ForeColor = System.Drawing.Color.Silver;
			this.label13.Location = new System.Drawing.Point(7, 82);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 17);
			this.label13.TabIndex = 24;
			this.label13.Text = "Password";
			// 
			// steamPWBox
			// 
			this.steamPWBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.steamPWBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.steamPWBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.steamPWBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.steamPWBox.Location = new System.Drawing.Point(10, 102);
			this.steamPWBox.Name = "steamPWBox";
			this.steamPWBox.PasswordChar = '*';
			this.steamPWBox.Size = new System.Drawing.Size(272, 25);
			this.steamPWBox.TabIndex = 7;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.BackColor = System.Drawing.Color.Transparent;
			this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.ForeColor = System.Drawing.Color.Silver;
			this.label12.Location = new System.Drawing.Point(7, 31);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(67, 17);
			this.label12.TabIndex = 22;
			this.label12.Text = "Username";
			// 
			// steamUNBox
			// 
			this.steamUNBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.steamUNBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.steamUNBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.steamUNBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.steamUNBox.Location = new System.Drawing.Point(10, 51);
			this.steamUNBox.Name = "steamUNBox";
			this.steamUNBox.Size = new System.Drawing.Size(272, 25);
			this.steamUNBox.TabIndex = 6;
			// 
			// btn_ok
			// 
			this.btn_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_ok.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_ok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_ok.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_ok.Location = new System.Drawing.Point(142, 193);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new System.Drawing.Size(79, 27);
			this.btn_ok.TabIndex = 44;
			this.btn_ok.Text = "Save";
			this.btn_ok.UseVisualStyleBackColor = false;
			this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
			// 
			// btn_cancel
			// 
			this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_cancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_cancel.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_cancel.Location = new System.Drawing.Point(227, 193);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new System.Drawing.Size(79, 27);
			this.btn_cancel.TabIndex = 45;
			this.btn_cancel.Text = "Cancel";
			this.btn_cancel.UseVisualStyleBackColor = false;
			this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
			// 
			// SteamSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(316, 232);
			this.Controls.Add(this.btn_cancel);
			this.Controls.Add(this.btn_ok);
			this.Controls.Add(this.panel2);
			this.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SteamSettings";
			this.Text = "SteamSettings";
			this.Load += new System.EventHandler(this.SteamSettings_Load);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.CheckBox steamAutoConnectCheck;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox steamPWBox;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox steamUNBox;
		private System.Windows.Forms.Button btn_ok;
		private System.Windows.Forms.Button btn_cancel;
	}
}