using Assets.Scripts.Managers;
using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Facebook.Unity;

public class StartAction : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject StandardButtonOver;

    [SerializeField]
     private GameObject RoomListingUI;

    private List<RoomListing> _roomListings;

    private RoomListingMenu _roomListingMenu;
    private byte[] tempPicture;

     void Awake()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();

        }
        PhotonNetwork.JoinLobby();
        Debug.Log("pokrenuo je join lobby");
    }
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


    public void OnClickAction()
    {
        var nameOfRoom = MasterManager.GameSettings.RoomName;
        //Debug.Log("broj:"+_roomListings.Count);

        if (PhotonNetwork.CountOfRooms == 0)
        {

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            roomOptions.PublishUserId = true;
            AssignTeam(0);
            PhotonNetwork.CreateRoom(nameOfRoom, roomOptions, TypedLobby.Default);
           
            Debug.Log("kreiranje sobe prvo");
            
        }
        else
        {
            if(_roomListings == null) {
             //   Debug.Log("ide log roomListing:");
            _roomListings = RoomListingMenu.Instance.GetRoomListings();
            }

            Debug.Log("joinovanje sobe" + _roomListings.Count);




            if (_roomListings.Count == 0)
            {

                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 4;
                roomOptions.PublishUserId = true;
                AssignTeam(0);
                PhotonNetwork.CreateRoom(nameOfRoom, roomOptions, TypedLobby.Default);
                
                Debug.Log("kreiranje sobe");


            }
            else
            {


                //PhotonNetwork.JoinRandomRoom();
                //_roomListings = _roomListingMenu.GeRoomListings();

                //loadBalancingClient.OpJoinRandomRoom();
                if (!PhotonNetwork.InRoom)
                {
                    foreach (var room in _roomListings)
                    {

                        if (room.RoomInfo.PlayerCount < 4)
                        {
                            AssignTeam(room.RoomInfo.PlayerCount);
                            PhotonNetwork.JoinRoom(room.RoomInfo.Name);
                            
                            Debug.Log("joinovanje sobe" + room.RoomInfo.PlayerCount);
                           
                        }
                        else
                        {
                            RoomOptions roomOptions = new RoomOptions();
                            roomOptions.MaxPlayers = 4;
                            roomOptions.PublishUserId = true;

                            AssignTeam(0);
                            PhotonNetwork.CreateRoom(nameOfRoom, roomOptions, TypedLobby.Default);
                            
                            Debug.Log("kreiranje zadnje");
                        }

                    }
                }

            }
        }
      
    }


    void AssignTeam(int size)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        // int size = PhotonNetwork.PlayerList.GetLength(0);
        //int size = PhotonNetwork.CurrentRoom.PlayerCount;
        

        if (size % 2 == 0)
        {
            
            hash.Add("Team", "Blue");
            
        }
        else
        {
            // hash.Add("Team", 1);
            hash.Add("Team", "Red");
           
        }
        hash.Add("Picture", MasterManager.GameSettings.PlayerImage);
        hash.Add("State", "active");

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public override void OnCreatedRoom()
    {
        // Debug.Log("you just created a room");

        // DontDestroyOnLoad(GameManagerSingleton.Instance.gameObject);
        // PhotonNetwork.LoadLevel(1);
        //NetworkManager.instance.ChangeScene("Game");
        
    }

    public override void OnJoinedRoom()
    {
        
      
       
        // Debug.Log("join");
        // DontDestroyOnLoad(GameManagerSingleton.Instance.gameObject);
        //PhotonNetwork.LoadLevel(1);
        NetworkManager.instance.ChangeScene("Game");


    }

    

   

   

   

}
