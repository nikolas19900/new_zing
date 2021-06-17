using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infastructure.Collections
{
    class DatumRegistracijeCollection
    {

        public ObjectId Id { get; set; }

        public int Godina { get; set; }

        public int Mjesec { get; set; }


        public int Dan { get; set; }

        public int Sat { get; set; }

        public int Minut { get; set; }

        public int Sekund { get; set; }
    }
}
