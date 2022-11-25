using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;

        public int CurrentHealth { get; private set; }
        private void Awake()
        {
            CurrentHealth = _maxHealth;
        }
        public void TakeDamage(int damage)
        {
            if (CurrentHealth >= damage)
            {
                CurrentHealth -= damage;
                CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
                Debug.Log($"{name}:{CurrentHealth}");
            }
            else Debug.Log($"{name} is died");
        }

        public void AddHealth(int bonusValue)
        {
            CurrentHealth += bonusValue;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
            Debug.Log($"{name}:{CurrentHealth}");
        }
    }
}
