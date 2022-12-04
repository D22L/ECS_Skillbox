using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class DependencyDistributionSystem : ComponentSystem
    {
        private EntityQuery _playersQuery;
        private EntityQuery _aiQuery;

        private HealthComponent _playerHealthComponent;
        private Transform _playerTransform;

        protected override void OnStartRunning()
        {
            // get user data
            _playersQuery = GetEntityQuery(
                ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<HealthComponent>(),
                ComponentType.ReadOnly<Transform>()
                );
            _aiQuery = GetEntityQuery(
                ComponentType.ReadOnly<AIAgent>()
                );

            Entities.With(_playersQuery).ForEach((Entity entity, Transform transform, HealthComponent health) => {
                _playerHealthComponent = health;
                _playerTransform = transform;
            });

            
              
        }
        protected override void OnUpdate()
        {
            //set references 
            Entities.With(_aiQuery).ForEach((Entity entity, BehaviorManager behaviorManager) => {

                foreach (var behave in behaviorManager.Behaviors)
                {
                    if (behave is INeedCharacterHealth healthSeeker)
                    {
                        healthSeeker.HealthComponent = _playerHealthComponent;
                    }
                    if (behave is INeedCharacterTransform transformSeeker)
                    {
                        transformSeeker.CharacterTransform = _playerTransform;
                    }

                }
            });
        }
    }
}
