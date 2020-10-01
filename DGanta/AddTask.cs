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
    public partial class AddTask : Form
    {
        public AddTask()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
                DataTransfer.nameTask = textBoxName.Text;
            else
                DataTransfer.nameTask = "Новая задача";

            if (textBox1.Text != "")
                DataTransfer.timeTask = int.Parse(textBox1.Text);
            else
                DataTransfer.timeTask = 1;

            if (DataTransfer.colorTask == null)
                DataTransfer.colorTask = Color.Red;

            DataTransfer.colorTask = panel1.BackColor;

            Close();
        }

        private void AddTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBoxName.Text != "")
                DataTransfer.nameTask = textBoxName.Text;
            else
                DataTransfer.nameTask = "Новая задача";

            if (textBox1.Text != "")
                DataTransfer.timeTask = int.Parse(textBox1.Text);
            else
                DataTransfer.timeTask = 1;

            if (DataTransfer.colorTask == null)
                DataTransfer.colorTask = Color.Red;

            DataTransfer.colorTask = panel1.BackColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            panel1.BackColor = colorDialog1.Color;
            DataTransfer.colorTask = colorDialog1.Color;
        }
    }
}
