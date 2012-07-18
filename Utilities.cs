using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinPack2
{
    public static class Utilities
    {
        // Helper to split item attributes from file into Item class
        private static Item GetItemFromLine(string line)
        {
            Item result = new Item(line.Split(' ')[0], int.Parse(line.Split(' ')[1]));
            return result;
        }

        // Loads item list from file at path
        public static void LoadItemCollectionFromFile(string path, ref ItemSet items)
        {
            int count = 0;

            try
            {
                // Attempt to load file
                using (StreamReader reader = new StreamReader(path))
                {
                    items = new ItemSet();

                    // First line contains number of items
                    count = int.Parse(reader.ReadLine());

                    // Get each item
                    string line = "";
                    for (int i = 0; i < count; i++)
                    {
                        line = reader.ReadLine();
                        items.Add(GetItemFromLine(line));
                    }
                };
            }
            catch (Exception ex)
            {

            }
        }

        // Generates all the subsets of itemset
        public static void GenerateSubsets(ref ItemSet itemset, int count, ref ItemSetCollection subsets)
        {
            ItemSetCollection allSubsets = new ItemSetCollection();

            try
            {
                int subsetCount = (int)Math.Pow(2, count);
                for (int i = 0; i < subsetCount; i++)
                {
                    ItemSet subset = new ItemSet();
                    for (int bitIndex = 0; bitIndex < count; bitIndex++)
                    {
                        if (((i >> bitIndex) & 1) == 1)
                        {
                            subset.Add(itemset[bitIndex]);
                        }
                    }
                    allSubsets.Add(subset);
                }
                subsets = allSubsets;
            }
            catch (OutOfMemoryException oomex)
            {
                throw;
            }
        }

        public static ItemSet PackBin_1(ref ItemSet itemset, int count)
        {
            //ItemSetCollection allSubsets = new ItemSetCollection();
            //Bin bin = new Bin(50);
            ItemSet best = null;

            try
            {
                int subsetCount = (int)Math.Pow(2, count);
                for (int bits0 = 0; bits0 < subsetCount; bits0++)
                {
                    // Pick a subset of items for bin B0
                    ItemSet B0 = new ItemSet();
                    for (int i = 0; i < count; i++)
                    {
                        if (((bits0 >> i) & 1) == 1)
                        {
                            B0.Add(itemset[i]);
                        }
                    }

                    // Check for overload
                    if (B0.Weight > 50)
                        continue;

                    // Check for best
                    if ((best == null) || (B0.Count > best.Count))
                    {
                        best = B0;
                    }
                }
                return best;
            }
            catch (OutOfMemoryException oomex)
            {
                throw;
            }
        }

        public static List<ItemSet> PackBin_2(ref ItemSet itemset, int count)
        {
            List<ItemSet> best = null;

            try
            {
                int subsetCount = (int)Math.Pow(2, count);
                for (int bits0 = 0; bits0 < subsetCount; bits0++)
                {
                    // Pick a subset of items for bin B0
                    ItemSet B0 = new ItemSet();
                    for (int i = 0; i < count; i++)
                    {
                        if (((bits0 >> i) & 1) == 1)
                        {
                            //if (!itemset[i].Packed)
                            B0.Add(itemset[i]);
                        }
                    }

                    // Pick a subset of items for bin B1
                    for (int bits1 = 0; bits1 < subsetCount; bits1++)
                    {
                        ItemSet B1 = new ItemSet();
                        for (int i = 0; i < count; i++)
                        {
                            if (((bits1 >> i) & 1) == 1)
                            {
                                //itemset[i].Packed = true;
                                B1.Add(itemset[i]);
                            }
                        }

                        // Check for overload in any bin
                        if (B0.Weight > 50 || B1.Weight > 50)
                            continue;

                        // Check for any duplicate items in P
                        bool dupe = false;
                        foreach(Item item0 in B0){
                            foreach(Item item1 in B1)
                            {
                                if (item0 == item1)
                                {
                                    dupe = true;
                                    continue;
                                }
                            }
                            if (dupe)
                                continue;
                        }
                        if (dupe)
                            continue;

                        // Check for best
                        if ((best == null) || ((B0.Count + B1.Count) > (best[0].Count + best[1].Count)))
                        {
                            List<ItemSet> P = new List<ItemSet>();
                            P.Add(B0);
                            P.Add(B1);
                            best = P;
                        }
                    }
                }

                return best;
            }
            catch (OutOfMemoryException oomex)
            {
                throw;
            }
        }

        public static List<ItemSet> PackBin_3(ref ItemSet itemset, int count)
        {
            List<ItemSet> best = null;

            try
            {
                int subsetCount = (int)Math.Pow(2, count);
                for (int bits0 = 0; bits0 < subsetCount; bits0++)
                {
                    // Pick a subset of items for bin B0
                    ItemSet B0 = new ItemSet();
                    for (int i = 0; i < count; i++)
                    {
                        if (((bits0 >> i) & 1) == 1)
                        {
                            //if (!itemset[i].Packed)
                            B0.Add(itemset[i]);
                        }
                    }

                    // Pick a subset of items for bin B1
                    for (int bits1 = 0; bits1 < subsetCount; bits1++)
                    {
                        ItemSet B1 = new ItemSet();
                        for (int i = 0; i < count; i++)
                        {
                            if (((bits1 >> i) & 1) == 1)
                            {
                                //itemset[i].Packed = true;
                                B1.Add(itemset[i]);
                            }
                        }

                        // Pick a subset of items for bin B1
                        for (int bits2 = 0; bits2 < subsetCount; bits2++)
                        {
                            ItemSet B2 = new ItemSet();
                            for (int i = 0; i < count; i++)
                            {
                                if (((bits2 >> i) & 1) == 1)
                                {
                                    //itemset[i].Packed = true;
                                    B2.Add(itemset[i]);
                                }
                            }

                            // Check for overload in any bin
                            if (B0.Weight > 50 || B1.Weight > 50 || B2.Weight > 50)
                                continue;

                            // Check for any duplicate items in P
                            bool dupe = false;
                            foreach (Item item0 in B0)
                            {
                                foreach (Item item1 in B1)
                                {
                                    if (item0 == item1)
                                    {
                                        dupe = true;
                                        continue;
                                    }
                                }
                                if (dupe)
                                    continue;
                            }
                            if (dupe)
                                continue;
                            foreach (Item item1 in B1)
                            {
                                foreach (Item item2 in B2)
                                {
                                    if (item1 == item2)
                                    {
                                        dupe = true;
                                        continue;
                                    }
                                }
                                if (dupe)
                                    continue;
                            }
                            if (dupe)
                                continue;
                            foreach (Item item0 in B0)
                            {
                                foreach (Item item2 in B2)
                                {
                                    if (item0 == item2)
                                    {
                                        dupe = true;
                                        continue;
                                    }
                                }
                                if (dupe)
                                    continue;
                            }
                            if (dupe)
                                continue;

                            // Check for best
                            if ((best == null) || ((B0.Count + B1.Count + B2.Count) > (best[0].Count + best[1].Count + best[2].Count)))
                            {
                                List<ItemSet> P = new List<ItemSet>();
                                P.Add(B0);
                                P.Add(B1);
                                P.Add(B2);
                                best = P;
                            }
                        }
                    }
                }

                return best;
            }
            catch (OutOfMemoryException oomex)
            {
                throw;
            }
        }

        //// Packs the bins using exhaustive search to find best fit for each bin
        //public static void PackBins(int count, int capacity, ref BinCollection bins, ref ItemSetCollection subsets)
        //{
        //    ItemSet best = null;

        //    bins = new BinCollection(count, capacity);

        //    //subsets.ResetPacked();

        //    foreach (ItemSet set in subsets)
        //    {
        //        // Check for overload
        //        if (set.Weight <= capacity)
        //        {
        //            if ((best == null) || (set.Count > best.Count))
        //            {
        //                //if (!set.IsItemPacked())
        //                //{
        //                //    if (best != null)
        //                //        best.MarkItemsAsPacked(false); // Remove packed flags from each item
        //                //    set.MarkItemsAsPacked(true); // Mark each item as packed
        //                best = set;
        //                //}
        //            }
        //        }
        //    }

        //    //best.MarkItemsAsPacked(true);
        //    bins[0].Items = best;
        //}

        //// Generates string representing output of subsets
        //public static string PrintSubsets(ItemSetCollection subsets)
        //{
        //    string output = "";

        //    foreach (ItemSet set in subsets)
        //    {
        //        output += "{";
        //        if (set.Count > 0)
        //        {
        //            foreach (Item item in set)
        //            {
        //                output += string.Format("{0}={1},", item.Name, item.Weight);
        //            }
        //            output = output.Substring(0, output.Length - 1);
        //        }
        //        output += string.Format("}} {0}\r\n", set.Weight);
        //    }
        //    return output;
        //}
    }
}
