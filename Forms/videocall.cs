using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class videocall : Form
    {
        public videocall()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
            label1.ForeColor = Color.Gray;
            label2.ForeColor = Color.Black;
        }
        private void label2_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
            label2.ForeColor = Color.Gray;
            label1.ForeColor = Color.Black;
        }
    }
}
