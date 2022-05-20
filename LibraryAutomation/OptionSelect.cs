using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryAutomation
{
    public partial class OptionSelect : Form
    {
        public OptionSelect()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyOrders myOrders = new MyOrders();
            myOrders.ShowDialog();
        }
    }
}
