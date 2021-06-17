using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
using Assets.Scripts.UI.Game.CheckCards;
using System.Linq;
using SVGImporter;
using Assets.Scripts.UI.Game.Utils;
using TMPro;
using System.Text.RegularExpressions;
using System;
using Facebook.Unity;
using Assets.Scripts.UI.Game;
using UnityEngine.SceneManagement;

public  class BeginningOfGame : MonoBehaviourPun
{

    [SerializeField]
    private  Canvas canvacesOfFirstDeck;
    private ZingDealer _zingDealer;

    [SerializeField]
    private Canvas canvacesOfCurrentPlayer;

    [SerializeField]
    private GameObject TimeOfMove;
   

    public static bool isAviableToMove =true;

    private List<string> listTalon;

    private List<string> _cardsOfFirstPlayer;

    private List<string> _cardsOfSecondPlayer;

    private string _lastCardOfDealerPlayer;

    private List<string> RemainingCardsList;

    //private List<GameObject> talonCards;
    private System.Random _random;

    //private float _positionTolerance = 1.5f;
    private float _positionTolerance = -1.5f;
    private List<float> _tolerances;
    
    private GameObject _currentCard;
    
    private GameObject _previousCard;
    
    public static BeginningOfGame player;

    private bool isArrangeCard = false;
    
    public static List<string> ListOfAllTakenCards { get; set; }
    public static List<string> _listOfZings { get; set; }
    [SerializeField]
    private List<string> _listOfCards = new List<string>();

    [SerializeField]
    private GameObject DealerImage;

    [SerializeField]
    private TMP_Text DealerName;

    [SerializeField]
    private Canvas LastCard;
    [SerializeField]
    private Canvas CardOfTeam;

    [SerializeField]
    private TMP_Text OpositePlayerName;

    [SerializeField]
    private GameObject OpositePlayerImage;

