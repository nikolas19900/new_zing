using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invite : MonoBehaviour
{

    [SerializeField]
    private GameObject InviteBack;

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

        InviteBack.active = true;
    }

    public void OnPointerExit()
    {
        InviteBack.active = false;
    }


    public void OnClickInvite()
    {
        Debug.Log("invite radi");
    }
}
