using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TournamentSort
{
    public partial class InputView : Form
    {
        public TextBox textBox2 = new TextBox();
        public InputView()
        {
            InitializeComponent();
            this.textBox2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = int.Parse(this.textBox1.Text);

            Models.ArrayModel model = new Models.ArrayModel(count);

            this.Controls.Clear();

            AppController.ShowArrayInput(this, model);

            Console.WriteLine(model.unsortedArray);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1_Click(sender, e);
            }
        }
    }
}

