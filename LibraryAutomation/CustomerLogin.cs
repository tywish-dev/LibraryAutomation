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
    public partial class CustomerLogin : Form
    {
        public static int cId;

        SqlConnection conn;
        SqlCommand cmd;
        SqlCommand cmd2;
        SqlDataReader dr;
        SqlDataReader newDr;
        public CustomerLogin()
        {
            InitializeComponent();
        }

        private void CustomerLogin_Load(object sender, EventArgs e)
        {
            string cs = "Data Source=.;Initial Catalog=Library;Integrated Security=True";
            conn = new SqlConnection(cs);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            string username = textBox1.Text;
            string password = textBox2.Text;

            string login = "Select * from Customer where username='" + username + "' and password='" + password + "'";

            cmd = new SqlCommand(login, conn);

            getId();
            
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                OptionSelect opt = new OptionSelect();
                opt.ShowDialog();
            }
            else
            {
                MessageBox.Show("Incorrect username or password!");
            }

            conn.Close();
        }
        void getId()
        {
            string getid = "Select id from Customer where Customer.username='" + textBox1.Text + "' and Customer.password='" + textBox2.Text +"'";

            cmd2 = new SqlCommand(getid, conn);

            string id = cmd2.ExecuteScalar().ToString();
            id.Trim();

            // Providing customer id info for order and myOrders
            cId = Convert.ToInt32(id);
        }
    }
}
