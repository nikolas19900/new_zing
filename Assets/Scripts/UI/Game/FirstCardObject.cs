using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Windows.Input;
using UnityEngine.EventSystems;


public  class FirstCardObject : MonoBehaviour
{
    [SerializeField]
    private GameObject FirstCardSelected;

    private GameObject _currentCard;
    private Vector2 _endPoint;
    private float _landingToleranceRadius;
    private System.Random _random;
   
    Canvas SizeOfCanvas;
    public static int countClick = 1;

    
   
    // Start is called before the first frame update
    void Start()
    {
        //start isto
        //Debug.Log("ime:");
        _endPoint = Vector2.zero;
        _random = new System.Random();
        _landingToleranceRadius = 1.3f;
        _currentCard = null;
        SizeOfCanvas = GameScript.player.GetFirstDeck();

      //  Debug.Log("ime:" + value.name);

        // Canvas.FindObjectOfType<GameObject>();

        // Debug.Log("velicina:" + GameObject.FindGameObjectsWithTag(value.name).Length);

        // Debug.Log("velicina:" + BeginningOfGame.enemy.SizeOfFirstDeck());
      //  Debug.Log("velicina:" + value.transform.childCount);
        countClick = SizeOfCanvas.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

        //SizeOfCanvas = GameScript.player.GetFirstDeck();
        //countClick = SizeOfCanvas.transform.childCount;
        
        
    }


  
    
    //private void OnMouseExit()
    //{
    //    //Debug.Log("Mouse is exit from GameObject.");
    //    //gameObject.active = false;
    //    var components = gameObject.GetComponents<Component>();

    //    //var image = gameObject.GetComponent<SVGImage>();

    //    foreach (var com in components)
    //    {
    //        var vv = com.GetType();
    //        if (typeof(SVGImporter.SVGRenderer).IsAssignableFrom(vv))
    //        {
    //            var image2 = (SVGImporter.SVGRenderer)com;
    //            image2.vectorGraphics = null;
    //            //Debug.Log("exit");
    //        }
    //    }

    //}

    public void MouseExitEvent()
    {
        FirstCardSelected.SetActive(false);

        var components = FirstCardSelected.GetComponents<Component>();

        
        foreach (var com in components)
        {
            var vv = com.GetType();
            if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
            {
                var image2 = (SVGImporter.SVGImage)com;
                image2.vectorGraphics = null;
                
            }
        }
    }

    //private void OnMouseUp()
    //{
    //    if (BeginningOfGame.isAviableToMove == false)
    //        return;
    //    //Debug.Log("click:"+countClick);

    //     //Debug.Log("ime objekta:" + transform.parent.name);
    //    float startPosition = 1.5f;
    //    float multiplier = 1.15f;
        
    //    string CardNameClone = transform.parent.name;
    //    var cc = transform.parent.gameObject;
    //   // Debug.Log("cc:" + cc);

    //    var index = CardNameClone.IndexOf("(");
    //    string CardName = CardNameClone.Substring(0, index);
    //    var tt = Resources.Load("Prefabs/CardPrefabsSvg/" + CardName);
        
    //    _currentCard = (GameObject) tt;

    //    //za current card objekat je neophodno setovati svgrender order na 1
    //    //_currentCard.transform.localScale = new Vector3(0.23f, 0.23f);

    //    _random = new System.Random();
    //    var valueX = 400 * (1 - (-0.6)) + (-0.6);
    //    //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
    //    float toleranceX = 2.3f;
    //    float x = (float)( valueX + _random.Next(-20,20) * toleranceX);
    //    var value = 340 * (1.5 - 0.6) + 0.6;
    //    float y = (float)(_endPoint.y + _random.Next(100,150) * _landingToleranceRadius + value);
        
        
    //    _currentCard.transform.position = new Vector3(x, y);
       
    //    Vector3 positionOfCurrentCard = new Vector3(x, y);
    //    //GameObject myBrick = PhotonNetwork.Instantiate("Prefabs/CardPrefabsSvg/" + CardName, _currentCard.transform.position, Quaternion.identity);
        
    //    GameObject myBrick = (GameObject) Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity) ;
        
    //    //var components = myBrick.GetComponents<Component>();

    //    ////var image = gameObject.GetComponent<SVGImage>();
    //    //// Debug.Log("vvv");
    //    //foreach (var com in components)
    //    //{
    //    //    var vv = com.GetType();
    //    //    if (typeof(SVGImporter.SVGRenderer).IsAssignableFrom(vv))
    //    //    {
    //    //        var order = (SVGImporter.SVGRenderer) com;
    //    //        //SVGImporter.SVGRenderer
    //    //        order.sortingOrder = countClick;
    //    //    }
    //    //}
    //    var tv = (Canvas) BeginningOfGame.player.GetCurrentPlayerCanvas();

