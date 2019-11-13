using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkerGame
{
    class CGame
    {
        enum GameStatus
        {
            Run, Pause, Sutdown
        }
        GameStatus eStatus;
        int iGameSpeedMod;      
        CPlayer rAntagonist, rProtagonist;
        CDeck rDeck;
        
        public CGame()
        {
            PrepareGame("Протагонист", "Антагонист", 0, 1);            
        }
        public int Atack()
        {
            int iResultOfAtack = -1;

            return iResultOfAtack;
        }
        public int SupportAtack()
        {
            int iResultOfAtack = -1;

            return iResultOfAtack;
        }        
       
        public void PrepareGame(string ProtagonistName, string AntagonistName,
            int iTypeOfDecks, int iCountOfDecks)
        {
            rAntagonist = new CPlayer(AntagonistName);
            rProtagonist = new CPlayer(ProtagonistName);
            rDeck = new CDeck(iTypeOfDecks, iCountOfDecks);
            GameSpeed = 1;           
            eStatus = GameStatus.Sutdown;
        }
        public string ProtagonistName
        {
            get
            {
                return rProtagonist.Name;
            }
            set
            {
                rProtagonist.Name = value;
            }
        }
        public string AntagonistName
        {
            get
            {
                return rAntagonist.Name;
            }
            set
            {
                rAntagonist.Name = value;
            }
        }
        public CDeck.TypeOfDeck Type
        {
            get
            {
                return rDeck.Type;
            }
            set
            {
                rDeck.Type = value;
            }
        }
        public int CountOfDecks
        {
            get
            {
                return rDeck.CountOfDecks;
            }
            set
            {
                rDeck.CountOfDecks = value;
            }
        }
        public int GameSpeed
        {
            get
            {
                return iGameSpeedMod;
            }
            set
            {
                Assert.IsTrue(value > 0 && value <= 100);
                iGameSpeedMod = value;
            }
        }
        public int Status
        {
            get
            {
                return (int)eStatus;
            }
            set
            {
                Assert.IsTrue(value < 3);
                eStatus = (GameStatus)value;
            }
        }
    }
}

