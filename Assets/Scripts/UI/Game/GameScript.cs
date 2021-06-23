﻿using Assets.Scripts.Managers;
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


    private bool isGameStarted = false;
   

    private ZingDealer _zingDealer;

    private List<string> RemainingCardsList;


    private List<string> listTalon;


    private string[] talonArray;
   
    private List<string> _listOfCards = new List<string>();

    private string _lastCardOfDealerPlayer;

    private List<string> _cardsOfFirstPlayer;

    private List<string> _cardsOfSecondPlayer;
    private System.Random _random;

    private float _positionTolerance = -1.5f;
    private List<float> _tolerances;

    private bool isArrangeCard = false;


    void Awake()
    {
        isArrangeCard = false;
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
            ArrangeCards();


    }

    private void InitTalonCards()
    {
        //float startPosition = 0.5f;
        float startPosition = 1200f;

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
           // _currentPhotonView.RPC("InformCardPosition", RpcTarget.Others, arrayVectors, listTalon.ToArray());
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


        if (canvacesOfFirstDeck.transform.childCount == 0)
        {
            SendTalon(arrayTalon);
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
    public void SendInitTalon(string[] Array)
    {
        float startPosition = 1200f;

        int i = 0;
        Vector3[] arrayPosition = new Vector3[4];
        string[] arrayCards = { "", "", "", "" };

        var root = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach(var temp in root)
        {
            if (temp.name.Contains("(Clone)")) {
                var card = temp.gameObject;

                card.transform.SetParent(canvacesOfFirstDeck.transform);
               // Destroy(temp.gameObject);
            }
          
        }
        
      
            
           // Debug.Log("karta:" + canvacesOfFirstDeck.transform.parent.parent.parent.parent.parent.parent.name);
           
         
            //var prefabs = Resources.Load("Prefabs/CardPrefabsStartSVG/"+obj);
            //GameObject gameObj = (GameObject)prefabs;

            //Vector3 position = new Vector3(startPosition, 700f);
            //gameObj.transform.localPosition = position;
            //gameObj.transform.localScale = new Vector3(0.789f, 0.789f, 0);
            //GameObject firstDeck = (GameObject)Instantiate(gameObj, new Vector3(startPosition, 700f, 0), Quaternion.identity);
            //firstDeck.transform.localScale = new Vector3(0.789f, 0.789f, 0);
            //arrayPosition[i] = position;
            //arrayCards[i] = "" + gameObj.name;
            //firstDeck.transform.SetParent(canvacesOfFirstDeck.transform);

            //i++;
            //startPosition += 100f;
            //multiplier -= 5f;

            

        
    }

    [PunRPC]
    public void SendTalon(string[] talonList)
    {
        listTalon = talonList.ToList<string>();
        //float startPosition = 0.5f;
        //float multiplier = 1.15f;
        float startPosition = 1200f;


        //talonCards = new List<GameObject>();
        foreach (var obj in listTalon)
        {
            var prefab = Resources.Load("Prefabs/CardPrefabsStartSVG/" + obj);

            var go = prefab as GameObject;
            if (go.name == obj)
            {
                GameObject gameObj = (GameObject)go;
                Vector3 position = new Vector3(startPosition, 700f);
                gameObj.transform.localPosition = position;
                GameObject firstDeck = (GameObject)Instantiate(gameObj, new Vector3(startPosition, 700f, 0), Quaternion.identity);

                firstDeck.transform.SetParent(canvacesOfFirstDeck.transform);
                startPosition += 100f;
                _listOfCards.Add(obj);
                //multiplier -= 5f;

            }
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

            string[] cardsOfSecondPlayer = new string[_zingDealer.CardsOfSecondPlayers.Count];

            int count = 0;
            foreach (var obj in _zingDealer.CardsOfSecondPlayers)
            {

                cardsOfSecondPlayer[count] = obj.name;
                count++;
            }


            _lastCardOfDealerPlayer = objLastCard.name;

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

        }
    }

    public void DeleteLastFourTalonCards()
    {
      

        int start = RemainingCardsList.Count - 5;
        RemainingCardsList.RemoveRange(start, 4);


    }

}