    //    // Debug.Log("first card object:" + tv.transform.childCount);
        
    //    myBrick.transform.SetParent(SizeOfCanvas.transform);

    //    cc.active = false;
    //    Destroy(transform.parent.gameObject);
        
    //    // player._listOfCards.Add(NameOfPrefab);
    //    var list = BeginningOfGame.player.GetOfListOfCards();
    //    list.Add(CardName);
        
        
    //        // Debug.Log("prosla karta:" + list.Count);
    //    BeginningOfGame.player.SetListOfCards(list);
    //    BeginningOfGame.player.photonView.RPC("ChangeMoveDropedCard", RpcTarget.Others, CardName, positionOfCurrentCard, countClick);
    //    TimeOfMoveObject.DeactiveGameObject();
       
    //    countClick++;
        
    //    //Debug.Log("protivnicka strana" + tv.transform.childCount);
        
    //    foreach(Transform element in tv.transform)
    //    {
    //        // Debug.Log(i++);
            
    //        var firstCard = element.Find("FirstCardSelected").gameObject;
    //        firstCard.active = false;
            
    //    }

    //}

    public void MouseClickEvent()
    {
        
        if (BeginningOfGame.isAviableToMove == false)
            return;
        //Debug.Log("click:"+countClick);

        //Debug.Log("ime objekta:" + transform.parent.name);
        float startPosition = 1.5f;
        float multiplier = 1.15f;

        string CardNameClone = transform.parent.name;
        var cc = transform.parent.gameObject;
        // Debug.Log("cc:" + cc);

        var index = CardNameClone.IndexOf("(");
        string CardName = CardNameClone.Substring(0, index);
        var tt = Resources.Load("Prefabs/CardPrefabsSvg/" + CardName);

        _currentCard = (GameObject) tt;

        //za current card objekat je neophodno setovati svgrender order na 1
        //_currentCard.transform.localScale = new Vector3(0.23f, 0.23f);

        _random = new System.Random();
        var valueX = _random.NextDouble() * (1 - (-0.6)) + (-0.6);
        float x = (float)(_endPoint.x + valueX * _landingToleranceRadius);
        var value = _random.NextDouble() * (1.5 - 0.6) + 0.6;
        float y = (float)(_endPoint.y + _random.Next(1, 2) * _landingToleranceRadius + value);

        _currentCard.transform.position = new Vector3(x, y);
        Vector3 positionOfCurrentCard = new Vector3(x, y);
        //GameObject myBrick = PhotonNetwork.Instantiate("Prefabs/CardPrefabs/" + CardName, _currentCard.transform.position, Quaternion.identity);

        GameObject myBrick = Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity) as GameObject;

        //var components = myBrick.GetComponents<Component>();

        ////var image = gameObject.GetComponent<SVGImage>();
        //// Debug.Log("vvv");
        //foreach (var com in components)
        //{
        //    var vv = com.GetType();
        //    if (typeof(SVGImporter.SVGRenderer).IsAssignableFrom(vv))
        //    {
        //        var order = (SVGImporter.SVGRenderer)com;
        //        //SVGImporter.SVGRenderer
        //        order.sortingOrder = countClick;
        //    }
        //}
        var tv = (Canvas)BeginningOfGame.player.GetCurrentPlayerCanvas();

        // Debug.Log("first card object:" + tv.transform.childCount);

        myBrick.transform.SetParent(SizeOfCanvas.transform);
        cc.active = false;
        Destroy(transform.parent.gameObject);

        // player._listOfCards.Add(NameOfPrefab);
        var list = BeginningOfGame.player.GetOfListOfCards();
        list.Add(CardName);


        // Debug.Log("prosla karta:" + list.Count);
        BeginningOfGame.player.SetListOfCards(list);
        BeginningOfGame.player.photonView.RPC("ChangeMoveDropedCard", RpcTarget.Others, CardName, positionOfCurrentCard, countClick);
        TimeOfMoveObject.DeactiveGameObject();

        countClick++;

        //Debug.Log("protivnicka strana" + tv.transform.childCount);

