using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gifts : MonoBehaviour
{
    [SerializeField]
    private GameObject GiftsBack;

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

        GiftsBack.active = true;
    }

    public void OnPointerExit()
    {
        GiftsBack.active = false;
    }


    public void OnClickGifts()
    {
        Debug.Log("Gifts radi");
    }

}
