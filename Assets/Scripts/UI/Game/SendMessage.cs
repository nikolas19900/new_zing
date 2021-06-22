using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SendMessage : MonoBehaviourPun, IPunObservable
{

    [SerializeField]
    private GameObject SendButtonOver;

    public Text ChatText;

    [SerializeField]
    private InputField ChatInputField;
    [SerializeField]
    private GameObject Content;

    [SerializeField]
    private PhotonView photonView;

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
        SendButtonOver.SetActive(true);
    }

    public void OnPointerExit()
    {
        SendButtonOver.SetActive(false);
    }

    public void OnClickMouse()
    {
        ChatText.text = PhotonNetwork.LocalPlayer.NickName + ":" + ChatInputField.text;
        //GameObject newGO = new GameObject("myTextGO");
        //GameObject vv = Instantiate(ChatText.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        //vv.transform.localScale = new Vector3(0.789f, 0.789f, 0);
        //vv.transform.SetParent(Content.transform);
        photonView.RPC("SendMsg", RpcTarget.All, ChatText.text);
        if (Content.transform.childCount == 40)
        {
            for (int i = 0; i < 10; i++)
            {

                Destroy(Content.transform.GetChild(i).gameObject);
            }

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new NotImplementedException();
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
