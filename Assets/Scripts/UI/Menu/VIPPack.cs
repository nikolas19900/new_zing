using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIPPack : MonoBehaviour
{

    [SerializeField]
    private GameObject VipPackBack;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter()
    {
        
        VipPackBack.active = true;
    }

    public void OnPointerExit()
    {
        VipPackBack.active = false;
    }


    public void OnClickVip()
    {
        Debug.Log("vip pack radi");
    }



}
