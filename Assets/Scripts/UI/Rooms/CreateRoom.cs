using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Facebook.Unity;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Assets.Scripts.Managers;
using System;
using UnityEngine.UI;
using Assets.Scripts.Infastructure.Collections;
using MongoDB.Driver;
using System.Linq;

public class CreateRoom : MonoBehaviourPun
{
    [SerializeField]
    private Text _roomName;

    [SerializeField]
    private GameObject PlayerImage;

    [SerializeField]
    private Text _titulaText;

    //private RoomsCanvases _roomsCanvases;

    //private List<RoomListing> _roomListings;

    //private RoomListingMenu _roomListingMenu;

    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject lobbyScreen;

    ////[Header("Lobby Screen")]
    ////public TextMeshProUGUI player1NameText;

    ////public TextMeshProUGUI player2NameText;

   // public TextMeshProUGUI gameStartingText;

    private string imePrezime = "";

    private List<string> perms;

    /**
     * teksutala polja lokalizacija
     * 
     * */
    [SerializeField]
    private Text _PlayClassicText;

    [SerializeField]
    private Text _PlaySpecialText;
    [SerializeField]
    private Text LeaderBoardText;
    [SerializeField]
    private Text PrivateGameText;

    [SerializeField]
    private Text VIPPACKText;

    [SerializeField]
    private Text InviteText;


    [SerializeField]
    private Text GiftsText;
    [SerializeField]
    private Text FriendsText;
    [SerializeField]
    private Text SpecialOfferText;
    [SerializeField]
    private Text NoticeText;

    [SerializeField]
    private Text TutorialText;
    [SerializeField]
    private Text TutorialRulesText;
    [SerializeField]
    private Text HelpText;

