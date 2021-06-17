using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCardObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _firstCardSelected;
    [SerializeField]
    private Canvas _canvas;

    private GameObject _currentCard;
    private Vector2 _endPoint;
    private float _landingToleranceRadius;
    private System.Random _random;
    // Start is called before the first frame update
    void Start()
    {
        _endPoint = Vector2.zero;
        _random = new System.Random();
        _landingToleranceRadius = 1.3f;
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

   

    public void OnExitMouse()
    {
        _firstCardSelected.SetActive(false);
    }

    public void OnEnterMouse()
    {
        _firstCardSelected.SetActive(true);
    }


    public void OnClickMouse()
    {
        string CardNameClone = transform.name;

        

        var tt = Resources.Load("Prefabs/TutorialSvg/" + CardNameClone);

        _currentCard = (GameObject)tt;

        

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
        myBrick.transform.SetParent(_canvas.transform);

        
        Destroy(transform.gameObject);
        StartingPoint.currentCursor++;
    }
}
