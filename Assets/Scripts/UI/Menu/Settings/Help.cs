using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{

    [SerializeField]
    private GameObject _btnOver;
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

        _btnOver.active = true;
    }

    public void onExitOver()
    {
        _btnOver.active = false;

    }
}