    [SerializeField]
    private GameObject GoldCoinText;
    [SerializeField]
    private GameObject CoinText;
    [SerializeField]
    private GameObject CollectNow;
    

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
            //
            FB.API("me?fields=id,name,email", HttpMethod.POST, OnDataReceived, new WWWForm());
            FB.API("/me/picture?type=square&height=72&width=74", HttpMethod.GET, DisplayCurrentPlayerPic);
            FB.API("/me/picture?type=square&height=90&width=85", HttpMethod.GET, DisplayCurrentPlayerPic2);
        }

       
      //   var tt = PhotonNetwork.AllocateViewID(photonView);

    }

     void Start()
    {
        FB.API("me?fields=id,name,email", HttpMethod.POST, OnDataReceived, new WWWForm());
    }


    void DisplayCurrentPlayerPic(IGraphResult result)
    {
        if (result.Texture != null)
        {

            Texture2D tempTex = result.Texture;
            byte[] value = tempTex.EncodeToPNG();
            Texture2D tex = new Texture2D(74, 72);

            tex.LoadImage(value);
            
            // Assign texture to renderer's material.
            //GetComponent<Renderer>().material.mainTexture = tex;
            UnityEngine.UI.Image ProfilePic = PlayerImage.GetComponent<UnityEngine.UI.Image>();
            ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 74, 72), new Vector2());
        }
    }

    void DisplayCurrentPlayerPic2(IGraphResult result)
    {
        if (result.Texture != null)
        {

            Texture2D tempTex = result.Texture;
            MasterManager.GameSettings.PlayerImage = tempTex.EncodeToPNG();

        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();

            perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("ne radi kako treba");
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.Log("Token is: " + aToken.TokenString);

            FB.API("me?fields=id,name,email", HttpMethod.POST, OnDataReceived, new WWWForm());
            
            
            //&height=100&width=100
            FB.API("/me/picture?type=square&height=72&width=74", HttpMethod.GET, DisplayCurrentPlayerPic);
            FB.API("/me/picture?type=square&height=90&width=85", HttpMethod.GET, DisplayCurrentPlayerPic2);
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }


    private void OnDataReceived(IGraphResult result)
    {
      
        PlayerInfo playerInfo = new PlayerInfo();
        IDictionary<string, object> info = result.ResultDictionary;
        string playerId = "0";
        info.TryGetValue("id", out playerId);

        string playerName = "";
        info.TryGetValue("name", out playerName);
        string email = "";
        info.TryGetValue("email", out email);
        MasterManager.GameSettings.PlayerId = "" + playerId;
       IgracRegistracija igracRegistracija = new IgracRegistracija( Convert.ToInt64(playerId), playerName, email);
        var isFirst = igracRegistracija.OdradiRegistraciju();

        if (isFirst)
        {
            NetworkManager.instance.ChangeScene("Tutorial");
        }

        TituleView titule = new TituleView(playerId);
        _titulaText.text = titule.VratiNaziv();

        InicijalnaLokalizacija il = new InicijalnaLokalizacija();

        var root = il.setujJezike();

        if (MasterManager.GameSettings.DefaultLanguage == "English")
        {
            _PlayClassicText.text = root.play_classic[0].english;
            _PlaySpecialText.text = root.play_special[0].english;
            LeaderBoardText.text = root.leaderboard[0].english;
            PrivateGameText.text = root.private_game[0].english;
            VIPPACKText.text = root.vip_pack[0].english;
    
            InviteText.text = root.invites[0].english;

            GiftsText.text = root.gifts[0].english;

            FriendsText.text = root.friends[0].english;
            SpecialOfferText.text = root.special_offers[0].english;
            NoticeText.text = root.notice[0].english;
            TutorialText.text = root.tutorial[0].english;
            TutorialRulesText.text = root.tutorial[0].english;

            HelpText.text = root.help[0].english;

            var components = GoldCoinText.GetComponents<Component>();

            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page/goldCoinsText");
                }
            }
            var componentsCoins = CoinText.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCoins)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page/coins");
                }
            }

            var componentsCollectNow = CollectNow.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCollectNow)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page/FreeCoins");
                }
            }

            
        }
           
        if (MasterManager.GameSettings.DefaultLanguage == "Spanish")
        {
            _PlayClassicText.text = root.play_classic[1].spanish;
            _PlaySpecialText.text = root.play_special[1].spanish;
            LeaderBoardText.text = root.leaderboard[1].spanish;
            PrivateGameText.text = root.private_game[1].spanish;
            VIPPACKText.text = root.vip_pack[1].spanish;

            InviteText.text = root.invites[1].spanish;

            GiftsText.text = root.gifts[1].spanish;

            FriendsText.text = root.friends[1].spanish;
            SpecialOfferText.text = root.special_offers[1].spanish;
            NoticeText.text = root.notice[1].spanish;

            TutorialText.text = root.tutorial[1].spanish;

            TutorialRulesText.text = root.tutorial[1].spanish;

            HelpText.text = root.help[1].spanish;

            var components = GoldCoinText.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/GoldCoins_ES"); 
                    
                    //Debug.Log("exit");
                }
            }
            var componentsCoins = CoinText.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCoins)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/Coins_ES");
                }
            }

            var componentsCollectNow = CollectNow.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCollectNow)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/FreeCoins_ES");
                }
            }
            


        }
            
        if (MasterManager.GameSettings.DefaultLanguage == "Portugales")
        {
            _PlayClassicText.text = root.play_classic[2].portuguese;
            _PlaySpecialText.text = root.play_special[2].portuguese;
            LeaderBoardText.text = root.leaderboard[2].portuguese;
            PrivateGameText.text = root.private_game[2].portuguese;
            VIPPACKText.text = root.vip_pack[2].portuguese;
            InviteText.text = root.invites[2].portuguese;

            GiftsText.text = root.gifts[2].portuguese;
            FriendsText.text = root.friends[2].portuguese;
            SpecialOfferText.text = root.special_offers[2].portuguese;
            NoticeText.text = root.notice[2].portuguese;

            TutorialText.text = root.tutorial[2].portuguese;

            TutorialRulesText.text = root.tutorial[2].portuguese;

            HelpText.text = root.help[2].portuguese;

            var components = GoldCoinText.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/GoldCoins_POR");
                }
            }


            var componentsCoins = CoinText.GetComponents<Component>();

            foreach (var com in componentsCoins)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/Coins_POR");
                }
            }


            var componentsCollectNow = CollectNow.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCollectNow)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/FreeCoins_POR");
                }
            }
        }
           
        if (MasterManager.GameSettings.DefaultLanguage == "Russian")
        {
            _PlayClassicText.text = root.play_classic[3].russian;
            _PlaySpecialText.text = root.play_special[3].russian;
            LeaderBoardText.text = root.leaderboard[3].russian;
            PrivateGameText.text = root.private_game[3].russian;
            VIPPACKText.text = root.vip_pack[3].russian;
            InviteText.text = root.invites[3].russian;

            GiftsText.text = root.gifts[3].russian;
            FriendsText.text = root.friends[3].russian;
            SpecialOfferText.text = root.special_offers[3].russian;
            NoticeText.text = root.notice[3].russian;

            TutorialText.text = root.tutorial[3].russian;

            TutorialRulesText.text = root.tutorial[3].russian;

            HelpText.text = root.help[3].russian;
        }
            

        playerInfo.Id = playerId;
        playerInfo.Name = playerName;

        Debug.Log(playerInfo.Id + ", " + playerInfo.Name+","+email);
        
        GameManagerSingleton.Instance.AddPlayerInfo(playerInfo);
        imePrezime = playerInfo.Name;
        _roomName.text = imePrezime;
    }


    //public void FirstInitialize(RoomsCanvases roomsCanvases)
    //{

    //    // Debug.Log("drugo");

    //    _roomListings = new List<RoomListing>();
    //    _roomsCanvases = roomsCanvases;
    //    RoomListingMenu menu = new RoomListingMenu();

    //    var roomListings = _roomsCanvases.CreateOrJoinRoomCanvas.transform.Find("RoomListings").gameObject;
    //    roomListings.GetComponent<RoomListingMenu>().enabled = true;
    //    // Debug.Log("prvi");
    //    _roomListingMenu = roomListings.GetComponent<RoomListingMenu>();
    //    _roomListingMenu.OnEnable();
    //    //   Debug.Log("drugi");

    //    //  Debug.Log("treci");
    //}

    public void OnClickCreateRoom()
    {

        // FB.API("/me/picture?redirect=false", HttpMethod.GET, ProfilePhotoCallback);
        //FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, ProfilePhotoCallback);
        Debug.Log("photon:" + photonView.ToString());
        if (!PhotonNetwork.IsConnected)
            return;
       //Debug.Log("photon:2:" + photonView.ToString());
        // Debug.Log("onclick");
       /// PhotonNetwork.NickName = imePrezime;
        PhotonNetwork.LocalPlayer.NickName = _roomName.text;
        //_roomListings = _roomListingMenu.GeRoomListings();
        //var nameOfRoom = MasterManager.GameSettings.RoomName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;

        
        lobbyScreen.SetActive(true);
        
        ////MasterManager.GameSettings.DefaultLanguage = lll;
        //if (PhotonNetwork.CountOfRooms == 0)
        //{

        //    RoomOptions roomOptions = new RoomOptions();
        //    roomOptions.MaxPlayers = 2;
        //    roomOptions.PublishUserId = true;
        //    try { 
        //    PhotonNetwork.CreateRoom(nameOfRoom, roomOptions, TypedLobby.Default);
        //    }catch(Exception ex)
        //    {
        //        NetworkManager manager = new NetworkManager();
        //        PhotonNetwork.CreateRoom(nameOfRoom, roomOptions, TypedLobby.Default);
        //    }
        //}
        //else
        //{

        //    if (_roomListings.Count == 0)
        //    {

        //            RoomOptions roomOptions = new RoomOptions();
        //            roomOptions.MaxPlayers = 2;
        //            roomOptions.PublishUserId = true;

        //        try
        //        {
        //            PhotonNetwork.CreateRoom(nameOfRoom, roomOptions, TypedLobby.Default);
        //        }
        //        catch (Exception ex)
        //        {
        //            NetworkManager manager = new NetworkManager();
        //            PhotonNetwork.CreateRoom(nameOfRoom, roomOptions, TypedLobby.Default);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var room in _roomListings)
        //        {

        //            if (room.RoomInfo.PlayerCount < 2)
        //            {
        //                PhotonNetwork.JoinRoom(room.RoomInfo.Name);
        //                // Debug.Log("tu sam" + _roomListings.Count);
        //                try
        //                {
        //                    PhotonNetwork.JoinRoom(room.RoomInfo.Name);
        //                }
        //                catch (Exception ex)
        //                {
        //                    NetworkManager manager = new NetworkManager();
        //                    PhotonNetwork.JoinRoom(room.RoomInfo.Name);
        //                }
        //            }

        //        }
        //    }

    }

    }

    //private void ProfilePhotoCallback(IGraphResult result)
    //{
    //    //if (String.IsNullOrEmpty(result.Error) && !result.Cancelled)
    //    //{ //if there isn't an error
    //    //    IDictionary data = result.ResultDictionary["data"] as IDictionary; 
    //    //    string photoURL = data["url"] as String;
    //    //    Debug.Log("url:" + photoURL); 
    //    //}
    //    if (result.Texture != null)
    //    {

    //       // Image ProfilePic = DialogProfilePic.GetComponent<Image>();

    //       // ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());

    //    }
    //}

    //public void OnLeaveRoom()
    //{
    //    _roomsCanvases.CurrentRoomCanvas.Hide();
    //    _roomsCanvases.CreateOrJoinRoomCanvas.Show();

    //    PhotonNetwork.LeaveRoom();
    //    //SetScreen(mainScreen);
    //}


    //public override void OnCreatedRoom()
    //{
    //    // Debug.Log("Created room successfully");
    //    print("OnCreatedRoom" + PhotonNetwork.LocalPlayer.NickName);

    //}


    //[PunRPC]
    //private void UpdateLobbyUI()
    //{
    //    // set the player name texts
    //    player1NameText.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;

    //    player2NameText.text = PhotonNetwork.PlayerList.Length == 2
    //        ? PhotonNetwork.CurrentRoom.GetPlayer(2).NickName
    //        : "...";

    //    // set the game starting text
    //    if (PhotonNetwork.PlayerList.Length == 2)
    //    {
    //        gameStartingText.gameObject.SetActive(true);
    //        if (PhotonNetwork.IsMasterClient)
    //            Invoke("TryStartGame", 3.0f);
    //    }
    //}

    //public void SetScreen(GameObject screen)
    //{
    //    // disable all screens
    //    mainScreen.SetActive(false);
    //    lobbyScreen.SetActive(false);
    //    // enable the requested screen
    //    screen.SetActive(true);
    //}

    //public void TryStartGame()
    //{
    //    if (PhotonNetwork.PlayerList.Length == 2)
    //    {
            
    //        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Game");
    //        _roomsCanvases.CurrentRoomCanvas.Hide();
    //        _roomsCanvases.CreateOrJoinRoomCanvas.Show();
    //    }

    //    else
    //        gameStartingText.gameObject.SetActive(false);
    //}

    //public override void OnLeftRoom()
    //{

    //    //Debug.Log("successfully lefted the room");
    //}

    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    UpdateLobbyUI();
    //}


    //public override void OnJoinedRoom()
    //{
        
    //    _roomsCanvases.CurrentRoomCanvas.Show();
    //    _roomsCanvases.CreateOrJoinRoomCanvas.Hide();
        
    //    photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    //}

    //public override void OnCreateRoomFailed(short returnCode, string message)
    //{
    //    //Debug.Log("Created room failed" + message);
    //}
    



