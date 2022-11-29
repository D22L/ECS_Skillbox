using System;
using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

namespace ECS_Project
{
    public class PlayerStatsComponent : MonoBehaviour, IConvertGameObjectToEntity
    {        
        public PlayerStatsData playerStatsData = new PlayerStatsData();
        public string PlayerName { get; set; }
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, playerStatsData);
        }
        public void AddCoin(int value)
        {
            playerStatsData.CoinValue += value;
            Debug.Log(playerStatsData.CoinValue);
        }       
    }
    public struct PlayerStatsData : IComponentData
    {
        public int CoinValue;
    }

    [Serializable]
    public struct PlayerDatabase
    {
        [SerializeField] private List<SavedPlayerStatsData> playersList;
     
        public void AddNewUser(SavedPlayerStatsData playerStatsData)
        {
            if(playersList==null) playersList = new List<SavedPlayerStatsData> ();           
            playersList.Add(playerStatsData);
        }

        public void UpdatePlayerData(SavedPlayerStatsData playerStatsData)
        {
            var index = playersList.FindIndex(x => x.DevaceID == SystemInfo.deviceUniqueIdentifier);
            if (index != -1) playersList[index] = playerStatsData;
        }
        public bool HavePlayer(string deviceID)
        {
            var _playerData = playersList.Find(x=>x.DevaceID == deviceID);
            return !_playerData.Equals(default(SavedPlayerStatsData));           
        }

        public SavedPlayerStatsData GetPlayerStats(string deviceID)
        {
            return playersList.Find(x => x.DevaceID == deviceID);            
        }
    }

    [Serializable]
    public struct SavedPlayerStatsData
    {
        public string PlayerName; 
        public int CoinValue;
        public string DevaceID;
    }
 
}
