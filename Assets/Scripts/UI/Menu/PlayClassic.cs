using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayClassic : MonoBehaviour
{
    [SerializeField]
    private GameObject _regularButton;

    [SerializeField]
    private GameObject _overButton;
    //[SerializeField]
    //private GameObject _ScrollRect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onEnterOver()
    {
        _regularButton.active = false;
        _overButton.active = true;
        //mozda ce za kasnije trebati
        //var components = _ScrollRect.GetComponents<Component>();
        
        //foreach (var com in components)
        //{
        //    var vv = com.GetType();
        //    if (typeof(ScrollRect).IsAssignableFrom(vv))
        //    {
        //        ScrollRect temp = (ScrollRect)com;
        //        if (temp.horizontalNormalizedPosition == 0)
        //        {
        //            temp.horizontalNormalizedPosition = 1;
        //        }
        //        else
        //        {
        //            temp.horizontalNormalizedPosition = 0;
        //        }
        //    }
        //}
    }
    
    public void onExitOver()
    {
        _overButton.active = false;
        _regularButton.active = true;
    }

    public void MouseDragEvent()
    {
       


        //Debug.Log("classic move radi");
    }
}