    private void Awake()
    {
        player = this;
        //Debug.Log("restart scene");

        _listOfZings = new List<string>();
        ListOfAllTakenCards = new List<string>();
        //Debug.Log("pocetak");
        if (PhotonNetwork.IsMasterClient)
        {
            if (SideOfTeam.ChangeSideOfCards == false)
            {
                isArrangeCard = false;
                _zingDealer = new ZingDealer();
                string[] remaingCardArray = new string[_zingDealer.RemainingCards.Count];

                int intValue = 0;
                RemainingCardsList = new List<string>();
                foreach (var obj in _zingDealer.RemainingCards)
                {

                    remaingCardArray[intValue] = obj.name;
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

                var objLastCard = _zingDealer.LastCard as GameObject;

                string[] cardsOfSecondPlayer = new string[_zingDealer.CardsOfSecondPlayers.Count];

                int count = 0;
                foreach (var obj in _zingDealer.CardsOfSecondPlayers)
                {

                    cardsOfSecondPlayer[count] = obj.name;
                    count++;
                }


                _lastCardOfDealerPlayer = objLastCard.name;

                // Debug.Log("zadnja karta:" + lastCardOfDealerPlayer);
                //string[] cardsValues = new string[_zingDealer.ListOfCardsOfSecondPlayers.Count];

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

                DeleteLastFourTalonCards();

                photonView.RPC("SetisArrangeCard", RpcTarget.Others);
                photonView.RPC("SendCards", RpcTarget.Others, remaingCardArray, cardsOfSecondPlayer, _lastCardOfDealerPlayer);
                photonView.RPC("SendTalon", RpcTarget.Others, array);

                

            }
            else
            {
                //Debug.Log("proba");
                _listOfZings = new List<string>();
                ListOfAllTakenCards = new List<string>();
                isArrangeCard = false;
                photonView.RPC("SetisArrangeCard", RpcTarget.Others);
                photonView.RPC("InitCards", RpcTarget.Others, SideOfTeam.ChangeSideOfCards);
            }
        }else
        {

        }

    }

    
    void Start()
    {
        var PlayerName = GameManagerSingleton.Instance.GetPlayerName();


        OpositePlayerName.text = PlayerName == PhotonNetwork.CurrentRoom.GetPlayer(2).NickName
           ? PhotonNetwork.CurrentRoom.GetPlayer(1).NickName
           : PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;

        FB.API("/me/picture?type=square&height=200&width=200", HttpMethod.GET, DisplayCurrentPlayerPic);
        if (PhotonNetwork.IsMasterClient)
        {
            if (SideOfTeam.ChangeSideOfCards == false)
            {
                
                DealerName.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
                
                string ttt = _lastCardOfDealerPlayer.Split('_')[1];
                //Debug.Log("value2:" + ttt);
                var value = LastCard.transform.GetChild(0);

                var components = value.GetComponents<Component>();
                foreach (var com in components)
                {
                    //Debug.Log("komponente");
                    var vv = com.GetType();
                    if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    {
                        
                        var image2 = (SVGImporter.SVGImage)com;
                        image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/CARDS_"+ ttt+"/"+_lastCardOfDealerPlayer);
                    }
                }

                //

               
                var CardOfTeamvalue = CardOfTeam.transform.GetChild(0);

                var componentsCardOfTeam = CardOfTeamvalue.GetComponents<Component>();
                foreach (var com in componentsCardOfTeam)
                {
                    //Debug.Log("komponente");
                    var vv = com.GetType();
                    if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                    {

                        var image2 = (SVGImporter.SVGImage)com;
                        if (!SideOfTeam.ChangeSideOfCards)
                        {
                            
                            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackBLUESide");
                        }
                        else
                            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackREDSide");

                    }
                }
                

                //LastCard = tempCard;
                FB.API("/me/picture?type=square&height=200&width=200", HttpMethod.GET, DisplayProfilePic);
                InitTalonCards();
                InitBoard();
                TimeOfMove.active = false;
                isAviableToMove = false;

                photonView.RPC("RestartCardsRedSide", RpcTarget.Others);

                //Debug.Log("else false");
                if (RecordInteration._interation > 0)
                {
                    RecordBoard._instance.SetTotalPoints();
                }
            }
            
        }

        
        if (SideOfTeam.ChangeSideOfCards == true)
        {
            //Debug.Log("if uslov");
            if (RecordInteration._interation > 0)
            {
                RecordBoard._instance.SetTotalPoints();
            }
        }
       
    }

     void Update()
     {
        if (PhotonNetwork.IsMasterClient)
        {
            if (SideOfTeam.ChangeSideOfCards == false)
            {
                if (!isArrangeCard)
                    ArrangeCards();
            }
        }
        
        if (SideOfTeam.ChangeSideOfCards == true)
        {

            if (!isArrangeCard)
                ArrangeCards();

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

            FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);

            //float value = -200.8f;
            //float valueY = -2.6f;
            //GameObject myBrick = Instantiate(FirstCardObject, new Vector3(value + multiplier, valueY, 0), Quaternion.identity) as GameObject;
            //myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
            //multiplier += 135f;
            float value = -3.6f;
            float valueY = -2.6f;
            GameObject myBrick = Instantiate(FirstCardObject, new Vector3(value + multiplier, valueY, 0), Quaternion.identity) as GameObject;
            myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
            multiplier += 1.75f;

        }
        
    }

    void DisplayProfilePic(IGraphResult result)
    {

        if (result.Texture != null)
        {

            UnityEngine.UI.Image ProfilePic = DealerImage.GetComponent<UnityEngine.UI.Image>();
            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 200, 200), new Vector2());
            Texture2D tempTex = result.Texture;
            
