using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;
using Dapper;


namespace sqlShop
{
    public partial class methods : Form
    {
        public methods()
        {
            InitializeComponent();
        }

        private void products_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void products_VisibleChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select rashod.kolvo,rashod.dataras from rashod where kolvo = (select max(kolvo) from rashod)");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select tovary.tovar, tovary.zena from tovary where zena = (select max(zena) from tovary)");
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select rashod.kolvo,rashod.dataras, pokupat.fio,pokupat.tel from rashod, pokupat where (rashod.npok = pokupat.npok) and kolvo = (select max(kolvo) from rashod)");
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select pokupat.fio,pokupat.tel from pokupat where exists(select rashod.npok from rashod where rashod.npok = pokupat.npok)");
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select rashod.dataras, count(rashod.npok) from rashod group by rashod.dataras");
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select tovary.tovar, coalesce((select sum(prihod.kolvo) from prihod where tovary.tovar = prihod.tovar),0) as prihod_kolvo from tovary");
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select tovary.tovar,coalesce((select sum(rashod.kolvo) from rashod where tovary.tovar = rashod.tovar),0) as rashod_kolvo from tovary");
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select tovary.tovar, coalesce((select sum(prihod.kolvo) from prihod where tovary.tovar = prihod.tovar),0) as prihod_kolvo, coalesce((select sum(rashod.kolvo) from rashod where tovary.tovar = rashod.tovar),0) as rashod_kolvo from tovary");
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select tovary.tovar, coalesce((select sum(prihod.kolvo) from prihod where tovary.tovar = prihod.tovar),0) as prihod_kolvo, coalesce((select sum(rashod.kolvo) from rashod where tovary.tovar = rashod.tovar),0) as rashod_kolvo, (coalesce((select sum(prihod.kolvo) from prihod where tovary.tovar = prihod.tovar),0) - coalesce((select sum(rashod.kolvo) from rashod where tovary.tovar = rashod.tovar),0)) as ostatok from tovary");
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select * from rashod where rashod.dataras = '" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "'");
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
