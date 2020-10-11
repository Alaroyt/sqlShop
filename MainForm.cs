using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using FirebirdSql.Data;
using Dapper;

namespace sqlShop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Services.openConn();
        }
        products form_products = new products();
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!form_clients.Visible & !form_products.Visible & !form_rashod.Visible & !form_prihod.Visible)
                form_products.Show();
        }
        clients form_clients = new clients();
        private void button2_Click(object sender, EventArgs e)
        {
            if (!form_clients.Visible & !form_products.Visible & !form_rashod.Visible & !form_prihod.Visible)
                form_clients.Show();
        }
        rashod form_rashod = new rashod();
        private void button3_Click(object sender, EventArgs e)
        {
            if (!form_clients.Visible & !form_products.Visible & !form_rashod.Visible & !form_prihod.Visible)
                form_rashod.Show();
        }
        prihod form_prihod = new prihod();
        private void button4_Click(object sender, EventArgs e)
        {
            if (!form_clients.Visible & !form_products.Visible & !form_rashod.Visible & !form_prihod.Visible)
                form_prihod.Show();
        }

        private void MainForm_Enter(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Services.GetTable_Tovary();
            dataGridView2.DataSource = Services.GetTable_Clients();
            dataGridView3.DataSource = Services.GetTable_Prihod();
            dataGridView4.DataSource = Services.GetTable_Rashod();
        }
    }
}
