using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{

    [SerializeField]
    private GameObject _goldCoinOver;
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

        _goldCoinOver.active = true;
    }

    public void onExitOver()
    {
        _goldCoinOver.active = false;
       
    }
}
