namespace IceBot
{
	partial class Admin
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panel2 = new System.Windows.Forms.Panel();
			this.dataUserList = new System.Windows.Forms.DataGridView();
			this.btn_save = new System.Windows.Forms.Button();
			this.btn_delete = new System.Windows.Forms.Button();
			this.btn_add = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.box_admin = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.box_soundPerm = new System.Windows.Forms.CheckBox();
			this.box_downPerm = new System.Windows.Forms.CheckBox();
			this.btn_close = new System.Windows.Forms.Button();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataUserList)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.dataUserList);
			this.panel2.Controls.Add(this.btn_save);
			this.panel2.Controls.Add(this.btn_delete);
			this.panel2.Controls.Add(this.btn_add);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Location = new System.Drawing.Point(12, 31);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(456, 238);
			this.panel2.TabIndex = 33;
			// 
			// dataUserList
			// 
			this.dataUserList.AllowUserToAddRows = false;
			this.dataUserList.AllowUserToDeleteRows = false;
			this.dataUserList.AllowUserToOrderColumns = true;
			this.dataUserList.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.DimGray;
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.dataUserList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataUserList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.dataUserList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataUserList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.dataUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataUserList.ColumnHeadersVisible = false;
			this.dataUserList.EnableHeadersVisualStyles = false;
			this.dataUserList.GridColor = System.Drawing.Color.Gray;
			this.dataUserList.Location = new System.Drawing.Point(7, 38);
			this.dataUserList.MultiSelect = false;
			this.dataUserList.Name = "dataUserList";
			this.dataUserList.ReadOnly = true;
			this.dataUserList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.dataUserList.RowHeadersVisible = false;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.PaleTurquoise;
			dataGridViewCellStyle2.NullValue = null;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.dataUserList.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dataUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataUserList.Size = new System.Drawing.Size(439, 151);
			this.dataUserList.TabIndex = 43;
			// 
			// btn_save
			// 
			this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_save.Enabled = false;
			this.btn_save.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_save.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_save.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_save.Location = new System.Drawing.Point(193, 195);
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new System.Drawing.Size(87, 27);
			this.btn_save.TabIndex = 42;
			this.btn_save.TabStop = false;
			this.btn_save.Text = "Save";
			this.btn_save.UseVisualStyleBackColor = false;
			// 
			// btn_delete
			// 
			this.btn_delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_delete.Enabled = false;
			this.btn_delete.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_delete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_delete.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_delete.Location = new System.Drawing.Point(100, 195);
			this.btn_delete.Name = "btn_delete";
			this.btn_delete.Size = new System.Drawing.Size(87, 27);
			this.btn_delete.TabIndex = 41;
			this.btn_delete.TabStop = false;
			this.btn_delete.Text = "Delete";
			this.btn_delete.UseVisualStyleBackColor = false;
			// 
			// btn_add
			// 
			this.btn_add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_add.Enabled = false;
			this.btn_add.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_add.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_add.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_add.Location = new System.Drawing.Point(7, 195);
			this.btn_add.Name = "btn_add";
			this.btn_add.Size = new System.Drawing.Size(87, 27);
			this.btn_add.TabIndex = 40;
			this.btn_add.TabStop = false;
			this.btn_add.Text = "Add";
			this.btn_add.UseVisualStyleBackColor = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label2.Location = new System.Drawing.Point(398, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 16);
			this.label2.TabIndex = 38;
			this.label2.Text = "Users";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.box_admin);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.box_soundPerm);
			this.panel1.Controls.Add(this.box_downPerm);
			this.panel1.Location = new System.Drawing.Point(12, 275);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(456, 133);
			this.panel1.TabIndex = 34;
			// 
			// box_admin
			// 
			this.box_admin.Appearance = System.Windows.Forms.Appearance.Button;
			this.box_admin.AutoSize = true;
			this.box_admin.BackColor = System.Drawing.Color.DarkCyan;
			this.box_admin.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.box_admin.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkTurquoise;
			this.box_admin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.box_admin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.box_admin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.box_admin.Location = new System.Drawing.Point(250, 35);
			this.box_admin.Name = "box_admin";
			this.box_admin.Size = new System.Drawing.Size(55, 27);
			this.box_admin.TabIndex = 38;
			this.box_admin.Text = "Admin";
			this.box_admin.UseVisualStyleBackColor = false;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label9.Location = new System.Drawing.Point(360, 7);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(82, 16);
			this.label9.TabIndex = 37;
			this.label9.Text = "Permissions";
			// 
			// box_soundPerm
			// 
			this.box_soundPerm.Appearance = System.Windows.Forms.Appearance.Button;
			this.box_soundPerm.AutoSize = true;
			this.box_soundPerm.BackColor = System.Drawing.Color.DarkCyan;
			this.box_soundPerm.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.box_soundPerm.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkTurquoise;
			this.box_soundPerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.box_soundPerm.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.box_soundPerm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.box_soundPerm.Location = new System.Drawing.Point(394, 35);
			this.box_soundPerm.Name = "box_soundPerm";
			this.box_soundPerm.Size = new System.Drawing.Size(55, 27);
			this.box_soundPerm.TabIndex = 36;
			this.box_soundPerm.Text = "Sound";
			this.box_soundPerm.UseVisualStyleBackColor = false;
			// 
			// box_downPerm
			// 
			this.box_downPerm.Appearance = System.Windows.Forms.Appearance.Button;
			this.box_downPerm.AutoSize = true;
			this.box_downPerm.BackColor = System.Drawing.Color.DarkCyan;
			this.box_downPerm.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.box_downPerm.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkTurquoise;
			this.box_downPerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.box_downPerm.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.box_downPerm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.box_downPerm.Location = new System.Drawing.Point(311, 35);
			this.box_downPerm.Name = "box_downPerm";
			this.box_downPerm.Size = new System.Drawing.Size(77, 27);
			this.box_downPerm.TabIndex = 35;
			this.box_downPerm.Text = "Download";
			this.box_downPerm.UseVisualStyleBackColor = false;
			// 
			// btn_close
			// 
			this.btn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_close.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_close.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_close.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_close.Location = new System.Drawing.Point(381, 414);
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new System.Drawing.Size(87, 27);
			this.btn_close.TabIndex = 41;
			this.btn_close.TabStop = false;
			this.btn_close.Text = "Close";
			this.btn_close.UseVisualStyleBackColor = false;
			this.btn_close.Click += new System.EventHandler(this.button1_Click);
			// 
			// Admin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(480, 448);
			this.Controls.Add(this.btn_close);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Admin";
			this.Text = "Admin";
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataUserList)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox box_soundPerm;
		private System.Windows.Forms.CheckBox box_downPerm;
		private System.Windows.Forms.Button btn_save;
		private System.Windows.Forms.Button btn_delete;
		private System.Windows.Forms.Button btn_add;
		private System.Windows.Forms.CheckBox box_admin;
		private System.Windows.Forms.DataGridView dataUserList;
		private System.Windows.Forms.Button btn_close;
	}
}