using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ECS_Project
{
    public class AddHealth : MonoBehaviour,IAbilityTarget
    {
        private int _health;

        [Inject] private IGameSettings _gameSettings;
        public List<GameObject> Targets { get; set; }

        private void Awake()
        {
            _health = _gameSettings.DefaultHealthReward;
        }
        public void Execute()
        {
            foreach (var item in Targets)
            {
                if (item.TryGetComponent(out HealthComponent healthComponent))
                {
                    healthComponent.AddHealth(_health);
                }
            }
        }

       
    }
}
