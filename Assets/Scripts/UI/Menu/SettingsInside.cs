using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsInside : MonoBehaviour
{

    [SerializeField]
    private GameObject _closeOver;

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


    public void MouseOverEvent()
    {
        _closeOver.active = true;
    }

    public void MouseClickEvent()
    {
        
        _settingsWindow.active = false;
    }

    public void MouseExitEvent()
    {

        _closeOver.active = false;
    }
}
