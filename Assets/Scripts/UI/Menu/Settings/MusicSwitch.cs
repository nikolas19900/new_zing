using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{

    [SerializeField]
    private GameObject _musicSwitch;


    [SerializeField]
    private GameObject _musicSwitchOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickMouse()
    {
        if (!_musicSwitch.active)
        {

        _musicSwitch.active = true;
            _musicSwitchOff.active = false;
        }
        else
        {
            _musicSwitch.active = false;
            _musicSwitchOff.active = true;
        }
    }

   
}
