using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gooprint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string print = textBox1.Text;
            textBox1.Text = "";
            foreach(char i in print)
            {
                textBox1.Text += "구구!"+String.Concat(Enumerable.Repeat("구", (int)i))+ "?구!"+ String.Concat(Enumerable.Repeat("구", 100)) + "?";
            }
            File.AppendAllText("a.pigeon",textBox1.Text);
        }
    }
}
