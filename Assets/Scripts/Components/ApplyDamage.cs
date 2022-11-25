using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public class ApplyDamage : MonoBehaviour, IAbilityTarget
    {
        [SerializeField] private int _damage;
        public List<GameObject> Targets { get; set; }

        public void Execute()
        {
            foreach (var target in Targets)
            {
                if (target.TryGetComponent(out HealthComponent healthComponent))
                {
                    healthComponent.TakeDamage(_damage);
                }
            }
        }
    }
}
