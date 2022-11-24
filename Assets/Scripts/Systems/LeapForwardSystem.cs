using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class LeapForwardSystem : ComponentSystem
    {
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(
                 ComponentType.ReadOnly<InputData>(),                 
                 ComponentType.ReadOnly<LeapForwardData>(),                 
                 ComponentType.ReadOnly<UserInputData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_query).ForEach((Entity entity, UserInputData userInputData, ref InputData inputData) =>
            {
                if (inputData.leapForward > 0f && userInputData.LeapForwardBehavior != null && userInputData.LeapForwardBehavior is IAbility ability)
                {
                    ability.Execute();
                }
            });
        }
    }
}
