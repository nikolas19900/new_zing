using Assets.Scripts.Infastructure.Collections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    class IgracRegistracija
    {

        private long PlayerId;
        private string Ime;
        private string Email;

        public IgracRegistracija(long player,string ime,string e)
        {
            PlayerId = player;
            Ime = ime;
            Email = e;
        }

        public bool OdradiRegistraciju()
        {
            var client = new MongoClient(MasterManager.GameSettings.DatabaseConnectionString);
            var database = client.GetDatabase("zing");

            var _IgracCollection = database.GetCollection<IgracCollection>("IgracCollection");
            var _DatumRegistracijeCollection = database.GetCollection<DatumRegistracijeCollection>("DatumRegistracijeCollection");

            var _GameSettingsCollection = database.GetCollection<GameSettingsCollection>("GameSettingsCollection");

            var _DnevniTokeniCollection = database.GetCollection<DnevniTokeniCollection>("DnevniTokeniCollection");

            var _TitulaCollection = database.GetCollection<TitulaCollection>("TitulaCollection");

            var _TrenutnaTitulaCollection = database.GetCollection<TrenutnaTitulaCollection>("TrenutnaTitulaCollection");

            var temp = database.
                GetCollection<IgracCollection>("IgracCollection").AsQueryable();
            long ccc = Convert.ToInt64(PlayerId);
            var igrac = from item in temp
                        where item.FBplayerId == ccc
                        select item;


           

            if (igrac.ToList().Count == 0)
            {

                DateTime newDate = DateTime.Now;
                DatumRegistracijeCollection datum = new DatumRegistracijeCollection()
                {
                    Godina = newDate.Year,
                    Mjesec = newDate.Month,
                    Dan = newDate.Day,
                    Sat = newDate.Hour,
                    Minut = newDate.Minute,
                    Sekund = newDate.Second
                };

                GameSettingsCollection gameSettings = new GameSettingsCollection()
                {
                    GameVersion = MasterManager.GameSettings.GameVersion,
                    DefaultLanguage = MasterManager.GameSettings.DefaultLanguage
                };
                
                _GameSettingsCollection.InsertOne(gameSettings);
                _DatumRegistracijeCollection.InsertOne(datum);


                var response = _DatumRegistracijeCollection.AsQueryable().OrderByDescending(c => c.Id).First();
                var responseLastGameSettings = _GameSettingsCollection.AsQueryable().OrderByDescending(c => c.Id).First();
                var responseDnevniTokeni = _DnevniTokeniCollection.AsQueryable().OrderByDescending(c => c.Id).First();

                IQueryable<TitulaCollection> tempTitula = from item in _TitulaCollection.AsQueryable()
                                 where item.ime == "BEGINNER"
                                 select item;
                var vv = tempTitula.ToList();
                foreach (var tt in vv)
                {
                    
                    TrenutnaTitulaCollection trenutnaTitula = new TrenutnaTitulaCollection()
                    {
                        TitulaId = tt.Id
                    };

                    _TrenutnaTitulaCollection.InsertOne(trenutnaTitula);
                    
                }

                var responseTrenutnaTitula = _TrenutnaTitulaCollection.AsQueryable().OrderByDescending(c => c.Id).First();

                IgracCollection newIgracCollection = new IgracCollection()
                {
                    FBplayerId = ccc,
                    ImePrezime = Ime,
                    Email = Email,
                    Poeni = 0,
                    Tokeni = 100,
                    ZlatniTokeni = 0,
                    DatumRegistracijeId = response.Id,
                    GameSettingsId = responseLastGameSettings.Id,
                    DnevniTokeniId = responseDnevniTokeni.Id,
                    TrenutnaTitulaId = responseTrenutnaTitula.Id

                };

                _IgracCollection.InsertOne(newIgracCollection);
                return true;
            }
            return false;
        }
    }
}
