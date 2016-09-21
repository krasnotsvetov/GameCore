using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameCoreTutorial00
{
    public partial class RunForm : Form
    {
        public RunForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var game = new Game1())
                game.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var game = new Game2())
                game.Run();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var game = new Game3())
                game.Run();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var game = new Game4())
                game.Run();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var game = new Game5())
                game.Run();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var game = new Game6())
                game.Run();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RunForm_Load(object sender, EventArgs e)
        {

        }
    }
}
