using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace IceBot
{
	public partial class SoundManager : Form
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

		IceBot.soundFuncs soundFuncs = new IceBot.soundFuncs();
		IceBot.logFuncs logFuncs = new IceBot.logFuncs();

		public static int dlProgress = -1;

		private static string[][] availPacksList = new string[0][];

		public SoundManager()
		{
			InitializeComponent();
		}

		private void SoundManager_Load(object sender, EventArgs e)
		{
			CheckInstalledPacks();

			checkInstPacks.Enabled = true;
			checkInstPacks.Start();

			RefreshAvailPacks();

		}

		private void RefreshAvailPacks()
		{
			DBConnect dbConnect = new DBConnect();

			string[][] list = dbConnect.Select();
			availPacksList = list;

			//Console.WriteLine("LIST :: {0} :: {1}", list[0][0], list [0][1]);

			foreach (string[] str in list)
			{
				//Console.WriteLine("{0} :: {1}", str[0], instPacks.Items.Contains(str[0]));

				if (instPacks.Items.Contains(str[0]) == false)
				{
					availPacks.Items.Add(str[0]);
				}

				//Console.WriteLine(str[0]);
			}
		}

		private void btn_steamConnect_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void DownloadPack(string[] pack)
		{
			Uri dlUrl;

			string baseUrl = "http://venom.jeremyferguson.me/files/soundpacks/";

			string url = baseUrl + pack[1] + ".zip";
			dlUrl = new Uri(url);

			//Console.WriteLine("DOWNLOAD :: {0}", url);

			using (WebClient webClient = new WebClient())
			{
				webClient.DownloadFile(dlUrl, Environment.CurrentDirectory + "/sounds/" + pack[1] + ".zip");
			}

			ZipFile.ExtractToDirectory(Environment.CurrentDirectory + "/sounds/" + pack[1] + ".zip", Environment.CurrentDirectory + "/sounds/");

			File.Delete(Environment.CurrentDirectory + "/sounds/" + pack[1] + ".zip");

			logFuncs.AddToLogs("Success! Installed '" + pack[0] + "' soundpack.", "SOUND");

			btn_installPack.Enabled = true;
			btn_installPack.Text = "Install";

			
		}

		private void CheckInstalledPacks()
		{
			//string[] soundPacks = soundFuncs.GetSoundPacks();

			//if (soundPacks.Length > 0)
			//{

			//	if (soundPacks.Length != instPacks.Items.Count) { 
			//		instPacks.Items.Clear();

			//		foreach (string str in soundPacks)
			//		{
			//			instPacks.Items.Add(str);
			//		}
			//	}

			//}

		}


		private void checkInstPacks_Tick(object sender, EventArgs e)
		{
			CheckInstalledPacks();
		}

		private void btn_installPack_Click(object sender, EventArgs e)
		{
			//Console.WriteLine("AVAIL SELECTED: {0}", availPacks.SelectedIndex);

			if (availPacks.SelectedIndex != -1)
			{
				int sel = availPacks.SelectedIndex;
				//Console.WriteLine("SEL: {0}", sel);

				btn_installPack.Enabled = false;
				btn_installPack.Text = "Installing";

				DownloadPack(availPacksList[sel]);

				availPacks.Items.RemoveAt(sel);

				soundFuncs.doInsertSounds();
			}
			else
			{
				MessageBox.Show("Select a sound pack to install!");
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{

			if (instPacks.SelectedIndex != -1) 
			{ 
				string dir;

				dir = instPacks.SelectedItem.ToString();
				dir = dir.ToLower();
				dir = dir.Replace(" ", "_");

				Console.WriteLine("REMOVE :: {0}", dir);

				Directory.Delete(Environment.CurrentDirectory + "/sounds/" + dir, true);
				int sel = instPacks.SelectedIndex;

				instPacks.Items.RemoveAt(instPacks.SelectedIndex);

				if((sel + 1) != instPacks.Items.Count){
					instPacks.SelectedIndex = sel + 1;
				}
				else
				{
					instPacks.SelectedIndex = -1;
				}

				soundFuncs.doInsertSounds();

				RefreshAvailPacks();

			}
			else
			{
				MessageBox.Show("Select a sound pack to remove!");
			}


		}


	}

}
