using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Data.SQLite;

using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using Microsoft.Win32;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;

using HundredMilesSoftware.UltraID3Lib;
using System.Globalization;
using System.Threading;

using PortableSteam;
using PortableSteam.Interfaces;
using PortableSteam.Fluent;
using PortableSteam.Infrastructure;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SteamKit2;
using SteamKit2.GC;

using TS3QueryLib.Core;
using TS3QueryLib.Core.Communication;
using TS3QueryLib.Core.Common;
using TS3QueryLib.Core.Common.Responses;
using TS3QueryLib.Core.Server;
using TS3QueryLib.Core.Server.Entities;
using TS3QueryLib.Core.Server.Responses;
using System.Net.Sockets;

using Dota2WebAPISDK;
using YoutubeExtractor;

using MySql.Data.MySqlClient;
using IniParser;

using SharpTalk;
using HtmlAgilityPack;
using System.Data;
using System.Diagnostics;


namespace IceBot
{
	public delegate void EventHandler();
	

	#region "Log Funcs"

	public class logFuncs
	{
		
		public static string[] logs = new string[0];

		public static event EventHandler LogUpdate;

		public void AddToLogs(string str, string prefix)
		{

			Array.Resize(ref logs, logs.Length + 1);
			int len = logs.Length - 1;
			logs[len] = "[" + prefix + "] -- " + str;

			LogUpdate.Invoke();
		}

	}

	#endregion


	#region "SQLite"
	public class SQLiteDatabase
	{
		String dbConnection;
		private static IceBot.logFuncs logFuncs = new IceBot.logFuncs();


		/// <summary>
		///     Default Constructor for SQLiteDatabase Class.
		/// </summary>
		public SQLiteDatabase()
		{
			dbConnection = "Data Source=data.s3db";
		}

		/// <summary>
		///     Single Param Constructor for specifying the DB file.
		/// </summary>
		/// <param name="inputFile">The File containing the DB</param>
		public SQLiteDatabase(String inputFile)
		{
			dbConnection = String.Format("Data Source={0}", inputFile);
		}

		/// <summary>
		///     Single Param Constructor for specifying advanced connection options.
		/// </summary>
		/// <param name="connectionOpts">A dictionary containing all desired options and their values</param>
		public SQLiteDatabase(Dictionary<String, String> connectionOpts)
		{
			String str = "";
			foreach (KeyValuePair<String, String> row in connectionOpts)
			{
				str += String.Format("{0}={1}; ", row.Key, row.Value);
			}
			str = str.Trim().Substring(0, str.Length - 1);
			dbConnection = str;
		}

		/// <summary>
		///     Allows the programmer to run a query against the Database.
		/// </summary>
		/// <param name="sql">The SQL to run</param>
		/// <returns>A DataTable containing the result set.</returns>
		public DataTable GetDataTable(string sql)
		{
			DataTable dt = new DataTable();
			try
			{
				SQLiteConnection cnn = new SQLiteConnection(dbConnection);
				cnn.Open();
				SQLiteCommand mycommand = new SQLiteCommand(cnn);
				mycommand.CommandText = sql;
				SQLiteDataReader reader = mycommand.ExecuteReader();
				dt.Load(reader);
				reader.Close();
				cnn.Close();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return dt;
		}

		/// <summary>
		///     Allows the programmer to interact with the database for purposes other than a query.
		/// </summary>
		/// <param name="sql">The SQL to be run.</param>
		/// <returns>An Integer containing the number of rows updated.</returns>
		public int ExecuteNonQuery(string sql)
		{
			SQLiteConnection cnn = new SQLiteConnection(dbConnection);

			cnn.Open();

			SQLiteCommand mycommand = new SQLiteCommand(cnn);
			mycommand.CommandText = sql;

			int rowsUpdated = mycommand.ExecuteNonQuery();

			cnn.Close();

			return rowsUpdated;
		}

		/// <summary>
		///     Allows the programmer to retrieve single items from the DB.
		/// </summary>
		/// <param name="sql">The query to run.</param>
		/// <returns>A string.</returns>
		public string ExecuteScalar(string sql)
		{
			SQLiteConnection cnn = new SQLiteConnection(dbConnection);
			cnn.Open();
			SQLiteCommand mycommand = new SQLiteCommand(cnn);
			mycommand.CommandText = sql;
			object value = mycommand.ExecuteScalar();
			cnn.Close();
			if (value != null)
			{
				return value.ToString();
			}
			return "";
		}

		/// <summary>
		///     Allows the programmer to easily update rows in the DB.
		/// </summary>
		/// <param name="tableName">The table to update.</param>
		/// <param name="data">A dictionary containing Column names and their new values.</param>
		/// <param name="where">The where clause for the update statement.</param>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public bool Update(String tableName, Dictionary<String, String> data, String where)
		{
			String vals = "";
			Boolean returnCode = true;
			if (data.Count >= 1)
			{
				foreach (KeyValuePair<String, String> val in data)
				{
					vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
				}
				vals = vals.Substring(0, vals.Length - 1);
			}
			try
			{
				this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
			}
			catch
			{
				returnCode = false;
			}
			return returnCode;
		}

		/// <summary>
		///     Allows the programmer to easily delete rows from the DB.
		/// </summary>
		/// <param name="tableName">The table from which to delete.</param>
		/// <param name="where">The where clause for the delete.</param>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public bool Delete(String tableName, String where)
		{
			Boolean returnCode = true;
			try
			{
				this.ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
			}
			catch (Exception fail)
			{
				logFuncs.AddToLogs(fail.Message.ToString(), "CRITICAL");
				returnCode = false;
			}
			return returnCode;
		}

		/// <summary>
		///     Allows the programmer to easily insert into the DB
		/// </summary>
		/// <param name="tableName">The table into which we insert the data.</param>
		/// <param name="data">A dictionary containing the column names and data for the insert.</param>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public bool Insert(String tableName, Dictionary<String, String> data)
		{
			String columns = "";
			String values = "";
			Boolean returnCode = true;
			foreach (KeyValuePair<String, String> val in data)
			{
				columns += String.Format(" {0},", val.Key.ToString());
				values += String.Format(" '{0}',", val.Value);
			}
			columns = columns.Substring(0, columns.Length - 1);
			values = values.Substring(0, values.Length - 1);
			try
			{
				this.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
			}
			catch (Exception fail)
			{
				logFuncs.AddToLogs(fail.Message.ToString(), "CRITICAL");
				returnCode = false;
			}
			return returnCode;
		}


		public void MassInsert(String tableName, Dictionary<int, Dictionary<string, string>> data)
		{

			using (var conn = new SQLiteConnection(dbConnection))
			{
				// Be sure you already created the Person Table!

				conn.Open();

				var stopwatch = new Stopwatch();
				stopwatch.Start();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{
						// 100,000 inserts
						Boolean returnCode = true;

						foreach(Dictionary<string,string> dict in data.Values)
						{
							String columns = "";
							String values = "";

							foreach (KeyValuePair<String, String> val in dict)
							{
								columns += String.Format(" {0},", val.Key.ToString());
								values += String.Format(" '{0}',", val.Value);
							}

							columns = columns.Substring(0, columns.Length - 1);
							values = values.Substring(0, values.Length - 1);

							//Console.WriteLine(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
							cmd.CommandText = String.Format("insert into {0}({1}) values({2});", tableName, columns, values);
							cmd.ExecuteNonQuery();
						}

						

						transaction.Commit();
					}
				}

				Console.WriteLine("{0} seconds with one transaction.", stopwatch.Elapsed.TotalSeconds);

				conn.Close();
			}

		}


		/// <summary>
		///     Allows the programmer to easily delete all data from the DB.
		/// </summary>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public bool ClearDB()
		{
			DataTable tables;
			try
			{
				tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
				foreach (DataRow table in tables.Rows)
				{
					this.ClearTable(table["NAME"].ToString());
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		///     Allows the user to easily clear all data from a specific table.
		/// </summary>
		/// <param name="table">The name of the table to clear.</param>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public bool ClearTable(String table)
		{
			try
			{

				this.ExecuteNonQuery(String.Format("delete from {0};", table));
				return true;
			}
			catch
			{
				return false;
			}
		}
	}

	#endregion


	#region "DB Connect"

	public class DBConnect
	{

		private MySqlConnection connection;
		private string server = IceBot.Properties.Settings.Default.sqlHostname;
		private string database = IceBot.Properties.Settings.Default.sqlDatabase;
		private string uid = IceBot.Properties.Settings.Default.sqlUser;
		private string password = IceBot.Properties.Settings.Default.sqlPassword;

		//Constructor
		public DBConnect()
		{
			Initialize();
		}

		//Initialize values
		private void Initialize()
		{
			string connectionString;
			connectionString = "SERVER=" + server + ";" + "DATABASE=" +
			database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

			connection = new MySqlConnection(connectionString);
		}


		//open connection to database
		private bool OpenConnection()
		{
			try
			{
				connection.Open();
				return true;
			}
			catch (MySqlException ex)
			{
				//When handling errors, you can your application's response based 
				//on the error number.
				//The two most common error numbers when connecting are as follows:
				//0: Cannot connect to server.
				//1045: Invalid user name and/or password.

				Console.WriteLine("SQL ERROR: {0}", ex.Number);

				switch (ex.Number)
				{
					case 0:
						//MessageBox.Show("Cannot connect to server.  Contact administrator");
						break;

					case 1045:
						//MessageBox.Show("Invalid username/password, please try again");
						break;
				}
				return false;
			}
		}

		//Close connection
		private bool CloseConnection()
		{
			try
			{
				connection.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("SQL CLOSE ERROR: {0}", ex.Message);

				//MessageBox.Show(ex.Message);
				return false;
			}
		}


		public string[][] Select()
		{
			string query = "SELECT * FROM `1_soundpacks`";

			//Create a list to store the result
			//List<string>[] list = new List<string>[3];
			//list[0] = new List<string>();
			//list[1] = new List<string>();
			//list[2] = new List<string>();

			string[][] list = new string[0][];


			//Open connection
			if (this.OpenConnection() == true)
			{
				//Create Command
				MySqlCommand cmd = new MySqlCommand(query, connection);
				//Create a data reader and Execute the command
				MySqlDataReader dataReader = cmd.ExecuteReader();

				//Read the data and store them in the list
				while (dataReader.Read())
				{
					int tmp = list.Length + 1;

					Array.Resize(ref list, tmp);
					tmp = tmp - 1;

					list[tmp] = new string[3];

					list[tmp][0] = dataReader["s_title"] + "";
					list[tmp][1] = dataReader["s_url"] + "";
					list[tmp][2] = dataReader["s_dateadded"] + "";
				}

				//close Data Reader
				dataReader.Close();

				//close Connection
				this.CloseConnection();

				//return list to be displayed
				return list;
			}
			else
			{
				return list;
			}
		}


	}

	#endregion

	#region "Global Funcs"

	public class gFuncs
	{
		public enum MsgType
		{
			TS3,
			Steam
		}

		public static bool IsOdd(int value)
		{
			return value % 2 != 0;
		}

		public void IsFirstRun()
		{

			RegistryKey rkSubKey = Registry.CurrentUser.OpenSubKey("Software\\IceBot", false);

			if (rkSubKey == null)
			{
				// It doesn't exist, which means it's the first run.
				var frm = new IceBot.FirstRun();

				frm.Show();

				//Create registry key.

			}

		}

		public string FirstLetterToUpper(string str)
		{
			if (str == null)
				return null;

			if (str.Length > 1)
				return char.ToUpper(str[0]) + str.Substring(1);

			return str.ToUpper();
		}

		public float formatVol(int volu)
		{
			float f = 0.0f;

			f = (float)volu / 100;

			return f;
		}


		public string ToTitleCase(string str)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
		}

		public string[] quoteArgs(string[] args)
		{
			string[] ret = { };

			int last = -1;
			string arg = "";
			int c = 0;

			foreach (string str in args)
			{
				//Console.WriteLine("STR2: {0}", str);

				if (str.StartsWith("\"") == true || str.EndsWith("\"") == true)
				{

					if (str.StartsWith("\"") == true && str.EndsWith("\"") == false)
					{
						arg = str;
						last = 0;
					}
					else if (str.EndsWith("\"") == true && str.StartsWith("\"") == false)
					{
						arg = arg + " " + str;

						arg = arg.Replace("\"", "");

						Array.Resize(ref ret, ret.Length + 1);
						ret[ret.Length-1] = arg;
						last = -1;

					} else if(str.StartsWith("\"") == true && str.EndsWith("\"") == true){
						//Console.WriteLine("")
						Array.Resize(ref ret, ret.Length + 1);
						string tmp = str.Replace("\"", "");

						ret[ret.Length - 1] = tmp;
						last = -1;
					}

				}
				else
				{
					if (last == -1)
					{
						// No quote.
						Array.Resize(ref ret, ret.Length + 1);
						ret[ret.Length-1] = str;
					}
					else
					{
						// Middle of a quoted argument.
						arg = arg + " " + str;
						last = 0;
					}

				}

			}

			return ret;
		}

		public void sendChatMsg(gFuncs.MsgType sender, string msg, string recip, string type = null, string color = null, string prefix = null, bool isError = false, TS3QueryLib.Core.Server.QueryRunner queryRun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (sender == gFuncs.MsgType.TS3) { 
			//msg = msg.Replace(" ", "\s")
			//Debug.Print("MSG: " + msg)
			//

				uint tmp;

				tmp = Convert.ToUInt32(recip);

				string pre = "[color=lightskyblue][b][IceBot][/b][/color] ";

				if (prefix != "" && prefix != null)
				{
					pre = pre + prefix + " ";
				}

				if (color != "" && color != null)
				{
					pre = pre + "[color=" + color + "]";
				}

				if (isError == true)
				{
					pre = pre + " [b]Error![/b] ";
				}

				msg = pre + msg + "[/color]";

				if (type == "private")
				{
					queryRun.SendTextMessage(TS3QueryLib.Core.CommandHandling.MessageTarget.Client, tmp, msg);
				}
				else if (type == "channel")
				{
					queryRun.SendTextMessage(TS3QueryLib.Core.CommandHandling.MessageTarget.Channel, tmp, msg);
				}
				else if (type == "server")
				{
					//bool chkS = queryRun.SelectVirtualServerByPort(10640).IsErroneous;


					//queryRun.SendTextMessage(TS3QueryLib.Core.CommandHandling.MessageTarget.Server, 1, msg);
				}

			}
			else
			{
				// Steam MSG;
				string tMsg = Regex.Replace(msg, @"\[[^]]+\]", "");

				//Console.WriteLine("MESSAGE: {0} :: {1}", steamID.IsChatAccount, steamID.AccountType);

				if (steamID.IsChatAccount == false) { 
					if (isError == true)
					{
						steamFriends.SendChatMessage(steamID, EChatEntryType.ChatMsg, "Error! " + tMsg);
					}
					else
					{
						steamFriends.SendChatMessage(steamID, EChatEntryType.ChatMsg, tMsg);
					}
				}
				else
				{
					if (isError == true)
					{
						steamFriends.SendChatRoomMessage(steamID, EChatEntryType.ChatMsg, "Error! " + tMsg);
					}
					else
					{
						steamFriends.SendChatRoomMessage(steamID, EChatEntryType.ChatMsg, tMsg);
					}
				}
			}
		}

		public string[] chatPage(SortedList<string, int> list, string title, string sep, string prefix, int limit = 550)
		{
			string[] ret = new string[0];

			string msg = "";
			string pre = "";

			if (prefix != null)
			{
				pre = prefix;
			}

			int ci = 0;

			foreach (string str in list.Keys){

				if (ci != list.Count() - 1) {
					
					if (msg.Length <= limit)
					{
						msg = msg + pre + str + sep;
					}
					else
					{
						msg = msg + pre+ str;

						int tmp = ret.Length + 1;
						//Console.WriteLine("TMP: {0}", tmp);
						Array.Resize(ref ret, tmp);
						ret[tmp-1] = msg;

						msg = "";
					}


				} else {
					msg = msg + pre + str;

					int tmp = ret.Length + 1;
					Array.Resize(ref ret, tmp);
					ret[tmp-1] = msg;

					msg = "";
				}

				ci += 1;
			} 


			//Add titles and notifications...
			ci = 1;

			foreach (string str in ret)
			{
				if (ci < ret.Length)
				{
					int npg = ci + 1;
					ret[ci - 1] = title + " -- Page [b]" + ci.ToString() +"[/b] of [b]" + ret.Length.ToString() + "[/b] -- \n" + str + "\n\n[color=green]And more on [b]Page " + npg.ToString() + "[/b][/color]";
				}
				else
				{
					ret[ci - 1] = title + " -- Page [b]" + ci.ToString() + "[/b] of [b]" + ret.Length.ToString() + "[/b] -- \n" + str;
				}

				ci += 1;
			}


			return ret;
		}

		public string[] chatPageByTimestamp(SortedList<double, string> list, string title, string sep, string prefix, int limit = 550)
		{
			string[] ret = new string[0];

			string msg = "";
			string pre = "";

			if (prefix != null)
			{
				pre = prefix;
			}

			int ci = 0;

			//list.Reverse();

			foreach (string str in list.Values)
			{

				if (ci != list.Count() - 1)
				{

					if (msg.Length <= limit)
					{
						msg = msg + pre + str + " - " + timeAgo(list.Keys[ci]) + sep;
					}
					else
					{
						msg = msg + pre + str + " - " + timeAgo(list.Keys[ci]);

						int tmp = ret.Length + 1;
						//Console.WriteLine("TMP: {0}", tmp);
						Array.Resize(ref ret, tmp);
						ret[tmp - 1] = msg;

						msg = "";
					}


				}
				else
				{
					msg = msg + pre + str + " - " + timeAgo(list.Keys[ci]);

					int tmp = ret.Length + 1;
					Array.Resize(ref ret, tmp);
					ret[tmp - 1] = msg;

					msg = "";
				}

				ci += 1;
			}


			//Add titles and notifications...
			ci = 1;

			foreach (string str in ret)
			{
				if (ci < ret.Length)
				{
					int npg = ci + 1;
					ret[ci - 1] = title + " -- Page [b]" + ci.ToString() + "[/b] of [b]" + ret.Length.ToString() + "[/b] -- \n" + str + "\n\n[color=green]And more on [b]Page " + npg.ToString() + "[/b][/color]";
				}
				else
				{
					ret[ci - 1] = title + " -- Page [b]" + ci.ToString() + "[/b] of [b]" + ret.Length.ToString() + "[/b] -- \n" + str;
				}

				ci += 1;
			}


			return ret;
		}

		public string[] chatPageByInt(SortedList<int, string> list, string title, string sep, string prefix, int limit = 550, bool desc = false)
		{
			string[] ret = new string[0];

			string msg = "";
			string pre = "";

			if (prefix != null)
			{
				pre = prefix;
			}

			int ci = 0;

			if (desc == true)
			{
				list.Reverse();
			}

			foreach (int i in list.Keys)
			{

				if (ci != list.Count() - 1)
				{

					if (msg.Length <= limit)
					{
						msg = msg + pre + list.Values[ci] + sep;
					}
					else
					{
						msg = msg + pre + list.Values[ci];

						int tmp = ret.Length + 1;
						//Console.WriteLine("TMP: {0}", tmp);
						Array.Resize(ref ret, tmp);
						ret[tmp - 1] = msg;

						msg = "";
					}


				}
				else
				{
					msg = msg + pre + list.Values[ci];

					int tmp = ret.Length + 1;
					Array.Resize(ref ret, tmp);
					ret[tmp - 1] = msg;

					msg = "";
				}

				ci += 1;
			}


			//Add titles and notifications...
			ci = 1;

			foreach (string str in ret)
			{
				if (ci < ret.Length)
				{
					int npg = ci + 1;
					ret[ci - 1] = title + " -- Page [b]" + ci.ToString() + "[/b] of [b]" + ret.Length.ToString() + "[/b] -- \n" + str + "\n\n[color=green]And more on [b]Page " + npg.ToString() + "[/b][/color]";
				}
				else
				{
					ret[ci - 1] = title + " -- Page [b]" + ci.ToString() + "[/b] of [b]" + ret.Length.ToString() + "[/b] -- \n" + str;
				}

				ci += 1;
			}


			return ret;
		}

		public string timeAgo(double _secs)
		{
			string ret = "";

			TimeSpan timeNow;

			timeNow = DateTime.Now.Subtract(new DateTime(1970, 1, 9, 0, 0, 00));

			double tmp = timeNow.TotalSeconds - _secs;

			int totSecs = Convert.ToInt32(tmp);

			TimeSpan t = TimeSpan.FromSeconds(totSecs);

			//Console.WriteLine("{0} SECONDS AGO", totSecs);

			string answer = string.Format("{0}m {1}s", Math.Floor(t.TotalMinutes), t.Seconds);

			ret = "" + answer + " ago";

			return ret;
		}

		public int GetDeviceNumber(string name)
		{
			int ret = -1;

			int waveOutDevices = WaveOut.DeviceCount;
			for (int waveOutDevice = 0; waveOutDevice < waveOutDevices; waveOutDevice++)
			{
				WaveOutCapabilities deviceInfo = WaveOut.GetCapabilities(waveOutDevice);

				if (deviceInfo.ProductName == name)
				{
					ret = waveOutDevice;
				}

			}

			return ret;
		}

		public int CheckIfPage(string str)
		{
			int pg = -1;

			try
			{
				pg = Convert.ToInt32(str);
			}
			catch
			{

			}

			return pg;
		}


		private string AddChars(string chars, int count)
		{
			int i = 0;
			string ret = "";

			while (i <= count)
			{
				ret = ret + chars;

				i += 1;
			}

			return ret;
		}


		public string NeatFormat(string[] headers, Dictionary<int, string[]> data, bool headBool = false)
		{
			string[] tmpH = new string[5] { "Name", "Hero", "KDA", "GPM", "XPM" };
			Dictionary<int, string[]> tmpD = new Dictionary<int,string[]>();

			tmpD.Add(0, new string[5] { "AJSD", "Lifestaler", "5/5/5", "1", "1" });
			tmpD.Add(1, new string[5] { "ASSSHUHUHU", "Riki", "51/5/5", "11", "123" });
			tmpD.Add(2, new string[5] { "HUHUHUASDFDFSA ASDF ", "Lifestaler", "5/5/5", "234", "234" });
			tmpD.Add(3, new string[5] { "UIIUOIOUOI", "Shadow Fiend", "5/50/5", "122", "424" });
			tmpD.Add(4, new string[5] { "AS", "Lifestaler", "5/5/50", "2331", "1111" });
			tmpD.Add(5, new string[5] { "OOOOPPP", "Anti-Mage", "50/50/50", "333", "666" });

			headers = tmpH;
			data = tmpD;

			Console.WriteLine("# Headers: {0} #", headers.Length);

			int[] sep = new int[0];
			int[] currMax = new int[headers.Length];

			//int maxLen = 0;

			foreach (KeyValuePair<int, string[]> str in data)
			{
				//Console.WriteLine("# {0} # {1} # {2} # {3} # {4} #", str.Value[0], str.Value[1], str.Value[2], str.Value[3], str.Value[4]);
				int i = 0;

				foreach (string s in str.Value)
				{
					//Console.WriteLine("{0}", str.Value[i].Length);

					if (currMax[i] == 0)
					{
						currMax[i] = str.Value[i].Length;

						//Console.WriteLine("NEW CURR MAX: {0} :: {1} # {2}", i, currMax[i], str.Value[i].Length);
					}
					else
					{
						//Not 0, compare.

						if (str.Value[i].Length > currMax[i])
						{
							//Console.WriteLine("NEW CURR MAX: {0} :: {1} # {2}", i, currMax[i], str.Value[i].Length);
							currMax[i] = str.Value[i].Length;
						}

					}

					i += 1;
				}

			}

			int maxLen = 0;

			foreach (int ind in currMax)
			{
				//Console.WriteLine("MAX: {0}", ind);

				maxLen = maxLen + ind;
			}


			string ret = "\n####################################\n# ";

			if (headBool == false) { 
				int di = 0;

				foreach (string head in headers)
				{
					int dif = currMax[di] - head.Length;

					Console.WriteLine("DIFF: {0}", dif);
					ret = ret + " " + head + AddChars(" ", dif) + "#";
					di += 1;
				}
			}

			ret = ret + "\n# ";

			foreach (KeyValuePair<int, string[]> str in data)
			{
				int i = 0;

				foreach (string s in str.Value)
				{
					//Console.WriteLine("{0} :: {1}", s.Length, s, currMax[i]);

					int dif = currMax[i] - s.Length;

					//Console.WriteLine("DIFFERENCE: {0}", dif);

					ret = ret + s + AddChars(" ", dif + 1) + "# ";


					i += 1;
				}

				if (str.Key != (data.Keys.Count-1))
				{
					Console.WriteLine("KEY: {0} :: {1}", str.Key, data.Keys.Count);

					ret = ret + "\n# ";
				}

			}
			ret = ret + "\n####################################";

			Console.WriteLine("RET: {0}", ret);

			return ret;

		}


		public int GetUnixTimestamp(DateTime when)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			TimeSpan span = (when - epoch);
			int unixTime = Convert.ToInt32(span.TotalSeconds);

			return unixTime;
		}

	}
	
