using Assets.Scripts.Infastructure.Collections;
using Assets.Scripts.Views;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    class TituleView
    {

        private string PlayerId;

        public TituleView(string player)
        {
            PlayerId = player;
        }


        public string VratiNaziv()
        {

            var client = new MongoClient(MasterManager.GameSettings.DatabaseConnectionString);
            var database = client.GetDatabase("zing");

            var _IgracCollection = database.GetCollection<IgracCollection>("IgracCollection");
            
            var _TitulaCollection = database.GetCollection<TitulaCollection>("TitulaCollection");

            var _TrenutnaTitulaCollection = database.GetCollection<TrenutnaTitulaCollection>("TrenutnaTitulaCollection");
            var _GameSettingsCollection = database.GetCollection<GameSettingsCollection>("GameSettingsCollection");

            var r = from t1 in _IgracCollection.AsQueryable()
                    join t2 in _TrenutnaTitulaCollection.AsQueryable() on t1.TrenutnaTitulaId equals t2.Id 
                    join t3 in _TitulaCollection.AsQueryable() on  t2.TitulaId  equals   t3.Id 
                    join t4 in _GameSettingsCollection.AsQueryable() on t1.GameSettingsId equals t4.Id
                    into result
                    select new NazivTitule()
                    {
                        Naziv = t3.ime
                    };

            var list = r.ToList();

            string naziv = "";

            foreach(var tt in list)
            {
                naziv = tt.Naziv;
            }

            return naziv;
        }
    }
}
