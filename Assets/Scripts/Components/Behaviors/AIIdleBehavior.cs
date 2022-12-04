using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

namespace ECS_Project
{
    public class AIIdleBehavior : BaseBehavior, INeedCharacterHealth
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private AnimStateManager _stateManager;
        [SerializeField] private NavMeshAgent _agent;
        public HealthComponent HealthComponent { get; set; }

        public override void Behave()
        {
            _stateManager.Play<IdleAnimStateComponent>();
            _agent.isStopped = true;
        }

        public override float Evaluate()
        {
            if(HealthComponent ==null || _health.CurrentHealth == 0) return 0f;

            if (HealthComponent.CurrentHealth == 0) return 1;
            else return 0;
        }
    }
}
