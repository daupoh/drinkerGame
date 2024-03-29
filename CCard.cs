﻿using System;
using System.Drawing;

namespace DrinkerGame
{
    class CCard
    {
        readonly int iSuit, iType, iCard, iCardImageColumn;
        readonly Bitmap rCardImage;
        const int OFFSET_X = 7, OFFSET_Y = 6, FREE_SPACE = 8, 
            WIDTH = 65, HEIGHT = 100;
        enum CardSuit
        {
            Hearts,Diamonds, Clubs, Spades, Special
        }
        enum TypeOfCard
        {
            SmallNumber,Number,Rank,Jocker, FaceDown
        }
        enum RanksCard
        {
            Jack, Queen, King, Ace
        }
        enum JockersCards
        {
            Black, Red
        }
        enum NumbersCard
        {
            Six, Seven, Eight, Nine, Ten
        }
        enum SmallNumbersCard
        {
            Two, Three, Four, Five
        }
        public Bitmap CardImage
        {
            get
            {
                return CutImage(rCardImage);
            }
        }
        public static CCard FaceDownCard()
        {
            return new CCard(4, 4, 0, 0);
        }
        public int Compare(CCard rCard)
        {
            int iResultOfComprassion = -1;
            if (iType > rCard.iType)
            {
                iResultOfComprassion = 1;
            }
            else if (iType == rCard.iType)
            {
                switch (iType)
                {
                    case 3: iResultOfComprassion = 0; break;
                    default:
                        if (iCard > rCard.iCard)
                        {
                            iResultOfComprassion = 1;
                        }
                        else if (iCard == rCard.iCard)
                        {
                            iResultOfComprassion = 0;
                        }
                        break;
                }
            }
            return iResultOfComprassion;
        }
        public static CCard[] BuildDeck36(int iCountDecks)
        {
            CCard[] aCards = new CCard[36 * iCountDecks];
            int iInd = 0;
            for (int k = 0; k < iCountDecks; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        aCards[iInd++] = new CCard(j, 2, i, i + 10);
                    }
                }
                aCards[iInd++] = new CCard(0, 2, 3, 0);
                aCards[iInd++] = new CCard(1, 2, 3, 0);
                aCards[iInd++] = new CCard(2, 2, 3, 0);
                aCards[iInd++] = new CCard(3, 2, 3, 0);
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        aCards[iInd++] = new CCard(j, 1, i, i + 5);
                    }
                }
            }
            return StirCards(aCards);
        }
        private Bitmap CutImage(Bitmap src)
        {
            int dx = OFFSET_X + iCardImageColumn*FREE_SPACE + iCardImageColumn * WIDTH
                , dy = OFFSET_Y + iSuit*FREE_SPACE + iSuit * HEIGHT;
            Rectangle rect = new Rectangle(dx, dy, WIDTH, HEIGHT);
            Bitmap bmp = new Bitmap(src.Width, src.Height); //создаем битмап
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel); //перерисовываем с источника по координатам
            return bmp;
        }
     
        private CCard(int iSuit, int iType, int iCard, int iCardImageColumn)
        {
            this.iSuit = iSuit;
            this.iType = iType;
            this.iCard = iCard;
            this.iCardImageColumn = iCardImageColumn;

            Image rImg = Properties.Resources.cards;
            rCardImage = new Bitmap(rImg);            
        }
    
        private static CCard[] StirCards(CCard[] aCards)
        {            
            Random rRand = new Random(aCards.Length);
            for (int i = 0; i < 1000; i++)
            {
                int iFirst, iSecond;
                CCard rTemp;
                do
                {
                    iFirst = rRand.Next(0, aCards.Length);
                    iSecond = rRand.Next(0, aCards.Length);
                } while (iFirst == iSecond);
                rTemp = aCards[iFirst];
                aCards[iFirst] = aCards[iSecond];
                aCards[iSecond]= rTemp;
            }
            return aCards;
        }
    }
}
