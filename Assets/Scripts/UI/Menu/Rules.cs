using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{

    [SerializeField]
    private GameObject _rulesOver;

    [SerializeField]
    private GameObject _rulesWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseExitEvent()
    {

        _rulesOver.active = false;
    }


    public void MouseOverEvent()
    {
        _rulesOver.active = true;
    }

    public void MouseClickEvent()
    {
        //Debug.Log("settings radi");

        _rulesWindow.SetActive(true);

    }
}
