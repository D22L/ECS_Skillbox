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
        private void OnEnable()
        {
            Destroy(this.gameObject, lifetime);
        }

        public void SetDirection(Vector3 dir)
        {
            _rb.velocity = dir * force;
        }
    }

    public struct BulletData : IComponentData
    { 
        
    }
}