	#endregion



	#region "Help Funcs"

	public class helpFuncs
	{
		private IceBot.gFuncs gFuncs = new IceBot.gFuncs();

		private static string[] helpParents = {"Sound", "Music", "Volume", "Last", "Help"};

		private static string[][] helpChild = new string[0][];

		private void AddChild(string name, string desc, string command, int parent)
		{
			// -- Music --
			//  -- List - Lists all music - .music list [artist] [*page*]

			int tmp = helpChild.Length;

			Array.Resize(ref helpChild, tmp+1);

			string par = (parent - 1).ToString();

			string[] tmpArr = new string[4] { name, desc, command, par };

			helpChild[tmp] = new string[4];

			helpChild[tmp] = tmpArr;

		}

		public void AddChildren()
		{
			AddChild("Volume (vol)", "Gets or sets volume.", ".vol [ 1-100 ]", 3);
			AddChild("Last", "Does last command.", ".last [ arg ]", 4);

			AddChild("Help (?)", "Shows help.", ".help [ category name ]", 5);

			AddChild("Sound (s)", "Plays a sound or used for sub-commands. -- Parent", ".sound [ soundname | sound1,sound2,sound3 | sub-command ]", 1);
			AddChild("List (l,v)", "List sound categories or sounds in a category.", ".sound list [ page | category name ] [ page ]", 1);
			AddChild("Search (s)", "Search for sounds.", ".sound search [ soundname ] ", 1);

			AddChild("Combo", "Allows easy sound combos", ".sound combo [ combo name / command ] ", 1);
			AddChild("Combo Save", "Save a combo for use later.", ".sound combo save [ name ] [ sound,sound,sound ]", 1);
			AddChild("Combo List", "Shows a list of your saved combos.", ".sound list [ page ]", 1);
			AddChild("Combo All", "Shows a list of all saved combos.", ".sound all [ page ]", 1);

			AddChild("Recent (r)", "Show a list of recently played sounds.", ".sound recent [ page ]", 1);
			AddChild("Queue (q)", "Shows the current queued sounds.", ".sound queue", 1);
			AddChild("New", "Shows newly added sounds.", ".sound new [ page ]", 1);
			AddChild("Pause (p)", "Pauses current sound.", ".sound pause", 1);
			AddChild("Stop", "Stops current sound and deletes queued sounds.", ".sound stop", 2);
			AddChild("Mode (m)", "", "Not working yet", 2);

			AddChild("Music", "Shows currently playing or used for sub-commands. -- Parent", ".music [ sub-command ]", 2);
			AddChild("Pause (p)", "Pauses current song.", ".music pause", 2);
			AddChild("Stop (s)", "Stops current song and removes it from queue.", ".music stop", 2);
			AddChild("Skip", "Skips current song and removes it from queue.", ".music skip", 2);
			AddChild("List (l,v)", "List music artists or tracks from an artist.", ".music list [ page | artist ] [ page ]", 2);
			AddChild("Queue (q)", "Shows queued tracks.", ".music queue [ page ]", 2);
			AddChild("Seek (g)", "Seek to a time in a track.", ".music seek [ Time Format (1:00) ]", 2);
			AddChild("Add (a)", "Adds a track to the queue.", ".music add [artist] [track]", 2);
			AddChild("Play (pl)", "Resumes the current queue or track.", ".music play", 2);
			AddChild("Type", "Change the type of music queue.", ".music type [ queue | shuffle ]", 2);

			//AddChild("List", "", "", 2);

		}

		//Show help categories when one is chosen output all commands/descriptions/usages.

		public string[] listHelp()
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string str in helpParents)
			{
				sort.Add(str, i);

				i += 1;
			}

			string[] res = gFuncs.chatPage(sort, " -- Help Categories", "  -  ", "", 200);

			return res;
		}

		public int searchHelp(string str)
		{
			int ret = -1;
			int i = 0;

			foreach (string par in helpParents)
			{
				string npar = par.ToLower();

				if (npar.Contains(str))
				{
					ret = i;
				}

				i += 1;
			}

			return ret;
		}

