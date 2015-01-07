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
	public partial class Admin : Form
	{
		IceBot.gFuncs gFuncs = new IceBot.gFuncs();
		IceBot.userFuncs userFuncs = new IceBot.userFuncs();

		private const int WM_NCHITTEST = 0x84;
		private const int HTCLIENT = 0x1;
		private const int HTCAPTION = 0x2;

		protected override void WndProc(ref Message message)
		{
			base.WndProc(ref message);

			if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
				message.Result = (IntPtr)HTCAPTION;
		}



		public DataTable userTbl = new DataTable();



		public Admin()
		{
			InitializeComponent();

			userTbl.Columns.Add("Username", typeof(string));
			userTbl.Columns.Add("Account ID", typeof(string));
			userTbl.Columns.Add("Admin", typeof(string));

			foreach(DataRow user in userFuncs.sqlUsers.Rows)
			{
				string adm = "False";

				if (user["u_admin"].ToString() == "1")
				{
					adm = "True";
				}

				userTbl.Rows.Add(user["u_username"].ToString(), user["u_acctid"].ToString(), adm);
			}

			dataUserList.DataSource = userTbl;
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
