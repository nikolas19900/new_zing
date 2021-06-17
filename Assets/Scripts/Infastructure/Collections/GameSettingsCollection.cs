using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infastructure.Collections
{
    class GameSettingsCollection
    {

        public ObjectId Id { get; set; }

        public string  GameVersion { get; set; }
        public string DefaultLanguage { get; set; }
    }
}