            byte [] value = tempTex.EncodeToPNG();
            photonView.RPC("SetPictureDealer", RpcTarget.Others, value, DealerName.text);
        }

    }

    void DisplayCurrentPlayerPic(IGraphResult result)
    {
        if (result.Texture != null)
        {

            Texture2D tempTex = result.Texture;
            byte[] value = tempTex.EncodeToPNG();
            photonView.RPC("SetOpositePlayerImage", RpcTarget.Others, value);
        }
    }

    [PunRPC]
    public void InitCards(bool setSide)
    {
        
        
        _listOfZings = new List<string>();
        ListOfAllTakenCards = new List<string>();

        SideOfTeam.ChangeSideOfCards = setSide;

        RecordBoard._instance.SetInitializationOfCards();
        
        _zingDealer = new ZingDealer();
        string[] remaingCardArray = new string[_zingDealer.RemainingCards.Count];

        int intValue = 0;
        RemainingCardsList = new List<string>();
        foreach (var obj in _zingDealer.RemainingCards)
        {

            remaingCardArray[intValue] = obj.name;
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

        var objLastCard = _zingDealer.LastCard as GameObject;

        string[] cardsOfSecondPlayer = new string[_zingDealer.CardsOfSecondPlayers.Count];

        int count = 0;
        foreach (var obj in _zingDealer.CardsOfSecondPlayers)
        {

            cardsOfSecondPlayer[count] = obj.name;
            count++;
        }


        _lastCardOfDealerPlayer = objLastCard.name;

        // Debug.Log("zadnja karta:" + lastCardOfDealerPlayer);
        //string[] cardsValues = new string[_zingDealer.ListOfCardsOfSecondPlayers.Count];

        _cardsOfFirstPlayer = new List<string>();
        foreach (var obj in _zingDealer.CardsOfFirstPlayers)
        {
            _cardsOfFirstPlayer.Add(obj.name);
        }


        _random = new System.Random();

        _tolerances = new List<float>();

        for (int j = 0; j < 4; j++)
        {
            float tol = (float)_random.Next(1, 2) * _positionTolerance;
            _tolerances.Add(tol);
        }
        
        DeleteLastFourTalonCards();

        DealerName.text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
        
        string ttt = _lastCardOfDealerPlayer.Split('_')[1];
        //Debug.Log("value2:" + ttt);
        var value = LastCard.transform.GetChild(0);

        var components = value.GetComponents<Component>();
        foreach (var com in components)
        {
            //Debug.Log("komponente");
            var vv = com.GetType();
            if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
            {

                var image2 = (SVGImporter.SVGImage)com;
                image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/CARDS_" + ttt + "/" + _lastCardOfDealerPlayer);
            }
        }
        
        var CardOfTeamvalue = CardOfTeam.transform.GetChild(0);

        var componentsCardOfTeam = CardOfTeamvalue.GetComponents<Component>();
        foreach (var com in componentsCardOfTeam)
        {
            //Debug.Log("komponente");
            var vv = com.GetType();
            if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
            {

                var image2 = (SVGImporter.SVGImage)com;
                if (!SideOfTeam.ChangeSideOfCards)
                {

                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackBLUESide");
                }
                else
                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackREDSide");

            }
        }
        

        FB.API("/me/picture?type=square&height=200&width=200", HttpMethod.GET, DisplayProfilePic);
        InitTalonCards();
        InitBoard();
        TimeOfMove.active = false;
        isAviableToMove = false;
        

        photonView.RPC("SendCards", RpcTarget.Others, remaingCardArray, cardsOfSecondPlayer, _lastCardOfDealerPlayer);
        photonView.RPC("SendTalon", RpcTarget.Others, array);
    }
    [PunRPC]
    private void SetisArrangeCard()
    {
        isArrangeCard = false;
    }

    [PunRPC]
    public void RestartCardsRedSide()
    {
        _listOfZings = new List<string>();
        ListOfAllTakenCards = new List<string>();
    }

    [PunRPC]
    public void  SetOpositePlayerImage(byte[] value)
    {
        Texture2D tex = new Texture2D(200, 200);

        tex.LoadImage(value);
        // Assign texture to renderer's material.
        //GetComponent<Renderer>().material.mainTexture = tex;
        UnityEngine.UI.Image ProfilePic = OpositePlayerImage.GetComponent<UnityEngine.UI.Image>();
        ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 200, 200), new Vector2());
    }

    [PunRPC]
    public void SetPictureDealer(byte [] value,string NickName)
    {
        Texture2D tex = new Texture2D(200, 200);

        tex.LoadImage(value);
        // Assign texture to renderer's material.
        //GetComponent<Renderer>().material.mainTexture = tex;
        UnityEngine.UI.Image ProfilePic = DealerImage.GetComponent<UnityEngine.UI.Image>();
         ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 200, 200), new Vector2());

         DealerName.text = NickName;
    }

    

    private void InitTalonCards()
    {
        //float startPosition = 0.5f;
        float startPosition = 1000f;
        
        int i = 0;
        Vector3[] arrayPosition = new Vector3[4];
        string[] arrayCards =  {"","","","" };
        foreach (var obj in _zingDealer.TalonCards)
        {
            GameObject gameObj = (GameObject)obj;
            // Preparation to init is next
            // Check later to scale svg. Probably we will have better sharpness.
            //gameObj.transform.localScale = new Vector3(0.23f, 0.23f);
            //gameObj.transform.localPosition = new Vector3(startPosition * multiplier - 6.80f, 2.5f, -1.0f);
            //Vector3 position = new Vector3(startPosition, 40.5f);
            Vector3 position = new Vector3(startPosition, 1000f);
            gameObj.transform.localPosition = position;
            //GameObject firstDeck = (GameObject) Instantiate(gameObj, new Vector3(startPosition * multiplier - 5.00f, 2.5f, 0), Quaternion.identity);
            GameObject firstDeck = (GameObject)Instantiate(gameObj, new Vector3(startPosition, 1000f, 0), Quaternion.identity);
            //GameObject firstDeck = (GameObject)Instantiate(gameObj, new Vector3(0, 0, 0), Quaternion.identity);
            arrayPosition[i] = position;
            arrayCards[i] = ""+ gameObj.name;
            firstDeck.transform.SetParent(canvacesOfFirstDeck.transform);
            
            i++;
            startPosition += 100f;
            //multiplier -= 5f;

        }
        //photonView.RPC("SendInitialCopyCards", RpcTarget.Others,arrayPosition,arrayCards);
        
    }
    [PunRPC]
    public void SendInitialCopyCards(Vector3[] positions,string[] values)
    {
        
        for (int i =0;i < positions.Length; i++)
        {
            //Debug.Log("card:" + values[i]);
            //var card = canvacesOfFirstDeck.transform.Find($"{values[i]}(Clone)").gameObject;

            var prefabs = Resources.Load("Prefabs/CardPrefabsSvg/"+values[i]);

            GameObject gameObj = (GameObject) prefabs;

            //Debug.Log(positions[i].x);
            //Debug.Log(positions[i].y);
            gameObj.transform.position = positions[i];
            //Debug.Log(card.transform.position.x);
            //Debug.Log(card.transform.position.y);
            //card.transform.localScale = new Vector3(0.23f, 0.23f);
            GameObject firstDeck = (GameObject)Instantiate(gameObj, new Vector3(positions[i].x, positions[i].y, 0), Quaternion.identity);
            firstDeck.transform.SetParent(canvacesOfFirstDeck.transform);

            
        }
    }

    public void DeleteRemainingCards()
    {
        RemainingCardsList.RemoveRange(0, 8);  
    }

    public void DeleteLastFourTalonCards()
    {
        //Debug.Log("talonska karta:" + RemainingCardsList[RemainingCardsList.Count - 2]);
        //Debug.Log("talonska karta:" + RemainingCardsList[RemainingCardsList.Count - 3]);
        //Debug.Log("talonska karta:" + RemainingCardsList[RemainingCardsList.Count - 4]);
        //Debug.Log("talonska karta:" + RemainingCardsList[RemainingCardsList.Count - 5]);
        
        int start = RemainingCardsList.Count - 5;
        RemainingCardsList.RemoveRange(start, 4);

        
    }


    public List<string> GetFirstPlayerCards()
    {
        List<string> cards = new List<string>();
        cards.Add(RemainingCardsList[2]);
        cards.Add(RemainingCardsList[3]);

        cards.Add(RemainingCardsList[6]);
        cards.Add(RemainingCardsList[7]);
        
        
        return cards;
    }

    public List<string> GetSecondPlayerCards()
    {
        List<string> cards = new List<string>();
        cards.Add(RemainingCardsList[0]);
        cards.Add(RemainingCardsList[1]);

        cards.Add(RemainingCardsList[4]);
        cards.Add(RemainingCardsList[5]);

        return cards;
    }

    private void InvokeMethod()
    {
        isArrangeCard = true;
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
                //Debug.Log("ime objekta:" + val);

                var card = canvacesOfFirstDeck.transform.Find($"{val}(Clone)").gameObject;

                
                float x = -Time.deltaTime * (card.transform.position.x + (float) _tolerances.ToArray().GetValue(i)) * 0.25f;
                //Debug.Log("x:"+x+",card name:"+card.name);
                //float myX = System.Math.Abs(x);
                Vector3 position = new Vector3(x,
                    Time.deltaTime * (-card.transform.position.y + (float)_tolerances.ToArray().GetValue(i)) * 0.25f);
                                                                  
                card.transform.position += position;
                float x2 = -Time.deltaTime * (startPosition + (float)_tolerances.ToArray().GetValue(i)) * 0.25f;
                //Debug.Log("x:"+x+",card name:"+card.name);
                //float myX = System.Math.Abs(x);
                Vector3 position2 = new Vector3(x2,
                    Time.deltaTime * (-1000f + (float)_tolerances.ToArray().GetValue(i)) * 0.25f);

                //float multiplier = 1.15f;
                //Vector3 vv = new Vector3(startPosition, 1000f);
                
                arrayVectors[i] = position;
                startPosition += 100f;
                //Debug.Log("pozicija:" + arrayVectors[i].x);
                //Debug.Log("pozicija y:" + arrayVectors[i].y);
                //card.transform.localScale = new Vector3(0.23f, 0.23f);

                //var components = card.GetComponents<Component>();

                ////var image = gameObject.GetComponent<SVGImage>();
                //// Debug.Log("vvv");
                //foreach (var com in components)
                //{
                //    var vv = com.GetType();
                //    if (typeof(SVGImporter.SVGRenderer).IsAssignableFrom(vv))
                //    {
                //        var order = (SVGImporter.SVGRenderer)com;
                //        //SVGImporter.SVGRenderer
                //        order.sortingOrder = i;
                //    }
                //}

                card.transform.SetParent(canvacesOfFirstDeck.transform);
                i++;

            }
            
            photonView.RPC("InformCardPosition", RpcTarget.Others, arrayVectors,listTalon.ToArray());
            Invoke("InvokeMethod", 3f);
        }
        catch (Exception ex)
        {
            //Debug.Log("uhvatio gresku tolerancije:" + ex.Message);
            Invoke("InvokeMethod", 3f);
        }
    }
    
    public  Canvas GetFirstDeck()
    {
        return  canvacesOfFirstDeck;
    }

    public Canvas GetCurrentPlayerCanvas()
    {
        return canvacesOfCurrentPlayer;
    }

    [PunRPC]
    public void SendCards(string[] mixCards, string[] cardsSecondP, string lastC)
    {
        
        _cardsOfSecondPlayer = cardsSecondP.ToList<string>();
        TimeOfMove.active = true;
        isAviableToMove = true;
        _lastCardOfDealerPlayer = lastC;

        //GameObject tempCard = (GameObject)Resources.Load("Prefabs/CardPrefabs/" + _lastCardOfDealerPlayer);
        ////float x = (float)-137.7;
        ////float y = (float)-101.5;
        //float x = (float)-5.8;
        //float y = (float)-2.5;
        //tempCard.transform.position = new Vector3(x, y);

        //tempCard.transform.localScale = new Vector3(0.23f, 0.23f);
        ////gameObj.transform.localPosition = new Vector3(startPosition * multiplier - 6.80f, 2.5f, -1.0f);
        
        //GameObject LastDeck = (GameObject)Instantiate(tempCard, new Vector3(x, y, 0), Quaternion.identity);

        //LastDeck.transform.SetParent(LastCard.transform);
        
        float multiplier = 1.2f;
        
        foreach (var obj in _cardsOfSecondPlayer)
        {
            
            var tt = Resources.Load("Prefabs/PlayerCards/" + obj);

            GameObject FirstCardObject = (GameObject) tt;

            FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);

            Vector3 localPositionParentingCanvas =  canvacesOfCurrentPlayer.transform.position;
            
            float valueX = -3.6f;
            float valueY = -2.6f;

            GameObject myBrick = Instantiate(FirstCardObject, new Vector3(valueX+multiplier, valueY, 0), Quaternion.identity) as GameObject;
            myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
            multiplier += 1.75f;
            
        }

        string ttt = _lastCardOfDealerPlayer.Split('_')[1];
        //Debug.Log("value2:" + ttt);
        var value = LastCard.transform.GetChild(0);

        var components = value.GetComponents<Component>();
        foreach (var com in components)
        {
            //Debug.Log("komponente");
            var vv = com.GetType();
            if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
            {

                var image2 = (SVGImporter.SVGImage)com;
                image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/CARDS_" + ttt + "/" + _lastCardOfDealerPlayer);
            }
        }

        
        var CardOfTeamvalue = CardOfTeam.transform.GetChild(0);

        var componentsCardOfTeam = CardOfTeamvalue.GetComponents<Component>();
        foreach (var com in componentsCardOfTeam)
        {
            //Debug.Log("komponente");
            var vv = com.GetType();
            if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
            {

                var image2 = (SVGImporter.SVGImage)com;
                if (!SideOfTeam.ChangeSideOfCards)
                {

                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackBLUESide");
                }
                else
                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_Cards/BACK_SIDE/BackREDSide");

            }
        }

    }

    [PunRPC]
    public  void ChangeMoveDropedCard(string NameOfPrefab,Vector3 position,int valueOfOrder)
    {
       // Debug.Log("suprotna strana");
        var tt = Resources.Load("Prefabs/CardPrefabsSvg/" + NameOfPrefab);
        _listOfCards.Add(NameOfPrefab);
        var card = (GameObject) tt;
        
        card.transform.position += position;
        
        GameObject myBrick = Instantiate(card, new Vector3(position.x, position.y, 0), Quaternion.identity) as GameObject;


        myBrick.transform.SetParent(canvacesOfFirstDeck.transform);
        
        if (canvacesOfCurrentPlayer.transform.childCount > 0)
        {
            if (canvacesOfFirstDeck.transform.childCount == 1)
            {
                if (canvacesOfCurrentPlayer.transform.childCount > 0)
                {
                    player.TimeOfMove.active = true;
                    isAviableToMove = true;
                    DroppedCardsOneLeft dropCard = new DroppedCardsOneLeft(canvacesOfCurrentPlayer);
                    dropCard.SetCanvas();
                    
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
                    player.TimeOfMove.active = true;
                    isAviableToMove = true;
                    //photonView.RPC("TakeCardsZing", RpcTarget.Others, listArray);
                    RecordBoard._instance.photonView.RPC("TakeCardsZing", RpcTarget.Others, listArray);

                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionJDropped();

                    player.TimeOfMove.active = true;
                    isAviableToMove = true;
                    RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    //photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        player.TimeOfMove.active = true;
                        isAviableToMove = true;
                        dropCard.SetCanvas();
                       
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

                    player.TimeOfMove.active = true;
                    isAviableToMove = true;
                    // photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    //  Debug.Log("val:"+ ListOfTakenCards.Count);
                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                   
                    string[] listArray = dropCard.TakeActionJDropped();
                    player.TimeOfMove.active = true;
                    isAviableToMove = true;

                    //  photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        player.TimeOfMove.active = true;
                        isAviableToMove = true;
                        dropCard.SetCanvas();  
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
                    player.TimeOfMove.active = true;
                    isAviableToMove = true;
                    DroppedCardsOneLeft dropedCardOneLeft = new DroppedCardsOneLeft(canvacesOfCurrentPlayer);
                    dropedCardOneLeft.SetCanvas();
                  
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
                    player.TimeOfMove.active = true;
                    isAviableToMove = true;

                    //photonView.RPC("TakeCardsZing", RpcTarget.Others, listArray);
                    RecordBoard._instance.photonView.RPC("TakeCardsZing", RpcTarget.Others, listArray);

                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                    string[] listArray = dropCard.TakeActionJDropped();
                    player.TimeOfMove.active = true;
                    isAviableToMove = true;

                    //photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                }
                else
                {
                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        player.TimeOfMove.active = true;
                        isAviableToMove = true;
                        dropCard.SetCanvas();
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
                    player.TimeOfMove.active = true;
                    isAviableToMove = true;

                    // photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    //  Debug.Log("val:"+ ListOfTakenCards.Count);
                }
                else if (goName2.Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                   
                    string[] listArray = dropCard.TakeActionJDropped();
                    player.TimeOfMove.active = true;
                    isAviableToMove = true;

                    //photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                    RecordBoard._instance.photonView.RPC("TakeCardsFromTalon", RpcTarget.Others, listArray);
                }
                else
                {

                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        player.TimeOfMove.active = true;
                        isAviableToMove = true;
                        dropCard.SetCanvas();
                    }
                }
            }
            photonView.RPC("CheckPLayerCardsCount", RpcTarget.Others);
            
        }
    }
    

    [PunRPC]
    public void CheckPLayerCardsCount()
    {
        
       // Debug.Log("ukupno11:" + canvacesOfCurrentPlayer.transform.childCount);
        //if (PhotonNetwork.IsMasterClient)
        //{

           // Debug.Log("pozivam 1");
            if (canvacesOfCurrentPlayer.transform.childCount == 0)
            {
                
                DeleteRemainingCards();
                //Debug.Log("ukupno karata proslo:"+ _listOfCards.Count);
                if(_listOfCards.Count < 51)
                {
                  //  Debug.Log("proslo karata:"+_listOfCards.Count);
                
                _cardsOfFirstPlayer = new List<string>();
                _cardsOfFirstPlayer = GetFirstPlayerCards();
                
                float multiplier = 1.2f;
              //  Debug.Log("ukupno elemenata:" + canvacesOfCurrentPlayer.transform.childCount);

                foreach(string obj in _cardsOfFirstPlayer)
                {
                    var tt = Resources.Load("Prefabs/PlayerCards/" + obj);

                    GameObject FirstCardObject = (GameObject) tt;

                    FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);

                    Vector3 localPositionParentingCanvas = canvacesOfCurrentPlayer.transform.position;
                        
                    float value = -3.6f;
                    float valueY = -2.6f;
                        
                    GameObject myBrick = Instantiate(FirstCardObject, new Vector3(value + multiplier, valueY, 0), Quaternion.identity) as GameObject;
                    myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
                    multiplier += 1.75f;
                }

                    if (canvacesOfCurrentPlayer.transform.childCount > 0)
                    {
                        player.TimeOfMove.active = false;
                        isAviableToMove = false;
                        foreach (Transform element in canvacesOfCurrentPlayer.transform)
                        {

                            var firstCard = element.Find("FirstCardSelected").gameObject;
                            firstCard.active = false;

                        }

                    }

                _cardsOfSecondPlayer = new List<string>();

                string[] cardsOfSecondPl = new string[_zingDealer.CardsOfSecondPlayers.Count];

                _cardsOfSecondPlayer = GetSecondPlayerCards();
            
                cardsOfSecondPl = _cardsOfSecondPlayer.ToArray();
            
            
                photonView.RPC("CleanDeskOnAnotherPlayer", RpcTarget.Others, cardsOfSecondPl);
                }else
                {
                //ovdje ce se raditi finalno azuriranje rezultata jer je igra zavrsena tj ovaj krug
                
                    List<string> list = new List<string>();
                    foreach (Transform transform in canvacesOfFirstDeck.transform)
                    {

                        GameObject tempGameObject = transform.gameObject;
                        string name = tempGameObject.name;
                        var index = name.IndexOf("(");
                        string CardName = name.Substring(0, index);
                        list.Add(CardName);

                        Destroy(transform.gameObject);
                    }
                
                //ovo dodavanje treba provjeriti jer na finalnom obracunu ne radi dobro kao i sledecem novom krugu
                //pocetka partija pravi problem jer sledece nosenje karata ne prikazuje dobro podatke
                //na polje cards
                    
               
                //ComparasionOfDate compDate = new ComparasionOfDate(_dateAndTimeOfTakenCards, list.ToArray(), PlayerId);
                //compDate.RunExecution();
                //Debug.Log("zadnji je plavi ponio");
                RecordBoard._instance.photonView.RPC("CovertDateNowRed", RpcTarget.Others, RecordBoard._instance.GetDateAndTimeOfTakenCards().ToString(), list.ToArray());
                    //photonView.RPC("CovertDateNowRed", RpcTarget.Others, _dateAndTimeOfTakenCards.ToString(), list.ToArray());
                    
                    
                }
           // }
        }
       
    }
    
   [PunRPC]
    public void CleanDeskOnAnotherPlayer(string[] cardsOfSecondPl)
    {

        //_zingDealer.DeleteDealerCard();
       
        _cardsOfSecondPlayer = cardsOfSecondPl.ToList<string>();
        TimeOfMove.active = true;
        isAviableToMove = true;
        

        float multiplier = 1.2f;

        foreach (var obj in _cardsOfSecondPlayer)
        {

            var tt = Resources.Load("Prefabs/PlayerCards/" + obj);

            GameObject FirstCardObject = (GameObject)tt;

            FirstCardObject.transform.localScale = new Vector3(0.23f, 0.23f);

            Vector3 localPositionParentingCanvas = canvacesOfCurrentPlayer.transform.position;

            float value = -3.6f;
            float valueY = -2.6f;

            GameObject myBrick = Instantiate(FirstCardObject, new Vector3(value + multiplier, valueY, 0), Quaternion.identity) as GameObject;
            myBrick.transform.SetParent(canvacesOfCurrentPlayer.transform);
            multiplier += 1.75f;

        }

     //   Debug.Log("ukupno 2:" + canvacesOfCurrentPlayer.transform.childCount);

    }

    [PunRPC]
    public void InformCardPosition(Vector3[] newPosition,string[] arrayTalon)
    {

        
        int i = 0;
        if(listTalon == null)
        {
            listTalon = arrayTalon.ToList();
        }

       
        if (canvacesOfFirstDeck.transform.childCount == 0)
        {
            SendTalon(arrayTalon);
        }

        if(canvacesOfCurrentPlayer.transform.childCount == 0)
        {
            
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
    public void SendTalon(string[] talonList)
    {
        listTalon = talonList.ToList<string>();
        //float startPosition = 0.5f;
        //float multiplier = 1.15f;
        float startPosition = 1000f;
        
        
        //talonCards = new List<GameObject>();
        foreach (var obj in listTalon)
        {
            var prefab = Resources.Load("Prefabs/CardPrefabsSvg/" + obj);

            var go = prefab as GameObject;
            if (go.name == obj)
            {
                GameObject gameObj = (GameObject) go;
                Vector3 position = new Vector3(startPosition, 1000f);
                gameObj.transform.localPosition = position;
                GameObject firstDeck = (GameObject) Instantiate(gameObj, new Vector3(startPosition, 1000f, 0), Quaternion.identity);
                
                firstDeck.transform.SetParent(canvacesOfFirstDeck.transform);
                startPosition += 100f;
                _listOfCards.Add(obj);
                //multiplier -= 5f;

            }
        }
       
        
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
