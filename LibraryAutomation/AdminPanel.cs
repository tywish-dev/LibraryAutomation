using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LibraryAutomation
{
    public partial class AdminPanel : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            string cs = "Data Source=.;Initial Catalog=Library;Integrated Security=True";
            conn = new SqlConnection(cs);
            getBook();
            getCustomer();
        }

        void getBook()
        {
            conn.Open();

            string select = "Select * from Book";
            da = new SqlDataAdapter(select, conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
        }

        void getCustomer()
        {
            conn.Open();

            string select = "Select * from Customer";
            da = new SqlDataAdapter(select, conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            conn.Close();
        }
        //
        //  Book
        //
        int i = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();

            string update = "Update Book Set bName=@bName, bWriter=@bWriter, price=@price, pageCount=@pageCount where id=@id";
            cmd = new SqlCommand(update, conn);

            cmd.Parameters.AddWithValue("@bname", textBox1.Text);
            cmd.Parameters.AddWithValue("@bWriter", textBox2.Text);
            cmd.Parameters.AddWithValue("@price", textBox3.Text);
            cmd.Parameters.AddWithValue("@pageCount", textBox4.Text);
            cmd.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value.ToString());

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Successfully Updated.");

            getBook();
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }
        void deleteBook(int id)
        {
            conn.Open();

            string delete = "Delete from Book where id=@id";
            cmd = new SqlCommand(delete, conn);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Successfully deleted.");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);

                deleteBook(id);

                getBook();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    string insert = "Insert into Book (bName, bWriter, price, pageCount) Values (@bName, @bWriter, @price, @pageCount)";
                    cmd = new SqlCommand(insert, conn);

                    cmd.Parameters.AddWithValue("@bname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@bWriter", textBox2.Text);
                    cmd.Parameters.AddWithValue("@price", textBox3.Text);
                    cmd.Parameters.AddWithValue("@pageCount", textBox4.Text);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    getBook();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                throw;
            }
        }
        //
        // Customer
        //
        int j = 0;
        private void button7_Click(object sender, EventArgs e)
        {
            conn.Open();

            string update = "Update Customer Set username=@username, password=@password where id=@id";
            cmd = new SqlCommand(update, conn);

            cmd.Parameters.AddWithValue("@username", textBox8.Text);
            cmd.Parameters.AddWithValue("@password", textBox7.Text);
            cmd.Parameters.AddWithValue("@id", dataGridView2.Rows[j].Cells[0].Value.ToString());

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Successfully Updated.");

            getCustomer();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            j = e.RowIndex;
            textBox8.Text = dataGridView2.Rows[j].Cells[1].Value.ToString();
            textBox7.Text = dataGridView2.Rows[j].Cells[2].Value.ToString();
        }
        void deleteCustomer(int id)
        {
            conn.Open();

            string delete = "Delete from Customer where id=@id";
            cmd = new SqlCommand(delete, conn);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Successfully deleted.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);

                deleteCustomer(id);

                getCustomer();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    string insert = "Insert into Customer (username, password) Values (@username, @password)";
                    cmd = new SqlCommand(insert, conn);

                    cmd.Parameters.AddWithValue("@username", textBox8.Text);
                    cmd.Parameters.AddWithValue("@password", textBox7.Text);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    getCustomer();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                throw;
            }
        }
    }
}
