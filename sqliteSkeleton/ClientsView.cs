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
    public partial class ClientsView : Form
    {
        SQLiteConnection db;
        public ClientsView(SQLiteConnection db)
        {
            InitializeComponent();
            this.db = db;
            string qry = "select * from clients";
            if (db.State == ConnectionState.Closed)
                db.Open();
            SQLiteCommand cmd = new SQLiteCommand(qry, db);
            SQLiteDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dgvClients.ColumnCount = dr.FieldCount;
                while (dr.Read())
                {
                    string[] row = new string[dr.FieldCount];
                    for (int i = 0; i < row.Length; i++)
                        row[i] = dr.GetString(i);
                    dgvClients.Rows.Add(row);
                }
                dr.Close();
            }
            db.Close();
        }
    }
}
