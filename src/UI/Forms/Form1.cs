using System;
using System.Windows.Forms;
using System.IO;

namespace Tubes2Stima
{
    public partial class Form1 : Form
    {
        private FolderBrowserDialog folder;
        private string filename = "";
        private bool allfiles = false;
        private string choice = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.folder = new FolderBrowserDialog();
            if (this.folder.ShowDialog() == DialogResult.OK)
            {
                label3.Text = this.folder.SelectedPath;
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(this.folder.SelectedPath);
                string[] folders = Directory.GetDirectories(this.folder.SelectedPath);

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.filename = textBox2.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.allfiles)
            {
                this.allfiles = false;
            } else
            {
                this.allfiles = true;
            }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.choice = "BFS";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.choice = "DFS";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.filename == "" || this.choice == "")
            {
                MessageBox.Show("Sorry, please give a valid input");
            } else
            {
                //create a form 
                System.Windows.Forms.Form form = new System.Windows.Forms.Form();
                //create a viewer object 
                Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
                //create a graph object 
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
                //create the graph content 
                graph.AddEdge("A", "B");
                graph.AddEdge("B", "C");
                graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
                graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
                c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
                c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                //bind the graph to the viewer 
                viewer.Graph = graph;
                //associate the viewer with the form 
                form.SuspendLayout();
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                form.Controls.Add(viewer);
                form.ResumeLayout();
                //show the form 
                form.ShowDialog();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
