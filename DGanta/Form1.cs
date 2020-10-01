using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGanta
{
    public partial class Form1 : Form
    {
        Graphics panel_g;
        Diagram diagram;

        TaskListModel tasks;
        Point curpos;

        int dX;

        public Form1()
        {
            InitializeComponent();

            panel_g = panel1.CreateGraphics();
            diagram = new Diagram();

            tasks = new TaskListModel();
            curpos = new Point();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            diagram.Select(e);
            dX = e.X - curpos.X;
            if (e.Button == MouseButtons.Right)
            {
                diagram.Move(dX);
            }
            else
            {
                for (int i = 0; i < diagram.cageTimes.Count; i++)
                {
                    if (diagram.cageTimes[i].inBorder(curpos))
                    {
                        Cursor.Current = Cursors.SizeWE;
                        if(e.Button == MouseButtons.Left)
                        {
                            diagram.Resize(curpos, dX);
                        }
                    }
                }
            }

            Scene(panel_g, panel1);

            curpos.X = e.X;
            curpos.Y = e.Y;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (diagram.ForTask(curpos))
                {
                    Form addTask = new AddTask();
                    addTask.ShowDialog();
                }

                diagram.CreateTask(curpos, panel_g, DataTransfer.nameTask, DataTransfer.timeTask, DataTransfer.colorTask);
                label1.Text = "Общее время: " + diagram.timeOfProject;
                Scene(panel_g, panel1);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            
        }

        public void Scene(Graphics g, Panel panel)
        {
            Bitmap bmp = new Bitmap(panel.Width, panel.Height, g);
            Graphics g1 = Graphics.FromImage(bmp);

            g1.Clear(panel.BackColor);
            Paint_old(g1);

            g.DrawImage(bmp, 0, 0);
        }

        public void Paint_old(Graphics g)
        {
            diagram.DrawDiagram(g);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
