using Assets.Scripts.Infastructure.JSON;
using Assets.Scripts.Infastructure.PARSER;
using Assets.Scripts.Managers;
using Assets.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Facebook.Unity;
using System.Collections.Generic;

public class StartingPoint : MonoBehaviour
{

    [SerializeField]
    private Text TutorText;

    private TutorialRoot root;

    public static int currentCursor = 0;

    [SerializeField]
    private GameObject _firstCard;

    [SerializeField]
    private GameObject _secondCard;

    [SerializeField]
    private GameObject _thirdCard;

    [SerializeField]
    private GameObject _fourthCard;

    [SerializeField]
    private Canvas _firstDeck;

    [SerializeField]
    private Text PointsValueRedText;

    [SerializeField]
    private Text CardValueRedText;

    [SerializeField]
    private Text TotalPointValueRedText;

    [SerializeField]
    private Text ZingValueRedText;

    [SerializeField]
    private Canvas FirstDeck;

    [SerializeField]
    private Image Next;

    [SerializeField]
    private GameObject DealerImage;

    private List<string> perms;

    [SerializeField]
    private GameObject _Leave;

    [SerializeField]
    private Text ImePrezimeDealer;
    [SerializeField]
    private Text RedNameText;


    private int points = 0;
    private int cards = 0;
    private int zing = 10;
    private int totalPoints = 0;
    
    
    private bool _isSecondScenario=false;

    private float _landingToleranceRadius;
    private Vector2 _endPoint;
    private System.Random _random;

    

