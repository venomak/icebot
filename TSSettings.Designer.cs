namespace IceBot
{
	partial class TSSettings
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label9 = new System.Windows.Forms.Label();
			this.connectCheck = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.PasswordTextBox = new System.Windows.Forms.TextBox();
			this.userNameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.sPortUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.HostTextBox = new System.Windows.Forms.TextBox();
			this.nickNameBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.PortUpDown = new System.Windows.Forms.NumericUpDown();
			this.btn_cancel = new System.Windows.Forms.Button();
			this.btn_ok = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sPortUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PortUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.connectCheck);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.PasswordTextBox);
			this.panel1.Controls.Add(this.userNameBox);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.sPortUpDown);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.HostTextBox);
			this.panel1.Controls.Add(this.nickNameBox);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.PortUpDown);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(515, 292);
			this.panel1.TabIndex = 26;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
			this.label9.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.label9.Location = new System.Drawing.Point(11, 6);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(109, 20);
			this.label9.TabIndex = 28;
			this.label9.Text = "Teamspeak 3";
			// 
			// connectCheck
			// 
			this.connectCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.connectCheck.AutoSize = true;
			this.connectCheck.BackColor = System.Drawing.Color.DarkCyan;
			this.connectCheck.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.connectCheck.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkTurquoise;
			this.connectCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.connectCheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.connectCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.connectCheck.Location = new System.Drawing.Point(355, 174);
			this.connectCheck.Name = "connectCheck";
			this.connectCheck.Size = new System.Drawing.Size(135, 27);
			this.connectCheck.TabIndex = 6;
			this.connectCheck.Text = "Connect on startup?";
			this.connectCheck.UseVisualStyleBackColor = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Silver;
			this.label1.Location = new System.Drawing.Point(11, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 17);
			this.label1.TabIndex = 14;
			this.label1.Text = "Server Address";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Silver;
			this.label6.Location = new System.Drawing.Point(11, 90);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(119, 17);
			this.label6.TabIndex = 23;
			this.label6.Text = "ServerQuery Name";
			// 
			// PasswordTextBox
			// 
			this.PasswordTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.PasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PasswordTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PasswordTextBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.PasswordTextBox.Location = new System.Drawing.Point(262, 111);
			this.PasswordTextBox.Name = "PasswordTextBox";
			this.PasswordTextBox.PasswordChar = '*';
			this.PasswordTextBox.Size = new System.Drawing.Size(228, 25);
			this.PasswordTextBox.TabIndex = 2;
			// 
			// userNameBox
			// 
			this.userNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.userNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.userNameBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.userNameBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.userNameBox.Location = new System.Drawing.Point(11, 111);
			this.userNameBox.Name = "userNameBox";
			this.userNameBox.Size = new System.Drawing.Size(238, 25);
			this.userNameBox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Silver;
			this.label2.Location = new System.Drawing.Point(8, 222);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 17);
			this.label2.TabIndex = 15;
			this.label2.Text = "Port";
			// 
			// sPortUpDown
			// 
			this.sPortUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.sPortUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sPortUpDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sPortUpDown.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.sPortUpDown.Location = new System.Drawing.Point(102, 242);
			this.sPortUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.sPortUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.sPortUpDown.Name = "sPortUpDown";
			this.sPortUpDown.Size = new System.Drawing.Size(84, 25);
			this.sPortUpDown.TabIndex = 5;
			this.sPortUpDown.Value = new decimal(new int[] {
            9989,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Silver;
			this.label4.Location = new System.Drawing.Point(259, 91);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(105, 17);
			this.label4.TabIndex = 18;
			this.label4.Text = "Admin Password";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Silver;
			this.label3.Location = new System.Drawing.Point(100, 222);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 17);
			this.label3.TabIndex = 20;
			this.label3.Text = "ServerQuery Port";
			// 
			// HostTextBox
			// 
			this.HostTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.HostTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HostTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HostTextBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.HostTextBox.Location = new System.Drawing.Point(11, 53);
			this.HostTextBox.Name = "HostTextBox";
			this.HostTextBox.Size = new System.Drawing.Size(238, 25);
			this.HostTextBox.TabIndex = 0;
			// 
			// nickNameBox
			// 
			this.nickNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.nickNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nickNameBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nickNameBox.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.nickNameBox.Location = new System.Drawing.Point(11, 176);
			this.nickNameBox.Name = "nickNameBox";
			this.nickNameBox.Size = new System.Drawing.Size(238, 25);
			this.nickNameBox.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Silver;
			this.label5.Location = new System.Drawing.Point(8, 156);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 17);
			this.label5.TabIndex = 19;
			this.label5.Text = "Username";
			// 
			// PortUpDown
			// 
			this.PortUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.PortUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PortUpDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PortUpDown.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.PortUpDown.Location = new System.Drawing.Point(10, 242);
			this.PortUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.PortUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PortUpDown.Name = "PortUpDown";
			this.PortUpDown.Size = new System.Drawing.Size(84, 25);
			this.PortUpDown.TabIndex = 4;
			this.PortUpDown.Value = new decimal(new int[] {
            9987,
            0,
            0,
            0});
			// 
			// btn_cancel
			// 
			this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_cancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_cancel.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_cancel.Location = new System.Drawing.Point(448, 310);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new System.Drawing.Size(79, 27);
			this.btn_cancel.TabIndex = 47;
			this.btn_cancel.Text = "Cancel";
			this.btn_cancel.UseVisualStyleBackColor = false;
			this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
			// 
			// btn_ok
			// 
			this.btn_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_ok.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_ok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_ok.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_ok.Location = new System.Drawing.Point(363, 310);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new System.Drawing.Size(79, 27);
			this.btn_ok.TabIndex = 46;
			this.btn_ok.Text = "Save";
			this.btn_ok.UseVisualStyleBackColor = false;
			this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
			// 
			// TSSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(538, 346);
			this.Controls.Add(this.btn_cancel);
			this.Controls.Add(this.btn_ok);
			this.Controls.Add(this.panel1);
			this.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "TSSettings";
			this.Text = "TSSettings";
			this.Load += new System.EventHandler(this.TSSettings_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sPortUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PortUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox connectCheck;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox PasswordTextBox;
		private System.Windows.Forms.TextBox userNameBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown sPortUpDown;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox HostTextBox;
		private System.Windows.Forms.TextBox nickNameBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown PortUpDown;
		private System.Windows.Forms.Button btn_cancel;
		private System.Windows.Forms.Button btn_ok;
	}
}