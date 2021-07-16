using Assets.Scripts.UI.Game;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeOfMoveRefactor : MonoBehaviour
{

    Canvas _tempCanvas;
    int countOfClick = 0;
    private System.Random _random;
    
    private Vector2 _endPoint;
    private float _landingToleranceRadius;

    public TimeOfMoveRefactor(Canvas valueCanvas)
    {
        _tempCanvas = valueCanvas;
        //countOfClick = click;


        _random = new System.Random();
        _endPoint = Vector2.zero;
        _landingToleranceRadius = 0.3f;
    }
    

    public void ConfigureDroppedCard()
    {
        var tv = (Canvas)GameScript.player.GetCurrentPlayerCanvas();

        
        int valueCard = _random.Next(0, tv.transform.childCount);

        var _tempTransoformCard = tv.transform.GetChild(valueCard);

        string CardNameClone = _tempTransoformCard.name;
        
        var index = CardNameClone.IndexOf("(");
        string CardName = CardNameClone.Substring(0, index);
       
        var tt = Resources.Load("Prefabs/CardPrefabsStartSvg/" + CardName);
        var _currentCard = (GameObject)tt;
        
        //za current card objekat je neophodno setovati svgrender order na 1
        //_currentCard.transform.localScale = new Vector3(0.23f, 0.23f);

        _random = new System.Random();
        //var valueX = _random.NextDouble() * (1 - (-0.6)) + (-0.6);
        var valueX = 300 * (1 - (-0.6)) + (-0.6);
        //float x = (float)(_endPoint.x + valueX  * _random.NextDouble() );
        float toleranceX = 2.3f;
        float x = (float)(valueX + _random.Next(-20, 0) * toleranceX);
        var value = 340 * (1.5 - 0.6) + 0.6;
        float y = (float)(_endPoint.y + _random.Next(100, 150) * _landingToleranceRadius + value);

        _currentCard.transform.position = new Vector3(x, y);
        _currentCard.transform.localScale = new Vector3(0.789f, 0.789f, 0);

        Vector3 positionOfCurrentCard = new Vector3(x, y);
        GameObject myBrick = Instantiate(_currentCard, new Vector3(x, y, 0), Quaternion.identity) as GameObject;

        // GameObject myBrick = PhotonNetwork.Instantiate("Prefabs/CardPrefabsStartSvg/" + CardName, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        
        myBrick.transform.SetParent(_tempCanvas.transform);

        Destroy(_tempTransoformCard.gameObject);
        if (GameScript.player.GetCurrentInstance() == 1)
        {
            //treba kod projveriti da li radi dobro
            
            var list = GameScript.player.GetCardsOfFirstPlayer();
           
            list.Remove(CardName);
            GameScript.player.SetCardsOfFirstPlayer(list);
            SideOfTeam.MoveInstance = 2;
            //saljem trenutnu instancu i svima kojima je jedan azuriram listu. i tako isto sa ostalim listama.
            GameScript.player.photonView.RPC("SetListForRequiredPlayerFirst", RpcTarget.Others, list.ToArray(), SideOfTeam.MoveInstance);
           

        }
        else if (GameScript.player.GetCurrentInstance() == 2)
        {
            var list = GameScript.player.GetCardsOfSecondPlayer();
           
            list.Remove(CardName);
            GameScript.player.SetCardsOfSecondPlayer(list);
            SideOfTeam.MoveInstance = 3;
            GameScript.player.photonView.RPC("SetListForRequiredPlayerSecond", RpcTarget.Others,  list.ToArray(), SideOfTeam.MoveInstance);
        }
        else if (GameScript.player.GetCurrentInstance() == 3)
        {
            var list = GameScript.player.GetCardsOfThirdPlayer();
          
            list.Remove(CardName);
            GameScript.player.SetCardsOfThirdPlayer(list);
            SideOfTeam.MoveInstance = 4;

            GameScript.player.photonView.RPC("SetListForRequiredPlayerThird", RpcTarget.Others, list.ToArray(), SideOfTeam.MoveInstance);
        }
        else if (GameScript.player.GetCurrentInstance() == 4)
        {
            var list = GameScript.player.GetCardsOfFourthPlayer();
           
            list.Remove(CardName);
            GameScript.player.SetCardsOfFourthPlayer(list);
            SideOfTeam.MoveInstance = 1;
            GameScript.player.photonView.RPC("SetListForRequiredPlayerFourth", RpcTarget.Others,  list.ToArray(), SideOfTeam.MoveInstance);
        }


        GameScript.player.photonView.RPC("ChangeMoveDropedCard", RpcTarget.Others,_currentCard.name, positionOfCurrentCard);

        //var list = BeginningOfGame.player.GetOfListOfCards();
        //var list = GameScript.player.GetOfListOfCards();
       // list.Add(CardName);
        // Debug.Log("prosla karta:" + list.Count);

        foreach (Transform element in tv.transform)
        {
            // Debug.Log(i++);
            element.GetComponent<EventTrigger>().enabled = false;
            //var firstCard = element.Find("FirstCardSelected").gameObject;
            //firstCard.active = false;

        }

        TimeOfMoveObject.DeactiveGameObject();
        GameScript.player.DeactivateTimeOfMove();
        bool isPickedUp = GameScript.player.PickUpCardsFromDeck();
        if (isPickedUp)
        {
            GameScript.player.photonView.RPC("CleanDesk", RpcTarget.Others);
        }
        GameScript.player.photonView.RPC("ActivatePlayerToPlay", RpcTarget.Others, PhotonNetwork.LocalPlayer.NickName);

      //  countOfClick++;

        
        //BeginningOfGame.player.SetListOfCards(list);
        //BeginningOfGame.player.photonView.RPC("ChangeMoveDropedCard", RpcTarget.Others, CardName, _currentCard.transform.position, countOfClick);
        //GameScript.player.SetListOfCards(list);
        // GameScript.player.photonView.RPC("ChangeMoveDropedCard", RpcTarget.Others, CardName, _currentCard.transform.position, countOfClick);
        // GameScript.player.photonView.RPC("ChangeMoveDropedCard", RpcTarget.Others, PhotonNetwork.LocalPlayer.NickName, PhotonNetwork.LocalPlayer.CustomProperties["Picture"]);
        

       

        

      

    }

}
