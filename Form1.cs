using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrinkerGame
{
    public partial class rGameForm : Form
    {
        int index = 0;
        readonly CCard[] a;
        public rGameForm()
        {
            InitializeComponent();            
           
          
           a = CCard.BuildDeck36(1);
            pbAtackAntagonist.Enabled = true;
            tmrNextStep.Interval = 1500;
            tmrNextStep.Start();
        }

        private void TmrNextStep_Tick(object sender, EventArgs e)
        {
            if (index>35)
            {
                tmrNextStep.Stop();
                pbAtackAntagonist.Image = null;
            }
            else
            {
                pbAtackAntagonist.Image = a[index].CardImage;
                index++;
            }

        }
    }
}
