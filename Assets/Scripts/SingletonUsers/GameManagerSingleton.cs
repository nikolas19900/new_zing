using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public sealed class GameManagerSingleton : MonoBehaviour
{
    private static GameManagerSingleton _instance;
    public static GameManagerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<GameManagerSingleton>();
                _instance.name = _instance.GetType().ToString();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    /// <summary>
    /// Active players 
    /// </summary>
    private static Dictionary<string, PlayerInfo> _players;

    private GameManagerSingleton()
    {
        if (_players == null)
        {
            _players = new Dictionary<string, PlayerInfo>();
        }

    }

    /// <summary>
    /// Get Player information. Return null if can't find any.
    /// </summary>
    /// <param name="playerId">Facebook ID</param>
    /// <returns></returns>
    private PlayerInfo FindPlayer(string playerId)
    {
        PlayerInfo playerInfo = null;
        foreach (var keyValuePair in _players)
        {
            if (keyValuePair.Key == playerId)
            {
                playerInfo = keyValuePair.Value;
                break;
            }
        }

        return playerInfo;
    }

    /// <summary>
    /// Add new PlayerInfo
    /// </summary>
    /// <param name="playerInfo">string key</param>
    public void AddPlayerInfo(PlayerInfo playerInfo)
    {
        if (FindPlayer(playerInfo.Id) == null)
        {
            _players.Add(playerInfo.Id, playerInfo);
        }
        
    }
    /// <summary>
    /// Delete PlayerInfo
    /// </summary>
    /// <param name="playerId"></param>
    public void RemovePlayerInfo(string playerId)
    {
        if (FindPlayer(playerId) != null)
        {
            _players.Remove(playerId);
        }
    }

    //public override string ToString()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    foreach (var keyValuePair in _players)
    //    {
    //        sb.Append(keyValuePair.Key);
    //        sb.Append(",");
    //        sb.Append(keyValuePair.Value.Name);
    //        sb.AppendLine("");
    //    }

    //    return sb.ToString();
    //}

    public long GetPlayerInfoId()
    {
        
        if(_players.Count == 1)
        {

            foreach (var keyValuePair in _players)
            {
                string value = keyValuePair.Key;
                return long.Parse(value);
            }
            
        }
        return 0;
    }


    public string GetPlayerName()
    {

        if (_players.Count == 1)
        {

            foreach (var keyValuePair in _players)
            {
                PlayerInfo player = keyValuePair.Value;
                string value = ""+player.Name;
                return value;
            }


        }
        return ""+0;
    }
}
