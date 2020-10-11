using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;


namespace sqlShop
{
    public partial class clients : Form
    {
        public clients()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" | textBox3.Text == "") throw new Exception("Заполните поля");

                using (var connection = new FbConnection(Services.connection_string.ToString()))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = new FbCommand("insert into pokupat(npok,fio,tel) values (" + Services.GetGetCurrentIdfromClients() + ",'" + textBox2.Text + "'," + textBox3.Text + ");",
                            connection,
                            transaction)
                                )
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
                dataGridView1.DataSource = Services.GetTable_Clients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == "") throw new Exception("Заполните поля");

                using (var connection = new FbConnection(Services.connection_string.ToString()))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = new FbCommand("Delete from pokupat where npok = " + textBox4.Text, connection, transaction))
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
                dataGridView1.DataSource = Services.GetTable_Clients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clients_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void clients_VisibleChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Services.GetTable_Clients();
        }
    }
}