    void DisplayCurrentPlayerPic(IGraphResult result)
    {
        if (result.Texture != null)
        {

            Texture2D tempTex = result.Texture;
            byte[] value = tempTex.EncodeToPNG();
            Texture2D tex = new Texture2D(64, 64);

            tex.LoadImage(value);
            // Assign texture to renderer's material.
            //GetComponent<Renderer>().material.mainTexture = tex;
            Image ProfilePic = DealerImage.GetComponent<Image>();
            ProfilePic.sprite = Sprite.Create(tex, new Rect(0, 0, 64, 64), new Vector2());
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

        ImePrezimeDealer.text = playerName;
        RedNameText.text = playerName;
    }

        // Start is called before the first frame update
        void Start()
        {

        FB.ActivateApp();
        //
        FB.API("/me/picture?type=square&height=64&width=64", HttpMethod.GET, DisplayCurrentPlayerPic);

        FB.API("me?fields=id,name", HttpMethod.POST, OnDataReceived, new WWWForm());

        ParseJson json = new ParseJson();
        root = json.DeserializeTutorial();

        
        if (MasterManager.GameSettings.DefaultLanguage == null)
        {
            
            InicijalnaLokalizacija il = new InicijalnaLokalizacija();
            root = il.setLanguageTutorial();
        }
        if (MasterManager.GameSettings.DefaultLanguage == "English")
        {
            TutorText.text = root._1[0].english;
            var component = _firstCard.GetComponent<EventTrigger>();

            component.enabled = false;

            var componentSecond = _secondCard.GetComponent<EventTrigger>();

            componentSecond.enabled = false;

            var componentThird = _thirdCard.GetComponent<EventTrigger>();

            componentThird.enabled = false;

            var componentFourth = _fourthCard.GetComponent<EventTrigger>();

            componentFourth.enabled = false;
        }
        if(MasterManager.GameSettings.DefaultLanguage == "Spanish")
        {
            TutorText.text = root._1[1].spanish;
        }
        
        if (MasterManager.GameSettings.DefaultLanguage == "Portugales"){

            TutorText.text = root._1[2].portuguese;
        }
        if (MasterManager.GameSettings.DefaultLanguage == "Russian")
        {
            TutorText.text = root._1[3].russian;
        }
            
    }

    // Update is called once per frame
    void Update()
    {

        
        int total = _firstDeck.transform.childCount;
        if(total > 0) { 
        if (_firstDeck.transform.GetChild(total - 1).gameObject.name.Contains("J_"))
        {
            if(currentCursor == 3) { 
                _isSecondScenario = true;
            }
                var obj = _firstDeck.transform.GetChild(total - 1).gameObject;
                //Debug.Log("uzeo sam:" + obj);
                points = 4;
                cards += total;
                totalPoints += points;
                foreach (Transform element in _firstDeck.transform)
                {
                    // Debug.Log(i++);
                    Destroy(element.gameObject);
                    if(_firstCard != null)
                    {
                        var component = _firstCard.GetComponent<EventTrigger>();

                        component.enabled = false;
                    }
                    
                    if(_secondCard != null)
                    {
                        var componentSecond = _secondCard.GetComponent<EventTrigger>();

                        componentSecond.enabled = false;
                    }
                    
                    if(_thirdCard != null)
                    {
                        var componentThird = _thirdCard.GetComponent<EventTrigger>();

                        componentThird.enabled = false;
                    }
                    
                    if(_fourthCard != null)
                    {
                        var componentFourth = _fourthCard.GetComponent<EventTrigger>();
                        componentFourth.enabled = false;
                    }
                    

                }
                PointsValueRedText.text = ""+points;
                CardValueRedText.text = "" + cards;
                TotalPointValueRedText.text = "" + totalPoints;
            }
        var lastObject = _firstDeck.transform.GetChild(total - 1).gameObject;
            if(total > 1) { 
                var previousObject = _firstDeck.transform.GetChild(total - 2).gameObject;
            
                string CardNameClone = lastObject.transform.name;
        
        
                var index = CardNameClone.IndexOf("_");
                string CardName = CardNameClone.Substring(0, index);

                string CardNameClonePreviuos = previousObject.transform.name;


                var indexPreviuos = CardNameClonePreviuos.IndexOf("_");
                string CardNamePreviuos = CardNameClonePreviuos.Substring(0, index);
                if (CardName.Equals(CardNamePreviuos))
                {
                    if (total == 2)
                    {
                        var indexLast = lastObject.name.IndexOf("(");
                        string CardNameLast = lastObject.name.Substring(0, indexLast);
                        var indexPreviousTemp = previousObject.name.IndexOf("(");
                        string CardNamePrevious = previousObject.name.Substring(0,indexPreviousTemp);
                        if (CardNameLast.Equals(CardNamePreviuos))
                        {

                        }else
                        {
                            ZingValueRedText.text = "" + zing;
                            totalPoints += zing;
                            TotalPointValueRedText.text = "" + totalPoints;
                        }
                        //ovo ce oznaciti da je napravio zing
                        
                    }
                    else
                    {


                        points = 3;
                        cards += total;
                        totalPoints += points;
                        PointsValueRedText.text = "" + points;
                        CardValueRedText.text = "" + cards;
                        TotalPointValueRedText.text = "" + totalPoints;

                        //Debug.Log("iste su karte:"+CardName+","+CardNamePreviuos);
                        
                    }
                    foreach (Transform element in _firstDeck.transform)
                    {
                        // Debug.Log(i++);
                        Destroy(element.gameObject);
                        if (_firstCard != null)
                        {
                            var component = _firstCard.GetComponent<EventTrigger>();

                            component.enabled = false;
                        }

                        if (_secondCard != null)
                        {
                            var componentSecond = _secondCard.GetComponent<EventTrigger>();

                            componentSecond.enabled = false;
                        }

                        if (_thirdCard != null)
                        {
                            var componentThird = _thirdCard.GetComponent<EventTrigger>();

                            componentThird.enabled = false;
                        }

                        if (_fourthCard != null)
                        {
                            var componentFourth = _fourthCard.GetComponent<EventTrigger>();
                            componentFourth.enabled = false;
                        }


                    }
                }

            }
        }
    }

    public void EnterMouse()
    {
        

        Image components = Next.GetComponent<Image>();

        components.sprite = Resources.Load<Sprite>("tutorial/NextButton01Over");
    }
    
    public void OnExitMouse()
    {
        Image components = Next.GetComponent<Image>();

        components.sprite = Resources.Load<Sprite>("tutorial/NextButton01");
    } 


    public void ClickNextMove()
    {
       
        if(currentCursor == 0)
        {
            FirstInterationTutor fit = new FirstInterationTutor();
            TutorText.text = ""+fit.GetTextTutor(root);
            currentCursor++;
        }
        else if(currentCursor == 1)
        {
            InterationTutor2 it = new InterationTutor2();
            TutorText.text = "" + it.GetTextTutor(root);
            currentCursor++;
        }
        else if(currentCursor == 2)
        {
            InterationTutor3 it = new InterationTutor3();
            TutorText.text = "" + it.GetTextTutor(root);
            

            var component = _firstCard.GetComponent<EventTrigger>();

            component.enabled = true;

            var componentSecond = _secondCard.GetComponent<EventTrigger>();

            componentSecond.enabled = false;

            var componentThird = _thirdCard.GetComponent<EventTrigger>();

            componentThird.enabled = false;

            var componentFourth = _fourthCard.GetComponent<EventTrigger>();

            componentFourth.enabled = true;
        }
        else if (currentCursor == 3 && _isSecondScenario == false)
        {
            InterationTutor4 it = new InterationTutor4();
            TutorText.text = "" + it.GetTextTutor(root);
            var tt = Resources.Load("Prefabs/TutorialSvg/8_CLUB");
            var valueCount = _firstDeck.transform.childCount;
            if (valueCount > 0)
            {
                var lastObject = _firstDeck.transform.GetChild(valueCount - 1).gameObject;
                var indexLast = lastObject.name.IndexOf("(");
                string CardNameLast = lastObject.name.Substring(0, indexLast);

                GameObject _currentCard = (GameObject) tt;

                if (_currentCard.name.Equals(CardNameLast) )
                {

                }
                else
                {

                    _endPoint = Vector2.zero;
                    _landingToleranceRadius = 1.3f;
                    _random = new System.Random();
                    var valueX = 350 * (1 - (-0.6)) + (-0.6);
                    //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                    float toleranceX = 2.3f;
                    float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                    var value = 200 * (1.5 - 0.6) + 0.6;
                    float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                    _currentCard.transform.position = new Vector3(x, y);
                    _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                    Vector3 positionOfCurrentCard = new Vector3(x, y);

                    GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                    myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                    myBrick.transform.SetParent(FirstDeck.transform);



                    if (_firstCard != null)
                    {
                        var component = _firstCard.GetComponent<EventTrigger>();

                        component.enabled = false;
                    }
                    if (_secondCard != null)
                    {
                        var componentSecond = _secondCard.GetComponent<EventTrigger>();

                        componentSecond.enabled = false;
                    }
                    if (_thirdCard != null)
                    {
                        var componentThird = _thirdCard.GetComponent<EventTrigger>();

                        componentThird.enabled = true;
                    }
                    if (_fourthCard != null)
                    {
                        var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                        componentFourth.enabled = false;
                    }
                }
            }else
            {
                GameObject _currentCard = (GameObject)tt;

                _endPoint = Vector2.zero;
                _landingToleranceRadius = 1.3f;
                _random = new System.Random();
                var valueX = 350 * (1 - (-0.6)) + (-0.6);
                //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                var value = 200 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                _currentCard.transform.position = new Vector3(x, y);
                _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                Vector3 positionOfCurrentCard = new Vector3(x, y);

                GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(FirstDeck.transform);



                if (_firstCard != null)
                {
                    var component = _firstCard.GetComponent<EventTrigger>();

                    component.enabled = false;
                }
                if (_secondCard != null)
                {
                    var componentSecond = _secondCard.GetComponent<EventTrigger>();

                    componentSecond.enabled = false;
                }
                if (_thirdCard != null)
                {
                    var componentThird = _thirdCard.GetComponent<EventTrigger>();

                    componentThird.enabled = true;
                }
                if (_fourthCard != null)
                {
                    var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                    componentFourth.enabled = false;
                }
            }
        }
        else if (currentCursor == 4 && _isSecondScenario == false)
        {
            InterationTutor5 it = new InterationTutor5();
            TutorText.text = "" + it.GetTextTutor(root);
            var valueCount = _firstDeck.transform.childCount;
            var tt = Resources.Load("Prefabs/TutorialSvg/9_HEART");

            if (valueCount > 0)
            {
                var lastObject = _firstDeck.transform.GetChild(valueCount - 1).gameObject;
                var indexLast = lastObject.name.IndexOf("(");
                string CardNameLast = lastObject.name.Substring(0, indexLast);

                GameObject _currentCard = (GameObject)tt;

                if (_currentCard.name.Equals(CardNameLast))
                {

                }
                else
                {

                    _endPoint = Vector2.zero;
                    _landingToleranceRadius = 1.3f;
                    _random = new System.Random();
                    var valueX = 350 * (1 - (-0.6)) + (-0.6);
                    //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                    float toleranceX = 2.3f;
                    float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                    var value = 200 * (1.5 - 0.6) + 0.6;
                    float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                    _currentCard.transform.position = new Vector3(x, y);
                    _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                    Vector3 positionOfCurrentCard = new Vector3(x, y);

                    GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                    myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                    myBrick.transform.SetParent(FirstDeck.transform);



                    if (_firstCard != null)
                    {
                        var component = _firstCard.GetComponent<EventTrigger>();

                        component.enabled = false;
                    }
                    if (_secondCard != null)
                    {
                        var componentSecond = _secondCard.GetComponent<EventTrigger>();

                        componentSecond.enabled = false;
                    }
                    if (_thirdCard != null)
                    {
                        var componentThird = _thirdCard.GetComponent<EventTrigger>();

                        componentThird.enabled = false;
                    }
                    if (_fourthCard != null)
                    {
                        var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                        componentFourth.enabled = true;
                    }
                }
            }
            else
            {
                GameObject _currentCard = (GameObject)tt;

                _endPoint = Vector2.zero;
                _landingToleranceRadius = 1.3f;
                _random = new System.Random();
                var valueX = 350 * (1 - (-0.6)) + (-0.6);
                //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                var value = 200 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                _currentCard.transform.position = new Vector3(x, y);
                _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                Vector3 positionOfCurrentCard = new Vector3(x, y);

                GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(FirstDeck.transform);



                if (_firstCard != null)
                {
                    var component = _firstCard.GetComponent<EventTrigger>();

                    component.enabled = false;
                }
                if (_secondCard != null)
                {
                    var componentSecond = _secondCard.GetComponent<EventTrigger>();

                    componentSecond.enabled = false;
                }
                if (_thirdCard != null)
                {
                    var componentThird = _thirdCard.GetComponent<EventTrigger>();

                    componentThird.enabled = false;
                }
                if (_fourthCard != null)
                {
                    var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                    componentFourth.enabled = true;
                }
            }

        }
        else if (currentCursor == 5 && _isSecondScenario == false)
        {
            InterationTutor6 it = new InterationTutor6();
            TutorText.text = "" + it.GetTextTutor(root);

        }
        else if (currentCursor == 3 && _isSecondScenario == true)
        {
            InterationTutor7 it = new InterationTutor7();
            TutorText.text = "" + it.GetTextTutor(root);
            var valueCount = _firstDeck.transform.childCount;
            var tt = Resources.Load("Prefabs/TutorialSvg/8_CLUB");
            if (valueCount > 0)
            {
                var lastObject = _firstDeck.transform.GetChild(valueCount - 1).gameObject;
                var indexLast = lastObject.name.IndexOf("(");
                string CardNameLast = lastObject.name.Substring(0, indexLast);

                GameObject _currentCard = (GameObject)tt;

                if (_currentCard.name.Equals(CardNameLast))
                {

                }
                else
                {

                    _endPoint = Vector2.zero;
                    _landingToleranceRadius = 1.3f;
                    _random = new System.Random();
                    var valueX = 350 * (1 - (-0.6)) + (-0.6);
                    //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                    float toleranceX = 2.3f;
                    float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                    var value = 200 * (1.5 - 0.6) + 0.6;
                    float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                    _currentCard.transform.position = new Vector3(x, y);
                    _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                    Vector3 positionOfCurrentCard = new Vector3(x, y);

                    GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                    myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                    myBrick.transform.SetParent(FirstDeck.transform);



                    if (_firstCard != null)
                    {
                        var component = _firstCard.GetComponent<EventTrigger>();

                        component.enabled = false;
                    }
                    if (_secondCard != null)
                    {
                        var componentSecond = _secondCard.GetComponent<EventTrigger>();

                        componentSecond.enabled = false;
                    }
                    if (_thirdCard != null)
                    {
                        var componentThird = _thirdCard.GetComponent<EventTrigger>();

                        componentThird.enabled = true;
                    }
                    if (_fourthCard != null)
                    {
                        var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                        componentFourth.enabled = false;
                    }
                }
            }
            else
            {
                GameObject _currentCard = (GameObject)tt;

                _endPoint = Vector2.zero;
                _landingToleranceRadius = 1.3f;
                _random = new System.Random();
                var valueX = 350 * (1 - (-0.6)) + (-0.6);
                //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                var value = 200 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                _currentCard.transform.position = new Vector3(x, y);
                _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                Vector3 positionOfCurrentCard = new Vector3(x, y);

                GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(FirstDeck.transform);



                if (_firstCard != null)
                {
                    var component = _firstCard.GetComponent<EventTrigger>();

                    component.enabled = false;
                }
                if (_secondCard != null)
                {
                    var componentSecond = _secondCard.GetComponent<EventTrigger>();

                    componentSecond.enabled = false;
                }
                if (_thirdCard != null)
                {
                    var componentThird = _thirdCard.GetComponent<EventTrigger>();

                    componentThird.enabled = true;
                }
                if (_fourthCard != null)
                {
                    var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                    componentFourth.enabled = false;
                }
            }
        }
        else if (currentCursor == 4 && _isSecondScenario == true)
        {
            InterationTutor8 it = new InterationTutor8();
            TutorText.text = "" + it.GetTextTutor(root);

            var valueCount = _firstDeck.transform.childCount;
            var tt = Resources.Load("Prefabs/TutorialSvg/9_HEART");

            if (valueCount > 0)
            {
                var lastObject = _firstDeck.transform.GetChild(valueCount - 1).gameObject;
                var indexLast = lastObject.name.IndexOf("(");
                string CardNameLast = lastObject.name.Substring(0, indexLast);

                GameObject _currentCard = (GameObject)tt;

                if (_currentCard.name.Equals(CardNameLast))
                {

                }
                else
                {

                    _endPoint = Vector2.zero;
                    _landingToleranceRadius = 1.3f;
                    _random = new System.Random();
                    var valueX = 350 * (1 - (-0.6)) + (-0.6);
                    //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                    float toleranceX = 2.3f;
                    float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                    var value = 200 * (1.5 - 0.6) + 0.6;
                    float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                    _currentCard.transform.position = new Vector3(x, y);
                    _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                    Vector3 positionOfCurrentCard = new Vector3(x, y);

                    GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                    myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                    myBrick.transform.SetParent(FirstDeck.transform);



                    if (_firstCard != null)
                    {
                        var component = _firstCard.GetComponent<EventTrigger>();

                        component.enabled = true;
                    }
                    if (_secondCard != null)
                    {
                        var componentSecond = _secondCard.GetComponent<EventTrigger>();

                        componentSecond.enabled = false;
                    }
                    if (_thirdCard != null)
                    {
                        var componentThird = _thirdCard.GetComponent<EventTrigger>();

                        componentThird.enabled = false;
                    }
                    if (_fourthCard != null)
                    {
                        var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                        componentFourth.enabled = false;
                    }
                }
            }
            else
            {
                GameObject _currentCard = (GameObject)tt;

                _endPoint = Vector2.zero;
                _landingToleranceRadius = 1.3f;
                _random = new System.Random();
                var valueX = 350 * (1 - (-0.6)) + (-0.6);
                //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                var value = 200 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                _currentCard.transform.position = new Vector3(x, y);
                _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                Vector3 positionOfCurrentCard = new Vector3(x, y);

                GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(FirstDeck.transform);



                if (_firstCard != null)
                {
                    var component = _firstCard.GetComponent<EventTrigger>();

                    component.enabled = true;
                }
                if (_secondCard != null)
                {
                    var componentSecond = _secondCard.GetComponent<EventTrigger>();

                    componentSecond.enabled = false;
                }
                if (_thirdCard != null)
                {
                    var componentThird = _thirdCard.GetComponent<EventTrigger>();

                    componentThird.enabled = false;
                }
                if (_fourthCard != null)
                {
                    var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                    componentFourth.enabled = false;
                }
            }
        }
        else if (currentCursor == 5 && _isSecondScenario == true)
        {
            InterationTutor9 it = new InterationTutor9();
            TutorText.text = "" + it.GetTextTutor(root);

            var tt = Resources.Load("Prefabs/TutorialSvg/7_DIAMOND");


            var valueCount = _firstDeck.transform.childCount;

            if (valueCount > 0)
            {
                var lastObject = _firstDeck.transform.GetChild(valueCount - 1).gameObject;
                var indexLast = lastObject.name.IndexOf("(");
                string CardNameLast = lastObject.name.Substring(0, indexLast);

                GameObject _currentCard = (GameObject)tt;

                if (_currentCard.name.Equals(CardNameLast))
                {

                }
                else
                {

                    _endPoint = Vector2.zero;
                    _landingToleranceRadius = 1.3f;
                    _random = new System.Random();
                    var valueX = 350 * (1 - (-0.6)) + (-0.6);
                    //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                    float toleranceX = 2.3f;
                    float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                    var value = 200 * (1.5 - 0.6) + 0.6;
                    float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                    _currentCard.transform.position = new Vector3(x, y);
                    _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                    Vector3 positionOfCurrentCard = new Vector3(x, y);

                    GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                    myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                    myBrick.transform.SetParent(FirstDeck.transform);


                    if (_firstCard != null)
                    {
                        var component = _firstCard.GetComponent<EventTrigger>();

                        component.enabled = false;
                    }
                    if (_secondCard != null)
                    {
                        var componentSecond = _secondCard.GetComponent<EventTrigger>();

                        componentSecond.enabled = true;
                    }
                    if (_thirdCard != null)
                    {
                        var componentThird = _thirdCard.GetComponent<EventTrigger>();

                        componentThird.enabled = false;
                    }
                    if (_fourthCard != null)
                    {
                        var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                        componentFourth.enabled = false;
                    }
                }
            }
            else
            {
                GameObject _currentCard = (GameObject)tt;

                _endPoint = Vector2.zero;
                _landingToleranceRadius = 1.3f;
                _random = new System.Random();
                var valueX = 350 * (1 - (-0.6)) + (-0.6);
                //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
                float toleranceX = 2.3f;
                float x = (float)(valueX + _random.Next(-20, 20) * toleranceX);

                var value = 200 * (1.5 - 0.6) + 0.6;
                float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);


                _currentCard.transform.position = new Vector3(x, y);
                _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

                Vector3 positionOfCurrentCard = new Vector3(x, y);

                GameObject myBrick = (GameObject)Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity);
                myBrick.transform.localScale = new Vector3(0.789f, 0.789f, 0);
                myBrick.transform.SetParent(FirstDeck.transform);


                if (_firstCard != null)
                {
                    var component = _firstCard.GetComponent<EventTrigger>();

                    component.enabled = false;
                }
                if (_secondCard != null)
                {
                    var componentSecond = _secondCard.GetComponent<EventTrigger>();

                    componentSecond.enabled = true;
                }
                if (_thirdCard != null)
                {
                    var componentThird = _thirdCard.GetComponent<EventTrigger>();

                    componentThird.enabled = false;
                }
                if (_fourthCard != null)
                {
                    var componentFourth = _fourthCard.GetComponent<EventTrigger>();

                    componentFourth.enabled = false;
                }
            }
            
        }
        else if (currentCursor == 6 && _isSecondScenario == true)
        {
            InterationTutor10 it = new InterationTutor10();
            TutorText.text = "" + it.GetTextTutor(root);
        }
        
    }


    public void onEnterOver()
    {
        //LeaveGameButtonOver

        Image components = _Leave.GetComponent<Image>();

        components.sprite = Resources.Load<Sprite>("tutorial/LeaveGameButtonOver");
        //_btnOver.active = true;
    }

    public void onExitOver()
    {
        //_btnOver.active = false;
        Image components = _Leave.GetComponent<Image>();

        components.sprite = Resources.Load<Sprite>("tutorial/LeaveGameButton");
    }

    public void onClickMouse()
    {
        currentCursor = 0;
        _isSecondScenario = false;
        NetworkManager.instance.ChangeScene("Menu");
    }
}
