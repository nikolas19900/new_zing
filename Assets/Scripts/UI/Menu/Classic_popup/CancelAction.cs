using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelAction : MonoBehaviour
{

    [SerializeField]
    private GameObject StandardButtonOver;


    
    public GameObject lobbyScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerOver()
    {
        StandardButtonOver.SetActive(true);
    }

    public void OnPointerExit()
    {
        StandardButtonOver.SetActive(false);
    }


    public void OnClickCancel()
    {
        lobbyScreen.SetActive(false);
    }
}
