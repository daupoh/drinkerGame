using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkerGame
{
    class CGame
    {
        int iGameSpeedMod;        
        public enum AtackResults
        {          
            ProtagonistAtack, AntagonistAtack,
            ProtagonistSupportFirst, AntagonistSupportFirst,
            ProtagonistSupportSecond, AntagonistSupportSecond,
            ProtagonistWin, ProtagonistDraw, AntagonistWin, AntagonistDraw,
            AntagonistGameOver, ProtagonistGameOver
        }      
        AtackResults eAtackResult;      
        CPlayer rAntagonist, rProtagonist;
        CCard rAtackAntagonistCard, rAtackProtagonistCard, rSuportAntagonistFirstCard, rSuportAntagonistSecondCard, 
            rSuportProtagonistFirstCard, rSuportProtagonistSecondCard;
        Stack<CCard> rBankCards = new Stack<CCard>();
        CDeck rDeck;
        
        public CGame()
        {
            PrepareGame("Протагонист", "Антагонист", 0, 1);            
        }
        public AtackResults GameMove()
        {           
            switch (eAtackResult)
            {
                case AtackResults.ProtagonistAtack:
                    rAtackProtagonistCard = rProtagonist.TakeCard();
                    if (rAtackProtagonistCard == null)
                    {
                        eAtackResult = AtackResults.ProtagonistGameOver;
                    }
                    else
                    {
                        if (NormalAtack())
                        {
                            eAtackResult = AtackResults.AntagonistAtack;
                        }
                        else 
                        {
                            int iResultComparing = rAtackAntagonistCard.Compare(rAtackProtagonistCard);
                            if (iResultComparing == -1)
                            {
                                eAtackResult = AtackResults.ProtagonistWin;
                            }
                            else if (iResultComparing == 0)
                            {
                                eAtackResult = AtackResults.AntagonistSupportFirst;
                            }
                            else
                            {
                                eAtackResult = AtackResults.AntagonistWin;
                            }                          
                        }
                    }
                    break;                    
                case AtackResults.AntagonistAtack:
                    rAtackAntagonistCard = rAntagonist.TakeCard();
                    if (rAtackAntagonistCard == null)
                    {
                        eAtackResult = AtackResults.AntagonistGameOver;
                    }
                    else
                    {
                        if (NormalAtack())
                        {
                            eAtackResult = AtackResults.ProtagonistAtack;
                        }
                        else
                        {
                            int iResultComparing = rAtackAntagonistCard.Compare(rAtackProtagonistCard);
                            if (iResultComparing == -1)
                            {
                                eAtackResult = AtackResults.ProtagonistWin;
                            }
                            else if (iResultComparing == 0)
                            {
                                eAtackResult = AtackResults.ProtagonistSupportFirst;
                            }
                            else
                            {
                                eAtackResult = AtackResults.AntagonistWin;
                            }
                        }
                    }
                    break;               
                case AtackResults.ProtagonistSupportFirst:

                    break;
                case AtackResults.AntagonistSupportFirst: break;
            }
           
            return eAtackResult;
        }
        private void AllBank()
        {

        }
        private void Comparing(AtackResults eDrawResult)
        {

        }
      private bool NormalAtack()
        {
            return (rAtackAntagonistCard == null || rAtackProtagonistCard == null);
        }
        private bool FirstSupportAtack()
        {
            return (rSuportAntagonistFirstCard == null || rSuportProtagonistFirstCard == null);
        }
        private bool SecondSupportAtack()
        {
            return (rSuportAntagonistSecondCard == null || rSuportProtagonistSecondCard == null);
        }
       
       
        public void PrepareGame(string ProtagonistName, string AntagonistName,
            int iTypeOfDecks, int iCountOfDecks)
        {
            rAntagonist = new CPlayer(AntagonistName);
            rProtagonist = new CPlayer(ProtagonistName);
            rDeck = new CDeck(iTypeOfDecks, iCountOfDecks);
            GameSpeed = 1;
            CCard[] aCards = rDeck.GenerateDeck();
            rAntagonist.ClearCards();
            rProtagonist.ClearCards();
            for (int i = 0; i < aCards.Length; i++)
            {
                if (i %2 ==0)
                {
                    rAntagonist.GetCardToPool(aCards[i]);
                }
                else
                {
                    rProtagonist.GetCardToSource(aCards[i]);
                }
            }
            rBankCards.Clear();
            eAtackResult = AtackResults.ProtagonistAtack;            
        }
        public Bitmap FaceDownCard
        {
            get
            {
                return CCard.FaceDownCard().CardImage;
            }
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
        public int Type
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
    
    }
}

