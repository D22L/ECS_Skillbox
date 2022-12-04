using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class AIBehaveSystem : ComponentSystem
    {
        private EntityQuery _entityQuery;

        protected override void OnCreate()
        {
            _entityQuery = GetEntityQuery(
                ComponentType.ReadOnly<AIAgent>()
                );        
        }
        protected override void OnUpdate()
        {
            Entities.With(_entityQuery).ForEach((Entity entity, BehaviorManager behaviorManager) => {

                behaviorManager.CurrentBehavior?.Behave();
                Debug.Log(behaviorManager?.CurrentBehavior);
            });
        }
    }
}
