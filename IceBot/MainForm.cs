using System;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Speech.Synthesis;
//using System.Speech.Recognition;
using System.Net;
using System.IO;

using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Drawing.Drawing2D;

using HtmlAgilityPack;


namespace IceBot
{
    public partial class MainForm : Form
    {

		IceBot.gFuncs gFuncs = new IceBot.gFuncs();
		IceBot.mp3Funcs mp3Funcs = new IceBot.mp3Funcs();
		IceBot.soundFuncs soundFuncs = new IceBot.soundFuncs();
		IceBot.dotaFuncs dotaFuncs = new IceBot.dotaFuncs();
		IceBot.steamFuncs steamFuncs = new IceBot.steamFuncs();
		IceBot.tsFuncs tsFuncs = new IceBot.tsFuncs();
		IceBot.cmdFuncs cmdFuncs = new IceBot.cmdFuncs();
		IceBot.helpFuncs helpFuncs = new IceBot.helpFuncs();
		IceBot.logFuncs logFuncs = new IceBot.logFuncs();
		IceBot.userFuncs userFuncs = new IceBot.userFuncs();
		IceBot.ttsFuncs ttsFuncs = new IceBot.ttsFuncs();
		IceBot.downloadFuncs downloadFuncs = new IceBot.downloadFuncs();


		private const int WM_NCHITTEST = 0x84;
		private const int HTCLIENT = 0x1;
		private const int HTCAPTION = 0x2;

		protected override void WndProc(ref Message message)
		{
			base.WndProc(ref message);

			if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
				message.Result = (IntPtr)HTCAPTION;
		}

        public MainForm()
        {
            InitializeComponent();

			helpFuncs.AddChildren();

			//gFuncs.NeatFormat(null, null);

			//dotaFuncs.GetHeroList();
			//userFuncs.SaveUsers();
			//userFuncs.ReadUsers();
        }


		private void CreateTooltips()
		{
			//System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();

			//ToolTip1.SetToolTip(this.btn_steamConnect, "Hello");
			//ToolTip1.SetToolTip(this.lbl_steamStatus, "Hello");

		}

		public void doStartAsync()
		{
			Thread oThread = new Thread(new ThreadStart(DoStartUp));

			// Start the thread
			oThread.IsBackground = true;
			oThread.Start();

			// Spin for a while waiting for the started thread to become
			// alive:
			while (!oThread.IsAlive) ;
		}


		private void DoStartUp()
		{
			Console.WriteLine("ENTER");

			SetText(lbl_steamStatus, "Disconnected");
			ToggleVisible(lbl_steamFriends, 0);

			SetText(lbl_ts3Status, "Disconnected");
			ToggleVisible(lbl_ts3Users, 0);

			CreateTooltips();

			gFuncs.IsFirstRun();

			//userFuncs.LoadUsers();

		}


