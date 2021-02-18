using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace sqliteSkeleton
{
    public partial class MainForm : Form
    {
        SQLiteConnection db;
        string connStr;
        public MainForm()
        {
            InitializeComponent();
            connStr = @"URI=file:data.sqlite3";
            db = new SQLiteConnection(connStr);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var clients = new ClientsView(db))
            {
                this.Hide();
                clients.ShowDialog();
                this.Show();
            }
        }
    }
}
