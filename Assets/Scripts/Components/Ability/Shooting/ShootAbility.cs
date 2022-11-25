using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using System.Collections.Generic;
using System;

namespace ECS_Project
{
    public class ShootAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private BulletComponent _pfbBullet;
        [SerializeField] private Transform _gunPoint;
        [SerializeField] private float _shootDelay;

        private float _shootTime = float.MinValue;
        private List<Type> abilities = new List<Type>();

        public void AddAbilityComponent(Type className)
        {
            abilities.Add(className);
        }
        void AddOneScript(GameObject go, Type scriptType)
        {
            go.AddComponent(scriptType);
        }

        public void Execute()
        {
            if (Time.time < _shootTime + _shootDelay) return;

            
            if (_pfbBullet != null)
            {
               var newBullet =  Instantiate(_pfbBullet, _gunPoint.position, transform.rotation);                
                newBullet.SetDirection(transform.forward);
                abilities.ForEach(x => AddOneScript(newBullet.gameObject,x));
            }
            else
            {
                Debug.LogError("pfb Bullet is null");
            }
            _shootTime = Time.time;
        }
       
    }
}
