using Assets.Scripts.Infastructure.Collections.Test;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers.Test
{
    class InitCardsQuery
    {
        private int _broj;

        public InitCardsQuery(int tempBroj)
        {
            _broj = tempBroj;
        }

        public void InsertValue()
        {
            var client = new MongoClient(MasterManager.GameSettings.DatabaseConnectionString);
            var database = client.GetDatabase("zing");

            var _RemaingCard = database.GetCollection<InitCardsCollection>("InitCardsCollection");

            InitCardsCollection temp = new InitCardsCollection()
            {
                broj = _broj
            };
            _RemaingCard.InsertOne(temp);

        }
    }
}