		private void MainForm_Shown(object sender, EventArgs e)
		{
			doStartAsync();

			//Event Handlers
			soundFuncs.ScanSoundUpdate += soundFuncs_ScanSoundUpdate;
			soundFuncs.ScanSoundStart += soundFuncs_ScanSoundStart;
			//soundFuncs.ScanSoundError += soundFuncs_ScanSoundError;

			mp3Funcs.ScanMusicUpdate += mp3Funcs_ScanMusicUpdate;
			mp3Funcs.MusicUpdate += mp3Funcs_MusicUpdate;
			mp3Funcs.ScanMusicStart += mp3Funcs_ScanMusicStart;

			steamFuncs.FriendsUpdate += steamFuncs_FriendsUpdate;
			steamFuncs.SteamConnect += steamFuncs_SteamConnect;
			steamFuncs.SteamDisconnect += steamFuncs_SteamDisconnect;
			steamFuncs.SteamConnectError += steamFuncs_SteamConnectError;

			tsFuncs.TS3Connect += tsFuncs_TS3Connect;
			tsFuncs.TS3Disconnect += tsFuncs_TS3Disconnect;
			tsFuncs.TS3Update += tsFuncs_TS3Update;
			tsFuncs.TS3ConnectError += tsFuncs_TS3ConnectError;

			logFuncs.LogUpdate += logFuncs_LogUpdate;


			try
			{
				//if (soundFuncs.g_soundList.Count == 0)
				//{
				//	//Log("SOUNDS: Loading Sounds");
				//	lbl_soundClips.Text = "Loading Sound Clips...";

				//	soundFuncs.doInsertSounds();
				//	//Log("SOUNDS: Sounds Loaded");
				//}

				soundFuncs.doInsertSounds();

				userFuncs.InsertUsers();

				if (mp3Funcs.musicList.Length == 0)
				{
					//Log("MUSIC: Loading Music");

					lbl_songs.Text = "Loading Music...";

					mp3Funcs.doScanMusic();

					//Log("MUSIC: Music Loaded == " + mp3Funcs.musicList.Length.ToString() + " Tracks Loaded");
				}

				//helpFuncs.PopulateHelp();

				if (Properties.Settings.Default.autoconnect == true)
				{

					btn_ts3Connect.Enabled = false;

					if (tsFuncs.IsConnected)
					{
						tsFuncs.Disconnect();
					}
					else
					{
						tsFuncs.Connect();
					}

				}

				mp3Funcs.StartTimer();

				soundFuncs.StartTimer();

				downloadFuncs.StartTimer();
				//downloadFuncs.SetupHandlers();
				cmdFuncs.SetupHandlers();

				if (Properties.Settings.Default.startshuffled)
				{
					mp3Funcs.mt = mp3Funcs.MusicType.Shuffle;

					if (mp3Funcs.mt == mp3Funcs.MusicType.Shuffle)
					{
						mp3Funcs.AddRandom();
						mp3Funcs.Play();
					}
				}

				// Set Dota hero list.
				//dotaFuncs.heroes = dotaFuncs.engine.GetHeroes();

				if (Properties.Settings.Default.steamenabled && Properties.Settings.Default.steamautostart)
				{
					if (Properties.Settings.Default.steamusername != "" && Properties.Settings.Default.steampassword != "")
					{
						btn_steamConnect.Enabled = false;

						steamFuncs.DoConnect();
					}

				}
			}
			catch (InvalidCastException exc)
			{
				MessageBox.Show(exc.Message);
			}

		}

		void logFuncs_LogUpdate()
		{
			//Update logs.
			//Add latest entry from logFuncs.logs.

			int lenLogs = logFuncs.logs.Length;

			//Console.WriteLine(lenLogs);

			if (lenLogs >= 1) 
			{
				AddText(logBox, logFuncs.logs[lenLogs - 1]);
			}

		}

		void tsFuncs_TS3ConnectError()
		{
			MessageBox.Show("Invalid Teamspeak 3 credentials!");

			ToggleEnabled(btn_ts3Connect, 1);
		}

		void steamFuncs_SteamConnectError()
		{
			MessageBox.Show("Invalid Steam account credentials!");

			ToggleEnabled(btn_steamConnect, 1);
		}

		void soundFuncs_ScanSoundError()
		{
			Console.WriteLine("ERROR");
		}

