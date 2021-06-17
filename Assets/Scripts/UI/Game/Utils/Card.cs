using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.UI.Game.Utils
{
    public class Card
    {
        public CardSignType CardSign { get; set; }
        public  CardValueType CardValue { get; set; }

        public GameObject Owner { get; set; }

        public Card(CardSignType cardSign, CardValueType cardValue, GameObject owner)
        {
            CardSign = cardSign;
            CardValue = cardValue;
            Owner = owner;
        }

    }
}
