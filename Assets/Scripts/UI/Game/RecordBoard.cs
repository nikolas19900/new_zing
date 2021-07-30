using Assets.Scripts.UI.Game.CheckCards;
using Assets.Scripts.UI.Game.Utils;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Game
{
    class RecordBoard : MonoBehaviourPun
    {

        //[SerializeField]
        //private Text _namesOfPlBlueValue;

        //[SerializeField]
        //private Text _namesOfPlRedValue;

        [SerializeField]
        private Text _cardsValue;

        [SerializeField]
        private Text _pointsValue;

        [SerializeField]
        private Text _zingsValue;

       // private int _totalZing = 0;

        [SerializeField]
        private Text _totalPointsValue;

        private int _totalPoints = 0;

        [SerializeField]
        private Text _cardsRedValue;

        [SerializeField]
        private Text _pointsRedValue;

        [SerializeField]
        private Text _zingsRedValue;

        [SerializeField]
        private Text _totalPointsRedValue;

        [SerializeField]
        private PhotonView _tempPhoton;

        private DateTime _dateAndTimeOfTakenCards;

        private string PlayerId;

        Canvas SizeOfCanvas;

        public static RecordBoard _instance;

        //[SerializeField]
        //private Canvas _EndOfGame;

        //[SerializeField]
        //private Text _NameOfWinner;


        void Start()
        {

            //_namesOfPlBlueValue.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
            //_namesOfPlRedValue.text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
            // _EndOfGame.gameObject.active = false;
            //SizeOfCanvas = BeginningOfGame.player.GetFirstDeck();
            SizeOfCanvas = GameScript.player.GetFirstDeck();
            if (_instance == null)
            {

                _instance = this;
            }
        }

        void Update()
        {
           // SizeOfCanvas = BeginningOfGame.player.GetFirstDeck();
        }

        public void SetTotalPoints()
        {
            _totalPoints = 0;
          //  _totalZing = 0;
            _tempPhoton.RPC("SetTotalPointsRed", RpcTarget.Others);

        }


        [PunRPC]
        public void SetTotalPointsRed()
        {
            _totalPoints = 0;
          //  _totalZing = 0;
            _cardsValue.text = "0";

            _cardsRedValue.text = "0";

            _pointsValue.text = "0";

            _zingsValue.text = "0";

            _pointsRedValue.text = "0";

            _zingsRedValue.text = "0";

            string blueScore = _totalPointsValue.text;

            string redScore = _totalPointsRedValue.text;

            photonView.RPC("SetTotalPointsBlue", RpcTarget.Others, blueScore, redScore);
        }

        [PunRPC]
        public void SetTotalPointsBlue(string blue, string red)
        {
            _totalPointsValue.text = blue;

            _totalPointsRedValue.text = red;
        }

        [PunRPC]
        public void TakeCardsFromTalon2(string sideOfTeam,string[] listArray)
        {
            int pointsValue = 0;
            int newPoints = 0;

            if (sideOfTeam.Equals("Blue"))
            {
                CalculatePoints points = new CalculatePoints(listArray.ToList());
                pointsValue = points.GetPoints();

                int tempCards = int.Parse(_cardsValue.text) + listArray.Length;
                _cardsValue.text = "" + tempCards;

                int pointsBlueValue = int.Parse(_pointsValue.text) + pointsValue;

                _pointsValue.text = "" + pointsBlueValue;

                int totalBluePoints = int.Parse(_totalPointsValue.text) + pointsValue;

                _totalPointsValue.text = "" + totalBluePoints;
            }
            else
            {
                CalculatePoints points = new CalculatePoints(listArray.ToList());
                pointsValue = points.GetPoints();

                int tempCards = int.Parse(_cardsRedValue.text) + listArray.Length;
                _cardsRedValue.text = "" + tempCards;


                int pointsRedValue = int.Parse(_pointsRedValue.text) + pointsValue;

                _pointsRedValue.text = "" + pointsRedValue;

                int totalRedPoints = int.Parse(_totalPointsRedValue.text) + pointsValue;

                _totalPointsRedValue.text = "" + totalRedPoints;
            }
        }

        [PunRPC]
        public void TakeRestOfCardsFirst(string[] listArray)
        {
            int pointsValue = 0;
            
            CalculatePoints points = new CalculatePoints(listArray.ToList());
            pointsValue = points.GetPoints();

            int tempCards = int.Parse(_cardsValue.text) + listArray.Length;
           
            int tempAditionalPoints = 0;
            if (tempCards > 26)
            {
                tempAditionalPoints = 3;
            }

            int tempCardsRed = int.Parse(_cardsRedValue.text);
            int tempAditionalPointsRed = 0;
            if(tempCardsRed > 26)
            {
                tempAditionalPointsRed = 3;
            }
            tempCards = 0;
            tempCardsRed = 0;
            _cardsValue.text = "" + tempCards;
            _cardsRedValue.text = "" + tempCardsRed;

            int pointsBlueValue = 0;

            _pointsValue.text = "" + pointsBlueValue;

            int pointsRedValue = 0;

            _pointsRedValue.text = "" + pointsRedValue;

            int zingBlueValue = 0;
            _zingsValue.text = "" + zingBlueValue;
           

            int zingRedValue = 0;
            _zingsRedValue.text = "" + zingRedValue;

            int totalBluePoints = int.Parse(_totalPointsValue.text) + pointsValue+ tempAditionalPoints;

            _totalPointsValue.text = "" + totalBluePoints;

            int totalRedPoints = int.Parse(_totalPointsRedValue.text)  + tempAditionalPointsRed;

            _totalPointsRedValue.text = "" + totalRedPoints;

            Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;

            foreach (var vv in values)
            {

                if (values[vv.Key].CustomProperties["Instance"].Equals(1))
                {
                    
                        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                        string gg = hash["Points"].ToString();
                        Debug.Log("igrac je sakupio poena:" + gg);
                        int pointsPlayer = int.Parse(gg) + pointsValue; 
                        hash["Points"] = pointsPlayer;
                        
                        PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);
                    
                }
            }

        }

        [PunRPC]
        public void TakeRestOfCardsFirstAI(string[] listArray)
        {
            int pointsValue = 0;
            
            CalculatePoints points = new CalculatePoints(listArray.ToList());
            pointsValue = points.GetPoints();


            int tempCards = int.Parse(_cardsValue.text) + listArray.Length;

            int tempAditionalPoints = 0;
            if (tempCards > 26)
            {
                tempAditionalPoints = 3;
            }

            int tempCardsRed = int.Parse(_cardsRedValue.text);
            int tempAditionalPointsRed = 0;
            if (tempCardsRed > 26)
            {
                tempAditionalPointsRed = 3;
            }
            tempCards = 0;
            tempCardsRed = 0;
            _cardsValue.text = "" + tempCards;
            _cardsRedValue.text = "" + tempCardsRed;


            int pointsBlueValue = 0;
            _pointsValue.text = "" + pointsBlueValue;

            int pointsRedValue = 0;
            _pointsRedValue.text = "" + pointsRedValue;

            int zingBlueValue = 0;
            _zingsValue.text = "" + zingBlueValue;
           
            int zingRedValue = 0;
            _zingsRedValue.text = "" + zingRedValue;

            

            int totalBluePoints = int.Parse(_totalPointsValue.text) + pointsValue + tempAditionalPoints;

            _totalPointsValue.text = "" + totalBluePoints;

            int totalRedPoints = int.Parse(_totalPointsRedValue.text) + tempAditionalPointsRed;

            _totalPointsRedValue.text = "" + totalRedPoints;



        }

        [PunRPC]
        public void TakeRestOfCardsSecond(string[] listArray)
        {
            int pointsValue = 0;
            
            CalculatePoints points = new CalculatePoints(listArray.ToList());
            pointsValue = points.GetPoints();


            int tempCards = int.Parse(_cardsRedValue.text) + listArray.Length;
            

            int tempAditionalPoints = 0;
            if (tempCards > 26)
            {
                tempAditionalPoints = 3;
            }

            int tempCardsBlue = int.Parse(_cardsValue.text);
            int tempAditionalPointsBlue = 0;
            if (tempCardsBlue > 26)
            {
                tempAditionalPointsBlue = 3;
            }
            tempCards = 0;
            tempCardsBlue = 0;
            _cardsValue.text = "" + tempCardsBlue;
            _cardsRedValue.text = "" + tempCards;

            int pointsBlueValue = 0;
            int pointsRedValue =0;

            _pointsRedValue.text = "" + pointsRedValue;

            _pointsValue.text = "" + pointsBlueValue;

            int zingBlueValue = 0;
            _zingsValue.text = "" + zingBlueValue;


            int zingRedValue = 0;
            _zingsRedValue.text = "" + zingRedValue;

            int totalBluePoints = int.Parse(_totalPointsValue.text)  + tempAditionalPointsBlue;

            _totalPointsValue.text = "" + totalBluePoints;

            int totalRedPoints = int.Parse(_totalPointsRedValue.text) + pointsValue + tempAditionalPoints;

            _totalPointsRedValue.text = "" + totalRedPoints;

            Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;

            foreach (var vv in values)
            {

                if (values[vv.Key].CustomProperties["Instance"].Equals(2))
                {

                    ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                    string gg = hash["Points"].ToString();
                    Debug.Log("igrac je sakupio poena 2:" + gg);
                    int pointsPlayer = int.Parse(gg) + pointsValue;
                    hash["Points"] = pointsPlayer;
                    
                    PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);

                }
            }

        }

        [PunRPC]
        public void TakeRestOfCardsSecondAI(string[] listArray)
        {
            int pointsValue = 0;
            
            CalculatePoints points = new CalculatePoints(listArray.ToList());
            pointsValue = points.GetPoints();

            int tempCards = int.Parse(_cardsRedValue.text) + listArray.Length;
            

            int tempAditionalPoints = 0;
            if (tempCards > 26)
            {
                tempAditionalPoints = 3;
            }

            int tempCardsBlue = int.Parse(_cardsValue.text);
            int tempAditionalPointsBlue = 0;
            if (tempCardsBlue > 26)
            {
                tempAditionalPointsBlue = 3;
            }
            tempCards = 0;
            tempCardsBlue = 0;
            _cardsValue.text = "" + tempCardsBlue;
            _cardsRedValue.text = "" + tempCards;

            int pointsBlueValue = 0;
            int pointsRedValue = 0;

            _pointsRedValue.text = "" + pointsRedValue;

            _pointsValue.text = "" + pointsBlueValue;

            int zingBlueValue = 0;
            _zingsValue.text = "" + zingBlueValue;


            int zingRedValue = 0;
            _zingsRedValue.text = "" + zingRedValue;

            int totalBluePoints = int.Parse(_totalPointsValue.text) + tempAditionalPointsBlue;

            _totalPointsValue.text = "" + totalBluePoints;

            int totalRedPoints = int.Parse(_totalPointsRedValue.text) + pointsValue + tempAditionalPoints;

            _totalPointsRedValue.text = "" + totalRedPoints;

        }


        [PunRPC]
        public void TakeRestOfCardsThird(string[] listArray)
        {
            int pointsValue = 0;
            
            CalculatePoints points = new CalculatePoints(listArray.ToList());
            pointsValue = points.GetPoints();

            int tempCards = int.Parse(_cardsValue.text) + listArray.Length;

            int tempAditionalPoints = 0;
            if (tempCards > 26)
            {
                tempAditionalPoints = 3;
            }

            int tempCardsRed = int.Parse(_cardsRedValue.text);
            int tempAditionalPointsRed = 0;
            if (tempCardsRed > 26)
            {
                tempAditionalPointsRed = 3;
            }
            tempCards = 0;
            tempCardsRed = 0;
            _cardsValue.text = "" + tempCards;
            _cardsRedValue.text = "" + tempCardsRed;

            int pointsBlueValue = 0;
            
            _pointsValue.text = "" + pointsBlueValue;
            int pointsRedValue = 0;

            _pointsRedValue.text = "" + pointsRedValue;


            int zingBlueValue = 0;
            _zingsValue.text = "" + zingBlueValue;
            

            int zingRedValue = 0;
            _zingsRedValue.text = "" + zingRedValue;

            int totalBluePoints = int.Parse(_totalPointsValue.text) + pointsValue + tempAditionalPoints;

            _totalPointsValue.text = "" + totalBluePoints;

            int totalRedPoints = int.Parse(_totalPointsRedValue.text) + tempAditionalPointsRed;

            _totalPointsRedValue.text = "" + totalRedPoints;

            Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;

            foreach (var vv in values)
            {

                if (values[vv.Key].CustomProperties["Instance"].Equals(3))
                {

                    ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                     string gg = hash["Points"].ToString();
                    Debug.Log("igrac je sakupio poena 3:" + gg);
                    int pointsPlayer = int.Parse(gg) + pointsValue;
                    hash["Points"] = pointsPlayer;
                    PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);

                }
            }

        }

        [PunRPC]
        public void TakeRestOfCardsThirdAI(string[] listArray)
        {
            int pointsValue = 0;
            
            CalculatePoints points = new CalculatePoints(listArray.ToList());
            pointsValue = points.GetPoints();

            int tempCards = int.Parse(_cardsValue.text) + listArray.Length;

            int tempAditionalPoints = 0;
            if (tempCards > 26)
            {
                tempAditionalPoints = 3;
            }

            int tempCardsRed = int.Parse(_cardsRedValue.text);
            int tempAditionalPointsRed = 0;
            if (tempCardsRed > 26)
            {
                tempAditionalPointsRed = 3;
            }
            tempCards = 0;
            tempCardsRed = 0;
            _cardsValue.text = "" + tempCards;
            _cardsRedValue.text = "" + tempCardsRed;

            int pointsBlueValue = 0;

            _pointsValue.text = "" + pointsBlueValue;
            int pointsRedValue = 0;

            _pointsRedValue.text = "" + pointsRedValue;

            int zingBlueValue = 0;
            _zingsValue.text = "" + zingBlueValue;


            int zingRedValue = 0;
            _zingsRedValue.text = "" + zingRedValue;


            int totalBluePoints = int.Parse(_totalPointsValue.text) + pointsValue + tempAditionalPoints;

            _totalPointsValue.text = "" + totalBluePoints;

            int totalRedPoints = int.Parse(_totalPointsRedValue.text) + tempAditionalPointsRed;

            _totalPointsRedValue.text = "" + totalRedPoints;

        }

        [PunRPC]
        public void TakeRestOfCardsFourth(string[] listArray)
        {
            int pointsValue = 0;
            
            CalculatePoints points = new CalculatePoints(listArray.ToList());
            pointsValue = points.GetPoints();

            int tempCards = int.Parse(_cardsRedValue.text) + listArray.Length;

            int tempAditionalPoints = 0;
            if (tempCards > 26)
            {
                tempAditionalPoints = 3;
            }

            int tempCardsBlue = int.Parse(_cardsValue.text);
            int tempAditionalPointsBlue = 0;
            if (tempCardsBlue > 26)
            {
                tempAditionalPointsBlue = 3;
            }
            tempCards = 0;
            tempCardsBlue = 0;
            _cardsValue.text = "" + tempCardsBlue;
            _cardsRedValue.text = "" + tempCards;


            int pointsBlueValue = 0;
            int pointsRedValue = 0;

            _pointsRedValue.text = "" + pointsRedValue;

            _pointsValue.text = "" + pointsBlueValue;

            int zingBlueValue = 0;
            _zingsValue.text = "" + zingBlueValue;


            int zingRedValue = 0;
            _zingsRedValue.text = "" + zingRedValue;

    
            int totalBluePoints = int.Parse(_totalPointsValue.text) + tempAditionalPointsBlue;

            _totalPointsValue.text = "" + totalBluePoints;

            int totalRedPoints = int.Parse(_totalPointsRedValue.text) + pointsValue + tempAditionalPoints;

            _totalPointsRedValue.text = "" + totalRedPoints;

            Dictionary<int, Player> values = PhotonNetwork.CurrentRoom.Players;

            foreach (var vv in values)
            {

                if (values[vv.Key].CustomProperties["Instance"].Equals(4))
                {

                    ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                    string gg = hash["Points"].ToString();
                    Debug.Log("igrac je sakupio poena 4:" + gg);
                    int pointsPlayer = int.Parse(gg) + pointsValue;
                    hash["Points"] = pointsPlayer;
                    PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);

                }
            }

        }

        [PunRPC]
        public void TakeRestOfCardsFourthAI(string[] listArray)
        {
            int pointsValue = 0;
            int newPoints = 0;
            CalculatePoints points = new CalculatePoints(listArray.ToList());
            pointsValue = points.GetPoints();

            int tempCards = int.Parse(_cardsRedValue.text) + listArray.Length;

            int tempAditionalPoints = 0;
            if (tempCards > 26)
            {
                tempAditionalPoints = 3;
            }

            int tempCardsBlue = int.Parse(_cardsValue.text);
            int tempAditionalPointsBlue = 0;
            if (tempCardsBlue > 26)
            {
                tempAditionalPointsBlue = 3;
            }
            tempCards = 0;
            tempCardsBlue = 0;
            _cardsValue.text = "" + tempCardsBlue;
            _cardsRedValue.text = "" + tempCards;

            int pointsBlueValue = 0;
            int pointsRedValue = 0;

            _pointsRedValue.text = "" + pointsRedValue;

            _pointsValue.text = "" + pointsBlueValue;

            int zingBlueValue = 0;
            _zingsValue.text = "" + zingBlueValue;

            int zingRedValue = 0;
            _zingsRedValue.text = "" + zingRedValue;

            int totalBluePoints = int.Parse(_totalPointsValue.text) + tempAditionalPointsBlue;

            _totalPointsValue.text = "" + totalBluePoints;

            int totalRedPoints = int.Parse(_totalPointsRedValue.text) + pointsValue + tempAditionalPoints;

            _totalPointsRedValue.text = "" + totalRedPoints;

            
            

        }



        [PunRPC]
        public void TakeCardsFromTalon(string[] listArray)
        {

            // Debug.Log("velicina liste talon:" + BeginningOfGame.ListOfAllTakenCards.Count);
            //bool firstInteration = false;
            //if (BeginningOfGame.ListOfAllTakenCards.Count == 0)
            //{
            //    BeginningOfGame.ListOfAllTakenCards = listArray.ToList<string>();

            //    firstInteration = true;
            //}
            //else
            //{
            //    List<string> newList = listArray.ToList<string>();

            //    BeginningOfGame.ListOfAllTakenCards.AddRange(newList);
            //}
            // Debug.Log("velicina liste talon posle:" + BeginningOfGame.ListOfAllTakenCards.Count);
            int pointsValue = 0;
            

            if (PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
            {
                CalculatePoints points = new CalculatePoints(listArray.ToList());
                    pointsValue = points.GetPoints();

                int tempCards = int.Parse(_cardsValue.text) + listArray.Length;
                _cardsValue.text = ""+tempCards;

                int pointsBlueValue = int.Parse(_pointsValue.text) + pointsValue;

                _pointsValue.text = "" + pointsBlueValue;

                int totalBluePoints = int.Parse(_totalPointsValue.text) + pointsValue;

                _totalPointsValue.text = "" + totalBluePoints;


                Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;

                foreach (var vv in value)
                {


                    if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key) == PhotonNetwork.LocalPlayer)
                    {
                        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                        hash["Cards"] = int.Parse(_cardsValue.text);
                        hash["Points"] = int.Parse(_pointsValue.text);
                        hash["Zing"] = int.Parse(_zingsValue.text);
                        hash["Total"] = int.Parse(_totalPointsValue.text);
                        PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);
                    }
                }

                _tempPhoton.RPC("UpdateTableRecord", RpcTarget.Others, PhotonNetwork.LocalPlayer.CustomProperties["Team"], _cardsValue.text,
                    _pointsValue.text, _zingsValue.text, _totalPointsValue.text);
            }
            else
            {
                CalculatePoints points = new CalculatePoints(listArray.ToList());
                pointsValue = points.GetPoints();

                int tempCards = int.Parse(_cardsRedValue.text) + listArray.Length;
                _cardsRedValue.text = "" + tempCards;


                int pointsRedValue = int.Parse(_pointsRedValue.text) + pointsValue;

                _pointsRedValue.text = "" + pointsRedValue;

                int totalRedPoints = int.Parse(_totalPointsRedValue.text) + pointsValue;

                _totalPointsRedValue.text = "" + totalRedPoints;

                Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;

                foreach (var vv in value)
                {


                    if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key) == PhotonNetwork.LocalPlayer)
                    {
                        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                        hash["Cards"] = int.Parse(_cardsRedValue.text);
                        hash["Points"] = int.Parse(_pointsRedValue.text);
                        hash["Zing"] = int.Parse(_zingsRedValue.text);
                        hash["Total"] = int.Parse(_totalPointsRedValue.text);
                        PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);
                    }
                }
                
                _tempPhoton.RPC("UpdateTableRecord", RpcTarget.Others, PhotonNetwork.LocalPlayer.CustomProperties["Team"], _cardsRedValue.text,
                    _pointsRedValue.text,_zingsRedValue.text,_totalPointsRedValue.text);
            }
            //if (firstInteration)
            //{
            //    var Player = PhotonNetwork.LocalPlayer.UserId;

            //    string playerPhoton2 = PhotonNetwork.CurrentRoom.GetPlayer(1).UserId;
            //    string valueFromContent = "";
            //    if (Player == playerPhoton2)
            //        valueFromContent = _pointsValue.text;
            //    else
            //    {
            //        valueFromContent = _pointsRedValue.text;
            //    }

            //    CalculatePoints points = new CalculatePoints(BeginningOfGame.ListOfAllTakenCards);
            //    pointsValue = points.GetPoints();
            //    newPoints = pointsValue;
            //    //Debug.Log("first interaction trenutno poena" + pointsValue);
            //    int currentPoints = int.Parse(valueFromContent);
            //    pointsValue += currentPoints;
            //    _pointsValue.text = "" + pointsValue;
            //    //Debug.Log("first interaction ukupno poena" + pointsValue);
            //    firstInteration = false;
            //}
            //else
            //{
            //    List<string> list = listArray.ToList<string>();
            //    var Player = PhotonNetwork.LocalPlayer.UserId;

            //    string playerPhoton2 = PhotonNetwork.CurrentRoom.GetPlayer(1).UserId;
            //    string valueFromContent = "";
            //    if (Player == playerPhoton2)
            //        valueFromContent = _pointsValue.text;
            //    else
            //    {
            //        valueFromContent = _pointsRedValue.text;
            //    }


            //    CalculatePoints points = new CalculatePoints(list);
            //    pointsValue = points.GetPoints();
            //    //Debug.Log("trenutno poena" +pointsValue);
            //    newPoints = pointsValue;
            //    int currentPoints = int.Parse(valueFromContent);
            //    pointsValue += currentPoints;
            //    //Debug.Log("ukupno poena" + pointsValue);

            //}

            //var PlayerName = PhotonNetwork.LocalPlayer.UserId;

            //DateTime date = DateTime.Now;
            //_dateAndTimeOfTakenCards = date;

            //PlayerId = PlayerName;
            //// Debug.Log("vrijeme:" + date.ToString());

            //string playerPhoton = PhotonNetwork.CurrentRoom.GetPlayer(1).UserId;
            //if (PlayerName == playerPhoton)
            //{
            //    //Debug.Log("plavi:");
            //    _cardsValue.text = "" + BeginningOfGame.ListOfAllTakenCards.Count;
            //    _pointsValue.text = "" + pointsValue;
            //    string valueFromZing = _zingsValue.text;
            //    string valueFromTotal = _totalPointsValue.text;

            //    int zingContent = int.Parse(valueFromZing);
            //    int totalPoints = 0;
            //    //var RecordInteration = RecordInterationValue.text;

            //    //int RecordInteValue = int.Parse(RecordInteration);
            //    //Debug.Log("vrijednost:" + RecordInteration);

            //    if (RecordInteration._interation == 0) {
            //        totalPoints = pointsValue + zingContent;
            //    } else
            //    {
            //        // Debug.Log("total:" + totalPoints);
            //        totalPoints = int.Parse(valueFromTotal);
            //        // Debug.Log("total 2:" + totalPoints);
            //        totalPoints += newPoints;
            //        // Debug.Log("total 3:" + totalPoints);
            //    }
            //    _totalPointsValue.text = "" + totalPoints;
            //    photonView.RPC("TotalCardBlue", RpcTarget.Others, BeginningOfGame.ListOfAllTakenCards.Count, pointsValue, totalPoints);
            //}
            //else
            //{
            //    //Debug.Log("crveni:");
            //    _cardsRedValue.text = "" + BeginningOfGame.ListOfAllTakenCards.Count;
            //    _pointsRedValue.text = "" + pointsValue;

            //    string valueFromZingRed = _zingsRedValue.text;
            //    string valueFromTotal = _totalPointsRedValue.text;
            //    int zingContentRed = int.Parse(valueFromZingRed);
            //    int totalPointsRed = 0;

            //    //var RecordInteration = RecordInterationValue.text;

            //    //int RecordInteValue = int.Parse(RecordInteration);

            //    if (RecordInteration._interation == 0)
            //    {
            //        totalPointsRed = pointsValue + zingContentRed;
            //    }
            //    else
            //    {
            //        //Debug.Log("total red:" + totalPointsRed);
            //        totalPointsRed = int.Parse(valueFromTotal);
            //        //Debug.Log("total red 1:" + totalPointsRed);
            //        totalPointsRed += newPoints;
            //        //Debug.Log("total red 2:" + totalPointsRed);
            //    }

            //    _totalPointsRedValue.text = "" + totalPointsRed;
            //    photonView.RPC("TotalCardRed", RpcTarget.Others, BeginningOfGame.ListOfAllTakenCards.Count, pointsValue, totalPointsRed);
            //}


            foreach (Transform transform in SizeOfCanvas.transform)
            {

                GameObject tempGameObject = transform.gameObject;

                Destroy(transform.gameObject);
            }

            //Debug.Log("igrac ponio ukupno:" + BeginningOfGame.ListOfTakenCards.Count);
        }

        [PunRPC]
        public void UpdateTableRecord(string strana,string cards,string points,string zing,string total)
        {
            if (strana.Equals("Blue"))
            {
                _cardsValue.text = cards;
                _pointsValue.text = points;
                _zingsValue.text = zing;
                _totalPointsValue.text = total;
            }
            else
            {
                _cardsRedValue.text = cards;
                _pointsRedValue.text = points;
                _zingsRedValue.text = zing;
                _totalPointsRedValue.text = total;
            }

            foreach (Transform transform in SizeOfCanvas.transform)
            {

                GameObject tempGameObject = transform.gameObject;

                Destroy(transform.gameObject);
            }
        }
        [PunRPC]
        public void TakeCardsZing2(string sideOfTeam,string[] listArray)
        {
            var lastCard = listArray[listArray.Length - 1];
            var previousLastCard = listArray[listArray.Length - 2];
            // var previousLastCard = BeginningOfGame._listOfZings[BeginningOfGame._listOfZings.Count - 2];
            int newPointsZing = 0;
            if (lastCard.Contains("J_") && previousLastCard.Contains("J_"))
            {
                // _totalZing += 20;
                newPointsZing = 20;
            }
            else
            {
                // _totalZing += 10;
                newPointsZing = 10;
            }

            if (sideOfTeam.Equals("Blue"))
            {
                int tempZing = int.Parse(_zingsValue.text);
                tempZing += newPointsZing;
                _zingsValue.text = "" + tempZing;
                int tempCards = int.Parse(_cardsValue.text) + listArray.Length;
                _cardsValue.text = "" + tempCards;

                string valueFromTotal = _totalPointsValue.text;

                int pointsTotalValue = int.Parse(valueFromTotal) + newPointsZing;

                _totalPointsValue.text = "" + pointsTotalValue;

                
            }
            else
            {
                int tempZing = int.Parse(_zingsRedValue.text);
                tempZing += newPointsZing;
                _zingsRedValue.text = "" + tempZing;
                int tempCards = int.Parse(_cardsRedValue.text) + listArray.Length;
                _cardsRedValue.text = "" + tempCards;

                string valueFromTotal = _totalPointsRedValue.text;

                int pointsTotalValue = int.Parse(valueFromTotal) + newPointsZing;

                _totalPointsRedValue.text = "" + pointsTotalValue;

                
            }
        }

        [PunRPC]
        public void TakeCardsZing(string[] listArray)
        {
            //if (BeginningOfGame._listOfZings.Count == 0)
            //{
            //    BeginningOfGame._listOfZings = listArray.ToList<string>();
            //    BeginningOfGame.ListOfAllTakenCards.AddRange(BeginningOfGame._listOfZings);
            //}
            //else
            //{

            //    List<string> newList = listArray.ToList<string>();
            //    BeginningOfGame._listOfZings.AddRange(newList);
            //    BeginningOfGame.ListOfAllTakenCards.AddRange(newList);
            //}

            // var lastCard = BeginningOfGame._listOfZings[BeginningOfGame._listOfZings.Count - 1];
            var lastCard = listArray[listArray.Length - 1];
            var previousLastCard = listArray[listArray.Length - 2];
           // var previousLastCard = BeginningOfGame._listOfZings[BeginningOfGame._listOfZings.Count - 2];
            int newPointsZing = 0;
            if (lastCard.Contains("J_") && previousLastCard.Contains("J_"))
            {
               // _totalZing += 20;
                newPointsZing = 20;
            }
            else
            {
               // _totalZing += 10;
                newPointsZing = 10;
            }

            if(PhotonNetwork.LocalPlayer.CustomProperties["Team"].Equals("Blue"))
            {
                int tempZing = int.Parse(_zingsValue.text);
                tempZing += newPointsZing;
                _zingsValue.text = "" + tempZing;
                int tempCards = int.Parse(_cardsValue.text) + listArray.Length;
                _cardsValue.text = "" + tempCards ;

                string valueFromTotal = _totalPointsValue.text;

                int pointsTotalValue = int.Parse(valueFromTotal) + newPointsZing;

                _totalPointsValue.text = ""+ pointsTotalValue;

                Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;

                foreach (var vv in value)
                {


                    if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key) == PhotonNetwork.LocalPlayer)
                    {
                        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                        hash["Cards"] = int.Parse(_cardsValue.text);
                        hash["Points"] = int.Parse(_pointsValue.text);
                        hash["Zing"] = int.Parse(_zingsValue.text);
                        hash["Total"] = int.Parse(_totalPointsValue.text);
                        PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);
                    }
                }

                _tempPhoton.RPC("UpdateTableRecord", RpcTarget.Others, PhotonNetwork.LocalPlayer.CustomProperties["Team"], _cardsValue.text,
                   _pointsValue.text, _zingsValue.text, _totalPointsValue.text);

            }
            else
            {
                int tempZing = int.Parse(_zingsRedValue.text);
                tempZing += newPointsZing;
                _zingsRedValue.text = "" + tempZing;
                int tempCards = int.Parse(_cardsRedValue.text) + listArray.Length;
                _cardsRedValue.text = "" + tempCards ;

                string valueFromTotal = _totalPointsRedValue.text;

                int pointsTotalValue = int.Parse(valueFromTotal) + newPointsZing;

                _totalPointsRedValue.text = "" + pointsTotalValue;

                Dictionary<int, Player> value = PhotonNetwork.CurrentRoom.Players;

                foreach (var vv in value)
                {


                    if (PhotonNetwork.CurrentRoom.GetPlayer(vv.Key) == PhotonNetwork.LocalPlayer)
                    {
                        ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.Players[vv.Key].CustomProperties;
                        hash["Cards"] = int.Parse(_cardsRedValue.text);
                        hash["Points"] = int.Parse(_pointsRedValue.text);
                        hash["Zing"] = int.Parse(_zingsRedValue.text);
                        hash["Total"] = int.Parse(_totalPointsRedValue.text);
                        PhotonNetwork.CurrentRoom.Players[vv.Key].SetCustomProperties(hash);
                    }
                }

                _tempPhoton.RPC("UpdateTableRecord", RpcTarget.Others, PhotonNetwork.LocalPlayer.CustomProperties["Team"], _cardsRedValue.text,
                   _pointsRedValue.text, _zingsRedValue.text, _totalPointsRedValue.text);
            }

          


            //var PlayerName = PhotonNetwork.LocalPlayer.UserId;

            //PlayerId = PlayerName;

            //DateTime date = DateTime.Now;
            //_dateAndTimeOfTakenCards = date;

            //string playerPhoton = PhotonNetwork.CurrentRoom.GetPlayer(1).UserId;

            //if (PlayerName == playerPhoton)
            //{
            //    int tempZing = int.Parse(_zingsValue.text);
            //    tempZing += newPointsZing;
            //    _zingsValue.text = "" + tempZing;
            //    _cardsValue.text = "" + BeginningOfGame.ListOfAllTakenCards.Count;
            //    string valueFromTotal = _totalPointsValue.text;


            //    string valueFromPoints = _pointsValue.text;
            //    int pointsValue = int.Parse(valueFromPoints);

            //    //var RecordInteration = RecordInterationValue.text;

            //    //int RecordInteValue = int.Parse(RecordInteration);
            //    int totalPoints = 0;
            //    if (RecordInteration._interation == 0)
            //    {
            //        totalPoints = pointsValue + _totalZing;
            //    }
            //    else
            //    {
            //        totalPoints = int.Parse(valueFromTotal);
            //        totalPoints += newPointsZing;
            //    }

            //    _totalPointsValue.text = "" + totalPoints;
            //    photonView.RPC("TotalCardBlue", RpcTarget.Others, BeginningOfGame.ListOfAllTakenCards.Count, pointsValue, totalPoints);
            //    photonView.RPC("TotalZingCardsBlue", RpcTarget.Others, tempZing);
            //}
            //else
            //{
            //    int tempZing = int.Parse(_zingsRedValue.text);
            //    tempZing += newPointsZing;
            //    _zingsRedValue.text = "" + tempZing;
            //    _cardsRedValue.text = "" + BeginningOfGame.ListOfAllTakenCards.Count;

            //    string valueFromTotal = _totalPointsRedValue.text;

            //    string valueFromPointsRed = _pointsRedValue.text;
            //    int pointsValueRed = int.Parse(valueFromPointsRed);


            //    //var RecordInteration = RecordInterationValue.text;

            //    //int RecordInteValue = int.Parse(RecordInteration);
            //    int totalPointsRed = 0;
            //    if (RecordInteration._interation == 0)
            //    {
            //        totalPointsRed = pointsValueRed + _totalZing;
            //    }
            //    else
            //    {
            //        totalPointsRed = int.Parse(valueFromTotal);
            //        totalPointsRed += newPointsZing;
            //    }

            //    _totalPointsRedValue.text = "" + totalPointsRed;
            //    photonView.RPC("TotalCardRed", RpcTarget.Others, BeginningOfGame.ListOfAllTakenCards.Count, totalPointsRed, totalPointsRed);
            //    photonView.RPC("TotalZingCardsRed", RpcTarget.Others, tempZing);
            //}


            foreach (Transform transform in SizeOfCanvas.transform)
            {

                GameObject tempGameObject = transform.gameObject;

                Destroy(transform.gameObject);
            }

        }

        [PunRPC]
        public void CovertDateNowRed(string datum, string[] array)
        {
            //Debug.Log("crveni");
            //  Debug.Log("plavi:" + datum);
            DateTime date = Convert.ToDateTime(datum);
            //  Debug.Log("crveni:" + _dateAndTimeOfTakenCards.ToString());
            int result = DateTime.Compare(date, _dateAndTimeOfTakenCards);

            if (result < 0)
            {
                int value = RecordInteration._interation;
                string playerPhoton = "";
                if (value % 2 == 0)
                {
                    playerPhoton = PhotonNetwork.CurrentRoom.GetPlayer(1).UserId;
                } else
                {
                    playerPhoton = PhotonNetwork.CurrentRoom.GetPlayer(2).UserId;
                }
                if (PlayerId == playerPhoton)
                {
                    //u ovaj uslov nece nikad uci
                    Debug.Log("zadovoljava if uslov crveni");
                    //BeginningOfGame.ListOfAllTakenCards.AddRange(array.ToList());
                    //photonView.RPC("ReturnComparasionOfDate", RpcTarget.Others, result, array);
                }
                else
                {
                    //uvijek ce biti ovaj uslov
                    Debug.Log("crveni je zadnji odnio");
                    BeginningOfGame.ListOfAllTakenCards.AddRange(array.ToList());
                    photonView.RPC("ReturnComparasionOfDate", RpcTarget.Others, result, array);
                }
            }
            if (result > 0)
            {
                int value = RecordInteration._interation;
                string playerPhoton = "";
                if (value % 2 == 0)
                {
                    playerPhoton = PhotonNetwork.CurrentRoom.GetPlayer(1).UserId;
                }
                else
                {
                    playerPhoton = PhotonNetwork.CurrentRoom.GetPlayer(2).UserId;
                }
                if (PlayerId == playerPhoton)
                {
                    //u ovaj uslov nece nikad uci
                    Debug.Log("zadovoljava if uslov plavi");
                    //photonView.RPC("ReturnComparasionOfDate", RpcTarget.Others, result, array);
                }
                else
                {
                    Debug.Log("plavi je zadnji odnio");
                    //uvijek ce biti ovaj uslov
                    photonView.RPC("ReturnComparasionOfDate", RpcTarget.Others, result, array);
                }
            }
        }

        [PunRPC]
        public void ReturnComparasionOfDate(int result, string[] array)
        {

            int pointsValue = 0;
            int tempPoints = 0;
            string valueFromContent = "";
            string valueFromContentRed = "";
            string valueFromZing = "";
            string valueFromZingRed = "";
            string valueFromTotalRed = "";
            string valueFromTotal = "";
            List<string> list = array.ToList<string>();
            CalculatePoints points = new CalculatePoints(list);
            pointsValue = points.GetPoints();
            tempPoints = pointsValue;
            int valInt = RecordInteration._interation;
            if (result < 0)
            {
                //crveni je odnio
                //Debug.Log("rezultat datum crveni:" + result);

                valueFromContentRed = _pointsRedValue.text;
                valueFromZingRed = _zingsRedValue.text;
                valueFromTotalRed = _totalPointsRedValue.text;
                int currentPoints = int.Parse(valueFromContentRed);

                //Debug.Log("trenutno poena crveni:" + currentPoints);

                int zingPoints = int.Parse(valueFromZingRed);
                pointsValue += currentPoints;
                //Debug.Log("sumiranje poena crveni:" + pointsValue);
                string cardsCountContent = _cardsRedValue.text;
                int cardsCount = int.Parse(cardsCountContent);

                cardsCount += list.Count;
                //  Debug.Log("ukupno karata crveni 2:" + cardsCount);
                int tempAditionalPoints = 0;
                //Debug.Log("points 1 blue:" + pointsValue);
                if (BeginningOfGame.ListOfAllTakenCards.Count == cardsCount)
                {
                    Debug.Log("indetican broj crveni");
                } else
                {

                    Debug.Log("nije indetican broj crveni");
                    //Debug.Log("lista:" + BeginningOfGame.ListOfAllTakenCards.Count);
                    //Debug.Log("broj karata:" + cardsCount);
                }
                if (cardsCount > 26)
                {
                    tempAditionalPoints = 3;
                }
                //Debug.Log("points 1 blue:" + pointsValue);
                pointsValue += tempAditionalPoints;
                //Debug.Log("karte poeni crveni:" + pointsValue);
                _pointsRedValue.text = "" + pointsValue;
                int total = 0;
                //Debug.Log("total crveni:" + total);
                if (RecordInteration._interation == 0)
                {
                    total = pointsValue + zingPoints;
                } else
                {
                    total = int.Parse(valueFromTotalRed);
                    //Debug.Log("total crveni content:" + total);
                    total += tempAditionalPoints + tempPoints;
                }
                //Debug.Log("total crveni 2:" + total);
                // Debug.Log("total poeni crveni:" + total);
                _totalPointsRedValue.text = "" + total;
                //Debug.Log("crveni je zadnji ponio:" + RecordInteration._interation);
                photonView.RPC("EraseRestOfCardsRed", RpcTarget.Others, pointsValue, total);
                if (cardsCount < 26)
                {
                    photonView.RPC("UpdateCardCountOpositeBlue", RpcTarget.Others);
                }
            }
            else
            {
                //plavi je odnio
                //Debug.Log("rezultat datum:" + result);
                BeginningOfGame.ListOfAllTakenCards.AddRange(list);
                valueFromContent = _pointsValue.text;
                valueFromZing = _zingsValue.text;
                valueFromTotal = _totalPointsValue.text;
                int currentPoints = int.Parse(valueFromContent);
                //Debug.Log("trenutno poena plavi:" + currentPoints);
                int zingPoints = int.Parse(valueFromZing);
                pointsValue += currentPoints;
                //Debug.Log("sumiranje poena plavi" + pointsValue);
                string cardsCountContent = _cardsValue.text;
                int cardsCount = int.Parse(cardsCountContent);

                cardsCount += list.Count;
                //  Debug.Log("ukupno karata plavi 2:" + cardsCount);
                int tempAditionalPoints = 0;
                //Debug.Log("points 1 blue:" + pointsValue);
                if (BeginningOfGame.ListOfAllTakenCards.Count == cardsCount)
                {
                    Debug.Log("indetican broj plavi");
                } else
                {
                    Debug.Log("nije indetican broj plavi");
                    
                }

                if (cardsCount > 26)
                {
                    tempAditionalPoints = 3;
                }
                //Debug.Log("points 1 blue:" + pointsValue);
                pointsValue += tempAditionalPoints;
                // Debug.Log("karte poeni plavi" + pointsValue);
                _pointsValue.text = "" + pointsValue;
                int total = 0;
                //Debug.Log("total plavi:" + total);
                if (RecordInteration._interation == 0)
                {
                    total = pointsValue + zingPoints;
                }
                else
                {
                    total = int.Parse(valueFromTotal);
                    //Debug.Log("total plavi content:" + total);
                    total += tempAditionalPoints + tempPoints;
                }

                //Debug.Log("total plavi 2:" + total);
                _totalPointsValue.text = "" + total;
                //Debug.Log("plavi je zadnji ponio:" + RecordInteration._interation);
                photonView.RPC("EraseRestOfCardsBlue", RpcTarget.Others, pointsValue, total);
                if (cardsCount < 26)
                {
                    photonView.RPC("UpdateCardCountOpositeRed", RpcTarget.Others);
                }
            }

            int valueBlueTotal = int.Parse(_totalPointsValue.text);
            int valueRedTotal = int.Parse(_totalPointsRedValue.text);

            if (valueBlueTotal >= 101 || valueRedTotal >= 101)
            {
               // _EndOfGame.gameObject.active = true;
                
                if (valueBlueTotal > valueRedTotal)
                {
                    //_NameOfWinner.text = _namesOfPlBlueValue.text + " Won";
                    //photonView.RPC("LaunchEndOfGame", RpcTarget.Others, _namesOfPlBlueValue.text);
                }
                else
                {
                    //_NameOfWinner.text = _namesOfPlRedValue.text + " Won";
                    //photonView.RPC("LaunchEndOfGame", RpcTarget.Others, _namesOfPlRedValue.text);
                }
                
            }
            else
            {
               // _EndOfGame.gameObject.active = false;

                if (!SideOfTeam.ChangeSideOfCards)
                    SideOfTeam.ChangeSideOfCards = true;
                else
                    SideOfTeam.ChangeSideOfCards = false;
                //moram kompletnu scenu da relodujem stim sto cu morati da pamtim odredjene vrijednosti
                //kao sto su strana tima i rezultati igraca

                valInt++;

                RecordInteration._interation = valInt;
                photonView.RPC("UpdateRecordInteration", RpcTarget.Others, valInt);
                if (PhotonNetwork.IsMasterClient)
                {
                    //Debug.Log("prvi restart"+ SideOfTeam.ChangeSideOfCards);
                    photonView.RPC("SetChangeSideOfCards", RpcTarget.Others, SideOfTeam.ChangeSideOfCards);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    //Debug.Log("drugi restart" + SideOfTeam.ChangeSideOfCards);
                    photonView.RPC("RestartScene", RpcTarget.Others, SideOfTeam.ChangeSideOfCards);
                }
            }

        }
        [PunRPC]
        public void LaunchEndOfGame(string nameOfPlayer)
        {
           // _EndOfGame.gameObject.active = true;
          //  _NameOfWinner.text = nameOfPlayer + " Won";
        }

        [PunRPC]
        public void SetChangeSideOfCards(bool value)
        {
            SideOfTeam.ChangeSideOfCards = value;
            //Debug.Log("SetChangeSideOfCards" + SideOfTeam.ChangeSideOfCards);
        }

        [PunRPC]
        public void RestartScene(bool side)
        {
            SideOfTeam.ChangeSideOfCards = side;
            //Debug.Log("restart scene:"+ SideOfTeam.ChangeSideOfCards);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        [PunRPC]
        public void UpdateRecordInteration(int value)
        {
            RecordInteration._interation = value;
        }

        [PunRPC]
        public void UpdateCardCountOpositeRed()
        {
            //Debug.Log("metoda UpdateCardCountOpositeRed");
            string cardsCountContent = _cardsRedValue.text;
            int cardsCount = int.Parse(cardsCountContent);

            string valueFromContentRed = _pointsRedValue.text;
            //string valueFromZingRed = _zingsRedValue.text;
            string valueFromTotalRed = _totalPointsRedValue.text;
            int tempAditionalPoints = 0;
            
            if (cardsCount > 26)
            {
                tempAditionalPoints = 3;
            }
            int currentPoints = int.Parse(valueFromContentRed);

            //Debug.Log("points 1 red:" + currentPoints);
            currentPoints += tempAditionalPoints;
            //Debug.Log("karte poeni crveni" + currentPoints);
            _pointsRedValue.text = "" + currentPoints;
            int total = 0;
           
            total = int.Parse(valueFromTotalRed);
            //Debug.Log("total poeni karte crveni" + total);
            total += tempAditionalPoints;
            //Debug.Log("total poeni karte crveni 1:" + total);
            _totalPointsRedValue.text = "" + total;
            
            photonView.RPC("UpdateCardCountOpositeRedReturn", RpcTarget.Others, currentPoints, total);
        }

        [PunRPC]
        public void UpdateCardCountOpositeRedReturn(int currentPoints, int total)
        {
            _pointsRedValue.text = "" + currentPoints;

            _totalPointsRedValue.text = "" + total;
        }

        [PunRPC]
        public void UpdateCardCountOpositeBlue()
        {
            //Debug.Log("metoda UpdateCardCountOpositeBlue");
            string cardsCountContent = _cardsValue.text;
            int cardsCount = int.Parse(cardsCountContent);

            string valueFromContentBlue = _pointsValue.text;
            //string valueFromZingBlue = _zingsValue.text;
            string valueFromTotalBlue = _totalPointsValue.text;

            int tempAditionalPoints = 0;
            
            if (cardsCount > 26)
            {
                tempAditionalPoints = 3;
            }
            int currentPoints = int.Parse(valueFromContentBlue);

            //Debug.Log("points 1 blue:" + currentPoints);
            currentPoints += tempAditionalPoints;
            //Debug.Log("karte poeni plavi" + currentPoints);
            _pointsValue.text = "" + currentPoints;
            
            int total = 0;

            total = int.Parse(valueFromTotalBlue);
            // Debug.Log("oposite side blue total :" + total);
            //Debug.Log("total poeni karte plavi:" + total);
            total += tempAditionalPoints;
            //Debug.Log("total poeni karte plavi 1:" + total);
            
            
           // Debug.Log("oposite side blue total 2:" + total);

            //total += tempAditionalPoints;
            _totalPointsValue.text = "" + total;


            photonView.RPC("UpdateCardCountOpositeBlueReturn", RpcTarget.Others, currentPoints,total);
        }

        [PunRPC]
        public void UpdateCardCountOpositeBlueReturn(int currentPoints, int total)
        {
            _pointsValue.text = "" + currentPoints;

            _totalPointsValue.text = "" + total;
        }


        [PunRPC]
        public void SetTableRecord(string namesOfBlue, string namesOfRed, string totalPointsBlue, string totalPointsRed)
        {
            //_namesOfPlBlueValue.text = namesOfBlue;

            //_namesOfPlRedValue.text = namesOfRed;

            _cardsValue.text = "0";

            _cardsRedValue.text = "0";

            _pointsValue.text = "0";

            _zingsValue.text = "0";

            _totalPointsValue.text = totalPointsBlue;

            _pointsRedValue.text = "0";

            _zingsRedValue.text = "0";

            _totalPointsRedValue.text = totalPointsRed;
        }

        [PunRPC]
        public void TotalCardBlue(int numberOfCards, int points, int totalPoints)
        {
            _cardsValue.text = "" + numberOfCards;
            _pointsValue.text = "" + points;
            _totalPointsValue.text = "" + totalPoints;
        }

        [PunRPC]
        public void TotalCardRed(int numberOfCards, int points, int totalPoints)
        {
            _cardsRedValue.text = "" + numberOfCards;
            _pointsRedValue.text = "" + points;
            _totalPointsRedValue.text = "" + totalPoints;
        }

        [PunRPC]
        public void TotalZingCardsBlue(int value)
        {
            _zingsValue.text = "" + value;
        }

        [PunRPC]
        public void TotalZingCardsRed(int value)
        {
            _zingsRedValue.text = "" + value;
        }


        public DateTime GetDateAndTimeOfTakenCards()
        {
            return _dateAndTimeOfTakenCards;
        }


        public void SetInitializationOfCards()
        {
            //string namesOfBlue = _namesOfPlBlueValue.text;

            //string namesOfRed = _namesOfPlRedValue.text;

            _cardsValue.text = "0";

            _cardsRedValue.text = "0";

            _pointsValue.text = "0";

            _zingsValue.text = "0";

            string totalPointsBlue = _totalPointsValue.text;

            _pointsRedValue.text = "0";

            _zingsRedValue.text = "0";

            string totalPointsRed = _totalPointsRedValue.text;

          //  photonView.RPC("SetTableRecord", RpcTarget.Others, namesOfBlue, namesOfRed, totalPointsBlue, totalPointsRed);
        }


        [PunRPC]
        public void EraseRestOfCardsBlue(int points,int total)
        {
            foreach (Transform transform in SizeOfCanvas.transform)
            {

                GameObject tempGameObject = transform.gameObject;
                string name = tempGameObject.name;

                Destroy(transform.gameObject);
            }
            _pointsValue.text = "" + points;
            
            _totalPointsValue.text = "" + total;

        }


        [PunRPC]
        public void EraseRestOfCardsRed(int points, int total)
        {
            foreach (Transform transform in SizeOfCanvas.transform)
            {

                GameObject tempGameObject = transform.gameObject;
                string name = tempGameObject.name;

                Destroy(transform.gameObject);
            }
            _pointsRedValue.text = "" + points;

            _totalPointsRedValue.text = "" + total;
        }
    }
}