        foreach (Transform element in tv.transform)
        {
            
            var firstCard = element.Find("FirstCardSelected").gameObject;
            firstCard.active = false;

        }


    }  

    public void  MouseOverEvent()
    {
        FirstCardSelected.SetActive(true);

        if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
        {
            var components = FirstCardSelected.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();
            // Debug.Log("fff");
            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                {
                    var image2 = (SVGImporter.SVGImage)com;
                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_red_2");
                }
            }
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
        {
            var components = FirstCardSelected.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();
            // Debug.Log("vvv");
            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                {
                    var image2 = (SVGImporter.SVGImage)com;
                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_blue_2");
                }
            }
        }


        //if (BeginningOfGame.isAviableToMove == false)
        //    return;
        //gameObject.active = true;
        //var PlayerName = PhotonNetwork.LocalPlayer.UserId;

        //string playerPhoton = PhotonNetwork.CurrentRoom.GetPlayer(1).UserId;


        //if (PlayerName == playerPhoton)
        //{

        //    //Color color;
        //    //ColorUtility.TryParseHtmlString("#0a2c6a", out color);
        //    var components = gameObject.GetComponents<Component>();

        //    //var image = gameObject.GetComponent<SVGImage>();
        //    // Debug.Log("vvv");
        //    foreach (var com in components)
        //    {
        //        var vv = com.GetType();
        //        if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
        //        {
        //            var image2 = (SVGImporter.SVGImage)com;
        //            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_blue_2");
        //        }
        //    }
        //    //Debug.Log("prvi");
        //}
        //else
        //{
        //    //Color color1;
        //    // ColorUtility.TryParseHtmlString("#b5252a", out color1);
        //    var components = gameObject.GetComponents<Component>();

        //    //var image = gameObject.GetComponent<SVGImage>();
        //    // Debug.Log("fff");
        //    foreach (var com in components)
        //    {
        //        var vv = com.GetType();
        //        if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
        //        {
        //            var image2 = (SVGImporter.SVGImage)com;
        //            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_red_2");
        //        }
        //    }
        //    //var image2 =(SVGImporter.SVGImage) component[2];
        //    //image.color = color1;
        //    //image2.color = color1;

        //    //image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_red");
        //    //Debug.Log("drugi");
        //}
    }

    //private void OnMouseOver()
    //{
    //    //if (BeginningOfGame.isAviableToMove == false)
    //    //    return;
    //    //gameObject.active = true;
    //    //var PlayerName = PhotonNetwork.LocalPlayer.UserId;

    //    //string playerPhoton = PhotonNetwork.CurrentRoom.GetPlayer(1).UserId;

    //    if(PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Red"))
    //    {
    //        var components = gameObject.GetComponents<Component>();

    //        //var image = gameObject.GetComponent<SVGImage>();
    //        // Debug.Log("fff");
    //        foreach (var com in components)
    //        {
    //            var vv = com.GetType();
    //            if (typeof(SVGImporter.SVGRenderer).IsAssignableFrom(vv))
    //            {
    //                var image2 = (SVGImporter.SVGRenderer)com;
    //                image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_red_2");
    //            }
    //        }
    //    }
    //    if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
    //    {
    //        var components = gameObject.GetComponents<Component>();

    //        //var image = gameObject.GetComponent<SVGImage>();
    //        // Debug.Log("vvv");
    //        foreach (var com in components)
    //        {
    //            var vv = com.GetType();
    //            if (typeof(SVGImporter.SVGRenderer).IsAssignableFrom(vv))
    //            {
    //                var image2 = (SVGImporter.SVGRenderer)com;
    //                image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_blue_2");
    //            }
    //        }
    //    }
    //    //if (PlayerName == playerPhoton)
    //    //{

    //    //    //Color color;
    //    //    //ColorUtility.TryParseHtmlString("#0a2c6a", out color);
    //    //    var components = gameObject.GetComponents<Component>();

    //    //    //var image = gameObject.GetComponent<SVGImage>();
    //    //    // Debug.Log("vvv");
    //    //    foreach (var com in components)
    //    //    {
    //    //        var vv = com.GetType();
    //    //        if (typeof(SVGImporter.SVGRenderer).IsAssignableFrom(vv))
    //    //        {
    //    //            var image2 = (SVGImporter.SVGRenderer)com;
    //    //            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_blue_2");
    //    //        }
    //    //    }
    //    //    //Debug.Log("prvi");
    //    //}
    //    //else
    //    //{
    //    //    if (BeginningOfGame.isAviableToMove == false)
    //    //        return;
    //    //    //Color color1;
    //    //    // ColorUtility.TryParseHtmlString("#b5252a", out color1);
    //    //    var components = gameObject.GetComponents<Component>();

    //    //    //var image = gameObject.GetComponent<SVGImage>();
    //    //    // Debug.Log("fff");
    //    //    foreach (var com in components)
    //    //    {
    //    //        var vv = com.GetType();
    //    //        if (typeof(SVGImporter.SVGRenderer).IsAssignableFrom(vv))
    //    //        {
    //    //            var image2 = (SVGImporter.SVGRenderer)com;
    //    //            image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_red_2");
    //    //        }
    //    //    }
    //    //    //var image2 =(SVGImporter.SVGImage) component[2];
    //    //    //image.color = color1;
    //    //    //image2.color = color1;

    //    //    //image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/box_card_pl_red");
    //    //    //Debug.Log("drugi");
    //    //}
    //}

   
}
