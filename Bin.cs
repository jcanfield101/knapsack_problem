using System;
using System.Collections.Generic;
using System.Text;

namespace BinPack2
{
    // Represents bin that holds packed items
    public class Bin
    {
        // Packed items
        public ItemSet Items { get; set; }

        // Weight capacity of bin
        public int Capacity { get; set; }

        // Total weight of all packed items
        public int Weight
        {
            get
            {
                return Items.Weight;
            }
        }

        // Constructor
        public Bin(int capacity)
        {
            Items = new ItemSet();
            Capacity = capacity;
        }

        // Generate string representing output for bin as defined in project handout
        public string Print()
        {
            string output = "";

            if (Items.Count > 0)
            {
                foreach (Item item in Items)
                {
                    output += String.Format("{0}={1}, ", item.Name, item.Weight);
                }
                output = output.Substring(0, output.Length - 2); // Get rid of trailing comma and space
            }

            output += String.Format("] = {0}/{1}\r\n", Weight, Capacity);

            return output;
        }
    }
}
