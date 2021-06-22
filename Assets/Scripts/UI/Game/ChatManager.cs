using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private PhotonView photonView;
    [SerializeField]
    private GameObject Chat;
    
    public Text ChatText;

    [SerializeField]
    private InputField ChatInputField;
    [SerializeField]
    private GameObject Content;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
        if (ChatInputField.isFocused)
        {
            
           
            if (Input.GetKeyUp(KeyCode.Return))
            {
                
                ChatText.text = PhotonNetwork.LocalPlayer.NickName+":" + ChatInputField.text;
                //GameObject newGO = new GameObject("myTextGO");
                //GameObject vv = Instantiate(ChatText.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                //vv.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                //vv.transform.SetParent(Content.transform);
                photonView.RPC("SendMsg", RpcTarget.All, ChatText.text);
                //if ( Content.transform.childCount == 40)
                //{
                //    for (int i = 0; i < 10;i++)
                //    {
                       
                //        Destroy(Content.transform.GetChild(i).gameObject);
                //    }
                    
                //}
            }
        }

    }

    [PunRPC]
    public void SendMsg(string msg)
    {

        ChatText.text = msg;
        GameObject vv = Instantiate(ChatText.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        vv.transform.localScale = new Vector3(0.789f, 0.789f, 0);
        vv.transform.SetParent(Content.transform);
        if (Content.transform.childCount == 40)
        {
            for (int i = 0; i < 10; i++)
            {

                Destroy(Content.transform.GetChild(i).gameObject);
            }

        }
    }
}
