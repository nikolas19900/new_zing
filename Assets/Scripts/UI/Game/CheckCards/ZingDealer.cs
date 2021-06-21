using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Assets.Scripts.UI.Game.Utils;
using System.Linq;

namespace Assets.Scripts.UI.Game.CheckCards
{
    class ZingDealer
    {
        public const int NumberOfCards = 52;
        /// <summary>
        /// This List represents dealer's cards
        /// </summary>
        public List<GameObject> RemainingCards { get; private set; }
        public List<GameObject> TalonCards { get; private set; }

        public GameObject LastCard { get; private set; }
        public List<GameObject> CardsOfFirstPlayers { get; private set; }

        public List<GameObject> CardsOfSecondPlayers { get; private set; }

        private readonly Random _random;

        public List<Card> ListOfCardsOfFirstPlayers { get; private set; }

        public List<Card> ListOfCardsOfSecondPlayers { get; private set; }

        public ZingDealer()
        {
            _random = new Random();


            InitAllCards();
            InitTalon();
            InitLastCard();
            InitCardsForPlayers();
        }

        public void AddCardToTalon()
        {
            // TODO: Dodati kartu na talon.
        }

        /// <summary>
        /// Load all Prefab Cards.
        /// </summary>
        private void InitAllCards()
        {
            RemainingCards = new List<GameObject>(NumberOfCards);

            // Load all Card prefabs
            // NOTE: All prefabs must be in Resources folder. In this case cards are in Assets/Resources/Prefabs/CardPrefabs.
            //var prefabs = Resources.LoadAll("Prefabs/CardPrefabs");
            var prefabs = Resources.LoadAll("Prefabs/CardPrefabsSvg");
            foreach (var prefab in prefabs)
            {
                var go = prefab as GameObject;
                go.transform.position = new Vector3(0f,0f);
                go.transform.localPosition = new Vector3(0f, 0f);
                // Init all cards
                VisualCard visualCard = go.GetComponent<VisualCard>() as VisualCard;
                Card card = new Card(visualCard.CardSignType, visualCard.CardValueType, go);
                visualCard.BaseCard = card;

                RemainingCards.Add(go);
            }

            System.Random rand = new System.Random();

            var mix = ShuffleCard.Shuffle<GameObject>(RemainingCards, rand);

            RemainingCards = mix.ToList<GameObject>();

            //foreach (var prefab in RemainingCards)
            //{
            //    Debug.Log(prefab.name);
               // var go = prefab as GameObject;

            //    // Init all cards
            //    VisualCard visualCard = go.GetComponent<VisualCard>() as VisualCard;
            //    Card card = new Card(visualCard.CardSignType, visualCard.CardValueType, go);
            //    visualCard.BaseCard = card;
            //}
        }

        private void InitLastCard()
        {
            LastCard = RemainingCards.ToArray().GetValue(RemainingCards.Count - 1) as GameObject;
        }


        private void InitTalon()
        {
            TalonCards = new List<GameObject>();


            TalonCards.Add(RemainingCards.ToArray().GetValue(RemainingCards.Count - 2) as GameObject);
            TalonCards.Add(RemainingCards.ToArray().GetValue(RemainingCards.Count - 3) as GameObject);
            TalonCards.Add(RemainingCards.ToArray().GetValue(RemainingCards.Count - 4) as GameObject);
            TalonCards.Add(RemainingCards.ToArray().GetValue(RemainingCards.Count - 5) as GameObject);

            //List<Card> cards = new List<Card>();

            //foreach (var go in TalonCards)
            //{
            //    cards.Add((go.GetComponent<VisualCard>() as VisualCard).BaseCard);
            //}

            //cards = CardsSorter.SortCards(cards);

            //TalonCards = new List<GameObject>();

            //foreach (var card in cards)
            //{

            //    TalonCards.Add(card.Owner);

            //}

        }

