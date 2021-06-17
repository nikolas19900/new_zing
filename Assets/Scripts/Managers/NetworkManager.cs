using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System;
using Assets.Scripts.Managers;
using MongoDB.Driver;
using Assets.Scripts.Infastructure.Collections;
using MongoDB.Bson;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        
    }


    void Start()
    {

        //ovdje provjeravam jezik u bazi  a onda kad lodujem setting ekran izvadim samo podatak i prikazem ga na gui.

       

        if (!PhotonNetwork.IsConnected) {
            print("Connecting to server");
            AppSettings app = new AppSettings();
            app.AppIdRealtime = "5c94b49b-403f-4e0d-9e3a-68ab0359aade";
            var value = PhotonNetwork.ConnectUsingSettings(app);
        }
    }



    public override void OnConnectedToMaster()
    {
        print("Connected to server");
        //  PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        //  PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        
        print(PhotonNetwork.LocalPlayer.NickName);
        //koristimo kako bi povukli listu
        //if (!PhotonNetwork.InLobby)
        //    PhotonNetwork.JoinLobby();

    }

    

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server:" + cause.ToString());
    }

    [PunRPC]
    // changes the scene using Photon's system
    public void ChangeScene(string sceneName)
    {
       // Debug.Log("ime scene:" + sceneName);
        DontDestroyOnLoad(GameManagerSingleton.Instance.gameObject);
        PhotonNetwork.LoadLevel(sceneName);
    }
}
