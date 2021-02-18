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
    public partial class ClientNewEdit : Form
    {
        SQLiteConnection db;
        string name, phone, address, datereg;
        bool isEdit;
        public ClientNewEdit(SQLiteConnection db)
        {
            InitializeComponent();
            this.db = db;
            this.name = "";
            this.phone = "";
            this.address = "";
            this.datereg = "";
            isEdit = false;
        }

        private void ClientNewEdit_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        public ClientNewEdit(SQLiteConnection db, string name, string phone, string address, string datereg)
        {
            InitializeComponent();
            this.db = db;
            this.name = name;
            this.phone = phone;
            this.address = address;
            this.datereg = datereg;
            isEdit = true;
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            PostData();
            if (isEdit)
                this.Close();
            else
                InitForm();
        }

        void PostData()
        {
            if (db.State != ConnectionState.Open)
                db.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = db;
            if (!isEdit)
            {
                // Вставка новой записи о клиенте
                cmd.CommandText = String.Format(@"insert into clients values('{0}','{1}','{2}','{3}');", 
                    tbName.Text, tbPhone.Text, tbAddress.Text, dtpDateReg.Text);
                cmd.ExecuteNonQuery();
            } else
            {
                // Изменение существующей записи о клиенте
                cmd.CommandText = String.Format(@"select rowid from clients where name='{0}' and phone='{1}' and address='{2}' and datereg='{3}'",
                    name,phone,address,datereg);
                SQLiteDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int rowid = dr.GetInt32(0);
                dr.Close();
                cmd.CommandText = String.Format(@"update clients set name='{0}',phone='{1}',address='{2}',datereg='{3}' where rowid={4}",
                    tbName.Text, tbPhone.Text, tbAddress.Text, dtpDateReg.Text, rowid);
                cmd.ExecuteNonQuery();
            }
            db.Close();
        }

        void InitForm()
        {
            tbName.Text = this.name;
            tbPhone.Text = this.phone;
            tbAddress.Text = this.address;
            dtpDateReg.Text = this.datereg;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
