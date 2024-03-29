﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkerGame
{
    class CPlayer
    {
        string sName;
        readonly Stack<CCard> aPoolCards = new Stack<CCard>();
        readonly Stack<CCard> aSourceCards = new Stack<CCard>();
        CCard rAtackCard, rSupportCardFirst, rSupportCardSecond;
        public string Name
        {
            get
            {
                return sName;
            }
            set
            {
                Assert.IsTrue(value.Length > 0);
                sName = value;
            }
        }
        public int CountOfPool
        {
            get
            {
                return aPoolCards.Count;
            }
        }
        public int CountOfSource
        {
            get
            {
                return aSourceCards.Count;
            }
        }
        public CCard AtackCard
        {
            get
            {
                return rAtackCard;
            }
            set
            {
                rAtackCard = value;
            }
        }
        public CCard SupportFirst
        {
            get
            {
                return rSupportCardFirst;
            }
            set
            {
                rSupportCardFirst = value;
            }
        }
        public CCard SupportSecond
        {
            get
            {
                return rSupportCardSecond;
            }
            set
            {
                rSupportCardSecond = value;
            }
        }
        public CPlayer(string sName)
        {
            Name = sName;
        }
       
        public void TakeCardToPool(CCard rCard)
        {
            Assert.IsTrue(rCard != null);
            aPoolCards.Push(rCard);
        }
        public CCard GetCard()
        {
            CCard rCard = null;
            if (aPoolCards.Count>0)
            {
                rCard = aPoolCards.Pop();
            }
            else if (aSourceCards.Count>0)
            {
                foreach (CCard rTempCard in aSourceCards)
                {
                    aPoolCards.Push(rTempCard);
                }
                aSourceCards.Clear();
                rCard = aPoolCards.Pop();
            }            
            return rCard;
        }
        public void TakeCardToSource(CCard rCard)
        {
            Assert.IsTrue(rCard != null);
            aSourceCards.Push(rCard);
        }
      
        public void ClearCards()
        {
            aPoolCards.Clear();
            aSourceCards.Clear();
        }
      
    }
}