		private void AddText(TextBox ctrl, string text)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action(() => AddText(ctrl, text)));
				return;
			}


			ctrl.AppendText(Environment.NewLine + text);

		}

		private void SetText(Control lbl, string text)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action(() => SetText(lbl, text)));
				return;
			}

			lbl.Text = text;
		}

		private void ToggleEnabled(Control ctrl, int tog = -1)
		{
			bool togg = false;

			if (tog == -1)
			{

				if (ctrl.Enabled == false) 
				{ 
					togg = true;
				}
				else
				{
					togg = false;
				}

			}
			else if (tog == 1)
			{
				togg = true;
			}

			if (this.InvokeRequired)
			{
				this.Invoke(new Action(() => ToggleEnabled(ctrl, tog)));
				return;
			}

			ctrl.Enabled = togg;

		}

		private void ToggleVisible(Control ctrl, int tog = -1)
		{
			bool togg = false;

			if (tog == -1)
			{

				if (ctrl.Visible == false)
					togg = true;

			}
			else if (tog == 1)
			{
				togg = true;
			}

			if (this.InvokeRequired)
			{
				this.Invoke(new Action(() => ToggleVisible(ctrl, tog)));
				return;
			}

			ctrl.Visible = togg;

		}


		void mp3Funcs_MusicUpdate()
		{
			Console.WriteLine("MUSIC UPDATE");

			//SetText(lbl_nowPlaying, mp3Funcs.nowPlaying);

		}
		
		void SteamDisconnect()
		{
			//Console.WriteLine("STEAM DISCONNECT");
			SetText(lbl_steamStatus, "Disconnected");
			ToggleVisible(lbl_steamFriends, 0);

			SetText(btn_steamConnect, "Connect");
			ToggleEnabled(btn_steamConnect, 1);
		}

		void SteamConnect()
		{
			//Console.WriteLine("STEAM CONNECT");

			SetText(lbl_steamStatus, "Connected");
			ToggleVisible(lbl_steamFriends, 1);

			SetText(btn_steamConnect, "Disconnect");
			ToggleEnabled(btn_steamConnect, 1);
		}

		void steamFuncs_SteamDisconnect()
		{
			SteamDisconnect();
		}

		void steamFuncs_SteamConnect()
		{
			SteamConnect();
		}


		void TS3Connect()
		{
			SetText(lbl_ts3Status, "Connected");
			ToggleVisible(lbl_ts3Users, 1);

			SetText(btn_ts3Connect, "Disconnect");
			ToggleEnabled(btn_ts3Connect, 1);
		}

		void TS3Disconnect()
		{
			SetText(lbl_ts3Status, "Disconnected");
			ToggleVisible(lbl_ts3Users, 0);

			SetText(btn_ts3Connect, "Connect");
			ToggleEnabled(btn_ts3Connect, 1);
		}

		void tsFuncs_TS3Update()
		{
			SetText(lbl_ts3Users, "" + tsFuncs.currUsers.ToString() + " / " + tsFuncs.maxUsers.ToString() + " Users Online");
		}

		void tsFuncs_TS3Disconnect()
		{
			TS3Disconnect();
		}

		void tsFuncs_TS3Connect()
		{
			TS3Connect();
		}

		private void steamFuncs_FriendsUpdate()
		{
			SetText(lbl_steamFriends, steamFuncs.numOnFriends.ToString() + " / " + steamFuncs.numFriends.ToString() + " Friends Online");
		}

		private void mp3Funcs_ScanMusicUpdate()
		{
			//Console.WriteLine("UPDATED: {0}", mp3Funcs.musicList.Length);

			SetText(lbl_songs, mp3Funcs.musicList.Length.ToString() + " Songs");
		}

		void mp3Funcs_ScanMusicStart()
		{
			SetText(lbl_songs, "Loading Music...");
		}


		private void soundFuncs_ScanSoundUpdate()
		{
			Console.WriteLine("UPDATED: {0}", soundFuncs.sqlSounds.Rows.Count);

			lock (lbl_soundClips.Text)
			{
				SetText(lbl_soundClips, soundFuncs.sqlSounds.Rows.Count.ToString() + " Sound Clips");
			}
			
		}


		void soundFuncs_ScanSoundStart()
		{
			SetText(lbl_soundClips, "Loading Sound Clips...");
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			soundFuncs.disposeWave2();
			mp3Funcs.disposeWave();
			ttsFuncs.disposeTTSWav();
			//Console.WriteLine("DISPOSED TTS WAV");

			Environment.Exit(Environment.ExitCode);
		}

		private void btn_steamConnect_Click(object sender, EventArgs e)
		{
			//Console.WriteLine("CONNECTED: {0}", steamFuncs.isConnected);

			btn_steamConnect.Enabled = false;

			if (steamFuncs.isConnected)
			{
				//Disconnect.
				steamFuncs.DoDisconnect();
				//SteamDisconnect();
			}
			else
			{
				//Connect.
				steamFuncs.DoConnect();
				//SteamConnect();
			}

		}

		private void btn_ts3Connect_Click(object sender, EventArgs e)
		{

			if (tsFuncs.IsConnected == true)
			{
				tsFuncs.Disconnect();
			}
			else { 
				tsFuncs.Connect();
			}

			ToggleEnabled(btn_ts3Connect);

		}

		private void btn_logToggle_Click(object sender, EventArgs e)
		{
			//Size with log = 572, 484
			//Size without log = 572, 262

			if (this.Height > 299)
			{
				this.Size = new Size(572, 299);
				btn_logToggle.Text = "Show Logs";
			}
			else
			{
				this.Size = new Size(572, 522);
				btn_logToggle.Text = "Hide Logs";

				
			}
			

		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new IceBot.About();
			frm.Show();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var frm = new IceBot.SoundManager();
			frm.Show();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var frm = new Settings();
			frm.Show();
		}

		private void btn_tsSettings_Click(object sender, EventArgs e)
		{
			var frm = new TSSettings();
			frm.Show();
		}

		private void btn_steamSettings_Click(object sender, EventArgs e)
		{
			var frm = new SteamSettings();
			frm.Show();
		}

		private void btn_admSettings_Click(object sender, EventArgs e)
		{
			var frm = new Admin();
			frm.Show();
		}



		private void btn_Minimize_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void btn_Close_Click(object sender, EventArgs e)
		{
			this.Close();

		}


    }

}