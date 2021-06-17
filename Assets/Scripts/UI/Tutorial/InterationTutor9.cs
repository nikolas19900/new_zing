using Assets.Scripts.Infastructure.JSON;
using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.Tutorial
{
    class InterationTutor9
    {
        public string GetTextTutor(TutorialRoot root)
        {
            if (MasterManager.GameSettings.DefaultLanguage == "English")
            {
                return root._10[0].english;
            }
            if (MasterManager.GameSettings.DefaultLanguage == "Spanish")
            {
                return root._10[1].spanish;
            }

            if (MasterManager.GameSettings.DefaultLanguage == "Portugales")
            {
                return root._10[2].portuguese;
            }
            if (MasterManager.GameSettings.DefaultLanguage == "Russian")
            {
                return root._10[3].russian;
            }
            return "";
        }
    }
}
