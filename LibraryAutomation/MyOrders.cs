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
    public partial class MyOrders : Form
    {

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        DataTable dt;
        SqlDataAdapter da;
        public MyOrders()
        {
            InitializeComponent();
        }

        private void MyOrders_Load(object sender, EventArgs e)
        {
            string cs = "Data Source=.;Initial Catalog=Library;Integrated Security=True";
            conn = new SqlConnection(cs);

            conn.Open();

            string book = "Select * from Book Inner Join dbo.[Order] as orderTbl on orderTbl.customerId='" + CustomerLogin.cId + "' AND orderTbl.bookId=Book.id ";

            da = new SqlDataAdapter(book, conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
        }
    }
}
