using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkerGame
{
    class CDeck
    {
        int iCountOfDecks;
       
        public CDeck(int iType, int iCountOfDecks)
        {         
            Type = iType;
            CountOfDecks = iCountOfDecks;
        }
        public enum TypeOfDeck
        {
            Cards36,
            Cards52,
            Cards54
        }
        TypeOfDeck eType;
        public int CountOfDecks
        {
            get
            {
                return iCountOfDecks;
            }
            set
            {
                Assert.IsTrue(value > 0 && value <= 100);
                iCountOfDecks = value;
            }
        }
        
        public CCard[] GenerateDeck()
        {
            CCard[] aCards=null;
            switch ((TypeOfDeck)Type)
            {
                case TypeOfDeck.Cards36:
                    aCards = CCard.BuildDeck36(CountOfDecks);
                    break;
                case TypeOfDeck.Cards52: break;
                case TypeOfDeck.Cards54: break;
            }
            return aCards;
        }
      
        public int Type
        {
            get
            {
                return (int)eType;
            }
            set
            {
                Assert.IsTrue(value < 3);
                eType = (TypeOfDeck)value;
            }
        }
    }
}


