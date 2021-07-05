
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class TimeOfMoveObject : MonoBehaviour
{
    
    
    [SerializeField]
    private Text timeValue;
    static float tempTimer = 0.0f;
    bool isTimerGone = false;
    public static TimeOfMoveObject _instance;
    
    public static int countClick = 1;
    Canvas SizeOfCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
        SizeOfCanvas = GameScript.player.GetFirstDeck();
        countClick = SizeOfCanvas.transform.childCount;
        if (_instance == null)
        {
            _instance = this;
        }

        tempTimer = 10;
        if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
        {

            //Color color;
            //ColorUtility.TryParseHtmlString("#0a2c6a", out color);
            var components = gameObject.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();
            // Debug.Log("vvv");
            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                {
                    var image2 = (SVGImporter.SVGImage)com;
                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/TimeOfMoveBlue");
                }
            }
            //Debug.Log("prvi");
        }else
        {
            var components = gameObject.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();
            // Debug.Log("vvv");
            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(SVGImporter.SVGImage).IsAssignableFrom(vv))
                {
                    var image2 = (SVGImporter.SVGImage)com;
                    image2.vectorGraphics = Resources.Load<SVGImporter.SVGAsset>("SVG_sprites/TimeOfMoveRed");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
       
       tempTimer -= (float) Time.deltaTime;

       if(tempTimer > 0)
       {
            int seconds = (int)tempTimer % 60;
            if (seconds == 0)
            {
                SizeOfCanvas = GameScript.player.GetFirstDeck();
                countClick = SizeOfCanvas.transform.childCount;

                gameObject.active = false;
                tempTimer = 0;
                
                TimeOfMoveRefactor timeOfMove = new TimeOfMoveRefactor(SizeOfCanvas, countClick);
                timeOfMove.ConfigureDroppedCard();
               
                return;
            }

            timeValue.text = "" + seconds;

       }
       
    }


    public static void DeactiveGameObject()
    {
        _instance.gameObject.active = false;
        tempTimer = 10;
        
    }
}
