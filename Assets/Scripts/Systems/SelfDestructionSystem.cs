using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class SelfDestructionSystem : ComponentSystem
    {
        private EntityQuery _entityQuery;
        private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;
        protected override void OnCreate()
        {
            _endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            _entityQuery = GetEntityQuery(
                ComponentType.ReadWrite<SelfDestructionData>(),
                ComponentType.ReadWrite<Transform>()
                );
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer();
            Entities.With(_entityQuery).ForEach((Entity entity, Transform transform, SelfDestructionAbility selfDestructionAbility) => {

                if (selfDestructionAbility.SelfDestructionData.IsAwaitsDestruction)
                {
                    commandBuffer.DestroyEntity(entity);
                    MonoBehaviour.Destroy(transform.gameObject);
                }
            });
                
        }
    }
}
