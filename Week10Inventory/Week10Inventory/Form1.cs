﻿using Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week10Inventory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form1_Load(this, EventArgs.Empty);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string filePath = @"C:\Users\52687\Desktop\grocery_inventory_itemss.txt";
                if (File.Exists(filePath))
                {
                    List<InventoryItem> items = ReadItemsFromFile(filePath);
                    Productlist.DataSource = items;
                    Productlist.DisplayMember = "FormattedInfo";
                }
                else
                {
                    MessageBox.Show("The file 'grocery_inventory_items.txt' does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load items: " + ex.Message);
            }
        }

        private List<InventoryItem> ReadItemsFromFile(string filePath)
        {
            List<InventoryItem> items = new List<InventoryItem>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine("Line read from file: " + line);
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        int itemNo = int.Parse(parts[0]);
                        string description = parts[1];
                        decimal price = decimal.Parse(parts[2]);
                        Console.WriteLine($"Parsed item: ItemNo={itemNo}, Description={description}, Price={price}");
                        items.Add(new InventoryItem { ItemNo = itemNo, Description = description, Price = price });
                    }
                }
            }

            return items;
        }
        private void Productlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Form2 addItemForm = new Form2();
            var result = addItemForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                RefreshItemsList();
            }
        }
        public void RefreshItemsList()
        {
            List<InventoryItem> items = InventoryDB.GetItems();


            Productlist.DataSource = null;

            Productlist.DisplayMember = "FormattedInfo";
            Productlist.ValueMember = "ItemNo";
            Productlist.DataSource = items;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Productlist.SelectedItem is InventoryItem selectedItem)
            {
                var confirmResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var items = Productlist.DataSource as List<InventoryItem>;
                    items.Remove(selectedItem);
                    InventoryDB.SaveItems(items);
                    RefreshItemsList();
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete first.");
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}