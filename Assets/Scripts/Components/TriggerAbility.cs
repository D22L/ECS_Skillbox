using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public class TriggerAbility : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _onTriggerEnterAction;
        private List<IAbility> _abilities = new List<IAbility>();

        private void Awake()
        {
            foreach (var action in _onTriggerEnterAction) 
            {
                if(action is IAbility) _abilities.Add((IAbility)action);
            }   
        }          

        private void OnTriggerEnter(Collider other)
        {
            foreach (var ab in _abilities)
            {
                if (ab is IAbilityTarget abilityTarget)
                {
                    abilityTarget.Targets = new List<GameObject>();
                    abilityTarget.Targets.Add(other.gameObject);
                }
                ab.Execute();
            }
        }
    }
}
