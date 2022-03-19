using System;
using System.Windows.Forms;
using System.IO;

namespace Tubes2Stima
{
    public partial class Form1 : Form
    {
        private string filename = "";
        private bool allfiles = false;
        private string choice = "";
        private string currentPath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private string getFilename(string file)
        {
            FileInfo File = new FileInfo(file);
            return File.Name;
        }

        private void makeGraph(Microsoft.Msagl.Drawing.Graph graph, string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                graph.AddEdge(getFilename(path), getFilename(file));
            }
            string[] folders = Directory.GetDirectories(path);
            if (folders.Length == 0)
            {
                //
            } else
            {
                foreach (string folder in folders)
                {
                    graph.AddEdge(getFilename(path), getFilename(folder));
                    this.makeGraph(graph, folder);
                }
            }
        }

        private void changeColor(Microsoft.Msagl.Drawing.Graph graph, string path, string child, Microsoft.Msagl.Drawing.Color color)
        {
            if (path == child)
            {
                //
            } else
            {
                FileInfo child_dir = new FileInfo(child);
                string parrent = child_dir.DirectoryName;
                graph.FindNode(getFilename(child_dir.Name)).Attr.FillColor = color;
                graph.AddEdge(getFilename(parrent), child_dir.Name).Attr.Color = color;
                changeColor(graph, path, parrent, color);
            }
        }

        private void BFS(Microsoft.Msagl.Drawing.Graph graph, string path, bool allOccurence, bool flag)
        {
            if (allOccurence)
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    if (getFilename(file) != this.filename)
                    {
                        changeColor(graph, path, file, Microsoft.Msagl.Drawing.Color.Red);
                    } else
                    {
                        changeColor(graph, this.currentPath, file, Microsoft.Msagl.Drawing.Color.Green);
                        graph.FindNode(getFilename(this.currentPath)).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                    }
                }
                string[] folders = Directory.GetDirectories(path);
                if (folders.Length == 0)
                {
                    //
                }
                else
                {
                    foreach (string folder in folders)
                    {
                        this.BFS(graph, folder, true, false);
                    }
                }
            } else
            {
                string[] files = Directory.GetFiles(path);
                int i = 0;
                while (!flag && i < files.Length)
                {
                    if (getFilename(files[i]) != this.filename)
                    {
                        changeColor(graph, path, files[i], Microsoft.Msagl.Drawing.Color.Red);
                        i++;
                    }
                    else
                    {
                        changeColor(graph, this.currentPath, files[i], Microsoft.Msagl.Drawing.Color.Green);
                        graph.FindNode(getFilename(this.currentPath)).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    string[] folders = Directory.GetDirectories(path);
                    if (folders.Length == 0)
                    {
                        //
                    }
                    else
                    {
                        foreach (string folder in folders)
                        {
                            this.BFS(graph, folder, false, flag);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dir = new FolderBrowserDialog();
            if (dir.ShowDialog() == DialogResult.OK)
            {
                this.currentPath = dir.SelectedPath;
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
                    string[] folde = Directory.GetDirectories(folder);
                    foreach (string fold in folde)
                    {
                        listBox1.Items.Add(fold);
                    }
                }
            }
            
        }

        // Label for UI
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
            if (this.filename == "" || this.choice == "" || this.currentPath == "")
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
                this.makeGraph(graph, this.currentPath);
                if (this.choice == "BFS")
                {
                    this.BFS(graph, this.currentPath, this.allfiles, false);
                }
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
