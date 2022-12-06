using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ECS_Project
{
    public class AddCoinAbility : MonoBehaviour, IAbilityTarget
    {
        private int count = 10;
        [Inject]private IGameSettings _gameSettings;

        private void Awake()
        {
            count = _gameSettings.DefaultCoinReward;
        }
        public List<GameObject> Targets { get; set; }

        public void Execute()
        {
            foreach (var target in Targets)
            {
                if (target.TryGetComponent(out PlayerStatsComponent playerStatsComponent))
                {
                    playerStatsComponent.AddCoin(count);
                }
            }
        }
    }
}
