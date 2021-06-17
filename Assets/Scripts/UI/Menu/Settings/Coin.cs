using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.Settings
{

    
    class Coin : MonoBehaviour
    {
        [SerializeField]
        private GameObject _CoinOver;

        public void onEnterOver()
        {

            _CoinOver.active = true;
        }

        public void onExitOver()
        {
            _CoinOver.active = false;

        }
    }
}
