using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infastructure.Collections
{
    class DnevniTokeniCollection
    {

        public ObjectId Id { get; set; }

        public int velicina { get; set; }
    }
}
