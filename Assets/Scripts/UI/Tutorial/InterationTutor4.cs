using Assets.Scripts.Infastructure.JSON;
using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.Tutorial
{
    class InterationTutor4
    {
        public string GetTextTutor(TutorialRoot root)
        {
            if (MasterManager.GameSettings.DefaultLanguage == "English")
            {
                return root._5[0].english;
            }
            if (MasterManager.GameSettings.DefaultLanguage == "Spanish")
            {
                return root._5[1].spanish;
            }

            if (MasterManager.GameSettings.DefaultLanguage == "Portugales")
            {
                return root._5[2].portuguese;
            }
            if (MasterManager.GameSettings.DefaultLanguage == "Russian")
            {
                return root._5[3].russian;
            }
            return "";
        }
    }
}
