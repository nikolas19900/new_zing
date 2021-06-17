using Assets.Scripts.Infastructure.Collections;
using Assets.Scripts.Infastructure.PARSER;
using Assets.Scripts.Managers;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class LanguageSwitch : MonoBehaviour
{

    
    [SerializeField]
    public Dropdown dropdown;

    [SerializeField]
    public Text _playClassic;

    [SerializeField]
    public Text _PlaySpecialText;
    [SerializeField]
    public Text _LeaderBoardText;

    [SerializeField]
    public Text PrivateGameText;
    [SerializeField]
    private Text VIPPACKText;

    [SerializeField]
    private Text InviteText;


    [SerializeField]
    private Text GiftsText;
    [SerializeField]
    private Text FriendsText;
    [SerializeField]
    private Text SpecialOfferText;
    [SerializeField]
    private Text NoticeText;

    [SerializeField]
    private Text TutorialText;
    [SerializeField]
    private Text TutorialRulesText;
    [SerializeField]
    private Text HelpText;
    [SerializeField]
    private GameObject GoldCoinText;
    [SerializeField]
    private GameObject CoinText;
    [SerializeField]
    private GameObject CollectNow;

    //[SerializeField]
    //private GameObject _languageSwitchOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        dropdown.value = dropdown.options.FindIndex(option => option.text == MasterManager.GameSettings.DefaultLanguage);
        
    }

    public void onClickMouse()
    {
        ParseJson json = new ParseJson();
        var root = json.Deserialize();
        if (dropdown.options[dropdown.value].text == "English")
        {
            _playClassic.text = root.play_classic[0].english;
            _PlaySpecialText.text = root.play_special[0].english;
            _LeaderBoardText.text = root.leaderboard[0].english;
            PrivateGameText.text = root.private_game[0].english;
            VIPPACKText.text = root.vip_pack[0].english;

            InviteText.text = root.invites[0].english;

            GiftsText.text = root.gifts[0].english;

            FriendsText.text = root.friends[0].english;
            SpecialOfferText.text = root.special_offers[0].english;
            NoticeText.text = root.notice[0].english;
            TutorialText.text = root.tutorial[0].english;
            TutorialRulesText.text = root.tutorial[0].english;

            HelpText.text = root.help[0].english;

            var components = GoldCoinText.GetComponents<Component>();

            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page/goldCoinsText");
                }
            }
            var componentsCoins = CoinText.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCoins)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page/coins");
                }
            }

            var componentsCollectNow = CollectNow.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCollectNow)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page/FreeCoins");
                }
            }
        }

        if (dropdown.options[dropdown.value].text == "Spanish")
        {
            _playClassic.text = root.play_classic[1].spanish;
            _PlaySpecialText.text = root.play_special[1].spanish;
            _LeaderBoardText.text = root.leaderboard[1].spanish;
            PrivateGameText.text = root.private_game[1].spanish;
            VIPPACKText.text = root.vip_pack[1].spanish;

            InviteText.text = root.invites[1].spanish;

            GiftsText.text = root.gifts[1].spanish;

            FriendsText.text = root.friends[1].spanish;
            SpecialOfferText.text = root.special_offers[1].spanish;
            NoticeText.text = root.notice[1].spanish;

            TutorialText.text = root.tutorial[1].spanish;

            TutorialRulesText.text = root.tutorial[1].spanish;

            HelpText.text = root.help[1].spanish;


            var components = GoldCoinText.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/GoldCoins_ES");

                    //Debug.Log("exit");
                }
            }
            var componentsCoins = CoinText.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCoins)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/Coins_ES");
                }
            }

            var componentsCollectNow = CollectNow.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCollectNow)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/FreeCoins_ES");
                }
            }
        }

        if (dropdown.options[dropdown.value].text == "Portugales")
        {
            _playClassic.text = root.play_classic[2].portuguese;
            _PlaySpecialText.text = root.play_special[2].portuguese;
            _LeaderBoardText.text = root.leaderboard[2].portuguese;
            PrivateGameText.text = root.private_game[2].portuguese;
            VIPPACKText.text = root.vip_pack[2].portuguese;
            InviteText.text = root.invites[2].portuguese;

            GiftsText.text = root.gifts[2].portuguese;
            FriendsText.text = root.friends[2].portuguese;
            SpecialOfferText.text = root.special_offers[2].portuguese;
            NoticeText.text = root.notice[2].portuguese;

            TutorialText.text = root.tutorial[2].portuguese;

            TutorialRulesText.text = root.tutorial[2].portuguese;

            HelpText.text = root.help[2].portuguese;
            var components = GoldCoinText.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in components)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/GoldCoins_POR");
                }
            }


            var componentsCoins = CoinText.GetComponents<Component>();

            foreach (var com in componentsCoins)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/Coins_POR");
                }
            }


            var componentsCollectNow = CollectNow.GetComponents<Component>();

            //var image = gameObject.GetComponent<SVGImage>();

            foreach (var com in componentsCollectNow)
            {
                var vv = com.GetType();
                if (typeof(Image).IsAssignableFrom(vv))
                {
                    var image2 = (Image)com;
                    image2.sprite = Resources.Load<Sprite>("main_page_local/FreeCoins_POR");
                }
            }

        }

        if (dropdown.options[dropdown.value].text == "Russian")
        {
            _playClassic.text = root.play_classic[3].russian;
            _PlaySpecialText.text = root.play_special[3].russian;
            _LeaderBoardText.text = root.leaderboard[3].russian;
            PrivateGameText.text = root.private_game[3].russian;
            VIPPACKText.text = root.vip_pack[3].russian;
            InviteText.text = root.invites[3].russian;

            GiftsText.text = root.gifts[3].russian;
            FriendsText.text = root.friends[3].russian;
            SpecialOfferText.text = root.special_offers[3].russian;
            NoticeText.text = root.notice[3].russian;

            TutorialText.text = root.tutorial[3].russian;

            TutorialRulesText.text = root.tutorial[3].russian;

            HelpText.text = root.help[3].russian;
        }
       
            

        var client = new MongoClient(
            MasterManager.GameSettings.DatabaseConnectionString);
        var database = client.GetDatabase("zing");

        GameSettingsCollection gsC = new GameSettingsCollection()
        {
            GameVersion = "0.0.1",
            DefaultLanguage = dropdown.options[dropdown.value].text
        };

        var id = MasterManager.GameSettings.PlayerId;
        var _IgracCollection = database.GetCollection<IgracCollection>("IgracCollection").AsQueryable();
        
        var settingsId = from item in _IgracCollection
                    where item.FBplayerId == long.Parse(id)
                    select item.GameSettingsId;

        var list = settingsId.ToList();


        foreach(var igrac  in list)
        {
            var _GameSettingsCollection = database.GetCollection<GameSettingsCollection>("GameSettingsCollection");

            var settings = from item in _GameSettingsCollection.AsQueryable()
                             where item.Id == igrac
                             select item;

            var _GameSettingsCollUpdate = database.GetCollection<BsonDocument>("GameSettingsCollection");

            if (settings.ToList().Count <= 0)
            {
                _GameSettingsCollection.InsertOneAsync(gsC);
            }
            else
            {
               

                var Filter = Builders<BsonDocument>.Filter.Eq("_id", igrac);

                var update = Builders<BsonDocument>.Update.Set("DefaultLanguage", dropdown.options[dropdown.value].text);

                _GameSettingsCollUpdate.UpdateOne(Filter, update);
            }
        }


        

        //Debug.Log("user:" + database.DatabaseNamespace);

        Debug.Log("user:" + dropdown.options[dropdown.value].text);

        MasterManager.GameSettings.DefaultLanguage = dropdown.options[dropdown.value].text;
        //if (!_languageSwitch.active)
        //{

        //    _languageSwitch.active = true;
        //    _languageSwitchOff.active = false;
        //}
        //else
        //{
        //    _languageSwitch.active = false;
        //    _languageSwitchOff.active = true;
        //}
    }
}
