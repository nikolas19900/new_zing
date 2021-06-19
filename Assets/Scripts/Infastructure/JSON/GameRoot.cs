using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infastructure.JSON
{
    [System.Serializable]
    class GameRoot
    {
        public List<cardsData> cards { get; set; }

        public List<pointsData> points { get; set; }

        public List<zingsData> zings { get; set; }

        public List<totalData> total { get; set; }

        public List<dealerData> dealer { get; set; }

        public List<leaveData> leave { get; set; }
        
    }
}
