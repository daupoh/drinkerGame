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
        public enum MoveTurn
        {
            Protagonist,Antagonist,Game
        }
        public enum PlayersStatus
        {          
            NoStatus, Atack, Support, GameOver
        }
        MoveTurn eTurn;
        PlayersStatus eStatus;
        CPlayer rAntagonist, rProtagonist;      
        readonly Stack<CCard> aBankCards = new Stack<CCard>();
        CDeck rDeck;
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
        public Bitmap FaceDownCard
        {
            get
            {
                return CCard.FaceDownCard().CardImage;
            }
        }
        public int[] DeskDecksCountOfCards
        {
            get
            {
                int[] aCounts = new int[5];
                aCounts[0] = rProtagonist.CountOfPool;
                aCounts[1] = rProtagonist.CountOfSource;
                aCounts[2] = rAntagonist.CountOfPool;
                aCounts[3] = rAntagonist.CountOfSource;
                aCounts[4] = aBankCards.Count;
                return aCounts;
            }
        }
        public PlayersStatus Status
        {
            get
            {
                return eStatus;
            }
        }
        public MoveTurn Turn
        {
            get
            {
                return eTurn;
            }
        }
        public CGame()
        {
            PrepareGame("Протагонист", "Антагонист", 0, 1);            
        }
        public void PrepareGame(string sProtagonistName, string sAntagonistName,
            int iTypeOfDecks, int iCountOfDecks)
        {
            rAntagonist = new CPlayer(sAntagonistName);
            rProtagonist = new CPlayer(sProtagonistName);
            rDeck = new CDeck(iTypeOfDecks, iCountOfDecks);           
            CCard[] aCards = rDeck.GenerateDeck();
            rAntagonist.ClearCards();
            rProtagonist.ClearCards();
            for (int i = 0; i < aCards.Length; i++)
            {
                if (i % 2 == 0)
                {
                    rAntagonist.TakeCardToPool(aCards[i]);
                }
                else
                {
                    rProtagonist.TakeCardToPool(aCards[i]);
                }
            }
            aBankCards.Clear();
            eTurn = MoveTurn.Protagonist;
            eStatus = PlayersStatus.NoStatus;
        }      
     
        public void Move()
        {
            Console.WriteLine("Prot Pool " + rProtagonist.CountOfPool.ToString());
            Console.WriteLine("Prot Source " + rProtagonist.CountOfSource.ToString());
            Console.WriteLine("At Pool " + rAntagonist.CountOfPool.ToString());
            Console.WriteLine("At Source " + rAntagonist.CountOfSource.ToString());
            Console.WriteLine("Bank " + aBankCards.Count);
            Console.WriteLine("Turn is " + eTurn.ToString());
            Console.WriteLine("Status is " + eStatus.ToString());
            switch (eTurn)
            {                
                case MoveTurn.Protagonist:                    
                    PlayerMove(rProtagonist);   
                    break;
                case MoveTurn.Antagonist:
                    PlayerMove(rAntagonist);
                    break;                
                case MoveTurn.Game:
                    GameMove();
                    break;
            }
        }
        private void PlayerMove(CPlayer rPlayer)
        {            
            if (rPlayer.AtackCard == null)
            {
                CCard rCard = rPlayer.GetCard();
                if (rCard == null)
                {
                    eStatus = PlayersStatus.GameOver;
                }
                else
                {
                    rPlayer.AtackCard = rCard;
                    eStatus = PlayersStatus.Atack;
                    eTurn = MoveTurn.Game;
                }
            }
            else if (rPlayer.SupportFirst == null)
            {
                CCard rFirstCard = rPlayer.GetCard(),
                    rSecondCard = rPlayer.GetCard();
                if (rFirstCard == null)
                {
                    eStatus = PlayersStatus.GameOver;
                }
                else if (rSecondCard == null)
                {
                    rPlayer.SupportFirst = rFirstCard;
                    eStatus = PlayersStatus.GameOver;                    
                }
                else
                {
                    rPlayer.SupportFirst = rFirstCard;
                    rPlayer.SupportSecond = rSecondCard;
                    eStatus = PlayersStatus.Support;
                    eTurn = MoveTurn.Game;
                }
                
            }
            
        }
        private void GameMove()
        {
           switch (eStatus)
            {
                case PlayersStatus.Atack:
                    if (rAntagonist.AtackCard == null)
                    {
                        eTurn = MoveTurn.Antagonist;
                    }
                    else if (rProtagonist.AtackCard == null)
                    {
                        eTurn = MoveTurn.Protagonist;
                    }
                    else
                    {                        
                        int iResultAtack = rProtagonist.AtackCard.Compare(rAntagonist.AtackCard);
                        if (iResultAtack == -1)
                        {
                            CardsToBank();
                            TakeBank(rAntagonist);
                            eTurn = MoveTurn.Antagonist;
                        }
                        else if (iResultAtack == 0)
                        {
                            eTurn = MoveTurn.Protagonist;
                        }
                        else
                        {
                            CardsToBank();
                            TakeBank(rProtagonist);
                            eTurn = MoveTurn.Protagonist;
                        }
                    }
                    break;

                case PlayersStatus.Support:
                    if (rAntagonist.SupportFirst == null)
                    {
                        eTurn = MoveTurn.Antagonist;
                    }
                    else if (rProtagonist.SupportFirst == null)
                    {
                        eTurn = MoveTurn.Protagonist;
                    }
                    else
                    {
                        int iSumResults = rProtagonist.SupportFirst.Compare(rAntagonist.SupportFirst) + rProtagonist.SupportSecond.Compare(rAntagonist.SupportSecond);
                        if (iSumResults<0)
                        {
                            CardsToBank();
                            TakeBank(rAntagonist);
                            eTurn = MoveTurn.Antagonist;
                        }
                        else if (iSumResults == 0)
                        {
                            CardsToBank();
                            eTurn = MoveTurn.Antagonist;
                        }
                        else
                        {
                            CardsToBank();
                            TakeBank(rProtagonist);
                            eTurn = MoveTurn.Protagonist;
                        }
                    }
                    break;                
            }
        }                   
        private void TakeBank(CPlayer rPlayer)
        {
            if (aBankCards.Count>0)
            {
                foreach (CCard rCard in aBankCards)
                {
                    rPlayer.TakeCardToSource(rCard);
                }
            }
            aBankCards.Clear();
        }
        private void CardsToBank()
        {
            switch (eStatus)
            {
                case PlayersStatus.Atack:
                    aBankCards.Push(rAntagonist.AtackCard);
                    aBankCards.Push(rProtagonist.AtackCard);                                        
                    break;
                case PlayersStatus.Support:
                    aBankCards.Push(rAntagonist.SupportFirst);
                    aBankCards.Push(rAntagonist.SupportSecond);
                    aBankCards.Push(rAntagonist.AtackCard);
                    aBankCards.Push(rProtagonist.SupportFirst);
                    aBankCards.Push(rProtagonist.SupportSecond);
                    aBankCards.Push(rProtagonist.AtackCard);                                        
                    break;
            }
            rAntagonist.SupportFirst = null;
            rAntagonist.SupportSecond = null;
            rAntagonist.AtackCard = null;
            rProtagonist.SupportFirst = null;
            rProtagonist.SupportSecond = null;
            rProtagonist.AtackCard = null;
        }
    }
}

