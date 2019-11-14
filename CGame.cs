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
            NoResult, ProtagonistAtack, AntagonistAtack, ProtagonistDefend, AntagonistDefend, ProtagonistSupport, AntagonistSupport
                ,Win, Draw,AntagonistGameOver, ProtagonistGameOver
        }
        enum AtackTypes
        {
            ProtagonistAtack, AntagonistAtack, ProtagonistDefend, AntagonistDefend, ProtagonistSupport, AntagonistSupport
        }
        AtackResults eAtackResult;
        AtackTypes eAtackType;
        CPlayer rAntagonist, rProtagonist;
        CCard rAtackAntagonistCard, rAtackProtagonistCard, rSuportAntagonistFirstCard, rSuportAntagonistSecondCard, 
            rSuportProtagonistFirstCard, rSuportProtagonistSecondCard;
        Stack<CCard> rBankCards = new Stack<CCard>();
        CDeck rDeck;
        
        public CGame()
        {
            PrepareGame("Протагонист", "Антагонист", 0, 1);            
        }
        public AtackResults Atack()
        {           
            switch (eAtackType)
            {
                case AtackTypes.ProtagonistAtack:
                    rAtackProtagonistCard = rProtagonist.TakeCard();
                    if (rAtackProtagonistCard == null)
                    {
                        eAtackResult = AtackResults.ProtagonistGameOver;
                    }
                    else
                    {
                        eAtackType = AtackTypes.AntagonistDefend;
                    }
                    break;                    
                case AtackTypes.AntagonistAtack:
                    rAtackAntagonistCard = rAntagonist.TakeCard();
                    if (rAtackAntagonistCard == null)
                    {
                        eAtackResult = AtackResults.AntagonistGameOver;
                    }
                    else
                    {
                        eAtackType = AtackTypes.ProtagonistDefend;
                    }
                    break;
                case AtackTypes.AntagonistDefend:
                    rAtackProtagonistCard = rProtagonist.TakeCard();
                    if (rAtackProtagonistCard == null)
                    {
                        eAtackResult = AtackResults.ProtagonistGameOver;
                    }
                    else
                    {
                        int iResultComparing = rAtackAntagonistCard.Compare(rAtackProtagonistCard);
                        if (iResultComparing == -1) 
                        {
                            
                        }
                        else if (iResultComparing==0)
                        {                            
                            eAtackType = AtackTypes.ProtagonistSupport;
                        }
                        else
                        {

                        }

                        eAtackType = AtackTypes.AntagonistDefend;
                    }
                    break;
                case AtackTypes.ProtagonistDefend: break;
                case AtackTypes.ProtagonistSupport: break;
                case AtackTypes.AntagonistSupport: break;
            }
           
            return eAtackResult;
        }
        private void AllBank()
        {

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
            eAtackResult = AtackResults.NoResult;
            eAtackType = AtackTypes.ProtagonistAtack;
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

