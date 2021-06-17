using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    [SerializeField]
    private GameObject _settingsOver;

    [SerializeField]
    private GameObject _settingsWindow;
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
        
        _settingsOver.active = false;
    }


    public void MouseOverEvent()
    {
        _settingsOver.active = true;
    }

    public void MouseClickEvent()
    {
       _settingsWindow.SetActive(true);  
    }

   



    }
