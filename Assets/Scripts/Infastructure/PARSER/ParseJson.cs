using Assets.Scripts.Infastructure.JSON;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;


namespace Assets.Scripts.Infastructure.PARSER
{
    class ParseJson
    {


        public Root Deserialize()
        {

            string path = Application.dataPath + "/Localization/json/play_classic.json";
            var  jsonString = File.ReadAllText(path);
           
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                //Root classic = JsonUtility.FromJson<Root>(json);
                Root classic = JsonConvert.DeserializeObject<Root>(json);
                return classic;
                //Root classic = JsonSerializer.Deserialize<Root>(jsonString);
                //Debug.Log("radi tu sam:" + classic.play_classic[0].english);
                //Debug.Log("radi tu spanski:" + classic.play_classic[1].spanish);

                //Debug.Log("tttttt:" + classic.play_special[0].english);
                //Debug.Log("vvvv:" + classic.play_special[1].spanish);


                //Debug.Log("qqqq:" + classic.leaderboard[0].english);
                //Debug.Log("aaaa:" + classic.leaderboard[1].spanish);

                //Debug.Log("qqqq:" + classic.private_game[0].english);
                //Debug.Log("aaaa:" + classic.private_game[1].spanish);


                //Debug.Log("qqqq:" + classic.notice[0].english);
                //Debug.Log("aaaa:" + classic.notice[1].spanish);


                //Debug.Log("qqqq:" + classic.tutorial[0].english);
                //Debug.Log("aaaa:" + classic.tutorial[1].spanish);

                //Debug.Log("qqqq:" + classic.help[0].english);
                //Debug.Log("aaaa:" + classic.help[1].spanish);

                //Debug.Log("qqqq:" + classic.vip_pack[0].english);
                //Debug.Log("aaaa:" + classic.vip_pack[1].spanish);

                //Debug.Log("qqqq:" + classic.invites[0].english);
                //Debug.Log("aaaa:" + classic.invites[1].spanish);

                //Debug.Log("qqqq:" + classic.gifts[0].english);
                //Debug.Log("aaaa:" + classic.gifts[1].spanish);

                //Debug.Log("qqqq:" + classic.friends[0].english);
                //Debug.Log("aaaa:" + classic.friends[1].spanish);


                //Debug.Log("qqqq:" + classic.friends[0].english);
                //Debug.Log("aaaa:" + classic.friends[1].spanish);

                //Debug.Log("qqqq:" + classic.special_offers[0].english);
                //Debug.Log("aaaa:" + classic.special_offers[1].spanish);

            }
            //(@"c:\play_classic.json"
            //RootPlayClassic classic = JsonUtility.FromJson<RootPlayClassic>(jsonString);
            //RootPlayClassic classic = (RootPlayClassic)JsonUtility.FromJson(jsonString, typeof(RootPlayClassic));
            //Assets.Scripts.Infastructure.JSON.PlayClassic classic = (Assets.Scripts.Infastructure.JSON.PlayClassic)JsonUtility.FromJson(path, typeof(Assets.Scripts.Infastructure.JSON.PlayClassic));
            //Assets.Scripts.Infastructure.JSON.PlayClassic classic = JsonConvert.DeserializeObject<Assets.Scripts.Infastructure.JSON.PlayClassic>(path);
            
        }


        public TutorialRoot DeserializeTutorial()
        {
            string path = Application.dataPath + "/Localization/json/tutorial_text.json";
            var jsonString = File.ReadAllText(path);

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                //Root classic = JsonUtility.FromJson<Root>(json);
                TutorialRoot start = JsonConvert.DeserializeObject<TutorialRoot>(json);

                //Debug.Log("qqqq:" + start._1[0].english);
                //Debug.Log("aaaa:" + start._1[1].spanish);

                
                return start;
            }
        }
    }
}
