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
            try
            {
                if (textBox2.Text == "" | textBox3.Text == "" | listBox1.SelectedItem == null) throw new Exception("Заполните поля");

                using (var connection = new FbConnection(Services.connection_string.ToString()))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = new FbCommand("insert into rashod(nras,tovar,dataras,kolvo,npok) values(" +
                            Services.GetGetCurrentIdfromRashod() + ",'" + listBox1.SelectedItem + "','" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "', " + textBox2.Text + "," + textBox3.Text + ");",
                            connection,
                            transaction)
                                )
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select * from rashod order by nras");
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
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select * from rashod order by nras");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            dataGridView1.DataSource = Services.GetTableBySqlCommand("select * from rashod order by nras");
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Services.GetArrayOfProducts());
            listBox2.Items.Clear();
            listBox2.Items.AddRange(Services.GetArrayOfProducts());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //if (textBox4.Text == "") throw new Exception("Заполните поля");
                using (var connection = new FbConnection(Services.connection_string.ToString()))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = new FbCommand("update rashod set tovar = '" + listBox2.SelectedItem + "', dataras = '" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "', kolvo = " + textBox6.Text + ", npok = " + textBox5.Text + " where nras =" + textBox1.Text, connection, transaction))
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
                dataGridView1.DataSource = Services.GetTableBySqlCommand("select * from rashod order by nras");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
