using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [CreateAssetMenu(menuName = "Singletons/MasterManager")]
    public class MasterManager : SingletonScriptableObject<MasterManager>
    {
        [SerializeField]
        private GameSettings _gameSettings;

        public static GameSettings GameSettings
        {
            get { return Instance._gameSettings; }
        }

    }
}
