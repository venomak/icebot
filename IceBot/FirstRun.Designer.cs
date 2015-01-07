namespace IceBot
{
	partial class FirstRun
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btn_close = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "Welcome";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(14, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Put something here";
			// 
			// btn_close
			// 
			this.btn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.btn_close.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_close.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_close.ForeColor = System.Drawing.Color.PaleTurquoise;
			this.btn_close.Location = new System.Drawing.Point(399, 354);
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new System.Drawing.Size(87, 27);
			this.btn_close.TabIndex = 30;
			this.btn_close.Text = "Close";
			this.btn_close.UseVisualStyleBackColor = false;
			this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
			// 
			// FirstRun
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(498, 393);
			this.Controls.Add(this.btn_close);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.ForeColor = System.Drawing.Color.Silver;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FirstRun";
			this.Text = "FirstRun";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn_close;
	}
}