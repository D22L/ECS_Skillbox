using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class PlayerDataDisplaySystem : ComponentSystem
    {
          private EntityQuery _entityQueryForDisplay;
          private EntityQuery _entityQueryPlayerStats;
          private PlayerStatsData _playerStats;
        
          private string _playerName;
          private float _playerHealth;

          protected override void OnCreate()
          {
              _entityQueryForDisplay = GetEntityQuery(ComponentType.ReadOnly<PlayerDisplayData>());
              _entityQueryPlayerStats = GetEntityQuery(ComponentType.ReadOnly<PlayerStatsData>());
          }
        
          protected override void OnUpdate()
          {
              Entities.With(_entityQueryPlayerStats).ForEach((Entity enity, PlayerStatsComponent statsComponent, HealthComponent health) => {
                  _playerStats = statsComponent.playerStatsData;
                  _playerName = statsComponent.PlayerName;
                  _playerHealth = (float)health.CurrentHealth / (float)health.Max;
              });

              Entities.With(_entityQueryForDisplay).ForEach((Entity enity, PlayerDataDisplayComponent dispComponent) => 
              {
                  dispComponent.SetName(_playerName);
                  dispComponent.SetCoin(_playerStats.CoinValue);                
                  dispComponent.SetHealth(_playerHealth);                
              });
          }        
    }
}
