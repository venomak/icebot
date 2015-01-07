namespace IceBot
{
	partial class SoundManager
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btn_installPack = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.instPacks = new System.Windows.Forms.ListBox();
			this.availPacks = new System.Windows.Forms.ListBox();
			this.btn_steamConnect = new System.Windows.Forms.Button();
			this.checkInstPacks = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.btn_installPack);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.instPacks);
			this.panel1.Controls.Add(this.availPacks);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(449, 207);
			this.panel1.TabIndex = 33;
			// 
			// btn_installPack
			// 
			this.btn_installPack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_installPack.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_installPack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_installPack.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_installPack.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_installPack.Location = new System.Drawing.Point(347, 169);
			this.btn_installPack.Name = "btn_installPack";
			this.btn_installPack.Size = new System.Drawing.Size(87, 27);
			this.btn_installPack.TabIndex = 39;
			this.btn_installPack.Text = "Install";
			this.btn_installPack.UseVisualStyleBackColor = false;
			this.btn_installPack.Click += new System.EventHandler(this.btn_installPack_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.button1.Location = new System.Drawing.Point(115, 169);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(87, 27);
			this.button1.TabIndex = 38;
			this.button1.Text = "Remove";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label3.Location = new System.Drawing.Point(227, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 15);
			this.label3.TabIndex = 37;
			this.label3.Text = "Available";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label2.Location = new System.Drawing.Point(11, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 15);
			this.label2.TabIndex = 36;
			this.label2.Text = "Installed";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label1.Location = new System.Drawing.Point(319, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 15);
			this.label1.TabIndex = 35;
			this.label1.Text = "Soundpack Manager";
			// 
			// instPacks
			// 
			this.instPacks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.instPacks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.instPacks.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.instPacks.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.instPacks.FormattingEnabled = true;
			this.instPacks.Location = new System.Drawing.Point(14, 44);
			this.instPacks.Name = "instPacks";
			this.instPacks.Size = new System.Drawing.Size(188, 119);
			this.instPacks.TabIndex = 34;
			// 
			// availPacks
			// 
			this.availPacks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.availPacks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.availPacks.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.availPacks.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.availPacks.FormattingEnabled = true;
			this.availPacks.Location = new System.Drawing.Point(230, 44);
			this.availPacks.Name = "availPacks";
			this.availPacks.Size = new System.Drawing.Size(204, 119);
			this.availPacks.TabIndex = 33;
			// 
			// btn_steamConnect
			// 
			this.btn_steamConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_steamConnect.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_steamConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_steamConnect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_steamConnect.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_steamConnect.Location = new System.Drawing.Point(374, 225);
			this.btn_steamConnect.Name = "btn_steamConnect";
			this.btn_steamConnect.Size = new System.Drawing.Size(87, 27);
			this.btn_steamConnect.TabIndex = 29;
			this.btn_steamConnect.Text = "Close";
			this.btn_steamConnect.UseVisualStyleBackColor = false;
			this.btn_steamConnect.Click += new System.EventHandler(this.btn_steamConnect_Click);
			// 
			// checkInstPacks
			// 
			this.checkInstPacks.Interval = 2500;
			this.checkInstPacks.Tick += new System.EventHandler(this.checkInstPacks_Tick);
			// 
			// SoundManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(473, 261);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btn_steamConnect);
			this.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SoundManager";
			this.Text = "Sound Manager";
			this.Load += new System.EventHandler(this.SoundManager_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox instPacks;
		private System.Windows.Forms.ListBox availPacks;
		private System.Windows.Forms.Button btn_steamConnect;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btn_installPack;
		private System.Windows.Forms.Timer checkInstPacks;


	}
}