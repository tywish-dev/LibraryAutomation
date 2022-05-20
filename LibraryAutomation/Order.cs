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
    public partial class Order : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        public Order()
        {
            InitializeComponent();
        }
        void getBooks()
        {
            conn.Open();

            string getBook = "Select * From Book";
            da = new SqlDataAdapter(getBook, conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
        }
        private void Order_Load(object sender, EventArgs e)
        {
            string cs = "Data Source=.;Initial Catalog=Library;Integrated Security=True";
            conn = new SqlConnection(cs);

            getBooks();
        }
        int j = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string insert = "Insert Into dbo.[Order] (customerId, bookId) Values (@customerId, @bookId)";
                cmd = new SqlCommand(insert, conn);

                cmd.Parameters.AddWithValue("@customerId", CustomerLogin.cId);
                cmd.Parameters.AddWithValue("@bookId", dataGridView1.Rows[j].Cells[0].Value.ToString());

                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                throw;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            j = e.RowIndex;
        }
    }
}
