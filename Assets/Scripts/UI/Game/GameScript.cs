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
using Assets.Scripts.UI.Game;
using Facebook.Unity;
using Assets.Scripts.UI.Game.CheckCards;
using Assets.Scripts.Infastructure.PARSER;
using System.Linq;

public class GameScript : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private PhotonView _currentPhotonView;
    [SerializeField]
    private Text BluePlayerNameValue;

    [SerializeField]
    private Text RedPlayerNameValue;

    [SerializeField]
    private Text DealerName;
    [SerializeField]
    private GameObject DealerImage;

    [SerializeField]
    private Text CardsBlueText;

    [SerializeField]
    private Text PointsBlueText;
    [SerializeField]
    private Text ZingBlueText;

    [SerializeField]
    private Text TotalBlueText;

    [SerializeField]
    private Text CardsRedText;

    [SerializeField]
    private Text PointsRedText;
    [SerializeField]
    private Text ZingRedText;

    [SerializeField]
    private Text TotalRedText;
    [SerializeField]
    private Text Text;

    [SerializeField]
    private Text LeaveGameText;

    [SerializeField]
    private Canvas canvacesOfFirstDeck;


    [SerializeField]
    private Image FirstPlayerBorder;

    [SerializeField]
    private Image FirstPlayerImage;

    [SerializeField]
    private Image SecondPlayerBorder;

    [SerializeField]
    private Image SecondPlayerImage;

    [SerializeField]
    private Image ThirdPlayerBorder;

    [SerializeField]
    private Image ThirdPlayerImage;

    [SerializeField]
    private Text FirstPlayerName;

    [SerializeField]
    private Text SecondPlayerName;

    [SerializeField]
    private Text ThirdPlayerName;

    [SerializeField]
    private Canvas LastCardCanvas;

    [SerializeField]
    private GameObject CardImageValueLastCard;

    [SerializeField]
    private GameObject TeamImageLastCard;

    [SerializeField]
    private Canvas canvacesOfCurrentPlayer;

    [SerializeField]
    private GameObject DealerBoard;

    [SerializeField]
    private GameObject TimeOfMove;

    public static GameScript player;

    public static bool isAviableToMove = false;

    public static bool isGameStarted = false;
   

    private ZingDealer _zingDealer;

    private List<string> RemainingCardsList;


    private List<string> listTalon;


    private string[] talonArray;
   
    private List<string> _listOfCards = new List<string>();


    private List<string> _cardsOfFirstPlayer;

    private List<string> _cardsOfSecondPlayer;

    private List<string> _cardsOfThirdPlayer;
    private List<string> _cardsOfFourthPlayer;
    private System.Random _random;

    private float _positionTolerance = -1.5f;
    private List<float> _tolerances;

    private bool isArrangeCard = false;

    private int currentInstance = 0;


    void Awake()
    {
        isArrangeCard = false;
        player = this;
    }

     void OnEnable()
    {
      
       
    }
    // Start is called before the first frame update
    void Start()
    {

        ParseJson json = new ParseJson();
        var root = json.DeserializeGame();

        if (MasterManager.GameSettings.DefaultLanguage == "English")
        {
            CardsBlueText.text = root.cards[0].english;
            PointsBlueText.text = root.points[0].english;
            ZingBlueText.text = root.zings[0].english;
            TotalBlueText.text = root.total[0].english;

            CardsRedText.text = root.cards[0].english;
            PointsRedText.text = root.points[0].english;
            ZingRedText.text = root.zings[0].english;
            TotalRedText.text = root.total[0].english;

            Text.text = root.dealer[0].english;

            LeaveGameText.text = root.leave[0].english;
        }
        if (MasterManager.GameSettings.DefaultLanguage == "Spanish")
        {
            CardsBlueText.text = root.cards[1].spanish;
            PointsBlueText.text = root.points[1].spanish;
            ZingBlueText.text = root.zings[1].spanish;
            TotalBlueText.text = root.total[1].spanish;

            CardsRedText.text = root.cards[1].spanish;
            PointsRedText.text = root.points[1].spanish;
            ZingRedText.text = root.zings[1].spanish;
            TotalRedText.text = root.total[1].spanish;

            Text.text = root.dealer[1].spanish;

            LeaveGameText.text = root.leave[1].spanish;
        }
        if (MasterManager.GameSettings.DefaultLanguage == "Portugales")
        {
            CardsBlueText.text = root.cards[2].portuguese;
            PointsBlueText.text = root.points[2].portuguese;
            ZingBlueText.text = root.zings[2].portuguese;
            TotalBlueText.text = root.total[2].portuguese;

            CardsRedText.text = root.cards[2].portuguese;
            PointsRedText.text = root.points[2].portuguese;
            ZingRedText.text = root.zings[2].portuguese;
            TotalRedText.text = root.total[2].portuguese;

            Text.text = root.dealer[2].portuguese;

            LeaveGameText.text = root.leave[2].portuguese;
        }
        if (MasterManager.GameSettings.DefaultLanguage == "Russian")
        {
            CardsBlueText.text = root.cards[3].russian;
            PointsBlueText.text = root.points[3].russian;
            ZingBlueText.text = root.zings[3].russian;
            TotalBlueText.text = root.total[3].russian;

            CardsRedText.text = root.cards[3].russian;
            PointsRedText.text = root.points[3].russian;
            ZingRedText.text = root.zings[3].russian;
            TotalRedText.text = root.total[3].russian;

            Text.text = root.dealer[3].russian;
            LeaveGameText.text = root.leave[3].russian;
        }
            _currentPhotonView.RPC("UpdatePlayersName", RpcTarget.All);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 4) {
          
           
            isGameStarted = true;
            photonView.RPC("StartGame", PhotonNetwork.CurrentRoom.GetPlayer(1), isGameStarted);
         
        }

        SetSideOfBorderImages();
        
    }

    

   

    // Update is called once per frame
    void Update()
    {
        try
        {

            var players = PhotonNetwork.CurrentRoom.Players;
            foreach (var current in players)
            {
                //vazno !!!!!!!!!!!!
                
                if (players[current.Key].CustomProperties["State"].Equals("inactive"))
                {
                    if (players[current.Key].IsMasterClient && isGameStarted == false)
                    {
                        _currentPhotonView.RPC("SwitchAllSceneAndLeave", RpcTarget.Others);
                    }
                    //ako jedan od igraca nije aktivan aktivirace se ova linija koda
                    // Debug.Log("radi");
                    //_currentPhotonView.RPC("ReadLine", players[current.Key]);
                }

                
            }


        }
        catch (Exception ex)
        {
            //Debug.Log("tacno");
        }
        if (!isArrangeCard)
        {

            
            ArrangeCards();
        }

    }

    [PunRPC]
    public void SwitchAllSceneAndLeave()
    {
        PhotonNetwork.LeaveRoom();
        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash["Picture"] = null;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        PhotonNetwork.LeaveLobby();
        //DontDestroyOnLoad(GameManagerSingleton.Instance.gameObject);
        //PhotonNetwork.LoadLevel(0);
        NetworkManager.instance.ChangeScene("Menu");
    }

    private void InitTalonCards()
    {
        //float startPosition = 0.5f;
        float startPosition = 1000f;

        int i = 0;
        Vector3[] arrayPosition = new Vector3[4];
        string[] arrayCards = { "", "", "", "" };
        foreach (var obj in _zingDealer.TalonCards)
        {
            GameObject gameObj = (GameObject)obj;

            
            Vector3 position = new Vector3(startPosition, 800f);
            gameObj.transform.localPosition = position;
            gameObj.transform.localScale = new Vector3(0.789f, 0.789f, 0);
            //PhotonNetwork.Instantiate("Prefabs/CardPrefabsStartSVG/" + gameObj.name, new Vector3(startPosition, 700f, 0), Quaternion.identity).transform.SetParent(canvacesOfFirstDeck.transform);
            GameObject firstDeck = (GameObject) PhotonNetwork.Instantiate("Prefabs/CardPrefabsStartSVG/"+gameObj.name, new Vector3(startPosition, 700f, 0), Quaternion.identity);
            
            firstDeck.transform.localScale = new Vector3(0.789f, 0.789f, 0);
            arrayPosition[i] = position;
            arrayCards[i] = "" + gameObj.name;
            firstDeck.transform.SetParent(canvacesOfFirstDeck.transform);

            i++;
            startPosition += 50f;
            //multiplier -= 5f;
           
        }

        

        _currentPhotonView.RPC("SendInitTalon", RpcTarget.Others, arrayCards);


        isArrangeCard = false;



    }


    public void ArrangeCards()
    {

        int i = 0;
        Vector3[] arrayVectors = new Vector3[4];

        try
        {
            float startPosition = 1000f;
            foreach (var val in listTalon)
            {
              

                var card = canvacesOfFirstDeck.transform.Find($"{val}(Clone)").gameObject;


                float x = -Time.deltaTime * (card.transform.position.x + (float)_tolerances.ToArray().GetValue(i)) * 0.25f;
               
                Vector3 position = new Vector3(x,
                    Time.deltaTime * (-card.transform.position.y + (float)_tolerances.ToArray().GetValue(i)) * 0.25f);

                card.transform.position += position;
                float x2 = -Time.deltaTime * (startPosition + (float)_tolerances.ToArray().GetValue(i)) * 0.25f;
              
                Vector3 position2 = new Vector3(x2,
                    Time.deltaTime * (-1000f + (float)_tolerances.ToArray().GetValue(i)) * 0.25f);

               

                arrayVectors[i] = position;
                startPosition += 50f;
               

                card.transform.SetParent(canvacesOfFirstDeck.transform);
                i++;

            }
          
            Invoke("InvokeMethod", 3f);
        }
        catch (Exception ex)
        {
            
            Invoke("InvokeMethod", 3f);
        }

       
          
    }


    private void InvokeMethod()
    {
        isArrangeCard = true;
    }


   

    [PunRPC]
    public void SendInitTalon(string[] Array)
    {
        float startPosition = 1000f;

        int i = 0;
        Vector3[] arrayPosition = new Vector3[4];
        string[] arrayCards = { "", "", "", "" };

        var root = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach(var temp in root)
        {

            if (temp.name.Contains("(Clone)")) {
                if (temp.name.Contains("CardImageValue"))
                {
                    var card = temp.gameObject;
                    card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                    card.transform.SetParent(LastCardCanvas.transform);
                }
                else { 
                    var card = temp.gameObject;
                    card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                    card.transform.SetParent(canvacesOfFirstDeck.transform);
                }
            }
          
        }
        
    }
    [PunRPC]
    public void setOtherImagesofPlayers()
    {
        

        Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;


        int tempI = 0;
        foreach (var vv in value)
        {
            if (!PhotonNetwork.LocalPlayer.NickName.Equals(PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName))
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"].Equals("Blue"))
                {
                    if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue")) { 
                        Texture2D tex = new Texture2D(83, 87);
                        byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                        tex.LoadImage(valuePicture);

                        UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                        ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                        ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                        if (PhotonNetwork.CurrentRoom.PlayerCount > 3 && FirstPlayerName.text == "")
                        {
                            Dictionary<int, Player> valuePlayers = PhotonNetwork.CurrentRoom.Players;

                            foreach (var kk in valuePlayers)
                            {
                                if (PhotonNetwork.CurrentRoom.GetPlayer(kk.Key).CustomProperties["Team"].Equals("Red"))
                                {
                                    if (!PhotonNetwork.CurrentRoom.GetPlayer(kk.Key).NickName.Equals(SecondPlayerName.text))
                                    {
                                        FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(kk.Key).NickName;
                                      
                                        //ovdje fali byte nije dobar
                                        Texture2D tex3 = new Texture2D(83, 87);
                                        byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(kk.Key).CustomProperties["Picture"];
                                        tex3.LoadImage(valuePicture3);
                                        // Assign texture to renderer's material.
                                        //GetComponent<Renderer>().material.mainTexture = tex;
                                        UnityEngine.UI.Image ProfilePic3 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                                        try { 
                                        ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());
                                        }catch(Exception ex)
                                        {
                                            Texture2D tex4= new Texture2D(8, 8);
                                            ProfilePic3.sprite = Sprite.Create(tex4, new Rect(0, 0, 8, 8), new Vector2());
                                        }


                                    }
                                }
                            }
                        }

                    }
                    else if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
                    {
                        if(SecondPlayerName.text != "") {

                            //ImageByte vf =
                            //    FirstPlayerImage.GetComponent<ImageByte>();
                            //vf.GetBytes();
                            //provjeriti kako rijesiti prepoznavanje igraca da nisu isti
                            //najbolja provjera da ide samo preko slike 

                            if (!SecondPlayerName.text.Equals(PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName)) { 
                                Texture2D tex4 = new Texture2D(83, 87);
                            byte[] valuePicture4 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                            tex4.LoadImage(valuePicture4);

                            UnityEngine.UI.Image ProfilePic4 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                            ProfilePic4.sprite = Sprite.Create(tex4, new Rect(0, 0, 83, 87), new Vector2());

                            FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName ;
                            }
                            
                        }
                        else {

                            Dictionary<int, Player> valuePlayers3= PhotonNetwork.CurrentRoom.Players;

                            foreach (var player3 in valuePlayers3)
                            {
                                if (PhotonNetwork.CurrentRoom.GetPlayer(player3.Key).CustomProperties["Team"].Equals("Blue"))
                                {
                                    if (!PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).Equals(PhotonNetwork.CurrentRoom.GetPlayer(player3.Key)))
                                    {

                                        Texture2D tex5 = new Texture2D(83, 87);
                                        byte[] valuePicture5 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(player3.Key).CustomProperties["Picture"];
                                        tex5.LoadImage(valuePicture5);

                                        UnityEngine.UI.Image ProfilePic5 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                                        ProfilePic5.sprite = Sprite.Create(tex5, new Rect(0, 0, 83, 87), new Vector2());

                                        SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(player3.Key).NickName ;
                                    }
                                }
                            }
                          
                        }
                    }

                }
                else if(PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"].Equals("Red"))
                {
                    if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
                    {
                        if (SecondPlayerName.text == "")
                        {
                            Texture2D tex2 = new Texture2D(83, 87);
                            byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                            tex2.LoadImage(valuePicture2);

                            UnityEngine.UI.Image ProfilePic2 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                            ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                            SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                        }
                        else
                        {
                            if (!SecondPlayerName.text.Equals(PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName))
                            {
                                var qq = PhotonNetwork.CurrentRoom.Players;
                                bool checkPlayer = false;
                                foreach(var q in qq)
                                {
                                    if (!SecondPlayerName.text.Equals(PhotonNetwork.CurrentRoom.GetPlayer(q.Key).NickName))
                                    {
                                        if (PhotonNetwork.CurrentRoom.GetPlayer(q.Key).CustomProperties["Team"].Equals("Red") 
                                            )
                                        {
                                            //ovdje pojavljuje dva ista igraca
                                            if (!PhotonNetwork.CurrentRoom.GetPlayer(q.Key).NickName.Equals(FirstPlayerName.text))
                                            {

                                            
                                            Texture2D tex2 = new Texture2D(83, 87);
                                            byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(q.Key).CustomProperties["Picture"];
                                            tex2.LoadImage(valuePicture2);

                                            UnityEngine.UI.Image ProfilePic2 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                                            try
                                            {
                                                ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());
                                            }
                                            catch (Exception ex)
                                            {
                                                Texture2D tex4 = new Texture2D(8, 8);
                                                ProfilePic2.sprite = Sprite.Create(tex4, new Rect(0, 0, 8, 8), new Vector2());
                                            }
                                           

                                            SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(q.Key).NickName;
                                            checkPlayer = true;
                                            }
                                        }
                                    }

                                }

                                if (!checkPlayer) { 
                                    Texture2D tex3 = new Texture2D(83, 87);
                                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                                    tex3.LoadImage(valuePicture3);

                                    UnityEngine.UI.Image ProfilePic3 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                                }
                            }
                        }
                    }
                    else if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
                    {
                        Texture2D tex = new Texture2D(83, 87);
                        byte[] valuePicture10 = (byte[]) PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                       
                        tex.LoadImage(valuePicture10);

                        UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                        try
                        {
                            ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());
                        }
                        catch (Exception ex)
                        {
                            Texture2D tex4 = new Texture2D(8, 8);
                            ProfilePic.sprite = Sprite.Create(tex4, new Rect(0, 0, 8, 8), new Vector2());
                        }
                       

                        ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    }
                }
                
            }
        }
    }

    public void SetSideOfBorderImages()
    {
        Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;

        if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
        {
            ThirdPlayerBorder.sprite = Resources.Load<Sprite>("game_page/PictureBlueBorderSmall");
            FirstPlayerBorder.sprite = Resources.Load<Sprite>("game_page/PictureRedBorderSmall");
            SecondPlayerBorder.sprite = Resources.Load<Sprite>("game_page/PictureRedBorderSmall");
        }else
        {
            ThirdPlayerBorder.sprite = Resources.Load<Sprite>("game_page/PictureRedBorderSmall");
            FirstPlayerBorder.sprite = Resources.Load<Sprite>("game_page/PictureBlueBorderSmall");
            SecondPlayerBorder.sprite = Resources.Load<Sprite>("game_page/PictureBlueBorderSmall");
        }
        
        foreach (var vv in value)
        {



            if (!PhotonNetwork.LocalPlayer.NickName.Equals(PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName))
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"].Equals("Blue"))
                {
                    if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
                    {
                        if (FirstPlayerName.text == "")
                        {
                            Texture2D tex2 = new Texture2D(83, 87);
                            byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                            tex2.LoadImage(valuePicture2);
                            // Assign texture to renderer's material.
                            //GetComponent<Renderer>().material.mainTexture = tex;
                            UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                            ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                            FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                        }
                        else
                        {
                            Texture2D tex3 = new Texture2D(83, 87);
                            byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                            tex3.LoadImage(valuePicture3);
                            // Assign texture to renderer's material.
                            //GetComponent<Renderer>().material.mainTexture = tex;
                            UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                            ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                            SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                        }
                    }
                    else if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
                    {
                        Texture2D tex = new Texture2D(83, 87);
                        byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                        tex.LoadImage(valuePicture);
                        // Assign texture to renderer's material.
                        //GetComponent<Renderer>().material.mainTexture = tex;
                        UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                        ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                        ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                        Dictionary<int, Player> valuePlayers = PhotonNetwork.CurrentRoom.Players;
                        //ovdje korekcija
                       if(PhotonNetwork.CurrentRoom.PlayerCount > 3 && SecondPlayerName.text =="")
                        {
                            foreach(var kk in valuePlayers)
                            {
                                if(PhotonNetwork.CurrentRoom.GetPlayer(kk.Key).CustomProperties["Team"].Equals("Red"))
                                {
                                    if (!PhotonNetwork.CurrentRoom.GetPlayer(kk.Key).NickName.Equals(FirstPlayerName.text))
                                    {
                                        Texture2D tex3 = new Texture2D(83, 87);
                                        byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(kk.Key).CustomProperties["Picture"];
                                        tex3.LoadImage(valuePicture3);
                                        // Assign texture to renderer's material.
                                        //GetComponent<Renderer>().material.mainTexture = tex;
                                        UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                                        ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                                        SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(kk.Key).NickName;
                                    }
                                }
                            }
                        }
                    }

                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"].Equals("Red"))
                {
                    if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
                    {
                        if (FirstPlayerName.text == "")
                        {
                            Texture2D tex2 = new Texture2D(83, 87);
                            byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                            tex2.LoadImage(valuePicture2);

                            UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                            ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                            FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                        }
                        else
                        {
                            Texture2D tex3 = new Texture2D(83, 87);
                            byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                            tex3.LoadImage(valuePicture3);

                            UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                            ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                            SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                        }
                    }
                    else if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
                    {

                        Texture2D tex = new Texture2D(83, 87);
                        byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                        tex.LoadImage(valuePicture);
                        UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                        ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                        ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    }
                }
                
            }

        }
        _currentPhotonView.RPC("setOtherImagesofPlayers", RpcTarget.Others);
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
    public void SetInstanceOfCurrentPlayer(int value,string[] cardsOfFirstPlayer, 
        string[] cardsOfSecondPlayer, string[] cardsOfThirdPlayer,string[] cardsOfFourthPlayer)
    {
        currentInstance = value;
        if(currentInstance == 1)
        {
            _cardsOfFirstPlayer = cardsOfFirstPlayer.ToList();

            float multiplier = 1.2f;

            foreach (var obj in _cardsOfFirstPlayer)
            {
                //Debug.Log("vrijednost:" + obj);
                var tt = Resources.Load("Prefabs/PlayerCards/" + obj);

                GameObject FirstCardObject = (GameObject)tt;

                //FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);

                FirstCardObject.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                GameObject myBrick = Instantiate(FirstCardObject, new Vector3(340 + multiplier, 100, 0), Quaternion.identity) as GameObject;
                //myBrick.transform.position = new Vector2(multiplier, 0);


                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
                multiplier += 90;

            }


        }
        if(currentInstance == 2)
        {
            _cardsOfSecondPlayer = cardsOfSecondPlayer.ToList();
            float multiplier = 1.2f;

            foreach (var obj in _cardsOfSecondPlayer)
            {
                //Debug.Log("vrijednost:" + obj);
                var tt = Resources.Load("Prefabs/PlayerCards/" + obj);

                GameObject FirstCardObject = (GameObject)tt;

                //FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);

                FirstCardObject.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                GameObject myBrick = Instantiate(FirstCardObject, new Vector3(340 + multiplier, 100, 0), Quaternion.identity) as GameObject;
                //myBrick.transform.position = new Vector2(multiplier, 0);


                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
                multiplier += 90;

            }
        }
        if(currentInstance == 3)
        {
            _cardsOfThirdPlayer = cardsOfThirdPlayer.ToList();

            float multiplier = 1.2f;

            foreach (var obj in _cardsOfThirdPlayer)
            {
                //Debug.Log("vrijednost:" + obj);
                var tt = Resources.Load("Prefabs/PlayerCards/" + obj);

                GameObject FirstCardObject = (GameObject)tt;

                //FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);

                FirstCardObject.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                GameObject myBrick = Instantiate(FirstCardObject, new Vector3(340 + multiplier, 100, 0), Quaternion.identity) as GameObject;
                //myBrick.transform.position = new Vector2(multiplier, 0);


                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
                multiplier += 90;

            }
        }
        if(currentInstance == 4)
        {
            _cardsOfFourthPlayer = cardsOfFourthPlayer.ToList();

            float multiplier = 1.2f;

            foreach (var obj in _cardsOfFourthPlayer)
            {
                //Debug.Log("vrijednost:" + obj);
                var tt = Resources.Load("Prefabs/PlayerCards/" + obj);

                GameObject FirstCardObject = (GameObject)tt;

                //FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);

                FirstCardObject.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                GameObject myBrick = Instantiate(FirstCardObject, new Vector3(340 + multiplier, 100, 0), Quaternion.identity) as GameObject;
                //myBrick.transform.position = new Vector2(multiplier, 0);


                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
                multiplier += 90;

            }
        }
    }


    void DisplayProfilePic(IGraphResult result)
    {

        if (result.Texture != null)
        {

            UnityEngine.UI.Image ProfilePic = DealerImage.GetComponent<UnityEngine.UI.Image>();
            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 64, 64), new Vector2());
            Texture2D tempTex = result.Texture;

            byte[] value = tempTex.EncodeToPNG();

            _currentPhotonView.RPC("SetPictureDealer", RpcTarget.Others, value, DealerName.text);
        }

    }

    [PunRPC]
    public void SetPictureDealer(byte[] value, string NickName)
    {
        Texture2D tex = new Texture2D(64, 64);

        tex.LoadImage(value);
        // Assign texture to renderer's material.
        //GetComponent<Renderer>().material.mainTexture = tex;
        UnityEngine.UI.Image ProfilePic = DealerImage.GetComponent<UnityEngine.UI.Image>();
        ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 64, 64), new Vector2());

        DealerName.text = NickName;
    }



    [PunRPC]
    public void StartGame(bool state)
    {
        isGameStarted = state;
        Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        if (SideOfTeam.CurrentPlayerSide == 1)
        {
            DealerName.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
        }
        else
        {
            var fff = value.Keys;
            foreach (var temp in fff)
            {
                if (temp == 2)
                {
                    DealerName.text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
                }
                else
                {
                    DealerName.text = PhotonNetwork.CurrentRoom.GetPlayer(temp).NickName;
                }
            }


        }

        FB.API("/me/picture?type=square&height=64&width=64", HttpMethod.GET, DisplayProfilePic);

        
        


        if (SideOfTeam.CurrentPlayerSide == 1 && isGameStarted)
        {
            _zingDealer = new ZingDealer();
            string[] remaingCardArray = new string[_zingDealer.RemainingCards.Count];
            int intValue = 0;
            RemainingCardsList = new List<string>();
            foreach (var obj in _zingDealer.RemainingCards)
            {

                remaingCardArray[intValue] = obj.name;
                ///Debug.Log("a:" + obj.name);
                RemainingCardsList.Add(obj.name);
                intValue++;
            }

            string[] array = new string[_zingDealer.TalonCards.Count];
            int i = 0;
            listTalon = new List<string>();
            _listOfCards = new List<string>();
            foreach (var obj in _zingDealer.TalonCards)
            {

                array[i] = obj.name;
                listTalon.Add(obj.name);
                _listOfCards.Add(obj.name);
                i++;
            }

            talonArray = listTalon.ToArray();

            var objLastCard = _zingDealer.LastCard as GameObject;

            _cardsOfFirstPlayer = new List<string>();
            foreach (var obj in _zingDealer.CardsOfFirstPlayers)
            {
                _cardsOfFirstPlayer.Add(obj.name);
            }

            string[] cardsOfSecondPlayer = new string[_zingDealer.CardsOfSecondPlayers.Count];

            int count = 0;
            foreach (var obj in _zingDealer.CardsOfSecondPlayers)
            {

                cardsOfSecondPlayer[count] = obj.name;
                count++;
            }

            string[] cardsOfThirdPlayer = new string[_zingDealer.CardsOfThirdPlayers.Count];

            int countThird = 0;
            foreach (var obj in _zingDealer.CardsOfThirdPlayers)
            {

                cardsOfThirdPlayer[countThird] = obj.name;
                countThird++;
            }

            string[] cardsOfFourthPlayer = new string[_zingDealer.CardsOfFourthPlayers.Count];

            int countFourth = 0;
            foreach (var obj in _zingDealer.CardsOfFourthPlayers)
            {

                cardsOfFourthPlayer[countFourth] = obj.name;
                countFourth++;
            }

            InitBoard();
            string ttt = objLastCard.name.Split('_')[1];
            //Debug.Log("value2:" + ttt);
           
            var components = CardImageValueLastCard.GetComponents<Component>();
            foreach (var com in components)
            {
                //Debug.Log("komponente");
                var vv = com.GetType();
                if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                {

                    var image2 = (SVGImporter.SVGImage)com;
                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/CARDS_" + ttt + "/" + objLastCard.name);
                }
            }
            string strana = "";
            if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
            {
                var component = TeamImageLastCard.GetComponents<Component>();
                foreach (var com in component)
                {
                    //Debug.Log("komponente");
                    var vv = com.GetType();
                    if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    {

                        var image2 = (SVGImporter.SVGImage)com;
                        image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackREDSide");
                    }
                }

                var componentsDealerBoard = DealerBoard.GetComponents<Component>();

                //var image = gameObject.GetComponent<SVGImage>();
                // Debug.Log("fff");
                foreach (var com in componentsDealerBoard)
                {
                    var vv = com.GetType();
                    if (typeof(Image).IsAssignableFrom(vv))
                    {
                        var image2 = (Image)com;
                        image2.sprite = Resources.Load<Sprite>("game_page/PictureRedBorderSmall");
                    }
                }

                strana = "Red";

            }else
            {
                var component = TeamImageLastCard.GetComponents<Component>();
                foreach (var com in component)
                {
                    //Debug.Log("komponente");
                    var vv = com.GetType();
                    if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    {

                        var image2 = (SVGImporter.SVGImage)com;
                        image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackBlueSide");
                    }
                }

                var componentsDealerBoard = DealerBoard.GetComponents<Component>();

                //var image = gameObject.GetComponent<SVGImage>();
                // Debug.Log("fff");
                foreach (var com in componentsDealerBoard)
                {
                    var vv = com.GetType();
                    if (typeof(Image).IsAssignableFrom(vv))
                    {
                        var image2 = (Image)com;
                        image2.sprite = Resources.Load<Sprite>("game_page/PictureBlueBorderSmall");
                    }
                }


                strana = "Blue";
            }
            _currentPhotonView.RPC("SetInitDealerConfig", RpcTarget.Others, objLastCard.name,strana);
            _cardsOfFirstPlayer = new List<string>();
            foreach (var obj in _zingDealer.CardsOfFirstPlayers)
            {
                _cardsOfFirstPlayer.Add(obj.name);
            }


            _random = new System.Random();

            _tolerances = new List<float>();

            for (int j = 0; j < 4; j++)
            {
                //float tol = (float)  _random.Next(1, 2) * _positionTolerance;
                float tol = (float)_random.NextDouble() * _positionTolerance;
                _tolerances.Add(tol);
            }
            InitTalonCards();
            DeleteLastFourTalonCards();
            TimeOfMove.active = true;
            isAviableToMove = true;
            var players = PhotonNetwork.CurrentRoom.Players;
            int tempValue = 1;
            foreach(var temp in players)
            {
                _currentPhotonView.RPC("SendToSecondPlayer",temp.Value,tempValue, _cardsOfFirstPlayer.ToArray(), cardsOfSecondPlayer.ToArray(),
                    cardsOfThirdPlayer.ToArray(),cardsOfFourthPlayer.ToArray());
                tempValue++;

            }
            
        }
    }

    [PunRPC]
    public void SetInitDealerConfig(string name,string strana)
    {
        isGameStarted = true;
        string ttt = name.Split('_')[1];
       

        var components = CardImageValueLastCard.GetComponents<Component>();
        foreach (var com in components)
        {
            
            var vv = com.GetType();
            if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
            {

                var image2 = (SVGImporter.SVGImage)com;
                image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/CARDS_" + ttt + "/" + name);
            }
        }
        if (strana.Equals("Blue"))
        {
            var componentDealer = DealerBoard.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();
            // Debug.Log("fff");
            foreach (var com2 in componentDealer)
            {
                var vv2 = com2.GetType();
                if (typeof(Image).IsAssignableFrom(vv2))
                {
                    var image3 = (Image)com2;
                    image3.sprite = Resources.Load<Sprite>("game_page/PictureBlueBorderSmall");
                }
            }
        }else
        {
            var componentDealer = DealerBoard.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();
            // Debug.Log("fff");
            foreach (var com2 in componentDealer)
            {
                var vv2 = com2.GetType();
                if (typeof(Image).IsAssignableFrom(vv2))
                {
                    var image3 = (Image)com2;
                    image3.sprite = Resources.Load<Sprite>("game_page/PictureRedBorderSmall");
                }
            }
        }


            var component = TeamImageLastCard.GetComponents<Component>();
        foreach (var com in component)
        {
            //Debug.Log("komponente");
            var vv = com.GetType();
            if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
            {

                var image2 = (SVGImporter.SVGImage)com;
                if (strana.Equals("Blue"))
                {
                  

                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackBlueSide");
                }else
                {

                   
                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackREDSide");
                }
               
            }
        }
       
    }


    private void InitBoard()
    {
        float multiplier = 1.2f;
        foreach (var obj in _cardsOfFirstPlayer)
        {
            //Debug.Log("vrijednost:" + obj);
            var tt = Resources.Load("Prefabs/PlayerCards/" + obj);

            GameObject FirstCardObject = (GameObject)tt;

            //FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);
           
            FirstCardObject.transform.localScale = new Vector3(0.789f, 0.789f, 0);

            //float value = -200.8f;
            //float valueY = -2.6f;
            //GameObject myBrick = Instantiate(FirstCardObject, new Vector3(value + multiplier, valueY, 0), Quaternion.identity) as GameObject;
            //myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
            //multiplier += 135f;
            float value = -3.6f;
            float valueY = -2.6f;
            GameObject myBrick = Instantiate(FirstCardObject, new Vector3(340 + multiplier, 100, 0), Quaternion.identity) as GameObject;
            //myBrick.transform.position = new Vector2(multiplier, 0);
           

            myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
            myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
            multiplier += 90;

        }

    }

    public void DeleteLastFourTalonCards()
    {
      
        int start = RemainingCardsList.Count - 5;
        RemainingCardsList.RemoveRange(start, 4);

    }

    public Canvas GetFirstDeck()
    {
        return canvacesOfFirstDeck;
    }

    public Canvas GetCurrentPlayerCanvas()
    {
        return canvacesOfCurrentPlayer;
    }

    public List<string> GetOfListOfCards()
    {
        return _listOfCards;
    }

    public void SetListOfCards(List<string> _list)
    {
        _listOfCards = _list;
    }

}
