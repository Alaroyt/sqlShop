using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;


namespace sqlShop
{
    public partial class prihod : Form
    {
        public prihod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" | listBox1.SelectedItem == null) throw new Exception("Заполните поля");

                using (var connection = new FbConnection(Services.connection_string.ToString()))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = new FbCommand("insert into prihod(npr,tovar,data_pr,kolvo) values(" +
                            Services.GetGetCurrentIdfromPrihod() + ",'" + listBox1.SelectedItem + "','" + dateTimePicker1.Value.ToString("dd.MM.yyyy") + "', " + textBox2.Text + ");",
                            connection,
                            transaction)
                                )
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
                dataGridView1.DataSource = Services.GetTable_Prihod();
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
                        using (var command = new FbCommand("Delete from prihod where npr = " + textBox4.Text, connection, transaction))
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
                dataGridView1.DataSource = Services.GetTable_Prihod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void prihod_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void prihod_VisibleChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Services.GetTable_Prihod();
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
                        using (var command = new FbCommand("update  prihod set  tovar = '" + listBox2.SelectedItem + "', data_pr = '" + dateTimePicker2.Value.ToString("dd.MM.yyyy") + "', kolvo = " + textBox1.Text + " where npr = " + textBox3.Text, connection, transaction))
                        {

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
                dataGridView1.DataSource = Services.GetTable_Prihod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
