using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSwitch : MonoBehaviour
{

    [SerializeField]
    private GameObject _soundSwitch;


    [SerializeField]
    private GameObject _soundSwitchOff;
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
        if (!_soundSwitch.active)
        {

            _soundSwitch.active = true;
            _soundSwitchOff.active = false;
        }
        else
        {
            _soundSwitch.active = false;
            _soundSwitchOff.active = true;
        }
    }
}
