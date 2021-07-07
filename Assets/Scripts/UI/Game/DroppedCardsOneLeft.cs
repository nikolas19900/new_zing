using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppedCardsOneLeft : MonoBehaviour
{
    private Canvas _canvas;


    public DroppedCardsOneLeft(Canvas canvas)
    {
        _canvas = canvas;
    }


    public void SetCanvas()
    {
        //Debug.Log("DroppedCardsOneLeft");
        //foreach (Transform element in _canvas.transform)
        //{
        //    var firstCard = element.Find("FirstCardSelected").gameObject;
        //    firstCard.active = true;
        //}

        foreach (Transform element in _canvas.transform)
        {

            element.GetComponent<EventTrigger>().enabled = true;
        }
    }

    
}
