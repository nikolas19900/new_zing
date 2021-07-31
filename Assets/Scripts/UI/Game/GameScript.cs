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
using UnityEngine.EventSystems;
using Assets.Scripts.UI.Game.Utils;

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

    [SerializeField]
    private Text FirstPlayerInstance;

    [SerializeField]
    private Text SecondPlayerInstance;

    [SerializeField]
    private Text ThirdPlayerInstance;

    [SerializeField]
    private Text FirstPlayerTeam;

    [SerializeField]
    private Text SecondPlayerTeam;

    [SerializeField]
    private Text ThirdPlayerTeam;

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
   
    private GameObject _previousCard;
    private GameObject _currentCard;
    private bool runOnceFirst = false;
    private bool runOnceSecond = false;
    private bool runOnceThird = false;
    private bool runOnceFourth = false;
    private bool isFirstRunDealingCards = false;

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
        runOnceThird = false;
        RemainingCardsList = new List<string>();

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

        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {


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
               
                if (players[current.Key].CustomProperties["State"].Equals("inactive") )
                {
                    if (players[current.Key].IsMasterClient && isGameStarted == false)
                    {
                        _currentPhotonView.RPC("SwitchAllSceneAndLeave", RpcTarget.Others);
                    }
                    //ako jedan od igraca nije aktivan aktivirace se ova linija koda
                   //  Debug.Log("ovaj igrac nije aktivan:"+ players[current.Key].NickName);
                    //_currentPhotonView.RPC("ReadLine", players[current.Key]);
                    int vv = int.Parse(""+ players[current.Key].CustomProperties["Instance"]);

                    if(vv == 1 && SideOfTeam.MoveInstance == 1)
                    {
                        Invoke("FirstDropCard", 1f);
                    }
                    if(vv == 2 && SideOfTeam.MoveInstance == 2)
                    {
                        Invoke("SecondDropCard", 1f);
                    }
                    if(vv == 3 && SideOfTeam.MoveInstance == 3)
                    {
                        Invoke("ThirdDropCard", 1f);
                    }
                    if(vv == 4 && SideOfTeam.MoveInstance == 4)
                    {
                        Invoke("FourthDropCard", 1f);
                        
                    }

                }

                
            }
            if(players.Count < 4 && isGameStarted) {
                


                    
                    List<int> temp = new List<int>();

                    foreach (var current in players)
                    {

                        int hh = int.Parse(PhotonNetwork.CurrentRoom.Players[current.Key].CustomProperties["Instance"] + "");
                        temp.Add(hh);

                    }

                
                //Debug.Log("vrij:" + SideOfTeam.MoveInstance);

                if (!temp.Contains(1) && SideOfTeam.MoveInstance == 1)
                {
                    
                    Invoke("FirstDropCard", 1f);
                    
                }

                 if (!temp.Contains(2) && SideOfTeam.MoveInstance == 2)
                {

                    Invoke("SecondDropCard", 1f);
                   
                }
                 if (!temp.Contains(3) && SideOfTeam.MoveInstance == 3)
                {
                    Invoke("ThirdDropCard", 1f);
                   
                }
                
                 if (!temp.Contains(4) && SideOfTeam.MoveInstance == 4)
                {
                    Debug.Log("sad sam pokrenuo ai na 4 instanci");
                    Invoke("FourthDropCard", 1f);
                    
                }
                            // Debug.Log("ovaj igrac nije aktican:" + play);
            
                    
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


    public void FirstDropCard()
    {
        if (!runOnceFirst)
        {

            float _landingToleranceRadius = 0.3f;
            Vector2 _endPoint = Vector2.zero;

           
           
            var list = GetCardsOfFirstPlayer();
            if (list.Count > 0)
            {
                Debug.Log("velicina lista prvog igraca:" + list.Count);
                
                var val = list[0];
                Debug.Log("val:" + val);
                var tt = Resources.Load("Prefabs/CardPrefabsSvg/" + val);

                GameObject card = (GameObject)tt;


                _random = new System.Random();

                var valueX = 300 * (1 - (-0.6)) + (-0.6);

                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 0) * toleranceX);
                var value = 340 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);

                card.transform.position = new Vector3(x, y);
                card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                Vector3 positionOfCurrentCard = new Vector3(x, y);
                ////GameObject myBrick = PhotonNetwork.Instantiate("Prefabs/CardPrefabs/" + CardName, _currentCard.transform.position, Quaternion.identity);

                GameObject myBrick = Instantiate(card, new Vector3(x, y, 0), Quaternion.identity) as GameObject;


                myBrick.transform.SetParent(canvacesOfFirstDeck.transform);

                //photonView.RPC("DropTheCard", RpcTarget.All, val);


                list.Remove(val);
                SetCardsOfFirstPlayer(list);

                if (list.Count == 0)
                {
                    if (SideOfTeam.CurrentPlayerSide == 1)
                    {
                        var tempPlayers = PhotonNetwork.CurrentRoom.Players;
                        List<int> temp = new List<int>();
                        List<PlayerInfoValue> listOfPlayers = new List<PlayerInfoValue>();
                        foreach (var current in tempPlayers)
                        {
                            if (tempPlayers[current.Key].CustomProperties["State"].Equals("active")) { 
                                int hh = int.Parse(tempPlayers[current.Key].CustomProperties["Instance"] + "");
                                temp.Add(hh);
                                PlayerInfoValue pi = new PlayerInfoValue();
                                pi._player = current.Value;
                                pi._instance = hh;
                                listOfPlayers.Add(pi);
                            }

                        }
                        isFirstRunDealingCards = false;
                        if (temp.Contains(2))
                        {
                            foreach (var tempValue in listOfPlayers)
                            {
                                if (tempValue._instance == 2)
                                {
                                    
                                    photonView.RPC("InitDealingTheCards", tempValue._player);
                                }
                            }
                        }
                        else if (temp.Contains(3))
                        {
                            

                            foreach (var tempValue in listOfPlayers)
                            {
                                if (tempValue._instance == 3)
                                {
                                   
                                    photonView.RPC("InitDealingTheCards", tempValue._player);
                                }
                            }
                        }
                        else if (temp.Contains(4))
                        {
                            foreach (var tempValue in listOfPlayers)
                            {
                                if (tempValue._instance == 4)
                                {
                                   
                                    photonView.RPC("InitDealingTheCards", tempValue._player);
                                }
                            }
                        }
                                         
                        
                    }
                }
                SideOfTeam.MoveInstance = 2;

               

                bool isPickedUp = PickUpCardsFromDeckWithoutPlayer("Blue");
                if (isPickedUp)
                {
                    SideOfTeam.LastPick = 1;

                }

                var players = PhotonNetwork.CurrentRoom.Players;
                if(players.Count == 1)
                {
                    photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.All, 1);
                }
                else
                {
                    int i = 0;
                    foreach (var current in players)
                    {
                        

                        if (players[current.Key].CustomProperties["State"].Equals("inactive"))
                        {
                            i++;
                        }
                    }
                    if( i == 0)
                    {
                        photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.Others, 1);
                    }
                    else
                    {
                        photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.All, 1);
                    }

                    
                }




            }
            
           
           
            runOnceFirst = true;


        }
    }


    public void SecondDropCard()
    {
        if (!runOnceSecond)
        {

            float _landingToleranceRadius = 0.3f;
            Vector2 _endPoint = Vector2.zero;


            Debug.Log("usao sam 2");
            var list = GetCardsOfSecondPlayer();
            if (list.Count > 0)
            {


                var val = list[0];

                var tt = Resources.Load("Prefabs/CardPrefabsSvg/" + val);

                GameObject card = (GameObject)tt;


                _random = new System.Random();

                var valueX = 300 * (1 - (-0.6)) + (-0.6);

                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 0) * toleranceX);
                var value = 340 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);

                card.transform.position = new Vector3(x, y);
                card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                Vector3 positionOfCurrentCard = new Vector3(x, y);
                ////GameObject myBrick = PhotonNetwork.Instantiate("Prefabs/CardPrefabs/" + CardName, _currentCard.transform.position, Quaternion.identity);

                GameObject myBrick = Instantiate(card, new Vector3(x, y, 0), Quaternion.identity) as GameObject;


                myBrick.transform.SetParent(canvacesOfFirstDeck.transform);

                //photonView.RPC("DropTheCard", RpcTarget.All, val);


                list.Remove(val);
                SetCardsOfSecondPlayer(list);
                SideOfTeam.MoveInstance = 3;



                bool isPickedUp = PickUpCardsFromDeckWithoutPlayer("Red");
                if (isPickedUp)
                {
                    SideOfTeam.LastPick = 2;

                }
                var players = PhotonNetwork.CurrentRoom.Players;
                if (players.Count == 1)
                {
                    photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.All, 2);
                }
                else
                {
                    int i = 0;
                    foreach (var current in players)
                    {


                        if (players[current.Key].CustomProperties["State"].Equals("inactive"))
                        {
                            i++;
                        }
                    }
                    if (i == 0)
                    {
                        photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.Others, 2);
                    }
                    else
                    {
                        photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.All, 2);
                    }
                }
                
                
            }
            if(list.Count == 0)
            {
                if (SideOfTeam.CurrentPlayerSide == 2)
                {
                    Debug.Log("usao sam na 2 AI koji kupi karte sa stola");
                    List<string> listTemp = new List<string>();
                    foreach (Transform transform in canvacesOfFirstDeck.transform)
                    {

                        GameObject tempGameObject = transform.gameObject;
                        string name = tempGameObject.name;
                        var index = name.IndexOf("(");
                        string CardName = name.Substring(0, index);
                        listTemp.Add(CardName);

                        Destroy(transform.gameObject);
                    }

                    


                    if (SideOfTeam.LastPick == 1)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsFirstPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(1))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsFirstPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.TakeRestOfCardsFirstAI(array);
                                    if(currentInstance == 1)
                                    {
                                        int pointsValue = 0;

                                        CalculatePoints points = new CalculatePoints(listTemp);
                                        pointsValue = points.GetPoints();
                                        Dictionary<int, Player> valuesPlayers = PhotonNetwork.CurrentRoom.Players;

                                        foreach (var vv in valuesPlayers)
                                        {

                                            if (valuesPlayers[vv.Key].CustomProperties["Instance"].Equals(1))
                                            {

                                                ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                                                string gg = hash["Points"].ToString();
                                                int pointsPlayer = int.Parse(gg) + pointsValue;
                                                hash["Points"] = pointsPlayer;

                                                PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!IsFirstPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.TakeRestOfCardsFirstAI(array);
                        }
                    }
                    else if (SideOfTeam.LastPick == 2)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsSecondPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(2))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsSecondPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.TakeRestOfCardsSecondAI(array);

                                    if (currentInstance == 2)
                                    {
                                        int pointsValue = 0;

                                        CalculatePoints points = new CalculatePoints(listTemp);
                                        pointsValue = points.GetPoints();
                                        Dictionary<int, Player> valuesPlayers = PhotonNetwork.CurrentRoom.Players;

                                        foreach (var vv in valuesPlayers)
                                        {

                                            if (valuesPlayers[vv.Key].CustomProperties["Instance"].Equals(2))
                                            {

                                                ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                                                string gg = hash["Points"].ToString();
                                                int pointsPlayer = int.Parse(gg) + pointsValue;
                                                hash["Points"] = pointsPlayer;

                                                PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);

                                            }
                                        }
                                    }
                                        
                                 }
                            }
                        }
                        if (!IsSecondPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.TakeRestOfCardsSecondAI(array);
                        }
                    }
                    else if (SideOfTeam.LastPick == 3)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsThirdPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(3))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsThirdPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.TakeRestOfCardsThirdAI(array);

                                    if (currentInstance == 3)
                                    {
                                        int pointsValue = 0;

                                        CalculatePoints points = new CalculatePoints(listTemp);
                                        pointsValue = points.GetPoints();
                                        Dictionary<int, Player> valuesPlayers = PhotonNetwork.CurrentRoom.Players;

                                        foreach (var vv in valuesPlayers)
                                        {

                                            if (valuesPlayers[vv.Key].CustomProperties["Instance"].Equals(3))
                                            {

                                                ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                                                string gg = hash["Points"].ToString();
                                                int pointsPlayer = int.Parse(gg) + pointsValue;
                                                hash["Points"] = pointsPlayer;

                                                PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);

                                            }
                                        }
                                    }

                                }
                            }
                        }
                        if (!IsThirdPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.TakeRestOfCardsThirdAI(array);
                        }
                    }
                    else if (SideOfTeam.LastPick == 4)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsFourthPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(4))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsFourthPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.TakeRestOfCardsFourthAI(array);

                                    if (currentInstance == 4)
                                    {
                                        int pointsValue = 0;

                                        CalculatePoints points = new CalculatePoints(listTemp);
                                        pointsValue = points.GetPoints();
                                        Dictionary<int, Player> valuesPlayers = PhotonNetwork.CurrentRoom.Players;

                                        foreach (var vv in valuesPlayers)
                                        {

                                            if (valuesPlayers[vv.Key].CustomProperties["Instance"].Equals(4))
                                            {

                                                ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                                                string gg = hash["Points"].ToString();
                                                int pointsPlayer = int.Parse(gg) + pointsValue;
                                                hash["Points"] = pointsPlayer;

                                                PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);

                                            }
                                        }
                                    }

                                }
                            }
                        }
                        if (!IsFourthPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.TakeRestOfCardsFourthAI(array);
                        }
                    }
                }
            }

            runOnceSecond = true;


        }
    }

    public void ThirdDropCard()
    {
        if(!runOnceThird)
        {
            float _landingToleranceRadius = 0.3f;
            Vector2 _endPoint = Vector2.zero;

            var list = GetCardsOfThirdPlayer();
            Debug.Log("usao sam 3" + list.Count);
            if (list.Count > 0)
            {


                var val = list[0];
                Debug.Log("vrijednost:" + val);
                var tt = Resources.Load("Prefabs/CardPrefabsSvg/" + val);

                GameObject card = (GameObject)tt;


                _random = new System.Random();

                var valueX = 300 * (1 - (-0.6)) + (-0.6);

                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 0) * toleranceX);
                var value = 340 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);

                card.transform.position = new Vector3(x, y);
                card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                Vector3 positionOfCurrentCard = new Vector3(x, y);
                //GameObject myBrick = PhotonNetwork.Instantiate("Prefabs/CardPrefabs/" + CardName, _currentCard.transform.position, Quaternion.identity);

                GameObject myBrick = Instantiate(card, new Vector3(x, y, 0), Quaternion.identity) as GameObject;


              

                myBrick.transform.SetParent(canvacesOfFirstDeck.transform);

             

                list.Remove(val);
                SetCardsOfThirdPlayer(list);
                SideOfTeam.MoveInstance = 4;

               

                bool isPickedUp = PickUpCardsFromDeckWithoutPlayer("Blue");

                if (isPickedUp)
                {
                    SideOfTeam.LastPick = 3;

                }

                var players = PhotonNetwork.CurrentRoom.Players;
                if (players.Count == 1)
                {
                    photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.All, 3);
                }
                else
                {
                    int i = 0;
                    foreach (var current in players)
                    {


                        if (players[current.Key].CustomProperties["State"].Equals("inactive"))
                        {
                            i++;
                        }
                    }
                    if (i == 0)
                    {
                        photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.Others, 3);
                    }
                    else
                    {
                        photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.All, 3);
                    }
                }
               
            }
            runOnceThird = true;


        }
        
    }


    public void FourthDropCard()
    {
        if (!runOnceFourth)
        {
            float _landingToleranceRadius = 0.3f;
            Vector2 _endPoint = Vector2.zero;

            var list = GetCardsOfFourthPlayer();
            //Debug.Log("usao sam 3" + list.Count);
            if (list.Count > 0)
            {


                var val = list[0];
                Debug.Log("vrijednost:" + val);
                var tt = Resources.Load("Prefabs/CardPrefabsSvg/" + val);

                GameObject card = (GameObject)tt;


                _random = new System.Random();

                var valueX = 300 * (1 - (-0.6)) + (-0.6);

                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 0) * toleranceX);
                var value = 340 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);

                card.transform.position = new Vector3(x, y);
                card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                Vector3 positionOfCurrentCard = new Vector3(x, y);
                //GameObject myBrick = PhotonNetwork.Instantiate("Prefabs/CardPrefabs/" + CardName, _currentCard.transform.position, Quaternion.identity);

                GameObject myBrick = Instantiate(card, new Vector3(x, y, 0), Quaternion.identity) as GameObject;




                myBrick.transform.SetParent(canvacesOfFirstDeck.transform);



                list.Remove(val);
                SetCardsOfFourthPlayer(list);
                SideOfTeam.MoveInstance = 1;



                bool isPickedUp = PickUpCardsFromDeckWithoutPlayer("Red");
                if (isPickedUp)
                {
                    SideOfTeam.LastPick = 4;

                }
                var players = PhotonNetwork.CurrentRoom.Players;
                if (players.Count == 1)
                {
                    photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.All, 4);
                }
                else
                {
                    int i = 0;
                    foreach (var current in players)
                    {


                        if (players[current.Key].CustomProperties["State"].Equals("inactive"))
                        {
                            i++;
                        }
                    }
                    if (i == 0)
                    {
                        photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.Others, 4);
                    }
                    else
                    {
                        photonView.RPC("ActivatePlayerToPlayInstance", RpcTarget.All, 4);
                    }
                }
                
            }
            runOnceFourth = true;


        }
        
    }

    [PunRPC]
    public void InitDealingTheCards()
    {
        
        if (!isFirstRunDealingCards)
        {
           
            photonView.RPC("DeleteRemainingCards", RpcTarget.All);
            _zingDealer = new ZingDealer();
            _zingDealer.RemainingCardsList = RemainingCardsList;



            if (RemainingCardsList.Count > 0)
            {
                _zingDealer.DealCardsToPlayersFirstSecondAI();
                _cardsOfFirstPlayer.Clear();
                foreach (var obj in _zingDealer.CardsOfFirstPlayers)
                {
                    _cardsOfFirstPlayer.Add(obj.name);
                }

                _cardsOfSecondPlayer.Clear();
                foreach (var obj in _zingDealer.CardsOfSecondPlayers)
                {
                    _cardsOfSecondPlayer.Add(obj.name);
                }
                _cardsOfThirdPlayer.Clear();
                foreach (var obj in _zingDealer.CardsOfThirdPlayers)
                {
                    _cardsOfThirdPlayer.Add(obj.name);
                }
                _cardsOfFourthPlayer.Clear();
                foreach (var obj in _zingDealer.CardsOfFourthPlayers)
                {
                    _cardsOfFourthPlayer.Add(obj.name);
                }
                
                photonView.RPC("SetCardsToPlayers", RpcTarget.All, _cardsOfFirstPlayer.ToArray(),
                    _cardsOfSecondPlayer.ToArray(), _cardsOfThirdPlayer.ToArray(),
                    _cardsOfFourthPlayer.ToArray(), RemainingCardsList.ToArray());

            }
            isFirstRunDealingCards = true;
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

            
            Vector3 position = new Vector3(startPosition, 700f);
            gameObj.transform.localPosition = position;
            gameObj.transform.localScale = new Vector3(0.789f, 0.789f, 0);
            //PhotonNetwork.Instantiate("Prefabs/CardPrefabsStartSVG/" + gameObj.name, new Vector3(startPosition, 700f, 0), Quaternion.identity).transform.SetParent(canvacesOfFirstDeck.transform);
            //GameObject firstDeck = (GameObject) PhotonNetwork.Instantiate("Prefabs/CardPrefabsStartSVG/"+gameObj.name, new Vector3(startPosition, 700f, 0), Quaternion.identity);
            GameObject firstDeck = (GameObject) Instantiate( gameObj, new Vector3(startPosition, 700f, 0), Quaternion.identity);

            firstDeck.transform.localScale = new Vector3(0.789f, 0.789f, 0);
            arrayPosition[i] = position;
            arrayCards[i] = "" + gameObj.name;
            firstDeck.transform.SetParent(canvacesOfFirstDeck.transform);

            i++;
            startPosition += 50f;
            //multiplier -= 5f;
           
        }

        

        _currentPhotonView.RPC("SendInitTalon", RpcTarget.Others, arrayCards,arrayPosition);


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
               

               

                arrayVectors[i] = position;
                startPosition += 50f;
               

                card.transform.SetParent(canvacesOfFirstDeck.transform);
                i++;

            }

            photonView.RPC("InformCardPosition", RpcTarget.Others, arrayVectors, listTalon.ToArray());

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
    public void InformCardPosition(Vector3[] newPosition, string[] arrayTalon)
    {


        int i = 0;
        if (listTalon == null)
        {
            listTalon = arrayTalon.ToList();
        }


        

        foreach (var val in listTalon)
        {

            var card = canvacesOfFirstDeck.transform.Find($"{val}(Clone)").gameObject;

            card.transform.position += newPosition[i];

            var components = card.GetComponents<Component>();

            card.transform.SetParent(canvacesOfFirstDeck.transform);

            i++;
        }
    }


    [PunRPC]
   public void DropTheCard(string name)
    {
        float _landingToleranceRadius = 0.3f;
        Vector2 _endPoint = Vector2.zero;

        var tt = Resources.Load("Prefabs/CardPrefabsStartSvg/" + name);

        GameObject card = (GameObject)tt;


        _random = new System.Random();

        var valueX = 300 * (1 - (-0.6)) + (-0.6);

        float toleranceX = 2.3f;
        float x = (float)(valueX + _random.Next(-20, 0) * toleranceX);
        var value = 340 * (1.5 - 0.6) + 0.6;
        float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);

        card.transform.position = new Vector3(x, y);
        Vector3 positionOfCurrentCard = new Vector3(x, y);
        //GameObject myBrick = PhotonNetwork.Instantiate("Prefabs/CardPrefabs/" + CardName, _currentCard.transform.position, Quaternion.identity);

        GameObject myBrick = Instantiate(card, new Vector3(x, y, 0), Quaternion.identity) as GameObject;



        // Debug.Log("first card object:" + tv.transform.childCount);

        myBrick.transform.SetParent(canvacesOfFirstDeck.transform);

    }

    [PunRPC]
    public void SendInitTalon(string[] Array,Vector3[] positions)
    {
        //float startPosition = 1000f;

        //int i = 0;
        //Vector3[] arrayPosition = new Vector3[4];
        //string[] arrayCards = { "", "", "", "" };

        //var root = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        //foreach(var temp in root)
        //{

        //    if (temp.name.Contains("(Clone)")) {
        //        if (temp.name.Contains("CardImageValue"))
        //        {
        //            var card = temp.gameObject;
        //            card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
        //            card.transform.SetParent(LastCardCanvas.transform);
        //        }
        //        else { 
        //            var card = temp.gameObject;
        //            card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
        //            card.transform.SetParent(canvacesOfFirstDeck.transform);
        //        }
        //    }

        //}

        for (int i = 0; i < positions.Length; i++)
        {
           
            var prefabs = Resources.Load("Prefabs/CardPrefabsSvg/" + Array[i]);

            GameObject gameObj = (GameObject)prefabs;

           
            gameObj.transform.position = positions[i];
            gameObj.transform.localScale = new Vector3(0.789f, 0.789f, 0);
            
            GameObject firstDeck = (GameObject)Instantiate(gameObj, new Vector3(positions[i].x, positions[i].y, 0), Quaternion.identity);
            firstDeck.transform.SetParent(canvacesOfFirstDeck.transform);


        }

    }
    [PunRPC]
    public void setOtherImagesofPlayers()
    {
        

        Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;

        if (PhotonNetwork.LocalPlayer.CustomProperties["Instance"].Equals(1))
        {
            foreach (var vv in value)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(2))
                {
                    Texture2D tex3 = new Texture2D(83, 87);
                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex3.LoadImage(valuePicture3);

                    UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                    SecondPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture3);

                    SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    SecondPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    SecondPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(3))
                {
                    Texture2D tex = new Texture2D(83, 87);
                    byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex.LoadImage(valuePicture);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                    ThirdPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture);

                    ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    ThirdPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    ThirdPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(4))
                {
                    Texture2D tex2 = new Texture2D(83, 87);
                    byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex2.LoadImage(valuePicture2);

                    UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                    FirstPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture2);

                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                    FirstPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    FirstPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }

            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Instance"].Equals(2))
        {
            foreach (var vv in value)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(1))
                {
                    Texture2D tex2 = new Texture2D(83, 87);
                    byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex2.LoadImage(valuePicture2);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                    FirstPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture2);

                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                    FirstPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    FirstPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(3))
                {
                    Texture2D tex3 = new Texture2D(83, 87);
                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex3.LoadImage(valuePicture3);

                    UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                    SecondPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture3);

                    SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    SecondPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    SecondPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(4))
                {
                    Texture2D tex = new Texture2D(83, 87);
                    byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex.LoadImage(valuePicture);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                    ThirdPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture);

                    ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    ThirdPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    ThirdPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Instance"].Equals(3))
        {
            foreach (var vv in value)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(4))
                {
                    Texture2D tex3 = new Texture2D(83, 87);
                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex3.LoadImage(valuePicture3);

                    UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                    SecondPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture3);

                    SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    SecondPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    SecondPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(1))
                {
                    Texture2D tex = new Texture2D(83, 87);
                    byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex.LoadImage(valuePicture);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                    ThirdPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture);

                    ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    ThirdPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    ThirdPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(2))
                {
                    Texture2D tex2 = new Texture2D(83, 87);
                    byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex2.LoadImage(valuePicture2);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                    FirstPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture2);

                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                    FirstPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    FirstPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Instance"].Equals(4))
        {
            foreach (var vv in value)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(1))
                {
                    Texture2D tex3 = new Texture2D(83, 87);
                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex3.LoadImage(valuePicture3);

                    UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                    SecondPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture3);

                    SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    SecondPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    SecondPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(2))
                {
                    Texture2D tex = new Texture2D(83, 87);
                    byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex.LoadImage(valuePicture);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                    ThirdPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture);

                    ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    ThirdPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    ThirdPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(3))
                {
                    Texture2D tex2 = new Texture2D(83, 87);
                    byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex2.LoadImage(valuePicture2);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                    FirstPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture2);

                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                    FirstPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    FirstPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
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


       if(PhotonNetwork.LocalPlayer.CustomProperties["Instance"].Equals(1))
        {
            foreach (var vv in value)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(2))
                {
                    Texture2D tex3 = new Texture2D(83, 87);
                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex3.LoadImage(valuePicture3);

                    UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                    SecondPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture3);

                    SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    SecondPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    SecondPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(3))
                {
                    Texture2D tex = new Texture2D(83, 87);
                    byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex.LoadImage(valuePicture);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                    ThirdPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture);

                    ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    ThirdPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    ThirdPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(4))
                {
                    Texture2D tex2 = new Texture2D(83, 87);
                    byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex2.LoadImage(valuePicture2);
                    
                    UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                    FirstPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture2);

                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                    FirstPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    FirstPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                
            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Instance"].Equals(2))
        {
            foreach (var vv in value)
            {
               if(PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(1))
                {
                    Texture2D tex2 = new Texture2D(83, 87);
                    byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex2.LoadImage(valuePicture2);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                    FirstPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture2);

                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                    FirstPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    FirstPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(3))
                {
                    Texture2D tex3 = new Texture2D(83, 87);
                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex3.LoadImage(valuePicture3);
                    
                    UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                    SecondPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture3);

                    SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    SecondPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    SecondPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(4))
                {
                    Texture2D tex = new Texture2D(83, 87);
                    byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex.LoadImage(valuePicture);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                    ThirdPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture);

                    ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    ThirdPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    ThirdPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Instance"].Equals(3))
        {
            foreach (var vv in value)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(4))
                {
                    Texture2D tex3 = new Texture2D(83, 87);
                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex3.LoadImage(valuePicture3);

                    UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                    SecondPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture3);

                    SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    SecondPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    SecondPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(1))
                {
                    Texture2D tex = new Texture2D(83, 87);
                    byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex.LoadImage(valuePicture);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                    ThirdPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture);

                    ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    ThirdPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    ThirdPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(2))
                {
                    Texture2D tex2 = new Texture2D(83, 87);
                    byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex2.LoadImage(valuePicture2);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                    FirstPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture2);

                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                    FirstPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    FirstPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Instance"].Equals(4))
        {
            foreach (var vv in value)
            {
                if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(1))
                {
                    Texture2D tex3 = new Texture2D(83, 87);
                    byte[] valuePicture3 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex3.LoadImage(valuePicture3);

                    UnityEngine.UI.Image ProfilePic3 = SecondPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic3.sprite = Sprite.Create(tex3, new Rect(0, 0, 83, 87), new Vector2());

                    SecondPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture3);

                    SecondPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    SecondPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    SecondPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(2))
                {
                    Texture2D tex = new Texture2D(83, 87);
                    byte[] valuePicture = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex.LoadImage(valuePicture);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic = ThirdPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 83, 87), new Vector2());

                    ThirdPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture);

                    ThirdPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;

                    ThirdPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    ThirdPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
                }
                else if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(3))
                {
                    Texture2D tex2 = new Texture2D(83, 87);
                    byte[] valuePicture2 = (byte[])PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Picture"];
                    tex2.LoadImage(valuePicture2);
                    // Assign texture to renderer's material.
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    UnityEngine.UI.Image ProfilePic2 = FirstPlayerImage.GetComponent<UnityEngine.UI.Image>();
                    ProfilePic2.sprite = Sprite.Create(tex2, new Rect(0, 0, 83, 87), new Vector2());

                    FirstPlayerImage.GetComponent<ImageByte>().SetBytes(valuePicture2);

                    FirstPlayerName.text = PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).NickName;
                    FirstPlayerInstance.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"];
                    FirstPlayerTeam.text = "" + PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"];
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
        int i = 0;
        List<int> keys = new List<int>();
        foreach (var vv in value)
        {
            if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Team"].Equals("Blue") && 
                PhotonNetwork.CurrentRoom.GetPlayer(vv.Key).CustomProperties["Instance"].Equals(3))
            {
                keys.Add(vv.Key);
            }
        }
        if(keys.Count == 2) { 
        if(keys[0] > keys[1])
        {
            PhotonNetwork.CurrentRoom.GetPlayer(keys[0]).CustomProperties["Team"] = "Red";
            PhotonNetwork.CurrentRoom.GetPlayer(keys[0]).CustomProperties["Instance"] = 2;
        }else if(keys[0] < keys[1])
        {
            PhotonNetwork.CurrentRoom.GetPlayer(keys[1]).CustomProperties["Team"] = "Red";
            PhotonNetwork.CurrentRoom.GetPlayer(keys[1]).CustomProperties["Instance"] = 2;
        }
        }


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
        string[] cardsOfSecondPlayer, string[] cardsOfThirdPlayer,string[] cardsOfFourthPlayer,string[] RemaingCards)
    {
        currentInstance = value;
        RemainingCardsList = RemaingCards.ToList();
        _cardsOfFirstPlayer = cardsOfFirstPlayer.ToList();
        _cardsOfSecondPlayer = cardsOfSecondPlayer.ToList();
        _cardsOfThirdPlayer = cardsOfThirdPlayer.ToList();
        _cardsOfFourthPlayer = cardsOfFourthPlayer.ToList();
        if (currentInstance == 1)
        {
           

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

    [PunRPC]
    public void SetCardsToPlayers( string[] cardsOfFirstPlayer,
       string[] cardsOfSecondPlayer, string[] cardsOfThirdPlayer, string[] cardsOfFourthPlayer, string[] RemaingCards)
    {
        RemainingCardsList.Clear();
         RemainingCardsList = RemaingCards.ToList();
        _cardsOfFirstPlayer.Clear();
        _cardsOfFirstPlayer = cardsOfFirstPlayer.ToList();
        _cardsOfSecondPlayer.Clear();
        _cardsOfSecondPlayer = cardsOfSecondPlayer.ToList();
        _cardsOfThirdPlayer.Clear();
        _cardsOfThirdPlayer = cardsOfThirdPlayer.ToList();
        _cardsOfFourthPlayer.Clear();
        _cardsOfFourthPlayer = cardsOfFourthPlayer.ToList();
        if (GetCurrentInstance() == 1)
        {


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
        if (GetCurrentInstance() == 2)
        {

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
        if (GetCurrentInstance() == 3)
        {


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
        if (GetCurrentInstance() == 4)
        {


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
            
            _zingDealer = new ZingDealer("start");
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
            _zingDealer.DeleteLastFourTalonCards();
            //TimeOfMove.active = true;
            //isAviableToMove = true;
            var players = PhotonNetwork.CurrentRoom.Players;
            int tempValue = 1;
            foreach(var temp in players)
            {
                
                _currentPhotonView.RPC("SetInstanceOfCurrentPlayer", temp.Value,tempValue, _cardsOfFirstPlayer.ToArray(), cardsOfSecondPlayer.ToArray(),
                    cardsOfThirdPlayer.ToArray(),cardsOfFourthPlayer.ToArray(),RemainingCardsList.ToArray());
                if(tempValue == 2)
                {
                    SideOfTeam.MoveInstance = 2;
                    _currentPhotonView.RPC("SetMoveInstancesOnOthersPlayers", RpcTarget.Others, SideOfTeam.MoveInstance);
                    _currentPhotonView.RPC("SetNextPlayerToPlay", temp.Value);
                }
                tempValue++;

            }
            
        }
    }

    [PunRPC]
    public void ChangeCurrentPlayerInstance(int value)
    {
        SideOfTeam.CurrentPlayerSide = value;
    }

    [PunRPC]
    public void SetNextPlayerToPlay()
    {
        TimeOfMove.active = true;
        isAviableToMove = true;
    }

    [PunRPC]
    public void SetMoveInstancesOnOthersPlayers(int value)
    {
        SideOfTeam.MoveInstance = value;
    }

    [PunRPC]
    public void ChangeMoveDropedCard(string NameOfPrefab, Vector3 position)
    {
        //var root = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        //foreach (var temp in root)
        //{

        //    if (temp.name.Contains("(Clone)"))
        //    {
        //        if (temp.name.Contains("CardImageValue"))
        //        {
        //            var card = temp.gameObject;
        //            card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
        //            card.transform.SetParent(LastCardCanvas.transform);
        //        }
        //        else
        //        {
        //            var card = temp.gameObject;
        //            card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
        //            card.transform.SetParent(canvacesOfFirstDeck.transform);
        //        }
        //    }

        //}

        var tt = Resources.Load("Prefabs/CardPrefabsSvg/" + NameOfPrefab);
        
        var card = (GameObject)tt;

        card.transform.position += position;
        card.transform.localScale = new Vector3(0.789f, 0.789f, 0);
        GameObject myBrick = Instantiate(card, new Vector3(position.x, position.y, 0), Quaternion.identity) as GameObject;


        myBrick.transform.SetParent(canvacesOfFirstDeck.transform);



    }


    [PunRPC]
    public void ActivatePlayerToPlay(string NickName)
    {
        if (FirstPlayerName.text.Equals(NickName))
        {
            var tv = (Canvas)canvacesOfCurrentPlayer;
            if(tv.transform.childCount > 0) { 
                ActivateTimeOfMove();

                GameScript.isAviableToMove = true;
                TimeOfMove.active = true;


                foreach (Transform element in tv.transform)
                {

                    element.GetComponent<EventTrigger>().enabled = true;

                    //    //var firstCard = element.Find("FirstCardSelected").gameObject;
                    //    //firstCard.active = false;

                }
            }



            if (tv.transform.childCount == 0)
            {
                if(SideOfTeam.CurrentPlayerSide == 2)
                {
                    List<string> listTemp = new List<string>();
                    foreach (Transform transform in canvacesOfFirstDeck.transform)
                    {

                        GameObject tempGameObject = transform.gameObject;
                        string name = tempGameObject.name;
                        var index = name.IndexOf("(");
                        string CardName = name.Substring(0, index);
                        listTemp.Add(CardName);

                        Destroy(transform.gameObject);
                    }

                     _currentPhotonView.RPC("CleanDesk", RpcTarget.Others, SideOfTeam.LastPick);


                    if (SideOfTeam.LastPick == 1)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsFirstPlayerAI = false;
                        foreach(var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(1))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsFirstPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.photonView.RPC("TakeRestOfCardsFirst", RpcTarget.All, array);

                                }
                            }
                        }
                        if (!IsFirstPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.photonView.RPC("TakeRestOfCardsFirstAI", RpcTarget.All, array);
                        }
                    }
                    else if(SideOfTeam.LastPick == 2)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsSecondPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(2))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsSecondPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.photonView.RPC("TakeRestOfCardsSecond", RpcTarget.All, array);

                                }
                            }
                        }
                        if (!IsSecondPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.photonView.RPC("TakeRestOfCardsSecondAI", RpcTarget.All,array);
                        }
                    }
                    else if(SideOfTeam.LastPick == 3)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsThirdPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(3))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsThirdPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.photonView.RPC("TakeRestOfCardsThird", RpcTarget.All, array);

                                }
                            }
                        }
                        if (!IsThirdPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.photonView.RPC("TakeRestOfCardsThirdAI", RpcTarget.All, array);
                        }
                    }
                    else if(SideOfTeam.LastPick == 4)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsFourthPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(4))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsFourthPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.photonView.RPC("TakeRestOfCardsFourth", RpcTarget.All, array );

                                }
                            }
                        }
                        if (!IsFourthPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.photonView.RPC("TakeRestOfCardsFourthAI", RpcTarget.All, array);
                        }
                    }
                    //_zingDealer = new ZingDealer("start", "two");
                    //string[] remaingCardArray = new string[_zingDealer.RemainingCards.Count];
                    //int intValue = 0;
                    //RemainingCardsList = new List<string>();
                    //foreach (var obj in _zingDealer.RemainingCards)
                    //{

                    //    remaingCardArray[intValue] = obj.name;
                    //    ///Debug.Log("a:" + obj.name);
                    //    RemainingCardsList.Add(obj.name);
                    //    intValue++;
                    //}

                    //string[] array = new string[_zingDealer.TalonCards.Count];
                    //int i = 0;
                    //listTalon = new List<string>();
                    //_listOfCards = new List<string>();
                    //foreach (var obj in _zingDealer.TalonCards)
                    //{

                    //    array[i] = obj.name;
                    //    listTalon.Add(obj.name);
                    //    _listOfCards.Add(obj.name);
                    //    i++;
                    //}

                    //talonArray = listTalon.ToArray();

                    //var objLastCard = _zingDealer.LastCard as GameObject;

                    //_cardsOfFirstPlayer = new List<string>();
                    //foreach (var obj in _zingDealer.CardsOfFirstPlayers)
                    //{
                    //    _cardsOfFirstPlayer.Add(obj.name);
                    //}

                    //string[] cardsOfSecondPlayer = new string[_zingDealer.CardsOfSecondPlayers.Count];

                    //int count = 0;
                    //foreach (var obj in _zingDealer.CardsOfSecondPlayers)
                    //{

                    //    cardsOfSecondPlayer[count] = obj.name;
                    //    count++;
                    //}

                    //string[] cardsOfThirdPlayer = new string[_zingDealer.CardsOfThirdPlayers.Count];

                    //int countThird = 0;
                    //foreach (var obj in _zingDealer.CardsOfThirdPlayers)
                    //{

                    //    cardsOfThirdPlayer[countThird] = obj.name;
                    //    countThird++;
                    //}

                    //string[] cardsOfFourthPlayer = new string[_zingDealer.CardsOfFourthPlayers.Count];

                    //int countFourth = 0;
                    //foreach (var obj in _zingDealer.CardsOfFourthPlayers)
                    //{

                    //    cardsOfFourthPlayer[countFourth] = obj.name;
                    //    countFourth++;
                    //}


                    //string ttt = objLastCard.name.Split('_')[1];
                    ////Debug.Log("value2:" + ttt);

                    //var components = CardImageValueLastCard.GetComponents<Component>();
                    //foreach (var com in components)
                    //{
                    //    //Debug.Log("komponente");
                    //    var vv = com.GetType();
                    //    if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    //    {

                    //        var image2 = (SVGImporter.SVGImage)com;
                    //        image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/CARDS_" + ttt + "/" + objLastCard.name);
                    //    }
                    //}
                    //string strana = "";
                    //if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
                    //{
                    //    var component = TeamImageLastCard.GetComponents<Component>();
                    //    foreach (var com in component)
                    //    {
                    //        //Debug.Log("komponente");
                    //        var vv = com.GetType();
                    //        if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    //        {

                    //            var image2 = (SVGImporter.SVGImage)com;
                    //            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackREDSide");
                    //        }
                    //    }

                    //    var componentsDealerBoard = DealerBoard.GetComponents<Component>();

                    //    //var image = gameObject.GetComponent<SVGImage>();
                    //    // Debug.Log("fff");
                    //    foreach (var com in componentsDealerBoard)
                    //    {
                    //        var vv = com.GetType();
                    //        if (typeof(Image).IsAssignableFrom(vv))
                    //        {
                    //            var image2 = (Image)com;
                    //            image2.sprite = Resources.Load<Sprite>("game_page/PictureRedBorderSmall");
                    //        }
                    //    }

                    //    strana = "Red";

                    //}
                    //else
                    //{
                    //    var component = TeamImageLastCard.GetComponents<Component>();
                    //    foreach (var com in component)
                    //    {
                    //        //Debug.Log("komponente");
                    //        var vv = com.GetType();
                    //        if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    //        {

                    //            var image2 = (SVGImporter.SVGImage)com;
                    //            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackBlueSide");
                    //        }
                    //    }

                    //    var componentsDealerBoard = DealerBoard.GetComponents<Component>();

                    //    //var image = gameObject.GetComponent<SVGImage>();
                    //    // Debug.Log("fff");
                    //    foreach (var com in componentsDealerBoard)
                    //    {
                    //        var vv = com.GetType();
                    //        if (typeof(Image).IsAssignableFrom(vv))
                    //        {
                    //            var image2 = (Image)com;
                    //            image2.sprite = Resources.Load<Sprite>("game_page/PictureBlueBorderSmall");
                    //        }
                    //    }


                    //    strana = "Blue";
                    //}
                    //_currentPhotonView.RPC("SetInitDealerConfig", RpcTarget.Others, objLastCard.name, strana);
                    //_cardsOfFirstPlayer = new List<string>();
                    //foreach (var obj in _zingDealer.CardsOfFirstPlayers)
                    //{
                    //    _cardsOfFirstPlayer.Add(obj.name);
                    //}


                    //_random = new System.Random();

                    //_tolerances = new List<float>();

                    //for (int j = 0; j < 4; j++)
                    //{
                    //    //float tol = (float)  _random.Next(1, 2) * _positionTolerance;
                    //    float tol = (float)_random.NextDouble() * _positionTolerance;
                    //    _tolerances.Add(tol);
                    //}
                    //InitTalonCards();
                    //DeleteLastFourTalonCards();
                    //_zingDealer.DeleteLastFourTalonCards();
                    ////TimeOfMove.active = true;
                    ////isAviableToMove = true;
                    //var players = PhotonNetwork.CurrentRoom.Players;

                    //_currentPhotonView.RPC("SetCardsToPlayers", RpcTarget.Others, _cardsOfFirstPlayer.ToArray(), cardsOfSecondPlayer.ToArray(),
                    //    cardsOfThirdPlayer.ToArray(), cardsOfFourthPlayer.ToArray(), RemainingCardsList.ToArray());

                    //SideOfTeam.MoveInstance = 3;
                    //_currentPhotonView.RPC("SetMoveInstancesOnOthersPlayers", RpcTarget.Others, SideOfTeam.MoveInstance);

                    //_currentPhotonView.RPC("ActivatePlayerToPlay", RpcTarget.Others, PhotonNetwork.LocalPlayer.NickName);
                    
                }
                
            }
            
           


        }
    }

    [PunRPC]
    public void ActivatePlayerToPlayInstance(int tempInst)
    {
        //Debug.Log("vri:" + FirstPlayerInstance.text);
        //Debug.Log("v:" + tempInst);
        if (FirstPlayerInstance.text.Equals(""+tempInst))
        {

            var tv = (Canvas)canvacesOfCurrentPlayer;
            if (tv.transform.childCount > 0)
            {
                ActivateTimeOfMove();

                GameScript.isAviableToMove = true;
                TimeOfMove.active = true;

            }



           

            if (tv.transform.childCount == 0)
            {
                if (SideOfTeam.CurrentPlayerSide == 2)
                {

                    List<string> listTemp = new List<string>();
                    foreach (Transform transform in canvacesOfFirstDeck.transform)
                    {

                        GameObject tempGameObject = transform.gameObject;
                        string name = tempGameObject.name;
                        var index = name.IndexOf("(");
                        string CardName = name.Substring(0, index);
                        listTemp.Add(CardName);

                        Destroy(transform.gameObject);
                    }

                    _currentPhotonView.RPC("CleanDesk", RpcTarget.Others, SideOfTeam.LastPick);

                    if (SideOfTeam.LastPick == 1)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsFirstPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(1))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsFirstPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.photonView.RPC("TakeRestOfCardsFirst", RpcTarget.All, array);

                                }
                            }
                        }
                        if (!IsFirstPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.photonView.RPC("TakeRestOfCardsFirstAI", RpcTarget.All, array);
                        }
                    }
                    else if (SideOfTeam.LastPick == 2)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsSecondPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(2))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsSecondPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.photonView.RPC("TakeRestOfCardsSecond", RpcTarget.All, array);

                                }
                            }
                        }
                        if (!IsSecondPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.photonView.RPC("TakeRestOfCardsSecondAI", RpcTarget.All, array);
                        }
                    }
                    else if (SideOfTeam.LastPick == 3)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsThirdPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(3))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsThirdPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.photonView.RPC("TakeRestOfCardsThird", RpcTarget.All, array);

                                }
                            }
                        }
                        if (!IsThirdPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.photonView.RPC("TakeRestOfCardsThirdAI", RpcTarget.All, array);
                        }
                    }
                    else if (SideOfTeam.LastPick == 4)
                    {
                        Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;
                        bool IsFourthPlayerAI = false;
                        foreach (var temp in values)
                        {
                            if (values[temp.Key].CustomProperties["Instance"].Equals(4))
                            {
                                if (values[temp.Key].CustomProperties["State"].Equals("active"))
                                {
                                    IsFourthPlayerAI = true;
                                    string[] array = listTemp.ToArray();
                                    RecordBoard._instance.photonView.RPC("TakeRestOfCardsFourth", RpcTarget.All, array);

                                }
                            }
                        }
                        if (!IsFourthPlayerAI)
                        {
                            string[] array = listTemp.ToArray();
                            RecordBoard._instance.photonView.RPC("TakeRestOfCardsFourthAI", RpcTarget.All, array);
                        }
                    }
                    //pocetak za novog djelioca 

                    //_zingDealer = new ZingDealer("start", "two");
                    //string[] remaingCardArray = new string[_zingDealer.RemainingCards.Count];
                    //int intValue = 0;
                    //RemainingCardsList = new List<string>();
                    //foreach (var obj in _zingDealer.RemainingCards)
                    //{

                    //    remaingCardArray[intValue] = obj.name;
                    //    ///Debug.Log("a:" + obj.name);
                    //    RemainingCardsList.Add(obj.name);
                    //    intValue++;
                    //}

                    //string[] array = new string[_zingDealer.TalonCards.Count];
                    //int i = 0;
                    //listTalon = new List<string>();
                    //_listOfCards = new List<string>();
                    //foreach (var obj in _zingDealer.TalonCards)
                    //{

                    //    array[i] = obj.name;
                    //    listTalon.Add(obj.name);
                    //    _listOfCards.Add(obj.name);
                    //    i++;
                    //}

                    //talonArray = listTalon.ToArray();

                    //var objLastCard = _zingDealer.LastCard as GameObject;

                    //_cardsOfFirstPlayer = new List<string>();
                    //foreach (var obj in _zingDealer.CardsOfFirstPlayers)
                    //{
                    //    _cardsOfFirstPlayer.Add(obj.name);
                    //}

                    //string[] cardsOfSecondPlayer = new string[_zingDealer.CardsOfSecondPlayers.Count];

                    //int count = 0;
                    //foreach (var obj in _zingDealer.CardsOfSecondPlayers)
                    //{

                    //    cardsOfSecondPlayer[count] = obj.name;
                    //    count++;
                    //}

                    //string[] cardsOfThirdPlayer = new string[_zingDealer.CardsOfThirdPlayers.Count];

                    //int countThird = 0;
                    //foreach (var obj in _zingDealer.CardsOfThirdPlayers)
                    //{

                    //    cardsOfThirdPlayer[countThird] = obj.name;
                    //    countThird++;
                    //}

                    //string[] cardsOfFourthPlayer = new string[_zingDealer.CardsOfFourthPlayers.Count];

                    //int countFourth = 0;
                    //foreach (var obj in _zingDealer.CardsOfFourthPlayers)
                    //{

                    //    cardsOfFourthPlayer[countFourth] = obj.name;
                    //    countFourth++;
                    //}


                    //string ttt = objLastCard.name.Split('_')[1];
                    ////Debug.Log("value2:" + ttt);

                    //var components = CardImageValueLastCard.GetComponents<Component>();
                    //foreach (var com in components)
                    //{
                    //    //Debug.Log("komponente");
                    //    var vv = com.GetType();
                    //    if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    //    {

                    //        var image2 = (SVGImporter.SVGImage)com;
                    //        image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/CARDS_" + ttt + "/" + objLastCard.name);
                    //    }
                    //}
                    //string strana = "";
                    //if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
                    //{
                    //    var component = TeamImageLastCard.GetComponents<Component>();
                    //    foreach (var com in component)
                    //    {
                    //        //Debug.Log("komponente");
                    //        var vv = com.GetType();
                    //        if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    //        {

                    //            var image2 = (SVGImporter.SVGImage)com;
                    //            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackREDSide");
                    //        }
                    //    }

                    //    var componentsDealerBoard = DealerBoard.GetComponents<Component>();

                    //    //var image = gameObject.GetComponent<SVGImage>();
                    //    // Debug.Log("fff");
                    //    foreach (var com in componentsDealerBoard)
                    //    {
                    //        var vv = com.GetType();
                    //        if (typeof(Image).IsAssignableFrom(vv))
                    //        {
                    //            var image2 = (Image)com;
                    //            image2.sprite = Resources.Load<Sprite>("game_page/PictureRedBorderSmall");
                    //        }
                    //    }

                    //    strana = "Red";

                    //}
                    //else
                    //{
                    //    var component = TeamImageLastCard.GetComponents<Component>();
                    //    foreach (var com in component)
                    //    {
                    //        //Debug.Log("komponente");
                    //        var vv = com.GetType();
                    //        if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    //        {

                    //            var image2 = (SVGImporter.SVGImage)com;
                    //            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackBlueSide");
                    //        }
                    //    }

                    //    var componentsDealerBoard = DealerBoard.GetComponents<Component>();

                    //    //var image = gameObject.GetComponent<SVGImage>();
                    //    // Debug.Log("fff");
                    //    foreach (var com in componentsDealerBoard)
                    //    {
                    //        var vv = com.GetType();
                    //        if (typeof(Image).IsAssignableFrom(vv))
                    //        {
                    //            var image2 = (Image)com;
                    //            image2.sprite = Resources.Load<Sprite>("game_page/PictureBlueBorderSmall");
                    //        }
                    //    }


                    //    strana = "Blue";
                    //}
                    //_currentPhotonView.RPC("SetInitDealerConfig", RpcTarget.Others, objLastCard.name, strana);
                    //_cardsOfFirstPlayer = new List<string>();
                    //foreach (var obj in _zingDealer.CardsOfFirstPlayers)
                    //{
                    //    _cardsOfFirstPlayer.Add(obj.name);
                    //}


                    //_random = new System.Random();

                    //_tolerances = new List<float>();

                    //for (int j = 0; j < 4; j++)
                    //{
                    //    //float tol = (float)  _random.Next(1, 2) * _positionTolerance;
                    //    float tol = (float)_random.NextDouble() * _positionTolerance;
                    //    _tolerances.Add(tol);
                    //}
                    //InitTalonCards();
                    //DeleteLastFourTalonCards();
                    //_zingDealer.DeleteLastFourTalonCards();
                    ////TimeOfMove.active = true;
                    ////isAviableToMove = true;
                    //var players = PhotonNetwork.CurrentRoom.Players;

                    //    _currentPhotonView.RPC("SetCardsToPlayers", RpcTarget.Others, _cardsOfFirstPlayer.ToArray(), cardsOfSecondPlayer.ToArray(),
                    //        cardsOfThirdPlayer.ToArray(), cardsOfFourthPlayer.ToArray(), RemainingCardsList.ToArray());

                    //    SideOfTeam.MoveInstance = 3;
                    //    _currentPhotonView.RPC("SetMoveInstancesOnOthersPlayers", RpcTarget.Others, SideOfTeam.MoveInstance);
                    //_currentPhotonView.RPC("ActivatePlayerToPlay", RpcTarget.Others, PhotonNetwork.LocalPlayer.NickName);


                }

            }

            foreach (Transform element in tv.transform)
            {
                
                element.GetComponent<EventTrigger>().enabled = true;
                
                //    //var firstCard = element.Find("FirstCardSelected").gameObject;
                //    //firstCard.active = false;

            }


        }
    }

    [PunRPC]
    public void CleanDesk(int value)
    {
        SideOfTeam.LastPick = value;
        foreach (Transform tt in canvacesOfFirstDeck.transform)
        {
            
            Destroy(tt.gameObject);
        }
    }

    public bool PickUpCardsFromDeckWithoutPlayer(string sideOfTeam)
    {
        if (canvacesOfCurrentPlayer.transform.childCount > 0)
        {
            if (canvacesOfFirstDeck.transform.childCount == 1)
            {
                if (canvacesOfCurrentPlayer.transform.childCount > 0)
                {
                    
                    DroppedCardsOneLeft dropCard = new DroppedCardsOneLeft(canvacesOfCurrentPlayer);
                    

                }
            }
            else if (canvacesOfFirstDeck.transform.childCount == 2)
            {
                _previousCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 2).gameObject;

                _currentCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 1).gameObject;

                string goName1 = _previousCard.name.Split('_')[0];
                string goName2 = _currentCard.name.Split('_')[0];
                
                DroppedCardsTwoLeft dropCard = new DroppedCardsTwoLeft(goName1, goName2, canvacesOfFirstDeck, canvacesOfCurrentPlayer);
                if (goName1.Equals(goName2, StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionEqualsName();

                    //RecordBoard._instance.photonView.RPC("TakeCardsZing2", RpcTarget.All,sideOfTeam, listArray);
                    RecordBoard._instance.TakeCardsZing2(sideOfTeam, listArray);
                    return true;
                    
                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionJDropped();


                    // RecordBoard._instance.photonView.RPC("TakeCardsFromTalon2", RpcTarget.All,sideOfTeam, listArray);
                    RecordBoard._instance.TakeCardsFromTalon2(sideOfTeam, listArray);
                    return true;
                    
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        

                    }

                }
            }
            else
            {
                _previousCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 2).gameObject;

                _currentCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 1).gameObject;

                string goName1 = _previousCard.name.Split('_')[0];
                string goName2 = _currentCard.name.Split('_')[0];
                
                DroppedCardsTwoLeft dropCard = new DroppedCardsTwoLeft(goName1, goName2, canvacesOfFirstDeck, canvacesOfCurrentPlayer);
                if (goName1.Equals(goName2, StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionEqualsName();


                    //RecordBoard._instance.photonView.RPC("TakeCardsFromTalon2", RpcTarget.All,sideOfTeam, listArray);
                    RecordBoard._instance.TakeCardsFromTalon2( sideOfTeam, listArray);
                    return true;
                    
                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {

                    string[] listArray = dropCard.TakeActionJDropped();
                    
                    
                    RecordBoard._instance.TakeCardsFromTalon2(sideOfTeam, listArray);
                    return true;
                    //RecordBoard._instance.TakeCardsFromTalon(listArray);
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        //player.TimeOfMove.active = true;
                        //isAviableToMove = true;
                        //dropCard.SetCanvas();
                    }

                }
            }
        }
        else
        {

            //ovdje se pise kod ukoliko su sve igraceve karte bacene na stolu.
            if (canvacesOfFirstDeck.transform.childCount == 1)
            {
                if (canvacesOfCurrentPlayer.transform.childCount > 0)
                {
                    
                    DroppedCardsOneLeft dropedCardOneLeft = new DroppedCardsOneLeft(canvacesOfCurrentPlayer);
                    

                }
            }
            else if (canvacesOfFirstDeck.transform.childCount == 2)
            {
                _previousCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 2).gameObject;

                _currentCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 1).gameObject;

                string goName1 = _previousCard.name.Split('_')[0];
                string goName2 = _currentCard.name.Split('_')[0];
                
                DroppedCardsTwoLeft dropCard = new DroppedCardsTwoLeft(goName1, goName2, canvacesOfFirstDeck, canvacesOfCurrentPlayer);
                if (goName1.Equals(goName2, StringComparison.OrdinalIgnoreCase))
                {

                    string[] listArray = dropCard.TakeActionEqualsName();

                    //RecordBoard._instance.photonView.RPC("TakeCardsZing2", RpcTarget.All, sideOfTeam, listArray);
                    RecordBoard._instance.TakeCardsZing2(sideOfTeam, listArray);
                    return true;
                    

                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionJDropped();

                    
                   // RecordBoard._instance.photonView.RPC("TakeCardsFromTalon2", RpcTarget.All, sideOfTeam, listArray);
                    RecordBoard._instance.TakeCardsFromTalon2(sideOfTeam, listArray);
                    return true;
                    
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        
                    }
                }
            }
            else
            {
                _previousCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 2).gameObject;

                _currentCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 1).gameObject;

                string goName1 = _previousCard.name.Split('_')[0];
                string goName2 = _currentCard.name.Split('_')[0];
                
                DroppedCardsTwoLeft dropCard = new DroppedCardsTwoLeft(goName1, goName2, canvacesOfFirstDeck, canvacesOfCurrentPlayer);

                if (goName1.Equals(goName2, StringComparison.OrdinalIgnoreCase))
                {

                    string[] listArray = dropCard.TakeActionEqualsName();

                    //RecordBoard._instance.photonView.RPC("TakeCardsFromTalon2", RpcTarget.All, sideOfTeam, listArray);
                    RecordBoard._instance.TakeCardsFromTalon2(sideOfTeam, listArray);
                    return true;
                    
                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {

                    string[] listArray = dropCard.TakeActionJDropped();

                    //RecordBoard._instance.photonView.RPC("TakeCardsFromTalon2", RpcTarget.All, sideOfTeam, listArray);
                    RecordBoard._instance.TakeCardsFromTalon2(sideOfTeam, listArray);
                    return true;
                    
                }
                else
                {

                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        
                    }
                }
            }
        }
        return false;
    }

    public bool PickUpCardsFromDeck()
    {
        if (canvacesOfCurrentPlayer.transform.childCount > 0)
        {
            if (canvacesOfFirstDeck.transform.childCount == 1)
            {
                if (canvacesOfCurrentPlayer.transform.childCount > 0)
                {
                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;
                    DroppedCardsOneLeft dropCard = new DroppedCardsOneLeft(canvacesOfCurrentPlayer);
                    //dropCard.SetCanvas();
                   
                }
            }
            else if (canvacesOfFirstDeck.transform.childCount == 2)
            {
                _previousCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 2).gameObject;

                _currentCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 1).gameObject;

                string goName1 = _previousCard.name.Split('_')[0];
                string goName2 = _currentCard.name.Split('_')[0];
                //Debug.Log("evo me");
                //Debug.Log("predhodna karta:" + goName1);
                //Debug.Log("zadnja karta:" + goName2);
                DroppedCardsTwoLeft dropCard = new DroppedCardsTwoLeft(goName1, goName2, canvacesOfFirstDeck, canvacesOfCurrentPlayer);
                if (goName1.Equals(goName2, StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionEqualsName();
                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;
                    //photonView.RPC("TakeCardsZing", RpcTarget.Others, listArray);
                     RecordBoard._instance.photonView.RPC("TakeCardsZing", PhotonNetwork.LocalPlayer, listArray);
                    return true;
                    //RecordBoard._instance.TakeCardsZing(listArray);
                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionJDropped();

                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;
                      RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", PhotonNetwork.LocalPlayer, listArray);
                    return true;
                    //photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    //RecordBoard._instance.TakeCardsFromTalon(listArray);
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        //player.TimeOfMove.active = true;
                        //isAviableToMove = true;
                        //dropCard.SetCanvas();

                    }
                    
                }
            }
            else
            {
                _previousCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 2).gameObject;

                _currentCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 1).gameObject;

                string goName1 = _previousCard.name.Split('_')[0];
                string goName2 = _currentCard.name.Split('_')[0];
                //Debug.Log("evo me");
                //Debug.Log("predhodna karta:" + goName1);
                //Debug.Log("zadnja karta:" + goName2);
                DroppedCardsTwoLeft dropCard = new DroppedCardsTwoLeft(goName1, goName2, canvacesOfFirstDeck, canvacesOfCurrentPlayer);
                if (goName1.Equals(goName2, StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionEqualsName();

                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;
                    // photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                      RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", PhotonNetwork.LocalPlayer, listArray);
                    return true;
                    //  Debug.Log("val:"+ ListOfTakenCards.Count);
                    //RecordBoard._instance.TakeCardsFromTalon(listArray);
                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {

                    string[] listArray = dropCard.TakeActionJDropped();
                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;

                    //  photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                       RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", PhotonNetwork.LocalPlayer, listArray);
                    return true;
                    //RecordBoard._instance.TakeCardsFromTalon(listArray);
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        //player.TimeOfMove.active = true;
                        //isAviableToMove = true;
                        //dropCard.SetCanvas();
                    }

                }
            }
        }
        else
        {

            //ovdje se pise kod ukoliko su sve igraceve karte bacene na stolu.
            if (canvacesOfFirstDeck.transform.childCount == 1)
            {
                if (canvacesOfCurrentPlayer.transform.childCount > 0)
                {
                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;
                    DroppedCardsOneLeft dropedCardOneLeft = new DroppedCardsOneLeft(canvacesOfCurrentPlayer);
                    //dropedCardOneLeft.SetCanvas();

                }
            }
            else if (canvacesOfFirstDeck.transform.childCount == 2)
            {
                _previousCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 2).gameObject;

                _currentCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 1).gameObject;

                string goName1 = _previousCard.name.Split('_')[0];
                string goName2 = _currentCard.name.Split('_')[0];
                //Debug.Log("evo me");
                //Debug.Log("predhodna karta:" + goName1);
                //Debug.Log("zadnja karta:" + goName2);
                DroppedCardsTwoLeft dropCard = new DroppedCardsTwoLeft(goName1, goName2, canvacesOfFirstDeck, canvacesOfCurrentPlayer);
                if (goName1.Equals(goName2, StringComparison.OrdinalIgnoreCase))
                {

                    string[] listArray = dropCard.TakeActionEqualsName();
                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;

                    //photonView.RPC("TakeCardsZing", RpcTarget.Others, listArray);
                       RecordBoard._instance.photonView.RPC("TakeCardsZing", PhotonNetwork.LocalPlayer, listArray);
                    return true;
                    //RecordBoard._instance.TakeCardsZing(listArray);

                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionJDropped();

                    //photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                       RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", PhotonNetwork.LocalPlayer, listArray);
                    return true;
                    //RecordBoard._instance.TakeCardsFromTalon(listArray);
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        //player.TimeOfMove.active = true;
                        //isAviableToMove = true;

                    }
                }
            }
            else
            {
                _previousCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 2).gameObject;

                _currentCard = canvacesOfFirstDeck.transform.GetChild(canvacesOfFirstDeck.transform.childCount - 1).gameObject;

                string goName1 = _previousCard.name.Split('_')[0];
                string goName2 = _currentCard.name.Split('_')[0];
                //Debug.Log("evo me");
                //Debug.Log("predhodna karta:" + goName1);
                //Debug.Log("zadnja karta:" + goName2);
                DroppedCardsTwoLeft dropCard = new DroppedCardsTwoLeft(goName1, goName2, canvacesOfFirstDeck, canvacesOfCurrentPlayer);

                if (goName1.Equals(goName2, StringComparison.OrdinalIgnoreCase))
                {

                    string[] listArray = dropCard.TakeActionEqualsName();
                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;

                    // photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                      RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", PhotonNetwork.LocalPlayer, listArray);
                    return true;
                    //RecordBoard._instance.TakeCardsFromTalon(listArray);
                    //  Debug.Log("val:"+ ListOfTakenCards.Count);
                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {

                    string[] listArray = dropCard.TakeActionJDropped();
                    //player.TimeOfMove.active = true;
                    //isAviableToMove = true;

                    //photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                     RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", PhotonNetwork.LocalPlayer, listArray);
                    return true;
                    //RecordBoard._instance.TakeCardsFromTalon(listArray);
                }
                else
                {

                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        //player.TimeOfMove.active = true;
                        //isAviableToMove = true;
                        //dropCard.SetCanvas();
                    }
                }
            }
        }
        return false;
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

    [PunRPC]
    public void SetListForRequiredPlayerFirst(string[] tempList,int side)
    {
      
            _cardsOfFirstPlayer = tempList.ToList();
            SideOfTeam.MoveInstance = side;
    }
    [PunRPC]
    public void SetListForRequiredPlayerSecond(string[] tempList,int side)
    {

        _cardsOfSecondPlayer = tempList.ToList();
        SideOfTeam.MoveInstance = side;
    }

    [PunRPC]
    public void SetListForRequiredPlayerThird(string[] tempList,int side)
    {
        
        _cardsOfThirdPlayer = tempList.ToList();
        SideOfTeam.MoveInstance = side;
    }


    [PunRPC]
    public void SetListForRequiredPlayerFourth(string[] tempList,int side)
    {

        _cardsOfFourthPlayer = tempList.ToList();
        SideOfTeam.MoveInstance = side;
    }
    [PunRPC]
    public void DeleteRemainingCards()
    {
        RemainingCardsList.RemoveRange(0, 16);

        
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

    public void DeactivateTimeOfMove()
    {
        TimeOfMove.SetActive(false);
    }

    public void ActivateTimeOfMove()
    {
        TimeOfMove.SetActive(true);
    }

    public int GetCurrentInstance()
    {
        return currentInstance;
    }

    public void SetCurrentInstance(int instance)
    {
        currentInstance = instance;
    }
    public List<string> GetCardsOfFirstPlayer()
    {
        return _cardsOfFirstPlayer;
    }
    
    public void SetCardsOfFirstPlayer(List<string> cards)
    {
        _cardsOfFirstPlayer = cards;
    }
    public List<string> GetCardsOfSecondPlayer()
    {
        return _cardsOfSecondPlayer;
    }
    public void SetCardsOfSecondPlayer(List<string> cards)
    {
        _cardsOfSecondPlayer = cards;
    }

    public List<string> GetCardsOfThirdPlayer()
    {
        return _cardsOfThirdPlayer;
    }

    public void SetCardsOfThirdPlayer(List<string> cards)
    {
        _cardsOfThirdPlayer = cards;
    }

    public List<string> GetCardsOfFourthPlayer()
    {
        return _cardsOfFourthPlayer;
    }

    public void SetCardsOfFourthPlayer(List<string> cards)
    {
        _cardsOfFourthPlayer = cards;
    }

    public bool GetRunOnceFirst()
    {
        return runOnceFirst;
    }

    public void SetRunOnceFirst(bool run)
    {
        runOnceFirst = run;
    }

    public bool GetRunOnceSecond()
    {
        return runOnceSecond;
    }

    public void SetRunOnceSecond(bool run)
    {
        runOnceSecond = run;
    }

    public bool GetRunOnceThird()
    {
        return runOnceThird;
    }

    public void SetRunOnceThird(bool run)
    {
        runOnceThird = run;
    }

    public bool GetRunOnceFourth()
    {
        return runOnceFourth;
    }

    public void SetRunOnceFourth(bool run)
    {
        runOnceFourth = run;
    }

    public void setZingDealer(ZingDealer zing)
    {
        _zingDealer = zing;
    }

    public ZingDealer GetZingDealer()
    {
        return _zingDealer;
    }

    public List<string> GetRemainingCardsList()
    {
        return RemainingCardsList;
    }

    public void SetRemaingCardsList(List<string> values)
    {
        RemainingCardsList = values;
    }


}
