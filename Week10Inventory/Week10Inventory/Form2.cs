using Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week10Inventory
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void lblItemNo_Click(object sender, EventArgs e)
        {

        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }

        private void tbxItemNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                InventoryItem newItem = new InventoryItem()
                {
                    ItemNo = int.Parse(tbxItemNo.Text),
                    Description = tbxDescription.Text,
                    Price = decimal.Parse(tbxPrice.Text) 
                };

                var items = InventoryDB.GetItems();
                items.Add(newItem);
                InventoryDB.SaveItems(items);

                MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();

                Form1 form2 = new Form1();
                form2.RefreshItemsList();
                form2.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}