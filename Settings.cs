using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;

namespace IceBot
{

	public partial class Settings : Form
	{
		IceBot.gFuncs gFuncs = new IceBot.gFuncs();

		private const int WM_NCHITTEST = 0x84;
		private const int HTCLIENT = 0x1;
		private const int HTCAPTION = 0x2;

		//
		// Handling the window messages
		//
		protected override void WndProc(ref Message message)
		{
			base.WndProc(ref message);

			if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
				message.Result = (IntPtr)HTCAPTION;
		}

		public Settings()
		{
			InitializeComponent();
		}


		private void CreateTooltips()
		{
			System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();

			//ToolTip1.SetToolTip(this.HostTextBox, "Hostname or IP of your Teamspeak server.");
			//ToolTip1.SetToolTip(this.userNameBox, "Serverquery Username.");
			//ToolTip1.SetToolTip(this.PasswordTextBox, "Serverquery Password.");
			//ToolTip1.SetToolTip(this.nickNameBox, "Nickname of the Serverquery client to use.");
			//ToolTip1.SetToolTip(this.PortUpDown, "Port of the Teamspeak server to connect to.");
			//ToolTip1.SetToolTip(this.sPortUpDown, "Serverquery port of the Teamspeak server to connect to.");
			//ToolTip1.SetToolTip(this.connectCheck, "Automatically connect to the Teamspeak server on startup?");


			//ToolTip1.SetToolTip(this.steamUNBox, "Steam Username (Use a spare account!)");
			//ToolTip1.SetToolTip(this.steamPWBox, "Steam Password (Use a spare account!)");
			//ToolTip1.SetToolTip(this.steamEnabledCheck, "Steam is enabled?");
			//ToolTip1.SetToolTip(this.steamAutoConnectCheck, "Automatically connect to Steam on startup?");

			//ToolTip1.SetToolTip(this.musicDirBox, "The directory(ies) which hold your music.");

			//ToolTip1.SetToolTip(this.outputBox, "Output sound device (What you hear / Line 1)");
			//ToolTip1.SetToolTip(this.inputBox, "Input sound device ()");

			//ToolTip1.SetToolTip(this.shuffleBox, "Automatically start music on shuffle when started?");

		}

		private void Settings_Load(object sender, EventArgs e)
		{

			try
			{
				CreateTooltips();

				

				//Populate audio in and out.

				//int waveInDevices = WaveIn.DeviceCount;
				//for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
				//{
				//	WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);

				//	inputBox.Items.Add(deviceInfo.ProductName);
				//}

				
				//inputBox.SelectedIndex = Properties.Settings.Default.inputdevice;

				int waveOutDevices = WaveOut.DeviceCount;
				for (int waveOutDevice = 0; waveOutDevice < waveOutDevices; waveOutDevice++)
				{
					WaveOutCapabilities deviceInfo = WaveOut.GetCapabilities(waveOutDevice);

					outputBox.Items.Add(deviceInfo.ProductName);

					if (Properties.Settings.Default.outputdevice != "")
					{

						if (deviceInfo.ProductName == Properties.Settings.Default.outputdevice)
						{
							Console.WriteLine("Selected: {0}", deviceInfo.ProductName);

							outputBox.SelectedIndex = waveOutDevice;
						}
						
					}

				}


				foreach (string str in Properties.Settings.Default.musicdirs)
				{
					musicDirBox.Items.Add(str);
				}

				if (musicDirBox.Items.Count > 0)
				{
					musicDirDialog.SelectedPath = musicDirBox.Items[0].ToString();
				}

				if (Properties.Settings.Default.startshuffled)
				{
					shuffleBox.Checked = true;
				}


			}
			catch (InvalidCastException exc)
			{
				MessageBox.Show(exc.Message);
			}

			
		}


		private void saveBtn_Click(object sender, EventArgs e)
		{

			if (outputBox.SelectedIndex != -1) { 
				Properties.Settings.Default.outputdevice = outputBox.SelectedItem.ToString();

				Console.WriteLine("SET DEVICE TO {0}", outputBox.SelectedItem.ToString());
			}

			//Properties.Settings.Default.inputdevice = inputBox.SelectedIndex;

			Properties.Settings.Default.musicdirs.Clear();

			foreach (string str in musicDirBox.Items)
			{
				Properties.Settings.Default.musicdirs.Add(str);
			}

			Properties.Settings.Default.startshuffled = shuffleBox.Checked;

			Properties.Settings.Default.Save();

			this.Close();
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void mbrowseBtn_Click(object sender, EventArgs e)
		{
			Console.WriteLine("MUSIC ADD");

			DialogResult res = musicDirDialog.ShowDialog();

			//Console.WriteLine("RES: {0}", res);
			if (res.ToString() == "OK")
			{
				musicDirBox.Items.Add(musicDirDialog.SelectedPath);
			}
		}

		private void btn_soundRescan_Click(object sender, EventArgs e)
		{
			IceBot.soundFuncs soundFuncs = new IceBot.soundFuncs();

			soundFuncs.doInsertSounds();
		}

		private void btn_musicRescan_Click(object sender, EventArgs e)
		{
			IceBot.mp3Funcs mp3Funcs = new IceBot.mp3Funcs();

			mp3Funcs.doScanMusic();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			if (musicDirBox.SelectedItem != null)
			{
				musicDirBox.Items.RemoveAt(musicDirBox.SelectedIndex);
			}
		}

	}
}
