using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _healt = 100;

        public void TakeDamage(int damage)
        {
            if (_healt >= damage)
            {
                _healt -= damage;
                Debug.Log($"{name}:{_healt}");
            }
            else Debug.Log($"{name} is died");
        }
    }
}
