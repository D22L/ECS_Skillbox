using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{   
    public class BulletComponent: MonoBehaviour
    {
        [SerializeField] private float lifetime = 2f;
        [SerializeField] private float force = 2f;
        [SerializeField] private Rigidbody _rb;

        public bool CanRicochet { get; set; }
        private Vector3 _direction;
        private void OnEnable()
        {
            Destroy(this.gameObject, lifetime);
        }

        public void SetDirection(Vector3 dir)
        {
            _direction = dir;
            _rb.velocity = _direction * force;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out WallComponent wall))
            {                
                if (TryGetComponent(out RicochetAbility ricochetAbility))
                {
                    var wallnormal = collision.contacts[0].normal;
                    _rb.velocity = Vector3.Reflect(_direction, wallnormal) * force;
                }
                else Destroy(this.gameObject);
            }
            
        }
    }

    
    public struct BulletData : IComponentData
    { 
        
    }
}
