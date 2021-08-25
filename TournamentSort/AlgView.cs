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
    public partial class AlgView : Form
    {
        Models.ArrayModel model;
        public AlgView()
        {
            InitializeComponent();
        }

        public AlgView(Models.ArrayModel model)
        {
            this.model = model;
            InitializeComponent();
        }
        
    }
}
