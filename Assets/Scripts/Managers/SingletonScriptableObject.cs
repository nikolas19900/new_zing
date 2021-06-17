using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {

                if (_instance == null)
                {

                    _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();

                }
                return _instance;
            }
        }

    }
}
