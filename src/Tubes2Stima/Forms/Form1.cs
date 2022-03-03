using System;
using System.Windows.Forms;
using System.IO;
using PathFinder;

namespace Tubes2Stima
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dir = new FolderBrowserDialog(); 
            if (dir.ShowDialog() == DialogResult.OK)
            {
                label3.Text = dir.SelectedPath;
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(dir.SelectedPath);
                string[] folders = Directory.GetDirectories(dir.SelectedPath);
                
                foreach (string file in files)
                {
                    listBox1.Items.Add(file);
                }
                foreach (string folder in folders)
                {
                    listBox1.Items.Add(folder);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
