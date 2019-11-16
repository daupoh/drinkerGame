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
            if (rGame.Status==CGame.PlayersStatus.GameOver && rGame.Turn==CGame.MoveTurn.Antagonist)
            {
                tmrNextStep.Stop();
                MessageBox.Show("Игрок "+rGame.AntagonistName + " проиграл!");
            }
            else if (rGame.Status == CGame.PlayersStatus.GameOver && rGame.Turn == CGame.MoveTurn.Protagonist)
            {
                tmrNextStep.Stop();
                MessageBox.Show("Игрок " + rGame.ProtagonistName + " проиграл!");
            }
            else
            {
                rGame.Move();
            }
        }
        private void StartGame()
        {
            rGame.PrepareGame(tbProtagonistName.Text, tbAntagonistName.Text, 
                cbCardsType.SelectedIndex, (int)nudCountOfDecks.Value);
            gbSettings.Enabled = false;
            bStartGame.Enabled = false;
            nudSpeedOfGame.Enabled = false;
            tmrNextStep.Interval = 10;
            tmrNextStep.Start();
        }
        
        private void bQuickStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bSaveSettings_Click(object sender, EventArgs e)
        {

        }

        private void bCancelSettings_Click(object sender, EventArgs e)
        {

        }
    }
}
