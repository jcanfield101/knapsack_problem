using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BinPack2
{
    public partial class Form1 : Form
    {
        ItemSet items = null;
        BinCollection bins = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Utilities.LoadItemCollectionFromFile("items2.txt", ref items);
            txtResult.Text = string.Format("Loaded {0} items with total weight {1}\r\n", items.Count, items.Weight);
        }

        //private void btnPrintSubsets_Click(object sender, EventArgs e)
        //{
        //    if (subsets == null)
        //    {
        //        MessageBox.Show("Subsets not yet generated");
        //        return;
        //    }

        //    txtResult.Text = Utilities.PrintSubsets(subsets);
        //}

        //private void btnGetSubsets_Click(object sender, EventArgs e)
        //{
        //    txtResult.Clear();
        //    string output;

        //    if (items == null)
        //    {
        //        MessageBox.Show("List is not yet loaded");
        //        return;
        //    }

        //    Stopwatch sw;

        //    try
        //    {
        //        // Run for each n
        //        for (int n = int.Parse(txtCountFrom.Text); n <= int.Parse(txtCountTo.Text); n++)
        //        {
        //            sw = Stopwatch.StartNew();
        //            Utilities.GenerateSubsets(ref items, n, ref subsets);
        //            Utilities.PackBins(int.Parse(txtBinCount.Text), int.Parse(txtBinCapacity.Text), ref bins, ref subsets);
        //            sw.Stop();

        //            // Print bin results
        //            output = string.Format("---- n={0} ----\r\n", n);
        //            output += String.Format("bin {0}: [", 0);
        //            output += bins.Items[0].Print();
        //            output += String.Format("elapsed time = {0} seconds\r\n", sw.Elapsed.TotalSeconds.ToString("0.000000"));
        //            txtResult.AppendText(output);
        //        }
        //    }
        //    catch (OutOfMemoryException oomex)
        //    {
        //        txtResult.AppendText("Out of memory");
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            string output;

            txtResult.AppendText("Loading items2.txt...\r\n");
            Utilities.LoadItemCollectionFromFile("items2.txt", ref items);
            txtResult.AppendText(String.Format("Loaded {0} items with total weight {1}\r\n", items.Count, items.Weight));

            if (items == null)
            {
                MessageBox.Show("List is not loaded");
                return;
            }

            Stopwatch sw;

            bins = new BinCollection(1, 50);

            try
            {
                // Run for each n
                for (int n = int.Parse(txtCountFrom.Text); n <= int.Parse(txtCountTo.Text); n++)
                {
                    sw = Stopwatch.StartNew();
                    ItemSet bin0 = Utilities.PackBin_1(ref items, n);
                    sw.Stop();
                    bins[0].Items = bin0;

                    // Print bin results
                    output = string.Format("---- n={0} ----\r\n", n);
                    output += String.Format("bin {0}: [", 0);
                    output += bins[0].Print();
                    output += String.Format("elapsed time = {0} seconds\r\n", sw.Elapsed.TotalSeconds.ToString("0.000000"));
                    txtResult.AppendText(output);

                    // Check for timeout (> 5 min)
                    if (sw.Elapsed.Seconds >= 300)
                    {
                        txtResult.AppendText("TIMEOUT");
                        break;
                    }
                }
                txtResult.AppendText("--- COMPLETED ----");
            }
            catch (OutOfMemoryException oomex)
            {
                txtResult.AppendText("Out of memory");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            string output;

            txtResult.AppendText("Loading items2.txt...\r\n");
            Utilities.LoadItemCollectionFromFile("items2.txt", ref items);
            txtResult.AppendText(String.Format("Loaded {0} items with total weight {1}\r\n", items.Count, items.Weight));

            if (items == null)
            {
                MessageBox.Show("List is not loaded");
                return;
            }

            Stopwatch sw;

            bins = new BinCollection(2, 50);

            try
            {
                // Run for each n
                for (int n = int.Parse(txtCountFrom.Text); n < (int.Parse(txtCountTo.Text) + 1); n++)
                {
                    sw = Stopwatch.StartNew();
                    List<ItemSet> BN = Utilities.PackBin_2(ref items, n);
                    sw.Stop();

                    bins[0].Items = BN[0];
                    bins[1].Items = BN[1];

                    // Print bin1 results
                    output = string.Format("---- n={0} ----\r\n", n);
                    output += String.Format("bin {0}: [", 0);
                    output += bins[0].Print();

                    // Print bin2 results
                    output += String.Format("bin {0}: [", 1);
                    output += bins[1].Print();

                    output += String.Format("elapsed time = {0} seconds\r\n", sw.Elapsed.TotalSeconds.ToString("0.000000"));
                    txtResult.AppendText(output);

                    // Check for timeout (> 5 min)
                    if (sw.Elapsed.Seconds >= 300)
                    {
                        txtResult.AppendText("--- TIMEOUT ----\r\n");
                        break;
                    }
                }
                txtResult.AppendText("--- COMPLETED ----\r\n");
            }
            catch (OutOfMemoryException oomex)
            {
                txtResult.AppendText("--- Out of memory ----\r\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            string output;

            txtResult.AppendText("Loading items2.txt...\r\n");
            Utilities.LoadItemCollectionFromFile("items2.txt", ref items);
            txtResult.AppendText(String.Format("Loaded {0} items with total weight {1}\r\n", items.Count, items.Weight));

            if (items == null)
            {
                MessageBox.Show("List is not loaded");
                return;
            }

            Stopwatch sw;

            bins = new BinCollection(3, 50);

            try
            {
                // Run for each n
                for (int n = int.Parse(txtCountFrom.Text); n < (int.Parse(txtCountTo.Text) + 1); n++)
                {
                    sw = Stopwatch.StartNew();
                    List<ItemSet> BN = Utilities.PackBin_3(ref items, n);
                    sw.Stop();

                    bins[0].Items = BN[0];
                    bins[1].Items = BN[1];
                    bins[2].Items = BN[2];

                    // Print bin1 results
                    output = string.Format("---- n={0} ----\r\n", n);
                    output += String.Format("bin {0}: [", 0);
                    output += bins[0].Print();

                    // Print bin2 results
                    output += String.Format("bin {0}: [", 1);
                    output += bins[1].Print();

                    // Print bin3 results
                    output += String.Format("bin {0}: [", 2);
                    output += bins[2].Print();

                    output += String.Format("elapsed time = {0} seconds\r\n", sw.Elapsed.TotalSeconds.ToString("0.000000"));
                    txtResult.AppendText(output);

                    // Check for timeout (> 5 min)
                    if (sw.Elapsed.Seconds >= 300)
                    {
                        txtResult.AppendText("--- TIMEOUT ----\r\n");
                        break;
                    }
                }
                txtResult.AppendText("--- COMPLETED ----\r\n");
            }
            catch (OutOfMemoryException oomex)
            {
                txtResult.AppendText("--- Out of memory ----\r\n");
            }
        }
    }
}