        public void InitCardsForPlayers()
        {
            CardsOfFirstPlayers = new List<GameObject>();

            CardsOfSecondPlayers = new List<GameObject>();

            CardsOfFirstPlayers.Clear();
            CardsOfSecondPlayers.Clear();
           

            CardsOfSecondPlayers.Add(RemainingCards.ToArray().GetValue(0) as GameObject);
            CardsOfSecondPlayers.Add(RemainingCards.ToArray().GetValue(1) as GameObject);

            CardsOfFirstPlayers.Add(RemainingCards.ToArray().GetValue(2) as GameObject);
            CardsOfFirstPlayers.Add(RemainingCards.ToArray().GetValue(3) as GameObject);

            CardsOfSecondPlayers.Add(RemainingCards.ToArray().GetValue(4) as GameObject);
            CardsOfSecondPlayers.Add(RemainingCards.ToArray().GetValue(5) as GameObject);

            CardsOfFirstPlayers.Add(RemainingCards.ToArray().GetValue(6) as GameObject);
            CardsOfFirstPlayers.Add(RemainingCards.ToArray().GetValue(7) as GameObject);

            List<Card> cardsFirst = new List<Card>();
            List<Card> cardsSecond = new List<Card>();

            cardsFirst.Clear();
            cardsSecond.Clear();

            ListOfCardsOfFirstPlayers = new List<Card>();
            ListOfCardsOfSecondPlayers = new List<Card>();

            ListOfCardsOfFirstPlayers.Clear();
            ListOfCardsOfFirstPlayers.Clear();

            foreach (var go1 in CardsOfFirstPlayers)
            {
                cardsFirst.Add((go1.GetComponent<VisualCard>() as VisualCard).BaseCard);

            }

            foreach (var go in CardsOfSecondPlayers)
            {
                cardsSecond.Add((go.GetComponent<VisualCard>() as VisualCard).BaseCard);

            }

            cardsFirst = CardsSorter.SortCards(cardsFirst);

            cardsSecond = CardsSorter.SortCards(cardsSecond);

            CardsOfSecondPlayers = new List<GameObject>();
            CardsOfFirstPlayers = new List<GameObject>();

            foreach (var card in cardsFirst)
            {
                CardsOfFirstPlayers.Add(card.Owner);
                ListOfCardsOfFirstPlayers.Add(card);
            }


            foreach (var card in cardsSecond)
            {
                CardsOfSecondPlayers.Add(card.Owner);
                ListOfCardsOfSecondPlayers.Add(card);
            }

            //Debug.Log("vv:" + ListOfCardsOfPlayers.Count);
        }

        public void DeleteSecondPlayerCard()
        {
           // Debug.Log("ukupno karata:" + RemainingCards.Count);
            //RemainingCards.RemoveAt(0);
            //RemainingCards.RemoveAt(1);

            //RemainingCards.RemoveAt(2);
            //RemainingCards.RemoveAt(3);
            
            //RemainingCards.RemoveAt(4);
            //RemainingCards.RemoveAt(5);

            //RemainingCards.RemoveAt(6);
            //RemainingCards.RemoveAt(7);
            RemainingCards.RemoveRange(0, 8);
         //   Debug.Log("ukupno karata:" + RemainingCards.Count);

        }

        //public void DeleteFirstPlayerCard()
        //{
        //    RemainingCards.Remove(RemainingCards.ToArray().GetValue(2) as GameObject);
        //    RemainingCards.Remove(RemainingCards.ToArray().GetValue(3) as GameObject);

        //    RemainingCards.Remove(RemainingCards.ToArray().GetValue(6) as GameObject);
        //    RemainingCards.Remove(RemainingCards.ToArray().GetValue(7) as GameObject);
        //}

        public void InterationOverRemaingCards()
        {

            for(int i = 0; i < 15; i++)
            {
                var value = RemainingCards.ToArray().GetValue(i) as GameObject;
                Debug.Log(value.name);
            }
            //foreach (var prefab in RemainingCards)
            //{
              //  Debug.Log(prefab.name);
                
            //}
        }
    }

        
}
