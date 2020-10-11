using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;


namespace sqlShop
{
    public partial class products : Form
    {
        public products()
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
                    using (var command = new FbCommand("insert into tovary(tovar,edizm,zena) values ('" + textBox1.Text.ToLower() + "','" + textBox2.Text
                    + "'," + textBox3.Text + ");", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
            }
            dataGridView1.DataSource = Services.GetTable_Tovary();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Services.ArrayOfProducts);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new FbConnection(Services.connection_string.ToString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new FbCommand("Delete from tovary where tovar = '" + listBox1.SelectedItem + "'", connection, transaction))
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
            dataGridView1.DataSource = Services.GetTable_Tovary();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Services.ArrayOfProducts);
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
            dataGridView1.DataSource = Services.GetTable_Tovary();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Services.ArrayOfProducts);
        }
    }
}
