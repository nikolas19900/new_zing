using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infastructure.Collections
{
    class IgracCollection
    {
        public ObjectId Id { get; set; }

        public long FBplayerId { get; set; }

        public string ImePrezime { get; set; }

        public string Email { get; set; }

        public int Poeni { get; set; }
        
        public int Tokeni { get; set; }

        public int ZlatniTokeni { get; set; }

        public ObjectId TrenutnaTitulaId { get; set; }

        public ObjectId DatumRegistracijeId { get; set; }
        

        public ObjectId DnevniTokeniId { get; set; }

        public ObjectId GameSettingsId { get; set; }
    }
}
