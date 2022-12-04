using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;
namespace ECS_Project
{
    public class AIMeleeAttackBehavior : BaseBehavior, INeedCharacterHealth, INeedCharacterTransform
    {
        [SerializeField] private NavMeshAgent _navAgent;
        [SerializeField] private AnimStateManager _animStateManager;
        public HealthComponent HealthComponent { get ; set ; }
        public Transform CharacterTransform { get; set; }

        public override void Behave()
        {
            _animStateManager.Play<MeleeAttackAnimStateComponent>();
        }

        public override float Evaluate()
        {
            if(HealthComponent == null || CharacterTransform == null) return 0f;
            var dist = Vector3.Distance(_navAgent.destination, transform.position);
            if (dist <= _navAgent.stoppingDistance)
            {
                return 1f;
            }
            else return 0;
        }
    }
}
