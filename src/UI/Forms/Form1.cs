using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Tubes2Stima
{
    public partial class Form1 : Form
    {
        private string currentPath = "";
        private string filename = "";
        private bool allfiles = false;
        private bool showprocess = false;
        private string choice = "";
        private List<string> listOfFiles = new List<string>();
        private bool isFound = false;
        private List<string> resultFiles = new List<string>();
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

        private void addBFSList(string path)
        {
            Queue<string> visitedDirsQueue = new Queue<string>();

            visitedDirsQueue.Enqueue(path);

            int i = 0;
            while (visitedDirsQueue.Count > 0)
            {
                string currentDir = visitedDirsQueue.Dequeue();
                if (i != 0)
                {
                    this.listOfFiles.Add(currentDir);
                }
                i++;

                if (Directory.Exists(currentDir))
                {
                    string[] children = Directory.GetDirectories(currentDir);
                    foreach (string child in children)
                    {
                        visitedDirsQueue.Enqueue(child);
                    }

                    string[] files = Directory.GetFiles(currentDir);
                    foreach (string file in files)
                    {
                        visitedDirsQueue.Enqueue(file);
                    }
                }
            }
        }

        private void addDFSList(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                this.listOfFiles.Add(file);
            }

            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                this.listOfFiles.Add(folder);
                addDFSList(folder);
            }
        }

        private void showForm(System.Windows.Forms.Form form, Microsoft.Msagl.GraphViewerGdi.GViewer viewer, Microsoft.Msagl.Drawing.Graph graph)
        {
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

        private void processGraph(System.Windows.Forms.Form form, Microsoft.Msagl.GraphViewerGdi.GViewer viewer, Microsoft.Msagl.Drawing.Graph graph)
        {
            if (this.allfiles)
            {
                while (this.listOfFiles.Count != 0)
                {
                    string process = this.listOfFiles.ToArray()[0];
                    FileAttributes attr = File.GetAttributes(process);
                    if (getFilename(process) != this.filename || (attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        FileInfo parrentFile = new FileInfo(process);
                        string parrent = parrentFile.DirectoryName;
                        changeColor(graph, parrent, process, Microsoft.Msagl.Drawing.Color.Red);
                    } else
                    {
                        changeColor(graph, this.currentPath, process, Microsoft.Msagl.Drawing.Color.Green);
                        graph.FindNode(getFilename(this.currentPath)).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                        this.isFound = true;
                        this.resultFiles.Add(process);
                    }
                    this.listOfFiles.Remove(process);
                    
                    if (this.showprocess)
                    {
                        showForm(form, viewer, graph);
                    }
                }
            } else
            {
                bool flag = false;
                while (this.listOfFiles.Count != 0 && !flag)
                {
                    string process = this.listOfFiles.ToArray()[0];
                    FileAttributes attr = File.GetAttributes(process);
                    if (getFilename(process) != this.filename || (attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        FileInfo parrentFile = new FileInfo(process);
                        string parrent = parrentFile.DirectoryName;
                        changeColor(graph, parrent, process, Microsoft.Msagl.Drawing.Color.Red);
                        this.listOfFiles.Remove(process);
                    } else
                    {
                        flag = true;
                    }

                    if (this.showprocess && !flag)
                    {
                        showForm(form, viewer, graph);
                    }
                }

                if (this.listOfFiles.Count != 0)
                {
                    changeColor(graph, this.currentPath, this.listOfFiles.ToArray()[0], Microsoft.Msagl.Drawing.Color.Green);
                    graph.FindNode(getFilename(this.currentPath)).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                    this.isFound = true;
                    this.resultFiles.Add(this.listOfFiles.ToArray()[0]);
                    while (this.listOfFiles.Count != 0)
                    {
                        this.listOfFiles.Remove(this.listOfFiles.ToArray()[0]);
                    }
                }
            }

            if (!this.isFound)
            {
                graph.FindNode(getFilename(this.currentPath)).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            }
            showForm(form, viewer, graph);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dir = new FolderBrowserDialog();
            if (dir.ShowDialog() == DialogResult.OK)
            {
                this.currentPath = dir.SelectedPath;
                label3.Text = dir.SelectedPath;
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.showprocess)
            {
                this.showprocess = false;
            }
            else
            {
                this.showprocess = true;
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
                this.isFound = false;
                this.resultFiles.Clear();
                this.makeGraph(graph, this.currentPath);
                if (this.choice == "BFS")
                {
                    addBFSList(this.currentPath);
                } else
                {
                    addDFSList(this.currentPath);
                }
                processGraph(form, viewer, graph);
                label5.Text = "Path File:";
                if (this.isFound)
                {
                    linkLabel1.Text = "";
                    foreach (string path in this.resultFiles)
                    {
                        linkLabel1.Text += path;
                        linkLabel1.Text += "\n";
                    }
                    
                    label6.Text = "";
                } else
                {
                    label6.Text = "No path found";
                    linkLabel1.Text = "";
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.isFound)
            {
                Process.Start(this.resultFiles.ToArray()[0]);
            }
        }
    }
}
