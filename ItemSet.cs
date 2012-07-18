using System;
using System.Collections.Generic;
using System.Text;

namespace BinPack2
{
    public class ItemSet : List<Item>
    {
        /// <summary>
        /// Get total weight of all items in ItemSet
        /// </summary>
        public int Weight
        {
            get
            {
                int result = 0;
                foreach (Item item in this)
                {
                    result += item.Weight;
                }
                return result;
            }
        }
    }
}
