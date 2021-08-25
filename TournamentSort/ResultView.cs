using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TournamentSort
{
    public partial class ResultView : Form
    {
        public Models.ArrayModel model;
        public ResultView()
        {
            InitializeComponent();
        }

        public ResultView(Models.ArrayModel model)
        {
            this.model = model;
        }

        private void ResultView_Load(object sender, EventArgs e)
        {

        }
    }
}
