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
	public partial class TSSettings : Form
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

		public TSSettings()
		{
			InitializeComponent();
		}

		private void TSSettings_Load(object sender, EventArgs e)
		{
			HostTextBox.Text = Properties.Settings.Default.hostName;
			userNameBox.Text = Properties.Settings.Default.username;
			PortUpDown.Value = Properties.Settings.Default.port;
			sPortUpDown.Value = Properties.Settings.Default.sport;
			nickNameBox.Text = Properties.Settings.Default.nickname;
			PasswordTextBox.Text = Properties.Settings.Default.password;

			if (Properties.Settings.Default.autoconnect == true)
			{
				connectCheck.Checked = true;
			}
		}

		private void btn_ok_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.hostName = HostTextBox.Text;
			Properties.Settings.Default.username = userNameBox.Text;
			Properties.Settings.Default.port = Convert.ToUInt16(PortUpDown.Value);
			Properties.Settings.Default.sport = Convert.ToUInt16(sPortUpDown.Value);
			Properties.Settings.Default.nickname = nickNameBox.Text;
			Properties.Settings.Default.password = PasswordTextBox.Text;
			Properties.Settings.Default.password = PasswordTextBox.Text;
			Properties.Settings.Default.autoconnect = connectCheck.Checked;

			Properties.Settings.Default.Save();

			this.Close();
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}

}
