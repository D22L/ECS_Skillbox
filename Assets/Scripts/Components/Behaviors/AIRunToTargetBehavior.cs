using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

namespace ECS_Project
{
    public class AIRunToTargetBehavior : BaseBehavior, INeedCharacterTransform, INeedCharacterHealth
    {        
        [SerializeField] private NavMeshAgent _navAgent;
        [SerializeField] private AnimStateManager _animStateManager;
        private Transform _characterTarget;
        public Transform CharacterTransform { 
            get { return _characterTarget; }
            set
            {
                _characterTarget = value;
                _navAgent.SetDestination(value.position);
            }
        }
        public HealthComponent HealthComponent { get; set; }

        public override void Behave()
        {
            _navAgent.isStopped = false;
            _navAgent.SetDestination(CharacterTransform.position);
            _animStateManager.Play<WalkAnimStateComponent>();
        }

        public override float Evaluate()
        {
            if (HealthComponent == null || CharacterTransform == null) return 0f;
            else
            {
                var dist = Vector3.Distance(_navAgent.transform.position, _navAgent.destination);
                if (dist > _navAgent.stoppingDistance) 
                    return 1f;
            }

            return 0;
        }
    }
}
