using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS_Project
{
    public class ShootAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private BulletComponent _pfbBullet;
        [SerializeField] private float _shootDelay;

        private float _shootTime = float.MinValue;

        public void Execute()
        {
            if (Time.time < _shootTime + _shootDelay) return;

            
            if (_pfbBullet != null)
            {
               var newBullet =  Instantiate(_pfbBullet,transform.position, transform.rotation);
                newBullet.SetDirection(transform.forward);
            }
            else
            {
                Debug.LogError("pfb Bullet is null");
            }
            _shootTime = Time.time;
        }
       
    }
}
