using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.Game.CheckCards
{
    class CalculatePoints
    {
        private List<string> _listOfCards;

        public CalculatePoints(List<string> list)
        {
            this._listOfCards = list;
            //Debug.Log("ukupno u listi" + _listOfCards.Count);
        }

        public int GetPoints()
        {
            int countA = 0;
            int countTWo = 0;
            int countT = 0;
            int countJ = 0;
            int countD = 0;
            int countK = 0;
            int points = 0;
            foreach (string card in _listOfCards)
            {
                //Debug.Log("karta:" + card);
                if (card.Contains("A_"))
                {
                    //countA++;
                    //Debug.Log("ukupno ace:" + countA);
                    countA++;
                }
                else if (card.Contains("10_D"))
                {
                    countT = 1;
                    //int value = 2;
                    //points += value; 
                    //Debug.Log("ukupno ten:" + countT);
                }
                else if (card.Contains("2_S"))
                {
                    countTWo = 1;
                    //int value = 2;
                    //points += value;
                   // Debug.Log("ukupno TWO:" + countTWo);
                }

                else if (card.Contains("J_"))
                {
                    //points++;
                    countJ++;
                    //Debug.Log("ukupno j:" + countJ);
                }
                else if (card.Contains("Q_"))
                {
                    countD++;
                    //Debug.Log("ukupno D:" + countD);
                    //points++;
                }
                else if (card.Contains("K_"))
                {
                    countK++;
                    //Debug.Log("ukupno K:" + countK);
                    //points++;
                }
            }

            points = countA + (countTWo * 2) + (countT * 2) + countJ + countD + countK;
            //Debug.Log("ppp" + points);
            return points;
        }
    }
}
