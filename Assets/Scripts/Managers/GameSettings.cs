using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.Managers
{
    [CreateAssetMenu(menuName = "Singletons/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField]
        private string _gameVersion = "0.0.1";

        [SerializeField]
        private string _defaultLanguage  = "English";

        [SerializeField]
        private string _databaseConnectionString = "mongodb://mongo:root123@127.0.0.1:27017";


        private string _playerId = "";

        public string GameVersion
        {
            get { return _gameVersion; }
            set
            {
                _gameVersion = value;
            }
        }

        public string DefaultLanguage
        {
            get { return _defaultLanguage; }
            set
            {
                _defaultLanguage = value;
            }
        }

        public string DatabaseConnectionString
        {
            get { return _databaseConnectionString; }
            set
            {
                _databaseConnectionString = value;
            }
        }


        public string PlayerId
        {
            get { return _playerId; }
            set
            {
                _playerId = value;
            }
        }

        [SerializeField] private string _nickName = "PunFish";

        public string RoomName
        {
            get
            {
                int value = Random.Range(0, 9999);
                return _nickName + value.ToString();
            }
        }

    }

}