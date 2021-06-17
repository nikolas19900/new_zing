using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.Game.Utils
{
    class CardComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if (x.CardValue > y.CardValue)
            {
                return 1;
            }
            else if (x.CardValue < y.CardValue)
            {
                return -1;
            }

            // TODO: It can not be equal!
            return 0;
        }
    }
}
