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
            BrowseClients();
        }

        void BrowseClients()
        {
            string qry = "select name as 'Клиент',phone as 'Телефон',address as 'Адрес',datereg as 'Дата регистрации' from clients";
            if (db.State != ConnectionState.Open)
                db.Open();
            SQLiteCommand cmd = new SQLiteCommand(qry, db);
            SQLiteDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dgvClients.ColumnCount = 0;
                dgvClients.ColumnCount = dr.FieldCount;
                foreach(DataGridViewColumn col in dgvClients.Columns)
                    col.HeaderText = dr.GetName(col.Index);
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

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            using(var newClient = new ClientNewEdit(db))
            {
                this.Hide();
                newClient.ShowDialog();
                BrowseClients();
                this.Show();
            }
        }

        private void dgvClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0)
                MessageBox.Show("Выберите строку данных для редактирования");
            else {
                int idx = dgvClients.SelectedRows[0].Index;
                string name = dgvClients[0, idx].Value.ToString();
                string phone = dgvClients[1, idx].Value.ToString();
                string address = dgvClients[2, idx].Value.ToString();
                string datereg = dgvClients[3, idx].Value.ToString();
                using (var editClient = new ClientNewEdit(db,name,phone,address,datereg))
                {
                    this.Hide();
                    editClient.ShowDialog();
                    BrowseClients();
                    this.Show();
                }
            }
        }
    }
}
