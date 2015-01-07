using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IceBot
{
	public partial class SteamSettings : Form
	{

		private const int WM_NCHITTEST = 0x84;
		private const int HTCLIENT = 0x1;
		private const int HTCAPTION = 0x2;

		protected override void WndProc(ref Message message)
		{
			base.WndProc(ref message);

			if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
				message.Result = (IntPtr)HTCAPTION;
		}

		public SteamSettings()
		{
			InitializeComponent();
		}

		private void SteamSettings_Load(object sender, EventArgs e)
		{

			steamUNBox.Text = Properties.Settings.Default.steamusername;
			steamPWBox.Text = Properties.Settings.Default.steampassword;

			if (Properties.Settings.Default.steamautostart)
			{
				steamAutoConnectCheck.Checked = true;
			}

		}

		private void btn_ok_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.steamusername = steamUNBox.Text;
			Properties.Settings.Default.steampassword = steamPWBox.Text;

			Properties.Settings.Default.steamautostart = steamAutoConnectCheck.Checked;

			Properties.Settings.Default.Save();

			this.Close();
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}

}