		public string[] showHelp(int parent)
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string[] str in helpChild)
			{
				//Console.WriteLine("PARENT: {0} :: {1}", Convert.ToInt32(str[3]), parent);

				if (Convert.ToInt32(str[3]) == parent)
				{
					//Console.WriteLine("STR2: {0}", str[2]);

					sort.Add("-- " + str[0] + "  -  " + str[1] + "\n	- Usage: " + str[2], i);
				}
				
			}
			//Console.WriteLine("RET: {0}", ret);

			string[] res = gFuncs.chatPage(sort, " == Help for '" + helpParents[parent] + "' ==", " - \n", "", 200);

			return res;
		}


	}

	#endregion

	#region "User Funcs"
	
	public class userFuncs
	{
		private IceBot.SQLiteDatabase sqlDB = new IceBot.SQLiteDatabase();
		private IceBot.gFuncs gFuncs = new IceBot.gFuncs();


		public void createTest(string acctID, gFuncs.MsgType msgType)
		{
			//var db = new SQLiteDatabase();

			//Dictionary<String, String> data = new Dictionary<String, String>();

			////Account ID
			//data.Add("u_acctid", acctID);

			////Username
			//if(msgType == gFuncs.MsgType.Steam)
			//{

			//} else {

			//}

			//data.Add("u_username", nameTextBox.Text);

			////Times
			//data.Add("u_created", nameTextBox.Text);
			//data.Add("u_lastlog", nameTextBox.Text);


			//try
			//{
			//	db.Insert("USERS", data);
			//}
			//catch (Exception crap)
			//{
			//	//MessageBox.Show(crap.Message);
			//}

		}

		public void selectTest()
		{
			Console.WriteLine("SELECT TEST");

			try
			{
				var db = new SQLiteDatabase();

				DataTable recipe;
				String query = "SELECT * from users;";

				recipe = db.GetDataTable(query);

				// The results can be directly applied to a DataGridView control
				//recipeDataGrid.DataSource = recipe;
				/*
				// Or looped through for some other reason
				 * 
				 */
				Console.WriteLine("ROWS: {0}", recipe.Rows.Count);

				foreach (DataRow r in recipe.Rows)
				{
					Console.WriteLine("{0}:{1} :: {2}", r["u_id"].ToString(), r["u_acctid"].ToString(), r["u_username"].ToString());
				}
	
				
			}
			catch (Exception fail)
			{
				String error = "The following error has occurred:\n\n";
				error += fail.Message.ToString() + "\n\n";

				Console.WriteLine(error);

				//MessageBox.Show(error);
				//this.Close();
			}

		}


		private void CreateUser()
		{

		}

		private void PurgeUsers()
		{

		}

		private void GetUserBool()
		{

		}

		private void GetUserData()
		{

		}

	}

	#endregion

	#region "MP3 Funcs"

	public class mp3Funcs
	{
		public static WaveOut waveOut;
		public static Mp3FileReader audioReader;

		private IceBot.gFuncs gF = new IceBot.gFuncs();
		private IceBot.logFuncs logFuncs = new IceBot.logFuncs();

		public static float mVol = 0.05f;

		public static List<int> mq = new List<int>();
		public static string[][] musicList = new string[0][];
		public static SortedList<string, int> musicArtists = new SortedList<string, int>();

		public static string nowPlaying;

		public Thread qThread;
		public System.Timers.Timer mTimer = new System.Timers.Timer(1000);
		private System.Timers.Timer scanTimer = new System.Timers.Timer(500);

		public static event EventHandler ScanMusicUpdate;
		public static event EventHandler MusicUpdate;
		public static event EventHandler ScanMusicStart;

		public Thread scanThread;

		public static MusicType mt = MusicType.Queue;

		public static bool isRunning = false;
		public TimeSpan currTime;
		public int lastSong = -1;

		private static bool isScanning = false;

		public enum MusicType
		{
			Queue,
			Shuffle
		}

		public void StartTimer()
		{
			mTimer.Elapsed += mTimer_Elapsed;
			mTimer.Interval = 1000;
			mTimer.Enabled = true;
			mTimer.Start();
		}

		public void StartScanTimer()
		{
			scanTimer.Elapsed += scanTimer_Elapsed;
			scanTimer.Interval = 1000;
			scanTimer.Enabled = true;
			scanTimer.Start();
		}

		void mTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{

			if (waveOut != null)
			{
				if (audioReader != null)
				{
					TimeSpan currTime = audioReader.CurrentTime;
					TimeSpan totalTime = audioReader.TotalTime;

					//Console.WriteLine("CURR: {0}:{1} - {2}:{3}", currTime.Minutes, currTime.Seconds, totalTime.Minutes, totalTime.Seconds);

					if (currTime.Minutes == totalTime.Minutes && currTime.Seconds == totalTime.Seconds)
					{
						Console.WriteLine("MUSIC :: NEXT SONG/SKIP");
						Skip();
						
					}

				}
			}

		}


		void scanTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{

			if(!isScanning){
				Console.WriteLine("DONE INVOKE");
				scanTimer.Stop();
				ScanMusicUpdate.Invoke();
			}
			

		}

		public void PlayMp3(int ID)
		{
			Console.WriteLine("PLAY MP3");

			disposeWave();
			
			waveOut = new WaveOut();

			int devNum = gF.GetDeviceNumber(Properties.Settings.Default.outputdevice);

			if (devNum != -1)
			{

				waveOut.DeviceNumber = devNum;

				Console.WriteLine("MUSIC INFO: {0} - {1}", musicList[ID][1], musicList[ID][2]);

				//Console.WriteLine("{0} : {1}", wavOut.DeviceNumber, fileName);

				audioReader = new Mp3FileReader(musicList[ID][3]);

				waveOut.Init(audioReader);
				waveOut.DesiredLatency = 100;

				waveOut.Volume = mVol;

				waveOut.PlaybackStopped += waveOut_PlaybackStopped;

				waveOut.Play();

				isRunning = true;

				nowPlaying = musicList[ID][1] + " - " + musicList[ID][2];
				MusicUpdate.Invoke();

				Console.WriteLine("PLAYED");

			}

			
		}

		private void waveOut_PlaybackStopped(object sender, StoppedEventArgs e)
		{
			//Console.WriteLine("MUSIC STOPPED: {0}", isRunning);

			//if (isRunning == true)
			//{
			//	Skip();
			//}
			//else
			//{

			//}

		}

		public bool ArtistExists(string art)
		{
			bool ret = false;

			foreach (string str in musicArtists.Keys)
			{
				if (str.ToLower() == art.ToLower())
				{
					ret = true;
				}
			}

			return ret;
		}

		public bool ArtistExistsLoose(string art)
		{
			bool ret = false;

			foreach (string str in musicArtists.Keys)
			{
				if (str.ToLower().Contains(art.ToLower()))
				{
					ret = true;
				}
			}

			return ret;
		}


		public string[] searchArtistTracks(string artist, string title)
		{
			string[] ret = new string[2];

			int matches = 0;

			Console.WriteLine("SEARCHING FOR {0} - {1}", artist, title);

			foreach (string[] str in musicList)
			{

				if (str[1].ToLower().Contains(artist.ToLower()) && str[2].ToLower().Contains(title.ToLower()))
				{
					//Console.WriteLine("MATCH: {0} - {1}", str[1], str[2]);
					int tmp = Convert.ToInt32(str[0]);
					tmp = tmp - 1;

					ret[1] = tmp.ToString();

					matches += 1;
				}

			}

			ret[0] = matches.ToString();


			return ret;
		}

		public string[] listArtists()
		{

			//var sortedList = musicArtists.OrderBy(x => x).ToList();

			Console.WriteLine("LIST ARTISTS: {0} : {1}", musicArtists.Count, musicList.Length);

			string[] res = gF.chatPage(musicArtists, "Artists", "   --   ", null, 300);

			return res;
		}

		public string[] listTracks(string artist)
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			string art = "";
			int i = 0;

			foreach (string[] str in musicList){

				if (str[1].ToLower().Contains(artist))
				{
					string trackDisp = " " + str[0] + ". [b]" + str[1] + " - [i]" + str[2] + "[/i][/b]  ( " + str[4] + " )";

					//Console.WriteLine("TRUE {0}", str[1]);
					if (sort.ContainsKey(trackDisp) == false) { 
						sort.Add(trackDisp, i);
						art = str[1];

						i += 1;
					} else {
						sort.Add(" " + str[0] + ". [b]" + str[1] + " - [i]" + str[2] + " [/i][/b]  ( " + str[4] + " )", i);
						art = str[1];

						i += 1;
					}
					//Console.WriteLine("TRACKDISP {0}", trackDisp);

				}

			}

			string[] res = gF.chatPage(sort, "Tracks from [b]" + art + "[/b]", "\n", " -- ", 300);

			return res;
		}


		public void checkQueue()
		{

			if(mq.Count > 0)
			{
				//Console.WriteLine("WAITING... {0}", mq.Count);
				// Play next song.
				//Console.WriteLine("SONGS IN QUEUE: {0} : {1}", mq[0], lastSong);
				//Console.WriteLine("QUEUE PLAY FUNC");
				Play();
				//Console.WriteLine("PLAYING");
			}
			else
			{
				disposeWave();
			}

		}

		public void AddRandom()
		{
			Random random = new Random(DateTime.UtcNow.Millisecond);

			//Console.WriteLine("RANDOM: {0}", DateTime.UtcNow.Millisecond);

			int rand = RandomNumber(random, 1, musicList.Length);

			mq.Add(rand - 1);

		}

		private int GetRandomTrack()
		{
			Random random = new Random(DateTime.Now.Second);

			return RandomNumber(random, 1, musicList.Length)-1;
		}

		static int RandomNumber(Random random, int min, int max)
		{
			return random.Next(min, max);
		}


		public string formatTime(TimeSpan time)
		{
			string ret = "";
			string totsecs = ((int)time.Seconds).ToString();
			string totmins = ((int)time.Minutes).ToString();

			string mins = totmins;
			string secs = totsecs;


			if (totmins.Length == 1) { 
				mins = "0" + mins;
			}

			if (totsecs.Length == 1) { 
				secs = "0" + secs;
			}

			ret = mins + ":" + secs;


			return ret;
		}

		public int formatTimeToSecs(TimeSpan time)
		{
			int ret = -1;

			int mins = time.Minutes;
			int secs = time.Seconds;

			ret = (mins * 60) + secs;


			return ret;
		}

		public TimeSpan formatSecsToTime(int secs)
		{
			int mins = secs / 60;
			int sec = secs % 60;

			TimeSpan ret = new TimeSpan(0, mins, sec);

			Console.WriteLine("TIME: {0}:{1}", mins, sec);

			return ret;
		}

		public bool checkTime(string str)
		{
			bool ret = false;

			string[] strSep = new string[] { ":" };
			string[] spl = str.Split(strSep, StringSplitOptions.RemoveEmptyEntries);

			if (spl.Length > 1) { 
				int mins = -1;

				try
				{
					mins = Convert.ToInt32(spl[0]);
				}
				catch
				{

				}

				if (mins != -1) { 

					int secs = -1;

					try
					{
						secs = Convert.ToInt32(spl[0]);
					}
					catch
					{

					}

					if (secs != -1)
					{
						ret = true;
					}

				}

			}

			return ret;
		}

		public int timeToSecs(string str)
		{
			int ret = -1;

			string[] strSep = new string[] { ":" };
			string[] spl = str.Split(strSep, StringSplitOptions.RemoveEmptyEntries);

			int mins = Convert.ToInt32(spl[0]);

			ret = mins * 60;
			ret = ret + Convert.ToInt32(spl[1]);


			return ret;
		}

		public void Stop()
		{
			isRunning = false;

			//disposeWave();
		}

		public void Play()
		{
			Console.WriteLine("QUEUE: {0} : {1}", mq.Count, mq[0]);

			if (mq.Count > 0)
			{
				// If length is greater than 0, play first song.
				Console.WriteLine("PLAY FUNC PLAY");

				PlayMp3(mq[0]);
			}

				
		}

		public void Pause()
		{
			if (waveOut != null) { 
				if (waveOut.PlaybackState == PlaybackState.Playing)
				{
					waveOut.Pause();
				}
				else if (waveOut.PlaybackState == PlaybackState.Paused)
				{
					waveOut.Play();
				}
			}
		}

		public void Skip()
		{

			//If higher than or equal to 1, remove top song (Playing song)
			if (mq.Count >= 1)
			{
				mq.RemoveAt(0);
			}

			Console.WriteLine("MUSIC TYPE: {0}", mt);

			//If queue, do checkQUeue function.
			//If shuffle, if queue is 0, add a random song then do play.

			if (mt == MusicType.Queue)
			{
				//Console.WriteLine("CHECK QUEUE");

				checkQueue();

			}
			else if (mt == MusicType.Shuffle)
			{
				Console.WriteLine("SKIPPING SHUFFLE: {0}", mq.Count);

				if (mq.Count == 0)
				{
					Console.WriteLine("SKIP ADDING RANDOM");
					AddRandom();
				}

				Play();

			}

			//Stop();

			//Console.WriteLine("QUEUE COUNT: {0}", mq.Count);

		}

		public bool Seek(string time)
		{
			//audioReader.
			bool ret = false;

			if (audioReader != null) { 
				int tim = timeToSecs(time);

				Console.WriteLine("SEEK: {0}", tim);
				TimeSpan t = formatSecsToTime(tim);


				if (audioReader.CanSeek)
				{
					//audioReader.CurrentTime 

					audioReader.CurrentTime = t;

				}

			}

			return ret;
		}

		public bool Jump(string type, int secs)
		{
			bool ret = false;

			if (audioReader.CanSeek)
			{
				if (type == "ff")
				{
					audioReader.Skip(secs);

					ret = true;
				}
				else
				{
					secs = secs * -1;
					audioReader.Skip(secs);

					ret = true;
				}
			}

			return ret;
		}

		public string NowPlaying()
		{
			string ret = "== Now Playing ==";

			if (mq.Count > 0) {

				if (waveOut != null) { 
					if (waveOut.PlaybackState != PlaybackState.Stopped)
					{
						string state = "[color=green][b]== Playing ==[/b][/color]";

						if (waveOut.PlaybackState == PlaybackState.Paused)
						{
							state = "[color=red][b]== Paused ==[/b][/color]";
						}

						int id = mq[0] + 1;

						ret = ret + "\n" + state + "   [b]" + id.ToString() + ". " + musicList[mq[0]][1] + " - [i]" + musicList[mq[0]][2] + "[/i] ( " + formatTime(audioReader.CurrentTime) + " / " + formatTime(audioReader.TotalTime) + " )[/b]";

					}
					else
					{
						ret = ret + "\nNo track playing";
					}
				}
				else
				{
					ret = ret + "\nNo track playing";
				}

			}
			else
			{
				ret = ret + "\nNo track playing";
			}

			return ret;
		}

		public string[] ViewQueue(){
			string[] ret = {};

			if (mq.Count > 0)
			{
				SortedList<string, int> sort = new SortedList<string, int>();

				int i = 0;
				foreach (int ID in mq)
				{
					int tmp = i + 1;

					sort.Add(tmp.ToString() + ". " + musicList[ID][1] + " - " + musicList[ID][2], i);

					i += 1;
				}

				//Console.WriteLine("LINE");

				string[] res = gF.chatPage(sort, "== Queued Tracks ==", "\n", "- ", 300);

				ret = res;
			}

			return ret;
		}

		public void AddToQueue(int ID)
		{
			string[] str = null;

			try
			{
				str = musicList[ID];
			}
			finally
			{
				mq.Add(ID);

				
				if (mt == MusicType.Queue && mq.Count == 1)
				{
					// Play that shit.
					Console.WriteLine("PLAY THAT SHIT");

					if (waveOut == null)
					{
						Play();
					}

				}

			}



		}

		public void RemoveFromQueue()
		{

		}

		public void disposeWave()
		{
			isRunning = false;

			if (waveOut != null)
			{
				//Console.WriteLine("STOPPING");
				if (waveOut.PlaybackState == PlaybackState.Playing || waveOut.PlaybackState == PlaybackState.Paused) { 
					waveOut.Stop();
				}
				//Console.WriteLine("STOPPED");
			}

			if (audioReader != null)
			{
				//Console.WriteLine("DISPOSE AUDIOREADER");
				audioReader.Dispose();
				audioReader = null;
				//Console.WriteLine("DISPOSED");
			}

			if (waveOut != null)
			{
				//Console.WriteLine("DISPOSE WAVEOUT");
				waveOut.Dispose();
				waveOut = null;
				//Console.WriteLine("DISPOSED");
			}

			isRunning = true;

		}

		public void scanMusic()
		{
			//Console.WriteLine("Cnt: {0}", Properties.Settings.Default.musicdirs.Count);
			int i = 1;

			logFuncs.AddToLogs("'" + Properties.Settings.Default.musicdirs.Count + "' music directories.", "MUSIC");

			if (Properties.Settings.Default.musicdirs.Count > 0)
			{
				//Reset arrays.
				musicList = new string[0][];
				musicArtists = new SortedList<string, int>();

				ScanMusicStart.Invoke();

				foreach (string str in Properties.Settings.Default.musicdirs)
				{

					if (System.IO.Directory.Exists(str))
					{

						logFuncs.AddToLogs("Scanning directory '" + str + "'...", "MUSIC");

						String[] allfiles = System.IO.Directory.GetFiles(str, "*.mp3", System.IO.SearchOption.AllDirectories);

						foreach (string s in allfiles)
						{
							//Console.WriteLine("S: {0}" + s);

							//Get ID3 tags if they exist.
							UltraID3 u = new UltraID3();
							u.Read(s);

							string artist = "Unknown Artist";
							string title = "Unknown Title";
							string album = "None";

							if (u.Artist != "")
							{
								artist = u.Artist;
							}

							if (u.Title != "")
							{
								title = u.Title;
							}
							else
							{

								string stri = s.Replace(str + "\\", "");
								stri = stri.Replace(".mp3", "");

								title = stri;
							}

							if (u.Album != "")
							{
								album = u.Album;
							}

							// Got our Artist and Track, we can add more later if needed.

							//Console.WriteLine("I: {0}", musicList.Length);

							Array.Resize(ref musicList, musicList.Length + 1);

							//Console.WriteLine("I: {0}", musicList.Length);
							musicList[musicList.Length - 1] = new string[5];

							musicList[musicList.Length - 1][0] = i.ToString();
							musicList[musicList.Length - 1][1] = artist;
							musicList[musicList.Length - 1][2] = title;
							musicList[musicList.Length - 1][3] = s;
							musicList[musicList.Length - 1][4] = album;

							if (ArtistExists(artist) == false)
							{
								int tmp = musicArtists.Count;
								//Console.WriteLine("ADDING ARTIST {0}", artist);
								musicArtists.Add(artist, tmp + 1);
							}

							i += 1;
						}

					}
					else
					{
						logFuncs.AddToLogs("ERROR! Directory '" + str + "' does not exist.", "MUSIC");
					}


				}

			}
			else
			{
				//Reset arrays.
				musicList = new string[0][];
				musicArtists = new SortedList<string, int>();
			}

			logFuncs.AddToLogs("Completed scanning music! '" + musicList.Length + "' tracks by '" + musicArtists.Count + "' artists.", "MUSIC");

			Console.WriteLine("RELOADED: {0}", musicList.Length);
			isScanning = false;
		}


		public void doScanMusic()
		{
			Thread oThread = new Thread(new ThreadStart(scanMusic));

			// Start the thread
			oThread.IsBackground = true;
			oThread.Start();

			// Spin for a while waiting for the started thread to become
			// alive:
			while (!oThread.IsAlive) ;


			StartScanTimer();

			isScanning = true;

		}


	}
	
	#endregion

	#region "Sound Funcs"

	public class soundFuncs
	{

		//private IceBot.SQLiteDatabase sqlDB = new IceBot.SQLiteDatabase();
		private static SQLiteDatabase sqlData = new SQLiteDatabase();
		

		public static WaveOut wavOut;
		public static Mp3FileReader soundReader;
		public static float sVol = 0.05f;
		public static bool loop = false;

		public static SoundPlayback SoundType = SoundPlayback.Queue;

		public static List<string> soundQueue = new List<String>();
		public static SortedList<double, string> soundRecent = new SortedList<double, string>();

		public enum SoundPlayback 
		{ 
			Interrupt, 
			Queue
		}

		public static bool isScanning = false;

		private IceBot.gFuncs gFuncs = new IceBot.gFuncs();
		private IceBot.logFuncs logFuncs = new IceBot.logFuncs();

		public string fileName { get; set; }

		public System.Timers.Timer sTimer = new System.Timers.Timer(500);

		//public static Dictionary<string, int> g_soundList = new Dictionary<string, int>();
		//public static string[] g_soundCats = { };
		//public static string[] g_soundFiles = { };
		//public static string[] g_soundsNew = { };

		//New variables.

		DataTable sqlSounds;



		#region "Sound Combo Functions"

		public static IniData _soundCombos = new IniData();
		private IniParser.FileIniDataParser parser = new FileIniDataParser();

		public void LoadCombos()
		{
			////Create an instance of a ini file parser
			IniData parsedData = new IniData();

			try
			{
				parsedData = parser.LoadFile("scombos.ini");
			}
			catch
			{
				Console.WriteLine("ERROR PARSING COMBOS");

				//Create users.ini.

				File.Create("scombos.ini");
			}
			finally
			{
				Console.WriteLine("PARSED COMBOS: {0}", parsedData.Sections.Count);

				_soundCombos = parsedData;
			}



		}

		public void SaveCombos()
		{
			parser.SaveFile("scombos.ini", _soundCombos);
		}

		//public bool SearchComboUsers(string id)
		//{
		//	bool ret = false;

		//	if (_soundCombos.Sections.Count > 0) { 

		//		foreach (SectionData sect in _soundCombos.Sections)
		//		{

		//			if (sect.SectionName == id)
		//			{
		//				ret = true;
		//			}

		//		}

		//	}

		//	return ret;
		//}

		//public SectionData GetUserCombos(string id)
		//{
		//	SectionData ret = new SectionData("");

		//	foreach (SectionData sect in _soundCombos.Sections)
		//	{

		//		if (sect.SectionName == id)
		//		{
		//			ret = sect;
		//		}

		//	}

		//	return ret;
		//}

		public void AddCombo(string id, string name, string[] sounds)
		{

			_soundCombos.Sections.AddSection(name);
			_soundCombos[name].AddKey("id", id);
			_soundCombos[name].AddKey("sounds", String.Join(",", sounds));
			_soundCombos[name].AddKey("plays", "0");
			//userData[id].AddKey("lastUse", DateTime.UtcNow.ToUniversalTime);

			// Save
			SaveCombos();
		}


		public string[] listUserCombos(string id)
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (SectionData sect in _soundCombos.Sections)
			{
				//Console.WriteLine("STR: {0} : {1}", sect.Keys["id"], sect.Keys["plays"]);

				if (sect.Keys["id"] == id) {
					sort.Add(sect.SectionName + " [ " + sect.Keys["sounds"] + " ] - " + sect.Keys["plays"] + " plays -", i);
					i += 1;
				}

			}

			if (i == 0)
			{
				sort.Add("- No sound combos!", 0);
			}

			string[] res = gFuncs.chatPage(sort, " -- Your Sound Combos", " - \n", "", 200);

			return res;

		}

		public string[] listAllCombos()
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (SectionData sect in _soundCombos.Sections)
			{
				//Console.WriteLine("STR: {0} : {1}", sect.Keys["id"], sect.Keys["plays"]);
				sort.Add(sect.SectionName + " - ( " + sect.Keys["sounds"] + " ) - " + sect.Keys["plays"] + " plays", i);
				i += 1;
			}

			if (i == 0)
			{
				sort.Add("- No sound combos!", i);
			}

			string[] res = gFuncs.chatPage(sort, " -- All Sound Combos", " - \n", "", 200);

			return res;

		}

		//public string[] 

		public bool SearchCombos(string name)
		{
			bool ret = false;

			if (_soundCombos.Sections.Count > 0) {

				foreach (SectionData sect in _soundCombos.Sections)
				{

					if (sect.SectionName == name)
					{
						ret = true;
					}

				}

			}

			return ret;
		}

		public string TrimName(string name)
		{
			string ret = name;

			ret = ret.Trim();

			ret = ret.Replace(" ", "-");

			return ret;
		}

		public string TrimSounds(string[] sounds)
		{
			string ret = "";

			return ret;
		}

		public string[] ConvertToStringArr(string sounds)
		{
			string[] sepSounds = new string[0];

			if (sounds.Contains(","))
			{
				//Seperated sounds.
				Console.WriteLine("= SEPERATED SOUNDS =");

				string[] del = new string[] { "," };
				string[] spl;
				spl = sounds.Split(del, StringSplitOptions.None);

				foreach (string str in spl)
				{
					//Console.WriteLine("STR: {0}", str.Trim());
					Array.Resize(ref sepSounds, sepSounds.Length + 1);
					sepSounds[sepSounds.Length - 1] = str.Trim();
				}

			}

			return sepSounds;
		}

		#endregion


		#region "Sound Tags"
			private static IniData tagData = new IniData();
			private IniParser.FileIniDataParser tagParser = new FileIniDataParser();

			public void LoadTags()
			{
				////Create an instance of a ini file parser
				IniData parsedData = new IniData();

				try
				{
					parsedData = tagParser.LoadFile("tags.ini");
				}
				catch
				{
					Console.WriteLine("ERROR PARSING TAGS");

					File.Create("tags.ini");
				}
				finally
				{
					Console.WriteLine("PARSED SOUND TAGS: {0}", parsedData.Sections.Count);

					tagData = parsedData;
				}



			}

			public void CheckAndAdd(string id)
			{
				if (!CheckTags(id))
				{
					AddTags(id);
				}
			}

			public bool CheckTags(string id)
			{
				bool ret = false;

				if (tagData != null)
				{
					//Console.WriteLine("USERDATA NOT NULL :: {0}", userData.Sections.Count);

					//Iterate through all the sections
					foreach (SectionData section in tagData.Sections)
					{
						//Console.WriteLine("[" + section.SectionName + "]");

						if (section.SectionName == id)
						{
							ret = true;
						}

					}

				}

				return ret;
			}

			public void AddTags(string id)
			{
				//Console.WriteLine("ADD USER :: {0}", userData);

				tagData.Sections.AddSection(id);
				tagData[id].AddKey("tags", "");

				// Save
				SaveTags();
			}

			private void SaveTags()
			{
				tagParser.SaveFile("tags.ini", tagData);
			}

			
			

		#endregion


		public static event EventHandler ScanSoundUpdate;
		//public static event EventHandler ScanSoundError;
		public static event EventHandler ScanSoundStart;

		public void StartTimer()
		{
			sTimer.Enabled = true;
			sTimer.Elapsed += sTimer_Elapsed;
			sTimer.Start();
		}

		void sTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
			if (SoundType == SoundPlayback.Queue)
			{
				//Console.WriteLine("ELAPSED :: {0}", wavOut);

				if (wavOut != null)
				{

					if (soundReader != null) { 
						double curr = soundReader.CurrentTime.TotalSeconds;
						double total = soundReader.TotalTime.TotalSeconds;

						if (soundReader.CurrentTime == soundReader.TotalTime)
						{
							//Console.WriteLine("NEXT");
							Skip();
						}
					//Console.WriteLine("{0} :: {1}", soundReader.CurrentTime, soundReader.TotalTime);
					}
				}
				else
				{
					if (soundQueue.Count > 0)
					{
						//Play.
						playSound(soundQueue[0]);
					}
				}

			}

		}

		private void Skip()
		{
			soundQueue.RemoveAt(0);

			foreach(string l in soundQueue)
			{
				//Console.WriteLine("K: {0}  ::  V: 1", l);
			}


			if (soundQueue.Count > 0)
			{
				//Console.WriteLine("SOUNDS: {0} :: {1}", soundQueue.Count, soundQueue[0]);
				playSound(soundQueue[0]);

			}
			else
			{
				disposeWave2();

			}

		}

		public string[] splitSound(string str)
		{

			string[] strSep = new string[] { "\\" };
			string[] spl = str.Split(strSep, StringSplitOptions.RemoveEmptyEntries);
			spl[1] = spl[1].Replace(".mp3", "");
			spl[1] = spl[1].Replace("-", " ");

			return spl;
		}


		public string[] listSoundQueued()
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string str in soundQueue)
			{
				//Console.WriteLine("STR: {0} : {1}", str, g_soundFiles[str]);
				string[] spl = splitSound(str);

				sort.Add(spl[1], i);
				i += 1;
			}

			if (i == 0)
			{
				sort.Add("No queued sounds!", 0);
			}

			string[] res = gFuncs.chatPage(sort, " -- Queued Sounds", " - \n", "", 200);

			return res;
		}

		public string[] listSoundRecent()
		{
			string[] res = gFuncs.chatPageByTimestamp(soundRecent, " -- Recently Played Sounds", " - \n", "", 200);

			return res;
		}

		public int srchSoundMultiValid(string[] names)
		{
			int ret = -1;
			int fail = -1;

			foreach (string nameStr in names) {
				bool valid = false;

				foreach (string str in g_soundFiles)
				{
					string[] spl = splitSound(str);

					if (spl[1].ToLower() == nameStr.ToLower())
					{
						ret = 1;
						valid = true;
						Console.WriteLine("{0} FOUND", nameStr);
						break;
					}

				}

				if (valid == false)
				{
					ret = -1;
					fail = 1;
					Console.WriteLine("{0} NOT FOUND", nameStr);
				}

			}

			return fail;
		}

		public string srchSoundMulti(string[] names)
		{
			string ret = "Invalid Sounds: ";
			int i = 0;

			foreach (string nameStr in names)
			{
				bool valid = false;

				foreach (string str in g_soundFiles)
				{
					string[] spl = splitSound(str);

					if (spl[1].ToLower() == nameStr.ToLower())
					{
						valid = true;
						Console.WriteLine("{0} FOUND", nameStr);
						break;
					}

				}

				if (valid == false)
				{
					Console.WriteLine("{0} NOT FOUND", nameStr);
					if (i == 0)
					{
						ret = ret + nameStr;
					}
					else
					{
						ret = ret + ", " + nameStr;
					}
					
					i += 1;
				}

			}

			if (i == 0)
			{
				ret = "";
			}

			return ret;
		}

		public int[] srchSoundFileMulti(string[] names)
		{

			int[] ret = new int[0];
			
			//Console.WriteLine("SOUNDS: {0}", g_soundFiles.Length);
			foreach (string nameStr in names) {
				int i = 0;

				foreach (string str in g_soundFiles)
				{
					string[] spl = splitSound(str);

					if (spl[1].ToLower() == nameStr.ToLower())
					{
						Console.WriteLine("SPL: {0}, {1}, {2}", spl[1], nameStr, i);


						Array.Resize(ref ret, ret.Length + 1);
						ret[ret.Length - 1] = i;

						break;
					}

					i += 1;
				}

			}

			return ret;

		}

		public int srchSoundFile(string name)
		{
			int ret = -1;
			int i = 0;

			//Console.WriteLine("SOUNDS: {0}", g_soundFiles.Length);

			foreach (string str in g_soundFiles)
			{
				string[] spl = splitSound(str);

				if (spl[1].ToLower() == name.ToLower())
				{
					ret = i;
					break;
				}

				i += 1;
			}

			return ret;
		}

		public int srchSounds(string name)
		{
			int ret = -1;

			//Console.WriteLine("SRCH SOUNDS: {0}", g_soundList.Count);

			if (g_soundList.Count != 0)
			{
				int i = 0;

				foreach (string so in g_soundList.Keys)
				{

					//Console.WriteLine("TEST: {0}", so, name);

					if (name.ToLower() == so.ToLower())
					{
						Console.WriteLine("NAME: {0}:{1}", so, name);

						ret = i;
						break;
					}

					i += 1;
					//if (name.ToLower() == retStr[1].ToLower())
					//{
					//    Console.WriteLine("TRUE");
					//    return i;
					//}
				}
			}

			return ret;
		}


		public string formatSoundDir(string dir)
		{

			dir = dir.Replace("_", " ");
			dir = new IceBot.gFuncs().ToTitleCase(dir);

			return dir;
		}

		public int srchSoundCats(string catName)
		{
			int ret = -1;

			int i = 0;

			foreach (string str in g_soundCats)
			{

				if (str == catName)
				{
					ret = i;
					break;
				}

				i += 1;
			}

			return ret;
		}

		public int[] srchSoundCatsLoose(string catName)
		{
			int[] ret = new int[2];

			int i = 0;

			foreach (string str in g_soundCats)
			{

				if (str.ToLower().Contains(catName.ToLower()))
				{
					ret[1] = i;
					ret[0] += 1;

					break;
				}

				i += 1;
			}

			return ret;
		}


		public string[] searchSounds(string stri)
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string str in g_soundList.Keys)
			{
				//Console.WriteLine("STR: {0} : {1}", str, g_soundList[str]);

				if (str.ToLower().Contains(stri))
				{
					sort.Add(str, i);
					i += 1;
				}

			}

			if (i == 0)
			{
				sort.Add("No sounds found matching your search!", 0);
			}

			string[] res = gFuncs.chatPage(sort, " -- Search for [b]'" + stri + "'[/b]", "  -  ", "", 200);

			return res;

		}


		public string searchSimilar(string stri)
		{
			SortedList<string, int> sort = new SortedList<string, int>();
			string retStr = "";

			int i = 0;
			int gi = 0;

			foreach (string str in g_soundList.Keys)
			{
				//Console.WriteLine("STR: {0} : {1}", str, g_soundList[str]);

				if (str.ToLower().Contains(stri))
				{
					sort.Add(str, i);
					i += 1;
				}

			}
			int len = sort.Count;

			foreach (System.Collections.Generic.KeyValuePair<string,int> str in sort)
			{

				if (gi >= 0 && gi <= 4) { 
					if (gi == 0)
					{
						retStr = str.Key;
					} 
					else if(gi == len) 
					{
						retStr = retStr + str.Key;
					}
					else
					{
						retStr = retStr + ", " + str.Key;
					}
				}

				gi += 1;
			}

			return retStr;
		}

		public string[] GetSoundPacks()
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string str in g_soundCats)
			{
				sort.Add(str, i);

				i += 1;
			}

			string[] ret = new string[0];

			foreach (string str in sort.Keys)
			{
				//Console.WriteLine("STR: {0}", str);

				int tmp = ret.Length + 1;

				Array.Resize(ref ret, tmp);
				ret[tmp - 1] = str;

			}

			return ret;
		}

		public string[] GetSoundPacksDet()
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string str in g_soundCats)
			{
				sort.Add(str, i);

				i += 1;
			}

			string[] ret = new string[0];

			foreach (string str in sort.Keys)
			{
				//Console.WriteLine("STR: {0}", str);

				int tmp = ret.Length + 1;

				Array.Resize(ref ret, tmp);
				ret[tmp - 1] = str;

			}

			return ret;
		}

		public string[] listSoundCats()
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string str in g_soundCats)
			{
				//Console.WriteLine("STR: {0} : {1}", i, str);

				sort.Add(str, i);

				i += 1;
			}

			if (i == 0)
			{
				sort.Add("No sounds found!", 0);
			}


			string[] res = gFuncs.chatPage(sort, " -- Sound Categories", "  -  ", "", 200);

			return res;
		}

		public string[] listSounds(int cat)
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string str in g_soundList.Keys)
			{
				//Console.WriteLine("STR: {0} : {1}", str, g_soundList[str]);

				if (g_soundList[str] == cat)
				{
					sort.Add(str, i);
					i += 1;
				}

			}

			if (i == 0)
			{
				sort.Add("No sounds!", 0);
			}

			string[] res = gFuncs.chatPage(sort, " -- Sounds in [b]" + g_soundCats[cat] + "[/b]", "  -  ", "", 200);

			return res;

		}

		public string[] listNewSounds()
		{
			SortedList<string, int> sort = new SortedList<string, int>();

			int i = 0;

			foreach (string str in g_soundsNew)
			{
				sort.Add(str, i);
				i += 1;
			}

			if (i == 0)
			{
				sort.Add("No new sounds!", 0);
			}

			string[] res = gFuncs.chatPage(sort, " -- Newly added sounds", "  -  ", "", 200);

			return res;
		}



		public void newSrchSounds(string name)
		{

		}

		public void newInsertSounds()
		{
			sqlData.ClearTable("sounds");

			sqlData.ExecuteNonQuery("delete from sounds; delete from sqlite_sequence where name='sounds';");

			if (Directory.Exists(Environment.CurrentDirectory + "\\sounds\\"))
			{

				logFuncs.AddToLogs("Scanning sounds...", "SOUND");

				isScanning = true;

				ScanSoundStart.Invoke();

				String[] allfiles = System.IO.Directory.GetFiles(Environment.CurrentDirectory + "\\sounds\\", "*.mp3", System.IO.SearchOption.AllDirectories);

				int i = 0;

				Dictionary<int, Dictionary<string, string>> soundData = new Dictionary<int, Dictionary<string, string>>();

				foreach (string s in allfiles)
				{
					string str = s.Replace(Environment.CurrentDirectory + "\\sounds\\", "");

					string[] spl = splitSound(str);

					string formDir = formatSoundDir(spl[0]);

					try
					{
						int unixTime = gFuncs.GetUnixTimestamp(DateTime.Now);
						//Console.WriteLine("UNIX: {0}", unixTime);

						Dictionary<string, string> dict = new Dictionary<string, string>();
						dict.Add("snd_aliases", spl[1]);
						dict.Add("snd_filename", spl[0] + "/" + spl[1]);
						dict.Add("snd_dateadd", unixTime.ToString());
						dict.Add("snd_tags", formDir + ";");
						//dict.Add("snd_category", "1");

						//Console.WriteLine("Added '{0}'", spl[1]);

						//sqlData.Insert("sounds", dict);
						soundData.Add(i, dict);
					}
					catch (Exception ex)
					{
						Console.WriteLine("EX: {0}", ex.Message);
					}

					i += 1;

				}

				isScanning = false;

				ScanSoundUpdate.Invoke();

				Console.WriteLine("Sound Scan Complete! Inserting rows!");

				sqlData.MassInsert("sounds", soundData);
				//LoadCombos();
			}
			else
			{
				logFuncs.AddToLogs("ERROR! Could not find 'sounds' directory.", "SOUND");
				ScanSoundUpdate.Invoke();
			}



		}



		public void insertSounds()
		{
			//Console.WriteLine(Environment.CurrentDirectory);

			//logFuncs.AddToLogs("Sound directory found!", "SOUND");

			if (Directory.Exists(Environment.CurrentDirectory + "\\sounds\\")) {

				logFuncs.AddToLogs("Scanning sounds...", "SOUND");

				isScanning = true;

				ScanSoundStart.Invoke();

				//Console.WriteLine("SCANNING {0}", Environment.CurrentDirectory + "\\sounds\\");

				String[] allfiles = System.IO.Directory.GetFiles(Environment.CurrentDirectory + "\\sounds\\", "*.mp3", System.IO.SearchOption.AllDirectories);

				//Console.WriteLine("READING");


				int i = 0;
				int cat = 0;

				////Reset sound arrays.
				g_soundList = new Dictionary<string, int>();
				g_soundCats = new string[0];
				g_soundFiles = new string[0];
				g_soundsNew = new string[0];
				UltraID3 u = new UltraID3();

				Dictionary<int, Dictionary<string, string>> soundData = new Dictionary<int, Dictionary<string, string>>();

				foreach (string s in allfiles)
				{
					string str = s.Replace(Environment.CurrentDirectory + "\\sounds\\", "");

					string[] spl = splitSound(str);

					string formDir = formatSoundDir(spl[0]);

					//Console.WriteLine("DIR: {0} : {1}", spl[0], formDir);

					string art = null;

					//Format directory name and use that.
					//Console.WriteLine("FORMAT DIR NAME");

					art = formDir;

					if (srchSoundCats(art) == -1)
					{
						Array.Resize(ref g_soundCats, g_soundCats.Length + 1);
						g_soundCats[g_soundCats.Length - 1] = art;
						cat = g_soundCats.Length - 1;

						logFuncs.AddToLogs("Added '" + g_soundCats[cat] + "' category. Scanning now...", "SOUND");
					}

					int srch = srchSounds(spl[1]);

					g_soundList[spl[1]] = cat;

					Array.Resize(ref g_soundFiles, i + 1);
					g_soundFiles[i] = str;


					//Console.WriteLine("STR: {0}", s);

					//Find date created.
					DateTime created = File.GetCreationTime(s);
					DateTime timeNow = DateTime.Now;

					int month = 12;

					if (timeNow.Month != 1)
					{
						month = timeNow.Month - 1;
					}

					//Console.WriteLine("DATE: {0}/{1}/{2}", month, timeNow.Day, timeNow.Year);

					TimeSpan diff = timeNow - created;

					if (diff.TotalDays <= 120)
					{
						//Do something to distinguish they're new.
						Array.Resize(ref g_soundsNew, g_soundsNew.Length + 1);
						g_soundsNew[g_soundsNew.Length - 1] = spl[1];
					}
					//soundList[soundList.Length - 1] = str;
					//Console.WriteLine("SL: {0}", g_soundFiles[i]);

					i += 1;
				}

				isScanning = false;

				ScanSoundUpdate.Invoke();

				//Console.WriteLine("Sounds: {0}", g_soundList.Count);

				if (g_soundsNew.Length > 0)
				{
					logFuncs.AddToLogs("'" + g_soundsNew.Length.ToString() + "' new sounds!", "SOUND");
				}

				//logFuncs.AddToLogs("Sound scan COMPLETE! '" + g_soundList.Count + "' total sound clips in '" + g_soundCats.Length + "' categories.", "SOUND");
				//Console.WriteLine("Sound Scan Complete! Inserting rows!");

				LoadCombos();
			}
			else
			{
				logFuncs.AddToLogs("ERROR! Could not find 'sounds' directory.", "SOUND");
				ScanSoundUpdate.Invoke();
			}

		}

		public void doInsertSounds()
		{
			Thread oThread = new Thread(new ThreadStart(insertSounds));

			// Start the thread
			oThread.IsBackground = true;
			oThread.Start();

			// Spin for a while waiting for the started thread to become
			// alive:
			while (!oThread.IsAlive) ;

		}



		public void SetLoop(bool looping)
		{
			loop = looping;
		}

		public void playSound(string file)
		{
			disposeWave2();

			wavOut = new WaveOut();

			int devNum = gFuncs.GetDeviceNumber(Properties.Settings.Default.outputdevice);

			if (devNum != -1)
			{
				wavOut.DeviceNumber = devNum;

				//Console.WriteLine("DEFAULT SOUND DIR: {0}", Properties.Settings.Default.sounddir);

				if (file.Contains(Environment.CurrentDirectory + "\\sounds\\") == false)
				{
					fileName = Environment.CurrentDirectory + "\\sounds\\" + file;
				}

				//Add to recently played list.
				//Console.WriteLine("FILE: {0}", file);
				string[] spl = splitSound(file);

				TimeSpan timeNow;

				timeNow = DateTime.Now.Subtract(new DateTime(1970, 1, 9, 0, 0, 00));
				//timeNow.TotalSeconds.ToString();
				//Console.WriteLine("TIME NOW: {0}", timeNow.TotalSeconds);

				Console.WriteLine("- ADDING");
				soundRecent.Add(timeNow.TotalSeconds, spl[1]);

				soundReader = new Mp3FileReader(fileName);

				wavOut.Init(soundReader);
				wavOut.DesiredLatency = 50;

				wavOut.Volume = sVol;

				wavOut.PlaybackStopped += wavOut_PlaybackStopped;

				wavOut.Play();
			}

		}

		private void wavOut_PlaybackStopped(object sender, StoppedEventArgs e)
		{
			//Console.WriteLine("STOPPED: {0}", fileName);

			//if (loop == true)
			//{
			//	var frm = new IceBot.MainForm();
			//	frm.Log("Looping...");

			//	playSound(fileName);
			//}

			//disposeWave2();
		}

		public void pauseSound()
		{
			//Console.WriteLine("STATE: {0}", wavOut.PlaybackState);

			if (wavOut != null)
			{
				if (wavOut.PlaybackState == PlaybackState.Playing)
				{
					wavOut.Pause();
				}
				else if (wavOut.PlaybackState == PlaybackState.Paused)
				{
					wavOut.Play();
				}
			}

		}

		public void stopSound()
		{

			if (wavOut != null)
			{
				if (wavOut.PlaybackState == PlaybackState.Playing || wavOut.PlaybackState == PlaybackState.Paused)
				{
					if (SoundType == SoundPlayback.Queue)
					{
						//Console.WriteLine("SOUND TYPE QUEUE");

						soundQueue.Clear();

						disposeWave2();

					}
					else
					{
						disposeWave2();
					}
					
				}
			}

		}

		public void disposeWave2()
		{
			if (wavOut != null)
			{
				if (wavOut.PlaybackState == PlaybackState.Playing)
				{
					wavOut.Stop();
					wavOut.Dispose();
					wavOut = null;
				}
			}

			if (soundReader != null)
			{

				soundReader.Dispose();
				soundReader = null;
			}

		}

	}
	
	#endregion

	#region "Dota Funcs"

	public class dotaFuncs
	{
		private IceBot.gFuncs gFuncs = new IceBot.gFuncs();

		public string[] _teams = new string[2] { "Radiant", "Dire" };
		public static List<PortableSteam.Interfaces.Game.Dota2.IDOTA2.GetHeroesRequestResponseHero> getHeroes = null;


		private void SetKey()
		{
			PortableSteam.SteamWebAPI.SetGlobalKey("FB8A8696BBCF4B6C0D47E24F43535CB0");
		}

		public void GetHeroList()
		{
			SetKey();

			getHeroes = PortableSteam.SteamWebAPI.Game().Dota2().IDOTA2().GetHeroes().GetResponse().Data.Heroes;

			//foreach (var hero in getHeroes)
			//{
				//Console.WriteLine(" ## {0} - {1} ## ", hero.ID, hero.LocalizedName);
				//sortedHeroes.Add(hero.LocalizedName, hero.ID);
			//}

		}

		public string GetHeroName(int ID)
		{
			string ret = null;

			foreach (var hero in getHeroes)
			{
				if (hero.ID == ID)
				{
					ret = hero.LocalizedName;
				}
			}

			return ret;
		}

		private PortableSteam.Interfaces.Game.Dota2.IDOTA2.GetMatchHistoryResponseMatch GetMatch(SteamID id)
		{
			PortableSteam.Interfaces.Game.Dota2.IDOTA2.GetMatchHistoryResponseMatch ret = null;

			var getMatch = PortableSteam.SteamWebAPI.Game().Dota2().IDOTA2Match().GetMatchHistory().Account(SteamIdentity.FromAccountID(Convert.ToInt64(id.AccountID))).MatchesRequested(1).GetResponse();

			if (getMatch.Response.Success)
			{
				ret = getMatch.Data.Matches[0];
			}

			return ret;
		}

		private PortableSteam.Interfaces.Game.Dota2.IDOTA2.GetMatchDetailsResponseData GetMatchDetails(long matchID)
		{
			PortableSteam.Interfaces.Game.Dota2.IDOTA2.GetMatchDetailsResponseData ret = null;

			var dets = PortableSteam.SteamWebAPI.Game().Dota2().IDOTA2Match().GetMatchDetails(matchID).GetResponse();

			if (dets.Response.Success)
			{
				ret = dets.Data;
			}

			return ret;
		}

		public void test(SteamID id)
		{
			SetKey();

			//PortableSteam.Interfaces.Game.Dota2.IDOTA2.

			var getMatch = GetMatch(id);

			if (getMatch != null)
			{
				//Console.WriteLine("# RESPONSE SUCCESS # ");

				//Console.WriteLine("# Lobby Type: {0} #", getMatch.LobbyType);

				var det = GetMatchDetails(getMatch.MatchID);

				if (det != null)
				{
					//Console.WriteLine("# SUCCESS # ");

					//Console.WriteLine("# Winner: {0}  #  Mode: {1} # Humans: {2}", det.RadiantWin, det.GameMode, det.HumanPlayers);


					//Console.WriteLine("## PLAYERS ##");

					SteamIdentity[] ids = new SteamIdentity[0];

					Dictionary<long, string> plyNames = new Dictionary<long,string>();

					foreach (var ply in det.Players)
					{
						Array.Resize(ref ids, ids.Length + 1);
						ids[ids.Length - 1] = ply.Identity;
					}

					var getNames = PortableSteam.SteamWebAPI.General().ISteamUser().GetPlayerSummaries(ids).GetResponse();

					foreach(var t in getNames.Data.Players)
					{
						//Console.WriteLine("# {0} :: {1} #", tmp.Identity.AccountID, tmp.PersonaName);
						plyNames.Add(t.Identity.AccountID, t.PersonaName);
					}

					foreach (var ply in det.Players)
					{
						string persName = "Anonymous";

						if (ply.Identity.AccountID != 4294967295)
						{
							persName = plyNames[ply.Identity.AccountID];
						}

						//Console.WriteLine("# {0} :: {1} #", persName, GetHeroName(ply.HeroID));


					}

				}
				else
				{
					Console.WriteLine("# ERROR #");
				}

			}
			else
			{
				Console.WriteLine("# ERROR #");
			}



		}

	}

	#endregion

	#region "Download Funcs"

	public class downloadFuncs
	{

		public void CheckYTUrl(string link)
		{
			WebClient x = new WebClient();
			string source = x.DownloadString(link);


			string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

			Console.WriteLine("TITLE: {0}", title);
		}



		public void DownloadVideo(string link){

			WebClient client = new WebClient();
			Stream stream = client.OpenRead("http://youtubeinmp3.com/fetch/?api=advanced&format=JSON&video=" + link + "");
			StreamReader reader = new StreamReader(stream);

			Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(reader.ReadLine());

			// instead of WriteLine, 2 or 3 lines of code here using WebClient to download the file
			Console.WriteLine("TEST: {0}", jObject);
			
			stream.Close();

			WebClient cl = new WebClient();

			cl.DownloadFileAsync(new System.Uri(jObject["link"].ToString()), "test.mp3");

			cl.DownloadProgressChanged += cl_DownloadProgressChanged;

			cl.DownloadFileCompleted += cl_DownloadFileCompleted;
			

		}

		void cl_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			Console.WriteLine("DOWNLOAD COMPLETED");
		}

		void cl_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			//Console.WriteLine("{0}%", e.ProgressPercentage);
		}

	}

	#endregion

	#region "TTS Funcs"

	public class ttsFuncs
	{
		private IceBot.gFuncs gFuncs = new IceBot.gFuncs();

		public static float ttsVol = 1.0f;
		public static FonixTalkEngine ttsEng = new FonixTalkEngine();

		private WaveFileReader wfr;
		private WaveChannel32 wc;
		private WaveOut ttsOutput = new WaveOut();

		private void DoTTSToFile(string txt)
		{
			disposeTTSWav();

			ttsEng.Reset();
			ttsEng.Voice = TtsVoice.Dennis;

			ttsEng.SpeakToWavFile("speech.wav", txt);

			using (var reader = new WaveFileReader("speech.wav"))
			{
				var newFormat = new WaveFormat(48000, 16, 2);
				using (var conversionStream = new WaveFormatConversionStream(newFormat, reader))
				{
					WaveFileWriter.CreateWaveFile("output.wav", conversionStream);
				}
			}

		}

		private void PlayTTS()
		{
			disposeTTSWav();

			ttsOutput = new WaveOut();

			int devNum = gFuncs.GetDeviceNumber(Properties.Settings.Default.outputdevice);

			if (devNum != -1)
			{
				ttsOutput.DeviceNumber = devNum;

				wfr = new WaveFileReader("output.wav");
				wc = new WaveChannel32(wfr, ttsVol, 0);

				ttsOutput.Init(wc);

				ttsOutput.Volume = 1.0f;

				ttsOutput.Play();
			}
			else
			{
				Console.WriteLine("NO DEVICE NUM");
			}

		}

		public void disposeTTSWav()
		{
			if (ttsOutput != null)
			{
				if (ttsOutput.PlaybackState == PlaybackState.Playing)
				{
					ttsOutput.Stop();
					ttsOutput.Dispose();
					ttsOutput = null;
				}
			}

			if (wfr != null)
			{
				wfr.Dispose();
				wfr = null;
			}

			if (wc != null)
			{
				wc.Dispose();
				wc = null;
			}

		}


		public void PlaySpeech(string txt)
		{
			DoTTSToFile(txt);

			PlayTTS();
		}

	}

	#endregion

	#region "CMD Funcs"

	public class cmdFuncs
	{
		private IceBot.gFuncs gFuncs = new IceBot.gFuncs();
		private IceBot.soundFuncs soundFuncs = new IceBot.soundFuncs();
		private IceBot.mp3Funcs mp3Funcs = new IceBot.mp3Funcs();
		private IceBot.dotaFuncs dotaFuncs = new IceBot.dotaFuncs();
		private IceBot.downloadFuncs downFuncs = new IceBot.downloadFuncs();
		private IceBot.helpFuncs helpFuncs = new IceBot.helpFuncs();
		private IceBot.logFuncs logFuncs = new IceBot.logFuncs();
		private IceBot.userFuncs userFuncs = new IceBot.userFuncs();
		private IceBot.ttsFuncs ttsFuncs = new IceBot.ttsFuncs();

		public string mp3Pre = "[color=red][b][Music][/b][/color]";
		public string sPre = "[color=darkblue][b][Sounds][/b][/color]";
		public string dotaPre = "[color=brown][b][Dota][/b][/color]";
		public string helpPre = "[color=blue][b][Help][/b][/color]";


		public static Dictionary<string, string[]> g_lastCmds = new Dictionary<string, string[]>();

		//VideoInfo dlVideo;

		private string[] getLastCmd(string steamID)
		{
			string[] ret = { };

			if (g_lastCmds.ContainsKey(steamID))
			{
				ret = g_lastCmds[steamID];
			}


			return ret;
		}

		private void setLastCmd(string steamID, string[] args)
		{

			//Check args.
			int t = -1;
			int len = args.Length - 1;

			try {
				t = Convert.ToInt32(args[len]);
			} catch {

			}

			string[] tmp = new string[args.Length];

			args.CopyTo(tmp, 0);

			if (t != -1)
			{
				// Number at the end. Remove it.
				//tmp = args;

				//tmp[tmp.Length - 1] = null;
				string[] temp = new string[tmp.Length - 1];
				int i = 0;

				foreach (string str in tmp)
				{
					if (i != tmp.Length - 1) { 
						temp[i] = str;
					}

					i += 1;
				}

				tmp = temp;

			}
			else
			{
				// No number at the end.
				tmp = args;
			}

			g_lastCmds[steamID] = new string[tmp.Length];
			tmp.CopyTo(g_lastCmds[steamID], 0);

		}

		private void doLastCmd(string steamID)
		{

		}

		#region "Music Functions"

		public void musicListFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			string[] artistRet = mp3Funcs.listArtists();

			if (artistRet.Length > 0) { 

				if (args.Length == 2)
				{

					gFuncs.sendChatMsg(sender, artistRet[0], clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);

				}
				else if (args.Length == 3)
				{
					int pg = -1;

					try
					{
						pg = Convert.ToInt32(args[2]);
					}
					catch (FormatException e)
					{
						//gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + artistRet.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun);
					}
					finally
					{
						Console.WriteLine("SUCCESS");
					}

					Console.WriteLine("PG: {0}", pg);

					if (pg != -1)
					{

						if (pg >= 1 && pg <= artistRet.Length)
						{

							gFuncs.sendChatMsg(sender, artistRet[pg - 1], clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
						}
						else
						{
							gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + artistRet.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
						}

					}
					else
					{
						//Search for artist name.
						Console.WriteLine("SEARCH FOR: {0}", args[2]);
						if (mp3Funcs.ArtistExistsLoose(args[2]))
						{
							string[] trackRet = mp3Funcs.listTracks(args[2]);

							gFuncs.sendChatMsg(sender, trackRet[0], clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
							//Console.WriteLine("LIST TRACKS FROM {0}", srchArt);
						}
						else
						{
							gFuncs.sendChatMsg(sender, "No artists matching: " + args[2], clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
						}

					}

				}
				else if (args.Length == 4)
				{
					//Search for artist name.
					int pg = -1;
					Console.WriteLine("SEARCH FOR: {0}", args[2]);

					if (mp3Funcs.ArtistExistsLoose(args[2]))
					{
						string[] trackRet = mp3Funcs.listTracks(args[2]);

						try
						{
							pg = Convert.ToInt32(args[3]);
						}
						catch (FormatException e)
						{
							gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + trackRet.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
						}
						finally
						{
							Console.WriteLine("SUCCESS");
						}

						Console.WriteLine("PG: {0}", pg);

						if (pg != -1)
						{

							if (pg >= 1 && pg <= trackRet.Length)
							{

								gFuncs.sendChatMsg(sender, trackRet[pg - 1], clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
							
							}
							else
							{

								gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + trackRet.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
							
							}

						}


					}
					else
					{

						gFuncs.sendChatMsg(sender, "No artists matching: " + args[2], clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
					
					}


				}
			}
			else
			{
				gFuncs.sendChatMsg(sender, "There is currently no music added!", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
			}

		}

		public void musicAddFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (args.Length == 4)
			{
				if (mp3Funcs.ArtistExistsLoose(args[2]))
				{
					//Console.WriteLine("ARTIST FOUND");

					//Artist found, search both artist and tracks.
					string[] srchRet = mp3Funcs.searchArtistTracks(args[2], args[3]);

					if (srchRet[0] == "1")
					{
						//Console.WriteLine("ADD TO QUEUE: {0}", srchRet[1]);

						mp3Funcs.AddToQueue(Convert.ToInt32(srchRet[1]));
						string[] tmp = mp3Funcs.musicList[Convert.ToInt32(srchRet[1])];

						gFuncs.sendChatMsg(sender, "You have added [b]" + tmp[1] + " - [i]" + tmp[2] + "[/i][/b] to the queue!", clID, "private", "green", mp3Pre, false, queryrun, steamFriends, steamID);
						
					}
					else
					{

						if (Convert.ToInt32(srchRet[0]) <= 1)
						{

							gFuncs.sendChatMsg(sender, "There were no results for the search: [b]" + args[2] + " - " + args[3] + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
							
						}
						else
						{

							gFuncs.sendChatMsg(sender, "There were too many results for the search: [b]" + args[2] + " - " + args[3] + "[/b]", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
							
						}

					}

				}
				else
				{
					gFuncs.sendChatMsg(sender, "No artists matching: [b]" + args[2] + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
				}

			}
			else
			{
				int trackID = -1;

				try
				{
					trackID = Convert.ToInt32(args[2]);
				}
				catch
				{
					gFuncs.sendChatMsg(sender, "You must specify an artist and a track title OR a Track ID to add it to the queue.", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
				}
				finally
				{

					//Console.WriteLine("TRACK ID: {0}", trackID);

					if (trackID >= 1 && trackID <= mp3Funcs.musicList.Length)
					{

						mp3Funcs.AddToQueue(trackID - 1);
						string[] tmp = mp3Funcs.musicList[trackID - 1];

						gFuncs.sendChatMsg(sender, "You have added [b]" + tmp[1] + " - [i]" + tmp[2] + "[/i][/b] to the queue!", clID, "private", "green", mp3Pre, false, queryrun, steamFriends, steamID);

					}
					else
					{
						gFuncs.sendChatMsg(sender, "Invalid Track ID! Must be a number between 1 and " + mp3Funcs.musicList.Length + ".", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
					}


				}

			}

		}

		public void musicViewFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			string[] viewQ = mp3Funcs.ViewQueue();

			if (viewQ.Length >= 1)
			{

				if (args.Length == 2)
				{
					// No page.
					gFuncs.sendChatMsg(sender, viewQ[0], clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
					
				}
				else if (args.Length == 3)
				{
					 //Page.
					int pg = -1;

					try
					{
						pg = Convert.ToInt32(args[2]);
					}
					catch (FormatException e)
					{

						gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + viewQ.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
						
					}
					finally
					{
						Console.WriteLine("SUCCESS");

						gFuncs.sendChatMsg(sender, viewQ[pg - 1], clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
						
					}

				}

			}
			else
			{

				gFuncs.sendChatMsg(sender, "There are no tracks queued!", clID, "private", "red", mp3Pre, false, queryrun, steamFriends, steamID);
				
			}

		}


		public void musicSeekFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (args.Length > 1)
			{
				//Check if running.

				if (mp3Funcs.waveOut != null) { 

					//Get current song length.
					int songLen = mp3Funcs.formatTimeToSecs(mp3Funcs.audioReader.TotalTime);

					Console.WriteLine("SONG LENGTH: {0}", songLen);

					if (args[1] == "seek" || args[1] == "go" || args[1] == "g")
					{

						if (args.Length == 3)
						{
							//Check time.
							bool chkTime = mp3Funcs.checkTime(args[2]);

							if (chkTime)
							{
								//Make sure it isn't below 0 or above the song length.
								int seekTime = mp3Funcs.timeToSecs(args[2]);

								//Console.WriteLine("SEEK TIME: {0}", seekTime);

								if (seekTime >= 0 && seekTime <= songLen)
								{
									//Seek.
									//Console.WriteLine("SEEK VALID");

									mp3Funcs.Seek(args[2]);

									gFuncs.sendChatMsg(sender, "Seeking to '" + args[2] + "' of '" + mp3Funcs.formatTime(mp3Funcs.audioReader.TotalTime) + "' in track " + mp3Funcs.nowPlaying + "", clID, "private", "red", mp3Pre, false, queryrun, steamFriends, steamID);
								}
								else
								{
									gFuncs.sendChatMsg(sender, "Invalid time to seek to!", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
								}

							}
							else
							{
								gFuncs.sendChatMsg(sender, "Invalid time to seek to!", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
							}
							

						}
						else
						{
							//Must specify a time to seek to.
							gFuncs.sendChatMsg(sender, "You must specify a time to seek to. ", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
						}

						//mp3Funcs.Seek("1:01");

					}
					else if (args[1] == "ff" || args[1] == "fr")
					{
						int t = 0;

						if (args[1] == "fr") { t = 1; }

						int intr = 5;

						if (args.Length == 3)
						{
							int itr = -1;

							try
							{
								itr = Convert.ToInt32(args[2]);
							}
							catch
							{

							}

							if (itr != -1)
							{
								intr = itr;
							}
							else
							{
								gFuncs.sendChatMsg(sender, "", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
							}

						}

						Console.WriteLine("INTERVAL: {0}", intr);

						// See if there is a interval.
						if (t == 0)
						{
							//Forward.
							mp3Funcs.Jump("ff", intr);
						}
						else
						{
							//Rewind.
							mp3Funcs.Jump("fr", intr);
						}

						
					}
					else
					{
						//Invalid command.
						gFuncs.sendChatMsg(sender, "Invalid command.", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
					}
				}
			}
			else
			{
				gFuncs.sendChatMsg(sender, "", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
			}


		}


		public void musicPlayFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (mp3Funcs.waveOut != null)
			{
				//Wave player not null, check playback state.
				PlaybackState pbs = mp3Funcs.waveOut.PlaybackState;

				if (pbs == PlaybackState.Paused || pbs == PlaybackState.Stopped)
				{
					//Play.
					mp3Funcs.waveOut.Play();

				}
				else
				{
					//Already playing...
					gFuncs.sendChatMsg(sender, "Already playing!", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
				}

			}
			else
			{
				//Wave player is null, check queue maybe?

				if (mp3Funcs.mq.Count > 0)
				{
					mp3Funcs.checkQueue();
				}
				else
				{
					gFuncs.sendChatMsg(sender, "There is nothing to play! Add a song to the queue.", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
				}
				

			}

		}

		public void musicFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (args[0] != null)
			{
				if (args.Length >= 2)
				{

					if (args[1] == "list")
					{
						musicListFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if (args[1] == "add" || args[1] == "a")
					{
						 //Add to queue?
						musicAddFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					}
					else if (args[1] == "del" || args[1] == "d")
					{
						 //Delete from queue.


					}
					else if (args[1] == "queue" || args[1] == "q")
					{
						 //View queue.
						musicViewFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if (args[1] == "seek" || args[1] == "go" || args[1] == "g" || args[1] == "ff" || args[1] == "fr")
					{
						musicSeekFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if (args[1] == "play" || args[1] == "pl")
					{
						musicPlayFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					}
					else if (args[1] == "pause" || args[1] == "p")
					{

						mp3Funcs.Pause();

					}
					else if (args[1] == "next" || args[1] == "skip")
					{

						mp3Funcs.Skip();

					}
					else if (args[1] == "type" || args[1] == "t")
					{

						if (args.Length == 3)
						{
							if (args[2] == "queue")
							{
								mp3Funcs.mt = mp3Funcs.MusicType.Queue;

								gFuncs.sendChatMsg(sender, "Set Playback Type to: [b]" + mp3Funcs.mt.ToString() + "[/b]", clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
								

							}
							else if (args[2] == "shuffle")
							{
								mp3Funcs.mt = mp3Funcs.MusicType.Shuffle;

								if (mp3Funcs.mq.Count == 0)
								{
									mp3Funcs.AddRandom();
									mp3Funcs.Play();
								}


								gFuncs.sendChatMsg(sender, "Set Playback Type to: [b]" + mp3Funcs.mt.ToString() + "[/b]", clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
								
							}
							else
							{

								gFuncs.sendChatMsg(sender, "You must specify a playback type to set. ([b]Queue[/b] or [b]Shuffle[/b])", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
								
							}
						}
						else if (args.Length == 2)
						{
							 //Show current playback type.

							gFuncs.sendChatMsg(sender, "Current Playback Type: [b]" + mp3Funcs.mt.ToString() + "[/b]", clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
							
						}
					}

				}
				else if (args.Length == 1)
				{
					 //Show now playing

					string np = mp3Funcs.NowPlaying();

					gFuncs.sendChatMsg(sender, np, clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);

				}

			}

		}

		#endregion

		#region "Sound Functions"


		public void soundListFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			string[] soundCats = soundFuncs.listSoundCats();

			if (soundCats.Length > 0) {

				if (args.Length >= 3)
				{
					//Arguments...

					int pg = -1;

					try
					{
						pg = Convert.ToInt32(args[2]);
					}
					catch (FormatException e)
					{
						//gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + soundCats.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
					}
					

					if (pg == -1)
					{
						int[] srchCat = soundFuncs.srchSoundCatsLoose(args[2]);

						//Console.WriteLine("SRCH CAT: {0} : {1}", srchCat[0], srchCat[1]);

						if (srchCat[0] == 1)
						{
							string[] catSounds = soundFuncs.listSounds(srchCat[1]);

							if (args.Length == 3)
							{
								gFuncs.sendChatMsg(sender, catSounds[0], clID, "private", "blue", sPre, false, queryrun, steamFriends, steamID);
							}
							else if (args.Length == 4)
							{
								int pg2 = -1;

								try
								{
									pg2 = Convert.ToInt32(args[3]);
								}
								catch (FormatException e)
								{
									gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + catSounds.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
								}

								Console.WriteLine("PG2: {0} : {1}", pg2, catSounds.Length);

								if (pg2 != -1) {

									if (pg2 >= 1 && pg2 <= catSounds.Length)
									{
										gFuncs.sendChatMsg(sender, catSounds[pg2 - 1], clID, "private", "blue", sPre, false, queryrun, steamFriends, steamID);
									}
									else
									{
										gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + catSounds.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
									}

								}
								else
								{
									gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + catSounds.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
								}

							}

						}
						else
						{
							if (srchCat[0] > 1)
							{
								// More than one match.
								gFuncs.sendChatMsg(sender, "More than one result for the search: " + args[3] + "", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
							}
							else
							{
								// No matches.
								Console.WriteLine("NO MATCHES");

								gFuncs.sendChatMsg(sender, "BLANK", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
								//gFuncs.sendChatMsg(sender, "No results for the search: " + args[3] + "", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
							}

						}

					}
					else
					{
						Console.WriteLine("PG: {0}", pg);

						if (pg >= 1 && pg <= soundCats.Length)
						{
							gFuncs.sendChatMsg(sender, soundCats[pg - 1], clID, "private", "blue", sPre, false, queryrun, steamFriends, steamID);
						}
						else
						{
							gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + soundCats.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
						}

					}

				}
				else
				{
					//No arguments.
					Console.WriteLine("NO ARGUMENTS");
					gFuncs.sendChatMsg(sender, soundCats[0], clID, "private", "blue", sPre, false, queryrun, steamFriends, steamID);
				}

			}
			else
			{
				gFuncs.sendChatMsg(sender, "There are no sound categories!", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
			}


			//int pg = -1;

			//try
			//{
			//	pg = Convert.ToInt32(args[2]);
			//}
			//catch (FormatException e)
			//{
			//	//gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + artistRet.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun);
			//}
			//finally
			//{
			//	Console.WriteLine("SUCCESS");
			//}

		}


		public void soundSearchFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (args.Length >= 2)
			{

				if (args.Length >= 3)
				{
					string[] srchSounds = soundFuncs.searchSounds(args[2]);

					if (args.Length == 4) { 
						int pg = -1;

						try
						{
							pg = Convert.ToInt32(args[3]);
						}
						catch (FormatException e)
						{
							gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + srchSounds.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
						}

						if (pg != -1)
						{
							gFuncs.sendChatMsg(sender, srchSounds[pg - 1], clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
						}
						else
						{
							gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + srchSounds.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
						}

					}
					else
					{
						gFuncs.sendChatMsg(sender, srchSounds[0], clID, "private", "red", sPre, false, queryrun, steamFriends, steamID);
					}
				}
				else
				{
					gFuncs.sendChatMsg(sender, "You must specify something to search for. (Ex. '.sound search good')", clID, "channel", "blue", sPre, true, queryrun, steamFriends, steamID);
				}

			}

		}


		public void soundNewFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			//Console.WriteLine("ARGS: {0}", args.Length);

			int pg = 0;

			string[] newSounds = soundFuncs.listNewSounds();

			if (args.Length == 3) { 
				try
				{
					pg = Convert.ToInt32(args[2]);
					pg = pg - 1;
				}
				catch
				{
					gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + newSounds.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
				}
			}

			gFuncs.sendChatMsg(sender, newSounds[pg], clID, "private", "blue", null, false, queryrun, steamFriends, steamID);

		}


		public void soundQueueFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			Console.WriteLine("== Sound Queue Func ==");

			int pg = 0;

			string[] quSounds = soundFuncs.listSoundQueued();

			if (args.Length == 3)
			{
				try
				{
					pg = Convert.ToInt32(args[2]);
					pg = pg - 1;
				}
				catch
				{
					gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + quSounds.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
				}
			}

			gFuncs.sendChatMsg(sender, quSounds[pg], clID, "private", "blue", null, false, queryrun, steamFriends, steamID);


		}

		public void soundRecentFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			Console.WriteLine("== Sound Recent Func ==");

			int pg = 0;

			string[] reSounds = soundFuncs.listSoundRecent();

			if (args.Length == 3)
			{
				try
				{
					pg = Convert.ToInt32(args[2]);
					pg = pg - 1;
				}
				catch
				{
					gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + reSounds.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
				}
			}

			if (reSounds.Length >= 1) { 
				gFuncs.sendChatMsg(sender, reSounds[pg], clID, "private", "blue", null, false, queryrun, steamFriends, steamID);
			}
			else
			{
				gFuncs.sendChatMsg(sender, "== Recently Played Sounds == \n- No recent sounds!", clID, "private", "blue", null, false, queryrun, steamFriends, steamID);
			}

		}



		public void soundComboListFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			

			string[] userCombos = soundFuncs.listUserCombos(steamID.AccountID.ToString());

			if (args.Length == 3)
			{
				//No page.
				gFuncs.sendChatMsg(sender, userCombos[0], clID, "private", "blue", sPre, false, queryrun, steamFriends, steamID);
			}
			else if (args.Length == 4)
			{
				int pg = gFuncs.CheckIfPage(args[3]);

				if (pg != -1)
				{
					if (pg >= 1 && pg <= userCombos.Length)
					{
						gFuncs.sendChatMsg(sender, userCombos[pg-1], clID, "private", "blue", sPre, false, queryrun, steamFriends, steamID);
					}
					else
					{
						gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + userCombos.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
					}
				}
				else
				{
					gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + userCombos.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
				}

			}
			else
			{
				//Invalid Arguments.

			}

		}

		public void soundComboAllFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{


			string[] allCombos = soundFuncs.listAllCombos();

			if (args.Length == 3)
			{
				//No page.
				gFuncs.sendChatMsg(sender, allCombos[0], clID, "private", "blue", sPre, false, queryrun, steamFriends, steamID);
			}
			else if (args.Length == 4)
			{
				int pg = gFuncs.CheckIfPage(args[3]);

				if (pg != -1)
				{
					if (pg >= 1 && pg <= allCombos.Length)
					{
						gFuncs.sendChatMsg(sender, allCombos[pg - 1], clID, "private", "blue", sPre, false, queryrun, steamFriends, steamID);
					}
					else
					{
						gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + allCombos.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
					}
				}
				else
				{
					gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + allCombos.Length.ToString() + "[/b].", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
				}

			}
			else
			{
				//Invalid Arguments.

			}

		}

		public void soundComboSaveFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			args = gFuncs.quoteArgs(args);

			if (args.Length == 5)
			{
				// Save Combo.
				Console.WriteLine("SAVE COMBO");

				//Check name.
				bool srchCombos = soundFuncs.SearchCombos(soundFuncs.TrimName(args[3]));
				string combName = soundFuncs.TrimName(args[3]);



				if(srchCombos == false)
				{
					//Name is valid, check sounds.
					Console.WriteLine("NAME VALID");

					int hi = 0;
					string sounds = "";

					foreach (string ar in args)
					{
						if (hi >= 4)
						{
							if (hi == 4)
							{
								sounds = ar;
							}
							else
							{
								sounds = ar + "," + sounds;
							}
						}

						hi += 1;
					}

					//Console.WriteLine("SOUND: {0}", sounds);

					string[] sepSounds = soundFuncs.ConvertToStringArr(sounds);


					int ret = soundFuncs.srchSoundMultiValid(sepSounds);
					string eMsg = soundFuncs.srchSoundMulti(sepSounds);

					//Console.WriteLine("RET: {0} :: {1}", ret, eMsg);

					if (ret == -1)
					{
						//No fail.

						soundFuncs._soundCombos.Sections.AddSection(combName);
						soundFuncs._soundCombos.Sections[combName].AddKey("sounds", String.Join(",", sepSounds));
						soundFuncs._soundCombos.Sections[combName].AddKey("plays", "0");
						soundFuncs._soundCombos.Sections[combName].AddKey("id", steamID.AccountID.ToString());

						soundFuncs.SaveCombos();

						////Show what sounds we added.
						gFuncs.sendChatMsg(sender, "Created combo '" + combName + "' , containing '" + sepSounds.Length.ToString() + "' sounds.", clID, "private", "red", sPre, false, queryrun, steamFriends, steamID);

					}
					else
					{
						//Fail.
						gFuncs.sendChatMsg(sender, eMsg, clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
					}

				} 
				else 
				{
					//Invalid name, already taken.
					gFuncs.sendChatMsg(sender, "Combo name already taken.", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
				}

			}
			else
			{
				Console.WriteLine("ERROR");
				//Error.

				//Check to see why not enough or too many args.

			}

		}

		public void soundComboFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (args.Length >= 3)
			{
				//HUH WHAT.
				//Console.WriteLine("COMBO OPTIONS");

				if (args[2] == "list" || args[2] == "l")
				{
					// Show users combos.
					soundComboListFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					//
				}
				else if (args[2] == "all" || args[2] == "a")
				{
					// Show all combos created by date desc.
					soundComboAllFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					//
				}
				else if (args[2] == "save" || args[2] == "s")
				{
					// Save combo if all sounds and name valid.
					soundComboSaveFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					//
				}
				else
				{
					// Trying to use a combo?
					// Check for combo name.
					Console.WriteLine("TRYING TO USE SOUND COMBO: {0}", args[2]);

					//Check combo name.
					bool comboExist = soundFuncs.SearchCombos(args[2]);

					//If exists, add the sounds to the queue.
					if (comboExist == true)
					{
						KeyDataCollection comboInfo = soundFuncs._soundCombos.Sections[args[2]];

						Console.WriteLine("COMBO INFO: {0}", comboInfo["sounds"]);

						if (soundFuncs.SoundType == soundFuncs.SoundPlayback.Queue)
						{
							string[] del = new string[] { "," };
							string[] spl;
							spl = comboInfo["sounds"].Split(del, StringSplitOptions.None);

							//Get file for each sound and add them to the queue.
							int[] mSounds = soundFuncs.srchSoundFileMulti(spl);

							//Console.WriteLine("MSOUNDS: {0}, {1}", mSounds.Length, mSounds[0]);
							foreach (int ms in mSounds)
							{
								soundFuncs.soundQueue.Add(soundFuncs.g_soundFiles[ms]);
							}

							//Show what sounds we added.
							gFuncs.sendChatMsg(sender, "Queued combo '" + args[2] + "' , containing '" + mSounds.Length + "' sounds.", clID, "private", "red", sPre, false, queryrun, steamFriends, steamID);
						}
						else
						{
							gFuncs.sendChatMsg(sender, "Sorry, you cannot do multiple sounds in 'Interrupt' mode.", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
						}

					}
					else
					{
						gFuncs.sendChatMsg(sender, "Combo name doesn't exist.", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
					}

				}

			}
			else if(args.Length < 3)
			{
				//Check to see if user has any combos.

				Console.WriteLine("SHOW COMBOS");
				//string[] ret = soundFuncs.listUserCombos(steamID.AccountID.ToString());
				string[] tmpArgs = new string[] { "sound", "combo", "list", "1" };

				soundComboListFunc(sender, tmpArgs, clID, clName, clUID, queryrun, steamFriends, steamID);

			}

		}


		public void soundModeFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			
		}

		public void soundRandFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			
		}


		public void soundFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (args[0] != null)
			{
				if (args.Length >= 2)
				{

					if (args[1] == "list" || args[1] == "l")
					{
						//Log("SOUND :: List Sound Categories");
						soundListFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if (args[1] == "mode" || args[1] == "m")
					{
						soundModeFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if (args[1] == "rand" || args[1] == "r")
					{
						soundRandFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if(args[1] == "view" || args[1] == "v")
					{


					}
					else if (args[1] == "combo" || args[1] == "c")
					{

						soundComboFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					}
					else if (args[1] == "queue" || args[1] == "q")
					{
						soundQueueFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if (args[1] == "recent" || args[1] == "r")
					{
						soundRecentFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if (args[1] == "new")
					{
						//New sounds.
						soundNewFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					} 
					else if (args[1] == "search" || args[1] == "s")
					{
						soundSearchFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
					}
					else if (args[1] == "pause" || args[1] == "p")
					{
						soundFuncs.pauseSound();
					}
					else if (args[1] == "stop")
					{
						soundFuncs.stopSound();
					}
					else
					{
						//Combine all args after 0;
						string soundName = "";
						int i = 0;

						foreach (string str in args)
						{
							if (i != 0)
							{
								if (i == 1)
								{
									soundName = str;
								}
								else
								{
									soundName = soundName + " " + str;
								}

							}

							i += 1;
						}

						string[] sepSounds = new string[0];
						bool multiSounds = false;

						if(soundName.Contains(","))
						{
							//Seperated sounds.
							//Console.WriteLine("= SEPERATED SOUNDS =");
							string[] del = new string[] { "," };
							string[] spl;
							spl = soundName.Split(del, StringSplitOptions.None);

							foreach (string str in spl)
							{
								//Console.WriteLine("STR: {0}", str.Trim());
								Array.Resize(ref sepSounds, sepSounds.Length + 1);
								sepSounds[sepSounds.Length - 1] = str.Trim();
							}

							multiSounds = true;
						}


						if (multiSounds == false) { 
							int ret = soundFuncs.srchSoundFile(soundName);


							if (ret != -1)
							{

								//Log("SOUNDS: Playing " + soundFuncs.g_soundFiles[ret].ToString());
								string[] spl = soundFuncs.splitSound(soundFuncs.g_soundFiles[ret]);

								if (soundFuncs.SoundType == soundFuncs.SoundPlayback.Queue)
								{
									soundFuncs.soundQueue.Add(soundFuncs.g_soundFiles[ret]);
								}
								else
								{
									soundFuncs.playSound(soundFuncs.g_soundFiles[ret]);
								}

								if (sender == gFuncs.MsgType.Steam)
								{
									string porq = "";

									if (soundFuncs.SoundType == soundFuncs.SoundPlayback.Interrupt)
									{
										//gFuncs.sendChatMsg(sender, "Played the sound '" + spl[1] + "'", clID, "channel", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
										porq = "played the sound";
									}
									else
									{
										if (soundFuncs.soundQueue.Count == 1)
										{
											//gFuncs.sendChatMsg(sender, "Played the sound '" + spl[1] + "'", clID, "channel", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
											porq = "played the sound";
										}
										else if (soundFuncs.soundQueue.Count > 1)
										{
											//gFuncs.sendChatMsg(sender, "Queued the sound '" + spl[1] + "'", clID, "channel", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
											porq = "queued the sounds";
										}

									}

									string who = "";

									if (steamID.IsChatAccount == true) 
									{
										who = "'" + clName + "' ";
									}
									else
									{
										who = "You ";
									}

									string sayMsg = who + porq + " '" + spl[1] + "'";

									gFuncs.sendChatMsg(sender, sayMsg, clID, "channel", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
								}

							}
							else
							{
								//Send error message saying sound not found, find similar sounds.
								string srchSim = soundFuncs.searchSimilar(soundName);

								//Console.WriteLine("SIMILAR: {0}", srchSim.Length);

								if (srchSim.Length != 0)
								{
									gFuncs.sendChatMsg(sender, "Invalid sound name!\nSuggested: " + srchSim + "", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
								}
								else
								{
									gFuncs.sendChatMsg(sender, "Invalid sound name!", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
								}

							}


						}
						else
						{

							int ret = soundFuncs.srchSoundMultiValid(sepSounds);
							string eMsg = soundFuncs.srchSoundMulti(sepSounds);

							Console.WriteLine("RET: {0} :: {1}", ret, eMsg);

							if (ret == -1)
							{
								//No fail.

								if (soundFuncs.SoundType == soundFuncs.SoundPlayback.Queue)
								{
									//Get file for each sound and add them to the queue.
									int[] mSounds = soundFuncs.srchSoundFileMulti(sepSounds);

									//Console.WriteLine("MSOUNDS: {0}, {1}", mSounds.Length, mSounds[0]);
									foreach (int ms in mSounds)
									{
										soundFuncs.soundQueue.Add(soundFuncs.g_soundFiles[ms]);
									}

									//Show what sounds we added.

									string who = "";

									if (steamID.IsChatAccount == true)
									{
										who = "'" + clName + "' ";
									}
									else
									{
										who = "You ";
									}

									gFuncs.sendChatMsg(sender, who + "queued '" + mSounds.Length + "' sounds.", clID, "channel", "blue", sPre, false, queryrun, steamFriends, steamID);
								}
								else
								{
									gFuncs.sendChatMsg(sender, "Sorry, you cannot do multiple sounds in 'Interrupt' mode.", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
								}

							}
							else
							{
								//Fail.
								gFuncs.sendChatMsg(sender, eMsg, clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
							}

						}

						
					}

				}
				else
				{
					//If nothing after .sound
					gFuncs.sendChatMsg(sender, "Invalid Parameters! Use '.help' for a list of commands.", clID, "private", "red", sPre, true, queryrun, steamFriends, steamID);
				}

			}

		}

		#endregion

		public void dotaFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			Console.WriteLine("DOTA FUNC");

			if (dotaFuncs.getHeroes == null)
			{
				Console.WriteLine("GET HERO LIST");
				dotaFuncs.GetHeroList();
			}

			dotaFuncs.test(steamID);


			//if (args[1] != null)
			//{

				//if (args[1] == "set")
				//{
				//	//dotaSetFunc(sender, args, clID, clName, clUID, queryrun);

				//}
				//else if (args[1] == "view")
				//{
				//	//dotaViewFunc(sender, args, clID, clName, clUID, queryrun);

				//}
				//else if (args[1] == "last")
				//{
				//	//dotaLastFunc(sender, args, clID, clName, clUID, queryrun);

				//}
				//else if (args[1] == "details")
				//{



				//}
				//else
				//{



				//}


			//}

		}


		public void volumeFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (args.Length == 1)
			{
				 //Show volume.
				int vol = Convert.ToInt32(mp3Funcs.mVol * 100);


				gFuncs.sendChatMsg(sender, "= Current volume is: [b]" + vol.ToString() + "%[/b]", clID, "private", "blue", null, false, queryrun, steamFriends, steamID);

			}
			else if (args.Length == 2)
			{
				int vol = -1;

				try
				{
					vol = Convert.ToInt32(args[1]);
				}
				catch
				{

					gFuncs.sendChatMsg(sender, "Volume must be a number between [b]1 and 100[/b].", clID, "private", "red", null, true, queryrun, steamFriends, steamID);

				}
				finally
				{
					if (vol >= 1 && vol <= 100)
					{
						mp3Funcs.mVol = gFuncs.formatVol(vol);
						soundFuncs.sVol = mp3Funcs.mVol;

						if (mp3Funcs.waveOut != null)
						{
							mp3Funcs.waveOut.Volume = gFuncs.formatVol(vol);
						}

						if (soundFuncs.wavOut != null)
						{
							soundFuncs.wavOut.Volume = gFuncs.formatVol(vol);
						}

						gFuncs.sendChatMsg(sender, "Set volume to: [b]" + vol.ToString() + "%[/b]", clID, "private", "blue", null, false, queryrun, steamFriends, steamID);

						logFuncs.AddToLogs("Set volume to: [b]" + vol.ToString() + "%[/b]", sender.ToString());
					}
					else
					{

						gFuncs.sendChatMsg(sender, "Volume must be a number between [b]1 and 100[/b].", clID, "private", "red", null, true, queryrun, steamFriends, steamID);

					}


				}

			}

		}


		public void downloadFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			//.dl sound "Jontron" "fuck that shit" *link*
			//.dl music "The Glitch Mob" "Goin' for the kill" *link*

			//downFuncs.testdl();

			//downFuncs.DownloadVideo(link);
			//downFuncs.CheckYTUrl(args[1]);

			if(args.Length == 5){

				// Our test youtube link
				

				//if (dlVideo == null)
				//{
				//	Console.WriteLine("DOWNLOAD VIDEO NULL");

				//	// Check arguments.
				//	if (args[1] == "sound" || args[1] == "s" || args[1] == "music" || args[1] == "m")
				//	{
				//		int type = -1;

				//		if (args[1] == "sound" || args[1] == "s")
				//		{
				//			type = 0;
				//		}
				//		else
				//		{
				//			type = 1;
				//		}





				//	}
				//	else
				//	{
				//		gFuncs.sendChatMsg(sender, "You must specify whether to add this to sounds or music. (Ex. '.dl music')", clID, "private", "red", null, true, queryrun, steamFriends, steamID);
				//	}

				//}
				//else
				//{
				//	Console.WriteLine("DOWNLOAD VIDEO NOT NULL");

				//	gFuncs.sendChatMsg(sender, "Someone is already downloading a video!", clID, "private", "red", null, true, queryrun, steamFriends, steamID);
				//}

			}
			else
			{
				gFuncs.sendChatMsg(sender, "", clID, "private", "red", null, true, queryrun, steamFriends, steamID);
			}

		}


		public void lastFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			//Console.WriteLine("TEST: {0} : {1}", steamID.ToString(), steamID.AccountID.ToString());

			string accID = steamID.AccountID.ToString();


			//Has a last cmd.
			if (g_lastCmds.ContainsKey(accID))
			{

				if (args.Length > 1)
				{

					if (args[1] != "")
					{
						//Has arguments and not "".
						//Add arguments to lastCmd.
						string[] last = getLastCmd(accID);

						Array.Resize(ref last, last.Length + 1);

						last[last.Length - 1] = args[1];

						coreMsgFunc(gFuncs.MsgType.Steam, last, clID, clName, "", null, steamFriends, steamID);
					}
					else
					{
						gFuncs.sendChatMsg(sender, "Invalid arguments.", clID, "private", "red", null, true, queryrun, steamFriends, steamID);
					}


				}
				else
				{
					string[] tmp = getLastCmd(accID);

					if (tmp.Length > 0)
					{
						coreMsgFunc(gFuncs.MsgType.Steam, tmp, clID, clName, "", null, steamFriends, steamID);
					}

				}

			}
			else
			{
				gFuncs.sendChatMsg(sender, "You have not used a command yet.", clID, "private", "red", null, true, queryrun, steamFriends, steamID);
			}


		}


		public void helpFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			string[] baseHelp = helpFuncs.listHelp();


			if (args.Length == 1)
			{
				//Show help cmds base.

				gFuncs.sendChatMsg(sender, baseHelp[0], clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);


			}
			else if (args.Length > 1)
			{
				int pg = -1;

				try
				{
					pg = Convert.ToInt32(args[1]);
				}
				catch
				{

				}

				if (pg != -1)
				{
					if (pg >= 1 && pg <= baseHelp.Length)
					{
						gFuncs.sendChatMsg(sender, baseHelp[0], clID, "private", "blue", mp3Pre, false, queryrun, steamFriends, steamID);
					}
					else
					{
						gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + baseHelp.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
					}
				}
				else
				{
					//Wants help for a command?
					int len = args.Length - 1;

					//Search for args[1].
					int srchHelp = helpFuncs.searchHelp(args[1]);
					

					if (srchHelp != -1)
					{
						//Command is valid, show it and it's children if any.
						//Console.WriteLine("COMMAND VALID :: {0}", srchHelp);

						string[] showHlp = helpFuncs.showHelp(srchHelp);

						if (args.Length == 2)
						{
							//No page.

							gFuncs.sendChatMsg(sender, showHlp[0], clID, "private", "red", helpPre, false, queryrun, steamFriends, steamID);

						}
						else if (args.Length == 3)
						{
							//Console.WriteLine("PAGE: {0} :: {1}", args[2], showHlp.Length);
							int pg2 = -1;
							
							try
							{
								pg2 = Convert.ToInt32(args[2]);
							}
							catch
							{
								gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + showHlp.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
							}

							if (pg2 != -1) { 
								if (pg2 >= 1 && pg2 <= showHlp.Length)
								{
									gFuncs.sendChatMsg(sender, showHlp[pg2 - 1], clID, "private", "red", helpPre, false, queryrun, steamFriends, steamID);
								}
								else
								{
									gFuncs.sendChatMsg(sender, "Page must be a number between [b]1[/b] and [b]" + showHlp.Length.ToString() + "[/b].", clID, "private", "red", mp3Pre, true, queryrun, steamFriends, steamID);
								}
							}

						}

					}
					else
					{
						gFuncs.sendChatMsg(sender, "Invalid command! Cannot find help for '" + args[len] + "'.", clID, "private", "red", helpPre, true, queryrun, steamFriends, steamID);
					}

				}

			}


		}

		public void ttsFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{

			if (args.Length >= 2)
			{
				//More than one argument.
				//Console.WriteLine("MORE THAN 1 TTS ARGUMENTS");
				string ttsStr = String.Join(" ", args);
				ttsStr = ttsStr.Replace(args[0] + " ", "");

				//Console.WriteLine("TTS STR: {0}", ttsStr);

				ttsFuncs.PlaySpeech(ttsStr);
			}
			else
			{
				//No Arguments.
				Console.WriteLine("NO TTS ARGUMENTS");

			}

		}


		public void steamFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			WebClient w = new WebClient();
			string s = w.DownloadString("http://www.reddit.com/r/Steam");

			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(s);

			HtmlNode tbl = doc.DocumentNode.SelectSingleNode("//div[@class='md']").SelectSingleNode("table").SelectSingleNode("tbody");

			Dictionary<string, string> dict = new Dictionary<string, string>();

			#region Parse Data
			foreach (HtmlNode row in tbl.SelectNodes("tr"))
			{
				//Console.WriteLine("row");
				int i = 0;
				string head = "";


				foreach (HtmlNode cell in row.SelectNodes("th|td"))
				{
					//Console.WriteLine(cell.InnerText);

					if (i == 0)
					{
						//I is 0, header.
						head = cell.InnerText;
					}
					else
					{
						//I is 1, value.
						//Console.WriteLine("ADDED: {0} : {1}", head, cell.InnerText);

						dict.Add(head, cell.InnerText);
					}

					i += 1;
				}

			}

			foreach (KeyValuePair<string, string> ent in dict)
			{
				if (ent.Value.Contains(" of "))
				{
					//Some servers down.
					string val = ent.Value;

				}

			}

			#endregion

			string chatStr = "#### Steam Status ####\n";

			int ci = 0;
			foreach (KeyValuePair<string, string> ent in dict)
			{

				if(ci == 0 || ci >= 6){
					//Console.WriteLine("CI: {0}  - {1} : {2}", ci, ent.Key, ent.Value);
					chatStr = chatStr + ent.Key + " - " + ent.Value + "\n";
				}

				ci += 1;
			}

			gFuncs.sendChatMsg(sender, chatStr, clID, "private", "blue", null, false, queryrun, steamFriends, steamID);

		}


		public void overviewFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			Console.WriteLine("Overview Func");

			int vol = Convert.ToInt32(mp3Funcs.mVol * 100);

			string sayMsg = "=== IceBot Overview === Vol: " + vol.ToString() + "% === \n";

			//if music is playing show now playing.
			string np = mp3Funcs.NowPlaying();

			if (np.Contains("No track") == false)
			{
				string[] lines = np.Split(new string[] { "\n" }, StringSplitOptions.None);

				sayMsg = sayMsg + lines[1] + "\n=====================\n";
			}


			//Send message.
			gFuncs.sendChatMsg(sender, sayMsg, clID, "private", "blue", null, false, queryrun, steamFriends, steamID);

		}



		public void coreMsgFunc(gFuncs.MsgType sender, string[] args, string clID, string clName, string clUID = "", TS3QueryLib.Core.Server.QueryRunner queryrun = null, SteamFriends steamFriends = null, SteamID steamID = null)
		{
			//Check user.
			bool chkUser = true;

			
			if (queryrun != null)
			{
				// TS ID.

			}
			else
			{
				//Steam ID.
				//Console.WriteLine("STEAM ID - CHECK AND ADD");
				if (steamID.IsChatAccount == false)
				{ 

				}

			}

			if (chkUser == false && steamID.IsChatAccount == false)
			{
				//Show first time message.
				string firstMsg = "==== Welcome to IceBot ====\n" +
					"This looks like your first time using IceBot. IceBot uses basic text commands to do actions.\n" +
					"For example: '.vol' would show you what the current volume is. Or '.vol 25' will set the volume to 25%.\n\n" +
					"Use '.help' (without quotes) for a list and help for all commands.\n" +
					"=======================================";

				gFuncs.sendChatMsg(sender, firstMsg, clID, "private", "blue", null, false, queryrun, steamFriends, steamID);

			}
			else
			{
				if (args[0] == ".dl" || args[0] == ".down")
				{
					Console.WriteLine("DOWNLOAD");

					downloadFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					//setLastCmd(steamID.AccountID.ToString(), args);

				}
				else if (args[0] == ".test" || args[0] == ".te")
				{
					Console.WriteLine("TEST");

					userFuncs.selectTest();

				}
				else if (args[0] == ".mp3" || args[0] == ".m")
				{
					Console.WriteLine("MP3");

					musicFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					setLastCmd(clID, args);

				}
				else if (args[0] == ".s" || args[0] == ".sound")
				{
					soundFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					setLastCmd(clID, args);
				}
				else if (args[0] == ".volume" || args[0] == ".vol")
				{
					volumeFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					setLastCmd(clID, args);
				}
				else if (args[0] == ".last" || args[0] == ".l")
				{
					lastFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
				}
				else if (args[0] == ".help" || args[0] == ".?")
				{
					helpFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					setLastCmd(clID, args);
				}
				else if (args[0] == ".t" || args[0] == ".tts" || args[0] == ".say")
				{
					ttsFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
				}
				else if (args[0] == ".dota")
				{
					dotaFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);
				}
				else if (args[0] == ".steam")
				{
					steamFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					setLastCmd(clID, args);
				} else if(args[0] == ".overview" || args[0] == ".over" || args[0] == ".o"){
					//Overview command.
					overviewFunc(sender, args, clID, clName, clUID, queryrun, steamFriends, steamID);

					setLastCmd(clID, args);
				} else if(args[0] == ".updates" || args[0] == ".changelog"){

					string chanLog = "=== Change Log ===\n" +
						"===== 1.0.1 =====\n" +
						"= Added chatroom support for IceBot. ==\n" +
						"= Updated some chat responses for group chats. ==\n" +
						"= Added .updates/.changelog command.\n" +
						"= Fixed '.help' not working with category pages.\n" +
						"== In Progress ==\n" +
						"= Overview command to view current volume, recent sounds, popular sound combos and music that is playing.\n" +
						"= Dota statics - Sharing, tracking and showing dota statistics.\n" +
						"= Teamspeak functionality continues to be an ongoing dilemma, still in bug testing.\n" +
						"=================";


					gFuncs.sendChatMsg(sender, chanLog, clID, "private", "blue", null, false, queryrun, steamFriends, steamID);


				}
				else
				{
					gFuncs.sendChatMsg(sender, "Invalid Command!", clID, "private", "red", null, true, queryrun, steamFriends, steamID);
				}

			}

		}


	}

	#endregion


	#region "Steam Funcs"

	public class steamFuncs
	{
		private IceBot.gFuncs gFuncs = new IceBot.gFuncs();
		private IceBot.cmdFuncs cmdFuncs = new IceBot.cmdFuncs();
		private IceBot.userFuncs userFuncs = new IceBot.userFuncs();

		private static SteamClient steamClient;
		private static CallbackManager manager;

		private static SteamUser steamUser;
		private static SteamFriends steamFriends;
		private static SteamUserStats steamUserStats;

		//Test these...
		private static SteamApps steamApps;
		
		public static int numFriends = 0;
		public static int numOnFriends = 0;
		public static bool isConnected = false;
		public static bool isRunning = false;

		private string user, pass;
		private string authCode;

		public Thread oTime;

		public System.Timers.Timer friendsTimer = new System.Timers.Timer(1000);
		
		public static event EventHandler FriendsUpdate;
		public static event EventHandler SteamConnect;
		public static event EventHandler SteamDisconnect;
		public static event EventHandler SteamConnectError;


		public bool DoConnect()
		{
			bool ret = false;

			Connect();

			return ret;
		}

		public bool DoDisconnect()
		{
			bool ret = false;

			Disconnect();

			return ret;
		}


		public void GetNumOnlineFriends()
		{
			if (steamFriends.GetPersonaName() != "[unassigned]") { 

				int nf = 0;
				int nof = 0;

				if (steamFriends != null)
				{
					//Console.WriteLine("STEAMFRIENDS:: NOT NULL :: {0}", steamFriends.GetPersonaName());

					int cnt = numFriends;
					int i = 0;

					//Console.WriteLine("FRIENDS: {0}", numFriends);

					while (i <= numFriends)
					{
						SteamID fr = steamFriends.GetFriendByIndex(i);

						string name = steamFriends.GetFriendPersonaName(fr);
						EPersonaState state = steamFriends.GetFriendPersonaState(fr);

						if (name != "[unknown]") { 

							//Console.WriteLine("FRIEND: {0} : {1}", name, state);

							if (state != EPersonaState.Offline)
							{
								nof += 1;
							}

							nf += 1;
						}

						i += 1;
					}

					//Console.WriteLine("NF: {0} : {1} : {2} : {3}", nf, numFriends, nof, numOnFriends);

					if (nf != numFriends || nof != numOnFriends)
					{
						numFriends = nf;
						numOnFriends = nof;

						Console.WriteLine("UPDATE FRIENDS");

						FriendsUpdate.Invoke();
					}


				}
			}
		}

		private void DoWait()
		{
			//Console.WriteLine("RUNNING");
			while (isRunning)
			{
				//Console.WriteLine("RUNNING");

				// in order for the callbacks to get routed, they need to be handled by the manager
				manager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
				
			}
		}


		private void Disconnect()
		{
			steamFriends.SetPersonaState(EPersonaState.Offline);
			steamUser.LogOff();
			
			steamClient.Disconnect();
			
			isConnected = false;
			isRunning = false;

			SteamDisconnect.Invoke();
		}

		private void Connect()
		{
			// save our logon details
			user = Properties.Settings.Default.steamusername;
			pass = Properties.Settings.Default.steampassword;

			//Console.WriteLine("USER: {0} -- PASS: {1}", user, pass);

			if (user != "" && pass != "") { 

				// create our steamclient instance
				steamClient = new SteamClient(System.Net.Sockets.ProtocolType.Tcp);

				// create the callback manager which will route callbacks to function calls
				manager = new CallbackManager(steamClient);

				// get the steamuser handler, which is used for logging on after successfully connecting
				steamUser = steamClient.GetHandler<SteamUser>();
				steamFriends = steamClient.GetHandler<SteamFriends>();
				steamUserStats = steamClient.GetHandler<SteamUserStats>();
				steamApps = steamClient.GetHandler<SteamApps>();

				// register a few callbacks we're interested in
				// these are registered upon creation to a callback manager, which will then route the callbacks
				// to the functions specified

				// this callback is triggered when the steam servers wish for the client to store the sentry file
				new Callback<SteamUser.UpdateMachineAuthCallback>(OnMachineAuth, manager);
				
				new Callback<SteamClient.ConnectedCallback>(OnConnected, manager);
				new Callback<SteamClient.DisconnectedCallback>(OnDisconnected, manager);
				
				new Callback<SteamUser.LoggedOnCallback>(OnLoggedOn, manager);
				new Callback<SteamUser.LoggedOffCallback>(OnLoggedOff, manager);
				new Callback<SteamUser.AccountInfoCallback>(OnAccountInfo, manager);
				
				
				new Callback<SteamFriends.FriendsListCallback>(OnFriends, manager);
				new Callback<SteamFriends.FriendMsgCallback>(OnFriendMsg, manager);
				new Callback<SteamFriends.FriendAddedCallback>(OnFriendAdd, manager);
				new Callback<SteamFriends.ChatInviteCallback>(OnChatInvite, manager);
				new Callback<SteamFriends.ChatMsgCallback>(OnChatMsg, manager);
				new Callback<SteamFriends.ChatEnterCallback>(OnChatEnter, manager);
				new Callback<SteamFriends.ChatActionResultCallback>(OnChatAction, manager);
				new Callback<SteamFriends.ChatMemberInfoCallback>(OnChatMemberInfo, manager);
				new Callback<SteamFriends.ProfileInfoCallback>(OnProfileInfo, manager);
				
				isRunning = true;

				Console.WriteLine("Connecting to Steam...");

				// initiate the connection
				steamClient.Connect();
				//Console.WriteLine("DID CONNECT FUNCTION");

				// create our callback handling loop
				oTime = new Thread(new ThreadStart(DoWait));
				oTime.Name = "DoWait";
				//oTime.IsBackground = true;

				oTime.Start();

				//while (!oTime.IsAlive) ;

				
			}
			else
			{
				
				SteamConnectError.Invoke();
			}


		}


		private void OnProfileInfo(SteamFriends.ProfileInfoCallback callback)
		{
			Console.WriteLine("PROFILE INFO: {0}", callback.CityName);
			
		}



		private void OnChatAction(SteamFriends.ChatActionResultCallback callback)
		{
			Console.WriteLine("CHAT ACTION :: {0}", callback.Action);

		}

		private void OnChatMemberInfo(SteamFriends.ChatMemberInfoCallback callback)
		{
			Console.WriteLine("CHAT MEMBER INFO :: {0}", callback.Type);

		}

		private void OnChatMsg(SteamFriends.ChatMsgCallback callback)
		{
			//Console.WriteLine("CHATROOM MESSAGE :: {0} : {1}", callback.ChatMsgType, callback.Message);

			if (callback.ChatMsgType == EChatEntryType.ChatMsg)
			{
				//Console.WriteLine("MESSAGE: {1}: {0}", callback.Message.ToLower(), callback.Sender.Render());
				string clID = callback.ChatterID.Render(true); //Convert.ToUInt32();
				string clName = steamFriends.GetFriendPersonaName(callback.ChatterID);

				//Console.WriteLine("STEAM: {0}", clID);

				//userFuncs.CheckAndAdd(clID);

				if (callback.Message.StartsWith("."))
				{
					//Console.WriteLine("STEAM COMMAND");
					string[] strSep = new string[] { " " };
					string[] spl = callback.Message.Split(strSep, StringSplitOptions.RemoveEmptyEntries);

					string[] qtArgs = gFuncs.quoteArgs(spl);


					if (spl.Length > 0)
					{
						//Console.WriteLine("CL: {0} : {1}", callback.Sender.Render(true), callback.Sender.Render());

						cmdFuncs.coreMsgFunc(gFuncs.MsgType.Steam, qtArgs, clID, clName, "", null, steamFriends, callback.ChatRoomID);

					}

				}

			}

		}

		private void OnChatEnter(SteamFriends.ChatEnterCallback callback)
		{
			Console.WriteLine("CHATROOM ENTER :: {0} : {1}", callback.ChatID, callback.EnterResponse);
			//callback.
			
		}

		private void OnChatInvite(SteamFriends.ChatInviteCallback callback)
		{
			Console.WriteLine("CHAT INVITATION :: {0} : {1}", callback.ChatRoomID.IsValid, callback.ChatRoomID.AccountType);

			//callback.ChatRoomID.AccountInstance = 0x80000;
			//callback.ChatRoomID.AccountType = EAccountType.Chat;
			
			steamFriends.JoinChat(callback.ChatRoomID);
		}



		private void OnConnected(SteamClient.ConnectedCallback callback)
		{
			if (callback.Result != EResult.OK)
			{
				Console.WriteLine("Unable to connect to Steam: {0}", callback.Result);

				isRunning = false;
				return;
			}

			Console.WriteLine("Connected to Steam! Logging in '{0}'...", user);

			byte[] sentryHash = null;
			if (File.Exists("sentry.bin"))
			{
				// if we have a saved sentry file, read and sha-1 hash it
				byte[] sentryFile = File.ReadAllBytes("sentry.bin");
				sentryHash = CryptoHelper.SHAHash(sentryFile);
			}

			steamUser.LogOn(new SteamUser.LogOnDetails
			{
				Username = user,
				Password = pass,

				// in this sample, we pass in an additional authcode
				// this value will be null (which is the default) for our first logon attempt
				AuthCode = authCode,

				// our subsequent logons use the hash of the sentry file as proof of ownership of the file
				// this will also be null for our first (no authcode) and second (authcode only) logon attempts
				SentryFileHash = sentryHash,
			});
			//steamUser.LogOnAnonymous();
		}

		private void OnDisconnected(SteamClient.DisconnectedCallback callback)
		{
			// after recieving an AccountLogonDenied, we'll be disconnected from steam
			// so after we read an authcode from the user, we need to reconnect to begin the logon flow again

			Console.WriteLine("Disconnected from Steam, reconnecting in 5...");

			Thread.Sleep(TimeSpan.FromSeconds(5));

			steamClient.Connect();
		}

		private void OnLoggedOn(SteamUser.LoggedOnCallback callback)
		{
			if (callback.Result == EResult.AccountLogonDenied)
			{
				Console.WriteLine("This account is SteamGuard protected!");
				Console.Write("Please enter the auth code sent to the email at {0}: ", callback.EmailDomain);


				authCode = Console.ReadLine();
				return;
			}

			if (callback.Result != EResult.OK)
			{
				Console.WriteLine("Unable to logon to Steam: {0} / {1}", callback.Result, callback.ExtendedResult);

				isRunning = false;
				return;
			}

			Console.WriteLine("Successfully logged on!");

			SteamConnect.Invoke();

			isConnected = true;

			GetNumOnlineFriends();

			friendsTimer.Elapsed += friendsTimer_Elapsed;
			friendsTimer.Start();

			
			// at this point, we'd be able to perform actions on Steam
		}

		void friendsTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			GetNumOnlineFriends();
		}

		private void OnLoggedOff(SteamUser.LoggedOffCallback callback)
		{
			Console.WriteLine("Logged off of Steam: {0}", callback.Result);

			SteamDisconnect.Invoke();

			isConnected = false;
			isRunning = false;
			friendsTimer.Stop();
		}

		private void OnMachineAuth(SteamUser.UpdateMachineAuthCallback callback)
		{
			Console.WriteLine("Updating sentryfile...");

			byte[] sentryHash = CryptoHelper.SHAHash(callback.Data);

			// write out our sentry file
			// ideally we'd want to write to the filename specified in the callback
			// but then this sample would require more code to find the correct sentry file to read during logon
			// for the sake of simplicity, we'll just use "sentry.bin"
			File.WriteAllBytes("sentry.bin", callback.Data);

			// inform the steam servers that we're accepting this sentry file
			steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
			{
				JobID = callback.JobID,

				FileName = callback.FileName,

				BytesWritten = callback.BytesToWrite,
				FileSize = callback.Data.Length,
				Offset = callback.Offset,

				Result = EResult.OK,
				LastError = 0,

				OneTimePassword = callback.OneTimePassword,

				SentryFileHash = sentryHash,
			});

			Console.WriteLine("Done!");
		}


		private void OnAccountInfo(SteamUser.AccountInfoCallback callback)
		{
			steamFriends.SetPersonaName("IceBot");
			steamFriends.SetPersonaState(EPersonaState.Online);
		}


		private void OnFriends(SteamFriends.FriendsListCallback callback)
		{
			numFriends = 0;

			foreach (var friend in callback.FriendList)
			{
				//If invite is waiting for you to accept and it isn't a group invite
				if (friend.Relationship == EFriendRelationship.RequestRecipient && friend.SteamID.IsIndividualAccount)
				{
					steamFriends.AddFriend(friend.SteamID);
				}

				numFriends += 1;

				FriendsUpdate.Invoke();

				steamFriends.RequestProfileInfo(friend.SteamID);

			}


		}

		private void OnFriendAdd(SteamFriends.FriendAddedCallback callback)
		{
			steamFriends.AddFriend(callback.SteamID);
		}

		private void OnFriendMsg(SteamFriends.FriendMsgCallback callback)
		{

			if (callback.EntryType == EChatEntryType.ChatMsg)
			{
				//Console.WriteLine("MESSAGE: {1}: {0}", callback.Message.ToLower(), callback.Sender.Render());
				string clID = callback.Sender.Render(true); //Convert.ToUInt32();
				string clName = steamFriends.GetFriendPersonaName(callback.Sender);

				//Console.WriteLine("STEAM: {0}", clID);

				//userFuncs.CheckAndAdd(clID);

				if (callback.Message.StartsWith("."))
				{
					//Console.WriteLine("STEAM COMMAND");
					string[] strSep = new string[] { " " };
					string[] spl = callback.Message.Split(strSep, StringSplitOptions.RemoveEmptyEntries);

					string[] qtArgs = gFuncs.quoteArgs(spl);
					

					if (spl.Length > 0)
					{
						//Console.WriteLine("CL: {0} : {1}", callback.Sender.Render(true), callback.Sender.Render());

						cmdFuncs.coreMsgFunc(gFuncs.MsgType.Steam, qtArgs, clID, clName, "", null, steamFriends, callback.Sender);

					}

				}
				else
				{
					//Show help.
					gFuncs.sendChatMsg(gFuncs.MsgType.Steam, "That is not a command! Use '.' before a command. Use '.help' for details.", clID, null, null, null, false, null, steamFriends, callback.Sender);
				}
				

			}

		}

	}

	#endregion

	#region "Teamspeak Funcs"
	public class tsFuncs
	{
		private IceBot.gFuncs gFuncs = new IceBot.gFuncs();
		private IceBot.cmdFuncs cmdFuncs = new IceBot.cmdFuncs();

		public static QueryRunner QueryRunner;
		public static AsyncTcpDispatcher Dispatcher;
		public static bool IsConnected { get { return Dispatcher != null && Dispatcher.IsConnected; } }

		public static int currUsers = 0;
		public static int maxUsers = 0;

		private System.Timers.Timer aliveTimer = new System.Timers.Timer(300000);
		private System.Timers.Timer checkTimer = new System.Timers.Timer(5000);

		//public static event EventHandler FriendsUpdate;
		public static event EventHandler TS3Connect;
		public static event EventHandler TS3Disconnect;
		public static event EventHandler TS3Update;
		public static event EventHandler TS3ConnectError;


		public bool CheckCredentials()
		{
			bool ret = true;

			if (Properties.Settings.Default.hostName == "" || Properties.Settings.Default.password == "" || Properties.Settings.Default.username == "" || Properties.Settings.Default.nickname == "")
			{
				ret = false;
			}

			return ret;
		}

		public void Connect()
		{
			Console.WriteLine("CREDENTIALS:: {0}", CheckCredentials());
			if (CheckCredentials() == true)
			{
				Dispatcher = new AsyncTcpDispatcher(Properties.Settings.Default.hostName.Trim(), Properties.Settings.Default.sport);
				Dispatcher.ReadyForSendingCommands += Dispatcher_ReadyForSendingCommands;
				Dispatcher.ServerClosedConnection += Dispatcher_ServerClosedConnection;
				Dispatcher.SocketError += Dispatcher_SocketError;
				Dispatcher.NotificationReceived += Dispatcher_NotificationReceived;


				QueryRunner = new QueryRunner(Dispatcher);

				try
				{
					//frm.Log("Connecting to: " + Properties.Settings.Default.hostName);
					Dispatcher.Connect();

				}
				catch (SocketException ex)
				{
					//frm.Log("Error occurred during connect: " + ex.Message);
					//SetConnectable();
					return;
				}
			}
			else
			{
				// Invoke connect error.
				TS3ConnectError.Invoke();
			}
		}


		public void Disconnect()
		{
			aliveTimer.Stop();

			try
			{
				if (Dispatcher != null)
					Dispatcher.Disconnect();

			}
			catch
			{

			}
			finally
			{
				//SetConnectable();
			}

			TS3Disconnect.Invoke();
		}


		private void Auth()
		{
			Console.WriteLine("Authenticating..");

			//IceBot 0A3eWpUK

			SimpleResponse loginResponse = new SimpleResponse();

			try
			{
				loginResponse = QueryRunner.Login(Properties.Settings.Default.username, Properties.Settings.Default.password);
			}
			catch
			{
				Console.WriteLine("ERROR");

				TS3ConnectError.Invoke();
			}


			if (loginResponse != null)
			{
				if (loginResponse.IsErroneous)
				{
					Console.WriteLine("ERR: {0}", loginResponse.ErrorId);

					Console.WriteLine("Authentication failed --> Invalid credentials!");

					Disconnect();

					if (aliveTimer.Enabled == true)
					{
						aliveTimer.Enabled = false;
						aliveTimer.Stop();
					}

					TS3ConnectError.Invoke();
				}
				//break;
				//return;
			}

			Console.WriteLine("Authentication succeeded..");

			Console.WriteLine("Selecting server with port " + Properties.Settings.Default.port.ToString() + "..");
			SimpleResponse selectServerResponse = QueryRunner.SelectVirtualServerByPort(Properties.Settings.Default.port);
			Console.WriteLine(selectServerResponse.IsErroneous ? selectServerResponse.ErrorMessage : "Server with port " + Properties.Settings.Default.port.ToString() + " was selected..");

			Console.WriteLine("Getting server info..");
			ServerInfoResponse serverInfoResponse = QueryRunner.GetServerInfo();

			if (!serverInfoResponse.IsErroneous)
			{
				Console.WriteLine(string.Empty);
				Console.WriteLine("[ServerInfo]");
				Console.WriteLine("- Server-Name: " + serverInfoResponse.Name);
				Console.WriteLine("- Server-Version: " + serverInfoResponse.Version);
				Console.WriteLine("- Server-Platform: " + serverInfoResponse.Platform);

				QueryRunner.RegisterForNotifications(TS3QueryLib.Core.Server.Entities.ServerNotifyRegisterEvent.TextChannel);
				QueryRunner.RegisterForNotifications(TS3QueryLib.Core.Server.Entities.ServerNotifyRegisterEvent.TextServer);
				QueryRunner.RegisterForNotifications(TS3QueryLib.Core.Server.Entities.ServerNotifyRegisterEvent.TextPrivate);

				TS3Connect.Invoke();

				// Set name.
				ClientModification mod = new ClientModification();
				mod.Nickname = Properties.Settings.Default.nickname;

				SimpleResponse resp = QueryRunner.UpdateCurrentQueryClient(mod);
				Console.WriteLine("ERROR: {0}", resp.IsErroneous);

				if (resp.IsErroneous)
				{
					Console.WriteLine("ERROR: {0}", resp.ErrorMessage);
					TS3ConnectError.Invoke();
				}

				aliveTimer.Elapsed += aliveTimer_Elapsed;

				aliveTimer.Enabled = true;
				aliveTimer.Start();

				checkTimer.Elapsed += checkTimer_Elapsed;

				checkTimer.Enabled = true;
				checkTimer.Start();
			}
			else
			{
				Console.WriteLine(serverInfoResponse.ErrorMessage);
			}
		}

		void checkTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			//Check if connected.
			//Get connected users out of maximum.
			//See if it's changed since last check.
			//Invoke Event

			if (Dispatcher.IsConnected)
			{
				ServerInfoResponse serverInfoResponse = QueryRunner.GetServerInfo();

				if (!serverInfoResponse.IsErroneous)
				{
					int max = Convert.ToInt32(serverInfoResponse.MaximumClientsAllowed);
					int curr = Convert.ToInt32(serverInfoResponse.NumberOfClientsOnline);

					if (max != maxUsers || curr != currUsers)
					{
						maxUsers = max;
						currUsers = curr;

						TS3Update.Invoke();
					}

				}

			}

		}

		void aliveTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{

			if (QueryRunner != null)
			{
				Console.WriteLine("KEEP ALIVE");

				QueryRunner.SendWhoAmI();
			}

		}

		public void Dispatcher_ReadyForSendingCommands(object sender, EventArgs e)
		{
			Auth();
		}

		public void Dispatcher_ServerClosedConnection(object sender, EventArgs e)
		{
			//frm.Log("Server has closed the connection!");

			aliveTimer.Enabled = false;
			aliveTimer.Stop();

			TS3Disconnect.Invoke();

		}

		public void Dispatcher_SocketError(object sender, SocketErrorEventArgs e)
		{
			switch (e.SocketError)
			{
				case SocketError.ConnectionReset:
					break;
				case SocketError.TimedOut:
					string timeoutMessage = string.Format("Connection to '{0}' at query port '{1}' timed out.", Dispatcher.Host, Dispatcher.Port);
					//frm.Log(timeoutMessage);
					break;
				default:
					string unhandledErrorMessage = "Unhandled socket error occured: " + e.SocketError;
					//frm.Log(unhandledErrorMessage);
					break;
			}

			TS3Disconnect.Invoke();
			//SetConnectable();
		}

		public void Dispatcher_BanDetected(object sender, TS3QueryLib.Core.Common.EventArgs<SimpleResponse> e)
		{
			Console.WriteLine("You're banned from the server: " + e.Value.BanExtraMessage);
			Disconnect();
		}


		private void Dispatcher_NotificationReceived(object sender, TS3QueryLib.Core.Common.EventArgs<String> e)
		{
			//frm.Log("NOTIFICATION");
			//Console.WriteLine("CHAT: {0}", e.Value);
			string[] formNote = formatNotification(e.Value.ToString());

			//Log(formNote[0] + " : " + formNote[1]);
			if (formNote[0] != "")
			{
				bool ret = handleChatMsg(formNote);
				//' Debug.Print("FORMNOTE 1: " + formNote(1).ToString)
				//Console.WriteLine("RET: {0}", ret);

				//Is it a command? (Prefixed with .?)
				if (ret == true)
				{
					//lastCommand = 
					string formMsg = formatMsg(formNote[2]);

					Console.WriteLine("FORM NOTE: {0}", formNote[2]);

					//foreach (string str in formNote) {
					//Console.WriteLine("FORM: {0}", str);
					//}

					string clID = formNote[3].Trim();

					string clName = formNote[4].Trim();
					string clUID = formNote[5].Trim();


					string cmd = formMsg.Substring(formMsg.IndexOf(".") + 1);
					string[] strSep = new string[] { " " };

					string[] spl = cmd.Split(strSep, StringSplitOptions.RemoveEmptyEntries);
					//Console.WriteLine("SPL: {0}", spl[0]);
					string[] chatArgs = formMsg.Split(strSep, StringSplitOptions.RemoveEmptyEntries);

					string[] qtArgs = gFuncs.quoteArgs(chatArgs);

					cmdFuncs.coreMsgFunc(gFuncs.MsgType.TS3, qtArgs, clID, clName, clUID, QueryRunner);

				}
				else
				{
					//frm.Log("EMPTY");
				}
			}
		}

		public string[] formatNotification(string str)
		{
			string[] strSep = new string[] { " " };
			string[] spl;
			spl = str.Split(strSep, StringSplitOptions.RemoveEmptyEntries);
			string[] ret = new string[] { };
			//Console.WriteLine("SPL: {0}", spl[0]);

			Array.Resize(ref ret, ret.Length + 1);
			ret[0] = "0";

			if (spl[0] == "notifytextmessage")
			{
				string targetMode = spl[1].Substring(spl[1].IndexOf("=") + 1);
				string msg = spl[2].Substring(spl[2].IndexOf("=") + 1);
				string target = "";
				string clID = spl[3].Substring(spl[3].IndexOf("=") + 1);
				string clName = spl[4].Substring(spl[4].IndexOf("=") + 1);
				string clUID = spl[5].Substring(spl[5].IndexOf("=") + 1);

				//Console.WriteLine("WAT: " + spl[3].Substring(0, spl[3].IndexOf("=")));

				if (spl[3].Substring(0, spl[3].IndexOf("=")) == "target")
				{
					//Console.WriteLine("TARGET");

					Array.Resize(ref ret, 7);

					target = clID;
					clID = clName;
					clName = clUID;
					clUID = spl[6].Substring(spl[6].IndexOf("=") + 1);

					ret[0] = "chat";

					if (targetMode == "1")
					{
						ret[1] = "client";
					}
					else if (targetMode == "2")
					{
						ret[1] = "channel";
					}
					else if (targetMode == "3")
					{
						ret[1] = "server";
					}

					ret[2] = msg;
					ret[3] = clID;
					ret[4] = clName;
					ret[5] = clUID;
					ret[6] = target;

				}
				else
				{
					//Console.WriteLine("NO TARGET");
					//ReDim ret(5)
					Array.Resize(ref ret, 6);

					ret[0] = "chat";

					if (targetMode == "1")
					{
						ret[1] = "client";
					}
					else if (targetMode == "2")
					{
						ret[1] = "channel";
					}
					else if (targetMode == "3")
					{
						ret[1] = "server";
					}

					ret[2] = msg;
					ret[3] = clID;
					ret[4] = clName;
					ret[5] = clUID;

				}


				//'1 = Client
				//'2 = Channel
				//'3 = Server

				//'Debug.Print("GOT ID: " + clID)

			}
			else if (spl[0] == "notifycliententerview")
			{

			}
			else if (spl[0] == "notifyclientleftview")
			{

			}

			return ret;
		}

		public string formatMsg(string msg)
		{
			string ret = "";

			string[] remove = { "\\s", "\\n", "\\", "url", "b", "u", "i" };

			int i = 0;

			//'Debug.Print("MSG: " + msg)

			while (i < remove.Length)
			{

				if (i == 0)
				{
					msg = msg.Replace(remove[i], " ");
				}
				else if (i >= 1 && i <= 2)
				{
					msg = msg.Replace(remove[i], "");
				}
				else
				{
					msg = msg.Replace("[" + remove[i] + "]", "");
					msg = msg.Replace("[/" + remove[i] + "]", "");

					msg = msg.Replace("[" + remove[i].ToLower() + "]", "");
					msg = msg.Replace("[/" + remove[i].ToLower() + "]", "");

				}


				i = i + 1;
			}

			//' Debug.Print("MSG: " + msg)

			ret = msg;

			return ret;
		}

		public bool handleChatMsg(string[] args)
		{
			bool ret;

			if (args[2].StartsWith(".") == true)
			{

				ret = true;

			}
			else
			{
				ret = false;
			}

			return ret;
		}

		public bool checkClientID(TS3QueryLib.Core.Server.QueryRunner queryrun, uint id)
		{
			bool ret = false;

			bool clChk = queryrun.GetClientList().IsErroneous;


			if (clChk == false)
			{

				System.Collections.Generic.List<ClientListEntry> clList = queryrun.GetClientList().Values;

				// Debug.Print("LEN: " + clArr.Length().ToString())

				foreach (ClientListEntry cl in clList)
				{
					//Debug.Print("CLARR: " + clList.Item(i).ClientId.ToString)
					if (cl.ClientId == id)
					{
						ret = true;
					}

				}

			}

			return ret;
		}

		public bool checkChannelID(TS3QueryLib.Core.Server.QueryRunner queryrun, uint id)
		{
			bool ret = false;



			bool chChk = queryrun.GetChannelList().IsErroneous;


			if (chChk == false)
			{
				System.Collections.Generic.List<ChannelListEntry> chList = queryrun.GetChannelList().Values;

				// Debug.Print("LEN: " + clArr.Length().ToString())

				int i = 0;

				foreach (ChannelListEntry cl in chList)
				{
					//Debug.Print("CLARR: " + clList.Item(i).ClientId.ToString)

					if (cl.ChannelId == id)
					{
						ret = true;
						break;
					}

					i += 1;
				}

			}

			return ret;
		}



	}

	#endregion

}
