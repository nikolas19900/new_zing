using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infastructure.JSON
{
    [System.Serializable]
    class TutorialRoot
    {

        public List<_1Data> _1 { get; set; }

        public List<_2Data> _2 { get; set; }

        public List<_3Data> _3 { get; set; }
        public List<_4Data> _4 { get; set; }
        public List<_5Data> _5 { get; set; }
        public List<_6Data> _6 { get; set; }
        public List<_7Data> _7 { get; set; }
        public List<_8Data> _8 { get; set; }
        public List<_9Data> _9 { get; set; }
        public List<_10Data> _10 { get; set; }
        public List<_11Data> _11 { get; set; }
       
        public int Count()
        {
            int count = this.GetType().GetProperties().Count();
            
            return count;
        }
    }
}
