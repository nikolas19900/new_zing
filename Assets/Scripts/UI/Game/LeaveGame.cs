using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLeaveRoom()
    {


        //PhotonNetwork.LeaveRoom();
       
        PhotonNetwork.LeaveLobby();
        //DontDestroyOnLoad(GameManagerSingleton.Instance.gameObject);
        //PhotonNetwork.LoadLevel(0);
        NetworkManager.instance.ChangeScene("Menu");
        //SetScreen(mainScreen);
    }
}
