using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;


namespace sqlShop
{
    public partial class rashod : Form
    {
        public rashod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new FbConnection(Services.connection_string.ToString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new FbCommand("insert into rashod(nras,tovar,dataras,kolvo,npok) values(" +
                        textBox1.Text + ",'" + listBox1.SelectedItem + "','" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "', " + textBox2.Text + "," + textBox3.Text + ");",
                        connection,
                        transaction)
                            )
                    {
                        MessageBox.Show(command.CommandText);
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
            }
            dataGridView1.DataSource = Services.GetTable_Rashod();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new FbConnection(Services.connection_string.ToString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new FbCommand("Delete from rashod where nras = " + textBox4.Text, connection, transaction))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, "SQL Error"); }
                    }
                }
            }
            dataGridView1.DataSource = Services.GetTable_Rashod();
        }

        private void rashod_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void rashod_VisibleChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Services.GetTable_Rashod();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Services.GetArrayOfProducts());
        }
    }
}
