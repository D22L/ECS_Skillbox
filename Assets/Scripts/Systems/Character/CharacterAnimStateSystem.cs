using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class CharacterAnimStateSystem : ComponentSystem
    {
        private EntityQuery _entityQuery;

        protected override void OnStartRunning()
        {
            _entityQuery = GetEntityQuery(
                ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<AnimStateData>()
                );
        }

        protected override void OnUpdate()
        {
            Entities.With(_entityQuery).ForEach((Entity entity, AnimStateManager animManager, ref InputData inputData) => {
                if (inputData.isMove) animManager.Play<WalkAnimStateComponent>();
                else animManager.Play<IdleAnimStateComponent>();
            });
        }
    }
}
