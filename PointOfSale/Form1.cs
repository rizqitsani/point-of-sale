using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public double cost_Of_Items()
        {
            Double sum = 0;

            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }

            return sum;
        }

        private double get_Total_Cost()
        {
            Double tax = 0.1, taxTotal;
            taxTotal = cost_Of_Items() * tax;
            if(dataGridView1.Rows.Count > 0)
            {
                label5.Text = String.Format("{0:c2}", taxTotal);
                label4.Text = String.Format("{0:c2}", cost_Of_Items());
                label6.Text = String.Format("{0:c2}", cost_Of_Items() + taxTotal);
            }

            return cost_Of_Items() + taxTotal;
        }

        public void get_Change()
        {
            Double cost, paid, change;
            cost = Convert.ToInt32(get_Total_Cost());
            paid = Convert.ToInt32(label8.Text);
            change = cost - paid;
            if (dataGridView1.Rows.Count > 0)
            {
                label7.Text = String.Format("{0:c2}", change);
            }
        }

        // Print
        Bitmap bitmap;
        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(bitmap, 0, 0);   
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        // Reset
        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
                label7.Text = "";
                label8.Text = "0";
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                comboBox1.Text = "";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void NumbersOnly(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (label8.Text == "0")
            {
                label8.Text = "";
                label8.Text = b.Text;
            }
            else
            {
                label8.Text = label8.Text + b.Text;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label8.Text = "0";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                MessageBox.Show("Pilih metode pembayaran!");
            }
            else
            {
                if (comboBox1.Text == "Tunai")
                {
                    get_Change();
                } else
                {
                    label7.Text = "";
                    label8.Text = "0";
                }
                
                Transaction trans = new Transaction(get_Total_Cost(), comboBox1.Text.Trim());
                DbTransaction.AddTransaction(trans);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if(Double.Parse((string) row.Cells[1].Value) > 1)
                {
                    Double newQty = Double.Parse((string)row.Cells[1].Value) - 1;
                    Double cost = (double)row.Cells[2].Value / Convert.ToDouble(row.Cells[1].Value);
                    row.Cells[1].Value = newQty.ToString();
                    row.Cells[2].Value = newQty * cost;
                }
                else
                {
                    dataGridView1.Rows.Remove(row);
                }
            }

            if (comboBox1.Text == "Tunai")
            {
                get_Change();
            }
            else
            {
                label7.Text = "";
                label8.Text = "0";
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Double cost = 15000;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string) dataGridView1.Rows[i].Cells[0].Value == "Pizza")
                    {
                        newData = false;
                        
                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Pizza", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Pizza", "1", cost);
            }

            get_Total_Cost();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Double cost = 5000;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells[0].Value == "Dorayaki")
                    {
                        newData = false;
                        
                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Dorayaki", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Dorayaki", "1", cost);
            }

            get_Total_Cost();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Double cost = 12000;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells[0].Value == "Pancake")
                    {
                        newData = false;

                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Pancake", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Pancake", "1", cost);
            }

            get_Total_Cost();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Double cost = 7000;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells[0].Value == "Hotdog")
                    {
                        newData = false;

                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Hotdog", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Hotdog", "1", cost);
            }

            get_Total_Cost();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Double cost = 20000;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells[0].Value == "Spaghetti")
                    {
                        newData = false;

                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Spaghetti", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Spaghetti", "1", cost);
            }

            get_Total_Cost();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Double cost = 6500;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells[0].Value == "Sandwich")
                    {
                        newData = false;

                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Sandwich", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Sandwich", "1", cost);
            }

            get_Total_Cost();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Double cost = 8500;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells[0].Value == "Taco")
                    {
                        newData = false;

                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Taco", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Taco", "1", cost);
            }

            get_Total_Cost();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Double cost = 5000;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells[0].Value == "Orange Juice")
                    {
                        newData = false;

                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Orange Juice", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Orange Juice", "1", cost);
            }

            get_Total_Cost();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Double cost = 8000;
            if (dataGridView1.Rows.Count > 1 && dataGridView1.Rows != null)
            {
                bool newData = true;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells[0].Value == "Milkshake")
                    {
                        newData = false;

                        Double newQty = Double.Parse((string)dataGridView1.Rows[i].Cells[1].Value) + 1;
                        dataGridView1.Rows[i].Cells[1].Value = newQty.ToString();
                        dataGridView1.Rows[i].Cells[2].Value = newQty * cost;
                        break;
                    }
                }

                if(newData)
                {
                    dataGridView1.Rows.Add("Milkshake", "1", cost);
                }
            }
            else
            {
                dataGridView1.Rows.Add("Milkshake", "1", cost);
            }

            get_Total_Cost();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Tunai");
            comboBox1.Items.Add("Kredit");
        }
    }
}
