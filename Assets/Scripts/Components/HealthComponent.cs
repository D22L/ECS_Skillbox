using System;
using UnityEngine;

namespace ECS_Project
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;

        public int CurrentHealth { get; private set; }
        public int Max => _maxHealth;

        public Action onDead;
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
                if(CurrentHealth == 0) onDead?.Invoke();
                Debug.Log($"{name}:{CurrentHealth}");
            }            
        }

        public void AddHealth(int bonusValue)
        {
            CurrentHealth += bonusValue;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
            Debug.Log($"{name}:{CurrentHealth}");
        }
    }
}
