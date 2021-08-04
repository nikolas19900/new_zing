using Assets.Scripts.Infastructure.Collections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    class RemaingCardQuery
    {
        private int _broj;

        public RemaingCardQuery(int tempBroj)
        {
            _broj = tempBroj;
        }


        public void InsertValue()
        {
            var client = new MongoClient(MasterManager.GameSettings.DatabaseConnectionString);
            var database = client.GetDatabase("zing");

            var _RemaingCard = database.GetCollection<RemaingCardListCollection>("RemaingCardListCollection");

            RemaingCardListCollection temp = new RemaingCardListCollection()
            {
                broj = _broj
            };
            _RemaingCard.InsertOne(temp);

        }
    }
}
