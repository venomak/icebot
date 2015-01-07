using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IceBot
{
	partial class About : Form
	{
		public About()
		{
			InitializeComponent();

			abtVer.Text = Application.ProductVersion;
		}

		private void btn_steamConnect_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}
}
