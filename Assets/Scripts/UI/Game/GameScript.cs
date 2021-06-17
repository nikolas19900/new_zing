using Assets.Scripts.Managers;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class GameScript : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private PhotonView _currentPhotonView;
    [SerializeField]
    private Text BluePlayerNameValue;

    [SerializeField]
    private Text RedPlayerNameValue;

    private List<RoomListing> _roomListings;

    private RoomListingMenu _roomListingMenu;


    private bool isGameStarted = false;
    private List<int> playerList = new List<int>();
  

    void Awake()
    {
       
    }

     void OnEnable()
    {
      
       
    }
    // Start is called before the first frame update
    void Start()
    {


        //Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;



        //Debug.Log("mine:" + PhotonNetwork.LocalPlayer.CustomProperties["Team"]);


        //foreach (var vv in value)
        //{
           
        //    if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"].Equals("Blue"))
        //    {
        //        BluePlayerNameValue.text += PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

        //    }
        //    else
        //    {
        //        RedPlayerNameValue.text += PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
        //    }

        //    playerList.Add(vv.Key);

        //}

        _currentPhotonView.RPC("UpdatePlayersName", RpcTarget.All);


        if (PhotonNetwork.CurrentRoom.PlayerCount == 4) {
          
           
            isGameStarted = true;
            photonView.RPC("StartGame", RpcTarget.All, isGameStarted);
         
        
        }
    }

    

   

    // Update is called once per frame
    void Update()
    {
        try { 
        if(PhotonNetwork.PlayerList[1].IsInactive)
        {
            
        }
        }catch(Exception ex)
        {
            //Debug.Log("tacno");
        }

       

    }

    [PunRPC]
    public void UpdatePlayersName()
    {
        Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;
        BluePlayerNameValue.text = "";
        RedPlayerNameValue.text = "";
        foreach (var vv in value)
        {
           
            
            if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"].Equals("Blue"))
            {
                BluePlayerNameValue.text += PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

            }
            else
            {
                RedPlayerNameValue.text += PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
            }

          

        }
    }
  


    [PunRPC]
public void StartGame(bool state)
{
    isGameStarted = state;

    var team = PunTeams.Team.blue;
    var teamRed = PunTeams.Team.red;
        //Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;

        ////PhotonNetwork.CurrentRoom.
        //int i = 0;
        //foreach (var vv in value)
        //{
        //    if (i % 2 == 0)
        //    {
        //        playerList.Add(vv.Value);
        //        vv.Value.SetTeam(team);
        //    }
        //    else
        //    {
        //        playerList.Add(vv.Value);
        //        vv.Value.SetTeam(teamRed);
        //    }
        //    i++;

        //    // Debug.Log(vv.Value.NickName);
        //}
        PhotonNetwork.CurrentRoom.GetPlayer(1).SetTeam(team);
        PhotonNetwork.CurrentRoom.GetPlayer(2).SetTeam(teamRed);
        PhotonNetwork.CurrentRoom.GetPlayer(3).SetTeam(team);
        PhotonNetwork.CurrentRoom.GetPlayer(4).SetTeam(teamRed);

        if (PhotonNetwork.CurrentRoom.GetPlayer(1).GetTeam() == PunTeams.Team.blue && PhotonNetwork.CurrentRoom.GetPlayer(3).GetTeam() == PunTeams.Team.blue)
        {
            BluePlayerNameValue.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName + PhotonNetwork.CurrentRoom.GetPlayer(3).NickName;
        }
        if (PhotonNetwork.CurrentRoom.GetPlayer(2).GetTeam() == PunTeams.Team.red && PhotonNetwork.CurrentRoom.GetPlayer(4).GetTeam() == PunTeams.Team.red)
        {
            RedPlayerNameValue.text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName + PhotonNetwork.CurrentRoom.GetPlayer(4).NickName;
        }


        Debug.Log("team igrac:" + PhotonNetwork.CurrentRoom.GetPlayer(1).GetTeam());
        if (PhotonNetwork.IsMasterClient && isGameStarted)
        {

        }
}
   
}
