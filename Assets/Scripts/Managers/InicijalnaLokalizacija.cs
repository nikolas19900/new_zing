using Assets.Scripts.Infastructure.Collections;
using Assets.Scripts.Infastructure.JSON;
using Assets.Scripts.Infastructure.PARSER;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    class InicijalnaLokalizacija
    {



        public Root setujJezike()
        {
            var client = new MongoClient(
          MasterManager.GameSettings.DatabaseConnectionString);
            var database = client.GetDatabase("zing");


            var id = MasterManager.GameSettings.PlayerId;
            var _IgracCollection = database.GetCollection<IgracCollection>("IgracCollection").AsQueryable();

            var settingsId = from item in _IgracCollection
                             where item.FBplayerId == long.Parse(id)
                             select item.GameSettingsId;

            var list = settingsId.ToList();


            foreach (var igrac in list)
            {
                var _GameSettingsCollection = database.GetCollection<GameSettingsCollection>("GameSettingsCollection");

                var settings = from item in _GameSettingsCollection.AsQueryable()
                               where item.Id == igrac
                               select item.DefaultLanguage;

                foreach (var temp in settings.ToList())
                {
                    MasterManager.GameSettings.DefaultLanguage = temp;
                }

            }

            ParseJson json = new ParseJson();
            var root = json.Deserialize();

            return root;

        }

        public TutorialRoot setLanguageTutorial()
        {
            var client = new MongoClient(
          MasterManager.GameSettings.DatabaseConnectionString);
            var database = client.GetDatabase("zing");


            var id = MasterManager.GameSettings.PlayerId;
            var _IgracCollection = database.GetCollection<IgracCollection>("IgracCollection").AsQueryable();

            var settingsId = from item in _IgracCollection
                             where item.FBplayerId == long.Parse(id)
                             select item.GameSettingsId;

            var list = settingsId.ToList();


            foreach (var igrac in list)
            {
                var _GameSettingsCollection = database.GetCollection<GameSettingsCollection>("GameSettingsCollection");

                var settings = from item in _GameSettingsCollection.AsQueryable()
                               where item.Id == igrac
                               select item.DefaultLanguage;

                foreach (var temp in settings.ToList())
                {
                    MasterManager.GameSettings.DefaultLanguage = temp;
                }

            }

            ParseJson json = new ParseJson();
            var root = json.DeserializeTutorial();

            return root;
        }
    }
}
