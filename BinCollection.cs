using System;
using System.Collections.Generic;
using System.Text;

namespace BinPack2
{
    // Represents collection of bins
    public class BinCollection : List<Bin>
    {
        public BinCollection(int count, int capacity)
        {
            //this = new List<Bin>();

            // Initialize count of bins
            for (int i = 0; i < count; i++)
            {
                this.Add(new Bin(capacity));
            }
        }
    }
}
