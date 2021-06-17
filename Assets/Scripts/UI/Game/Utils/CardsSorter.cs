using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.Game.Utils
{
    class CardsSorter
    {
        public static List<Card> SortCards(List<Card> cards)
        {
            if (cards == null)
                return null;

            List<Card> sortedList = new List<Card>();

            List<Card> heartCards = SortCardsByType(CardSignType.Heart, cards);
            List<Card> clubCards = SortCardsByType(CardSignType.Club, cards);
            List<Card> diamondCards = SortCardsByType(CardSignType.Diamond, cards);
            List<Card> spadeCards = SortCardsByType(CardSignType.Spade, cards);

            foreach (var card in heartCards)
            {
                sortedList.Add(card);
            }

            foreach (var card in clubCards)
            {
                sortedList.Add(card);
            }

            foreach (var card in diamondCards)
            {
                sortedList.Add(card);
            }

            foreach (var card in spadeCards)
            {
                sortedList.Add(card);
            }

            return sortedList;
        }

        private static List<Card> SortCardsByType(CardSignType signType, List<Card> cards)
        {
            List<Card> mylist = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].CardSign == signType)
                {
                    mylist.Add(cards[i]);
                }
            }

            mylist.Sort(new CardComparer());

            return mylist;
        }


    }
}
