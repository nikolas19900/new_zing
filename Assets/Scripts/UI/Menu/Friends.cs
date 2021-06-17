using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : MonoBehaviour
{

    [SerializeField]
    private GameObject FriendsBack;
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

        FriendsBack.active = true;
    }

    public void OnPointerExit()
    {
        FriendsBack.active = false;
    }


    public void OnClickFriends()
    {
        Debug.Log("Friends radi");
    }
}
