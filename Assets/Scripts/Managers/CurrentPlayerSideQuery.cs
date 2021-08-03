using Assets.Scripts.Infastructure.Collections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    class CurrentPlayerSideQuery
    {

        private int _currentSide;

        public CurrentPlayerSideQuery(int tempBroj)
        {
            _currentSide = tempBroj;
        }

        public void InsertValue()
        {
            var client = new MongoClient(MasterManager.GameSettings.DatabaseConnectionString);
            var database = client.GetDatabase("zing");

            var _RemaingCard = database.GetCollection<CurrentPlayerSideCollection>("CurrentPlayerSideCollection");

            CurrentPlayerSideCollection temp = new CurrentPlayerSideCollection()
            {
                currentSide = _currentSide
            };
            _RemaingCard.InsertOne(temp);

        }
    }
}
