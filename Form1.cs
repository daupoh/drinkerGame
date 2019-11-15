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

        readonly CGame rGame;
        public rGameForm()
        {
            InitializeComponent();
            rGame = new CGame();



        }

        private void TmrNextStep_Tick(object sender, EventArgs e)
        {            

        }
        private void StartGame()
        {
            rGame.PrepareGame(tbProtagonistName.Text,)
        }
        private void bQuickStart_Click(object sender, EventArgs e)
        {

        }

        private void bSaveSettings_Click(object sender, EventArgs e)
        {

        }

        private void bCancelSettings_Click(object sender, EventArgs e)
        {

        }
    }
}
