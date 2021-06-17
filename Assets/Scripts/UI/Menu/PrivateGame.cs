using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateGame : MonoBehaviour
{

    [SerializeField]
    private GameObject _regularButton;

    [SerializeField]
    private GameObject _overButton;


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
    }

    public void onExitOver()
    {
        _overButton.active = false;
        _regularButton.active = true;
    }
}
