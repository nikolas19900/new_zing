using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCardsTwoLeft : MonoBehaviour
{

    private string _firstCard;
    private string _secondCard;
    private Canvas _canvasFirstDeck;
    private Canvas _canvasCurrentPlayer;

    public DroppedCardsTwoLeft(string firstCard,string secondCard,Canvas canvasFirstDeck,Canvas canvasCurrentPlayer)
    {
       // Debug.Log("DroppedCardsTwoLeft");
        _firstCard = firstCard;
        _secondCard = secondCard;
        _canvasFirstDeck = canvasFirstDeck;
        _canvasCurrentPlayer = canvasCurrentPlayer;
    }
    

    public string[]  TakeActionEqualsName()
    {
        
            int i = 0;
            List<string> list = new List<string>();
            foreach (Transform transform in _canvasFirstDeck.transform)
            {

                GameObject tempGameObject = transform.gameObject;
                string value = tempGameObject.name;
                var index = value.IndexOf("(");
                string CardName = value.Substring(0, index);
                
                list.Add(CardName);
                Destroy(transform.gameObject);
            }
            string[] listArray = list.ToArray();
        
            foreach (Transform element in _canvasCurrentPlayer.transform)
            {

                var firstCard = element.Find("FirstCardSelected").gameObject;
                firstCard.active = true;
            }
        return listArray;    
    }

    public string[] TakeActionJDropped()
    {
        int i = 0;
        List<string> list = new List<string>();
        foreach (Transform transform in _canvasFirstDeck.transform)
        {

            GameObject tempGameObject = transform.gameObject;
            string value = tempGameObject.name;
            var index = value.IndexOf("(");
            string CardName = value.Substring(0, index);
            list.Add(CardName);
            Destroy(transform.gameObject);
        }
        string[] listArray = list.ToArray();

        foreach (Transform element in _canvasCurrentPlayer.transform)
        {
            var firstCard = element.Find("FirstCardSelected").gameObject;
            firstCard.active = true;
        }

        return listArray;
    }


    public void SetCanvas()
    {
        
        foreach (Transform element in _canvasCurrentPlayer.transform)
        {
            var firstCard = element.Find("FirstCardSelected").gameObject;
            firstCard.active = true;
        }
    }

}
