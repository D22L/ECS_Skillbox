using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public class AddHealth : MonoBehaviour,IAbilityTarget
    {
        [SerializeField] private int _health;

        public List<GameObject> Targets { get; set; }

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
