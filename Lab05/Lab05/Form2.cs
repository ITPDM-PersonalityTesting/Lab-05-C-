using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace Lab05
{
    public partial class Form2 : Form
    {
        DataSet order_ds;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(CultureInfo cl)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = cl;
            Thread.CurrentThread.CurrentUICulture = cl;

            ResourceManager rmg = new ResourceManager("Lab05.Form2", 
                System.Reflection.Assembly.GetExecutingAssembly());

            this.Text = this.Text + "-" + DateTime.Now.ToLongDateString();
            button1.Text = rmg.GetString("button1.Text");
            button2.Text = rmg.GetString("button2.Text");
            button3.Text = rmg.GetString("button3.Text");
            label1.Text = rmg.GetString("label1.Text");
            label2.Text = rmg.GetString("label2.Text");
            label3.Text = rmg.GetString("label3.Text");
            label4.Text = rmg.GetString("label4.Text");
            label5.Text = rmg.GetString("label5.Text");
            label6.Text = rmg.GetString("label6.Text");
            label7.Text = rmg.GetString("label7.Text");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'itemDBDataSet.Items' table. You can move, or remove it, as needed.
            this.itemsTableAdapter.Fill(this.itemDBDataSet.Items);
            pictureBox1.DataBindings.Add("ImageLocation", itemsBindingSource,
                "Image", false, DataSourceUpdateMode.Never);


            order_ds = new DataSet();
            DataTable orderTb1 = new DataTable("Order");
            DataColumn c1 = new DataColumn("ItemNo",typeof(int));
            DataColumn c2 = new DataColumn("Price", typeof(double));
            DataColumn c3 = new DataColumn("Quantity", typeof(int));
            DataColumn c4 = new DataColumn("Total", typeof(double));
            c4.Expression = "Price * Quantity";
            orderTb1.Columns.Add(c1);
            orderTb1.Columns.Add(c2);
            orderTb1.Columns.Add(c3);
            orderTb1.Columns.Add(c4);
            orderTb1.PrimaryKey = new DataColumn[] { c1 };
            order_ds.Tables.Add(orderTb1);
            dataGridView2.DataSource = order_ds.Tables[0];
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int id = (int)itemsTableAdapter.getMaxId();
            textBox1.Text = "" + (id + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            itemsTableAdapter.Insert(
                int.Parse(textBox1.Text),
                textBox2.Text,
                int.Parse(textBox3.Text),
                decimal.Parse(textBox4.Text),
                pictureBox1.ImageLocation);

            MessageBox.Show("Item Added!");
            this.itemsTableAdapter.Fill(this.itemDBDataSet.Items);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            itemsTableAdapter.Update(
                textBox2.Text,
                int.Parse(textBox3.Text),
                decimal.Parse(textBox4.Text),
                pictureBox1.ImageLocation,
                int.Parse(textBox1.Text));

            MessageBox.Show("Item Updated!");
            this.itemsTableAdapter.Fill(this.itemDBDataSet.Items);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            itemsTableAdapter.Delete(int.Parse(textBox1.Text));
            MessageBox.Show("Item Remove!");
            this.itemsTableAdapter.Fill(this.itemDBDataSet.Items);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataRow r = order_ds.Tables[0].NewRow();
            r[0] = int.Parse(textBox1.Text);
            r[1] = double.Parse(textBox4.Text);
            r[2] = int.Parse(textBox7.Text);
            order_ds.Tables[0].Rows.Add(r);
            MessageBox.Show("Item added to the order.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataRow r = order_ds.Tables[0].Rows.Find(
                dataGridView2.SelectedRows[0].Cells[0].Value);
            order_ds.Tables[0].Rows.Remove(r);
            MessageBox.Show("Item Remove from the order.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double tot=0;
            String orderDes = "";

            foreach (DataRow r in order_ds.Tables[0].Rows) {
                orderDes = orderDes + " " + r[0] + " " + r[2];
                tot = tot + (double)r[3];
            }

            label8.Text = "Total :" + tot;
            ordersTableAdapter1.Insert(textBox5.Text,textBox6.Text, orderDes, DateTime.Now,tot);
            MessageBox.Show("Order placed.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            order_ds.Tables[0].Rows.Clear();
            label8.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
