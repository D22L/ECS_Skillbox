using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public class AddCoinAbility : MonoBehaviour, IAbilityTarget
    {
        [SerializeField] private int count = 10;
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
