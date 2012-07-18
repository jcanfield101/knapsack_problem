using System;
using System.Collections.Generic;
using System.Text;

namespace BinPack2
{
    // Represents each item to be packed
    public class Item
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public bool Packed { get; set; }

        public Item(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}
