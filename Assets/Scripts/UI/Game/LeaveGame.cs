using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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
        Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;

        foreach (var vv in value)
        {


            if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key) == PhotonNetwork.LocalPlayer)
            {
                ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                hash["State"] = "inactive";
                PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);
            }
        }
        if(GameScript.isGameStarted == false)
        {
            PhotonNetwork.LeaveRoom();
        }
           // if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"].Equals("Blue"))
            PhotonNetwork.LeaveLobby();
        //DontDestroyOnLoad(GameManagerSingleton.Instance.gameObject);
        //PhotonNetwork.LoadLevel(0);
        NetworkManager.instance.ChangeScene("Menu");
        //SetScreen(mainScreen);
    }
}
