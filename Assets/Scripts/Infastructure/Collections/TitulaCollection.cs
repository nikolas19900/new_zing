using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infastructure.Collections
{
    class TitulaCollection
    {

        public ObjectId Id { get; set; }


        public string ime { get; set; }

        public int poeni { get; set; }

        public int tokeni { get; set; }

        public bool isPro { get; set; }

    }
}
