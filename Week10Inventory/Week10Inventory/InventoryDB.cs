﻿using System;
using System.Collections.Generic;
using System.IO;
using Week10Inventory;

namespace Inventory
{
    public static class InventoryDB
    {
        private static readonly string Path = "C:\\Users\\52687\\Desktop\\grocery_inventory_itemss.txt";
        private const string Delimiter = "|";

        public static List<InventoryItem> GetItems()
        {
            var items = new List<InventoryItem>();
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
                return items;
            }

            using (var textIn = new StreamReader(new FileStream(Path, FileMode.Open, FileAccess.Read)))
            {
                string row;
                while ((row = textIn.ReadLine()) != null)
                {
                    string[] columns = row.Split(new string[] { Delimiter }, StringSplitOptions.None);
                    if (columns.Length == 3)
                    {
                        var item = new InventoryItem
                        {
                            ItemNo = int.Parse(columns[0]),
                            Description = columns[1],
                            Price = decimal.Parse(columns[2])
                        };
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public static void SaveItems(List<InventoryItem> items)
        {
            using (var textOut = new StreamWriter(new FileStream(Path, FileMode.Create, FileAccess.Write)))
            {
                foreach (var item in items)
                {
                    textOut.Write(item.ItemNo + Delimiter);
                    textOut.Write(item.Description + Delimiter);
                    textOut.WriteLine(item.Price);
                }
            }
        }
    }
}