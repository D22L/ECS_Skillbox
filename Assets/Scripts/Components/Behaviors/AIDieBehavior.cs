using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace ECS_Project
{
    public class AIDieBehavior : BaseBehavior
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private AnimStateManager _animStateManager;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        public override void Behave()
        {
            _navMeshAgent.isStopped = true;
            _animStateManager.Play<DieAnimStateComponent>();
        }

        public override float Evaluate()
        {
            if (_healthComponent.CurrentHealth == 0) return 1f;
            else return 0f;
        }
    }
}
