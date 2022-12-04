using Unity.Entities;

namespace ECS_Project
{
    public class CharacterAnimStateSystem : ComponentSystem
    {
        private EntityQuery _entityQuery;
        private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;
        private EntityCommandBuffer _commandBuffer;
        protected override void OnStartRunning()
        {
            _endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            

            _entityQuery = GetEntityQuery(
                ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<AnimStateData>()
                );


            Entities.With(_entityQuery).ForEach((Entity entity, AnimStateManager animManager, HealthComponent health) =>
            {
                health.onDead += () => OnDead(entity, animManager, health);
            });
        }

        private void OnDead(Entity entity, AnimStateManager animManager, HealthComponent health)
        {
            _commandBuffer = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer();
            health.onDead -= () => OnDead(entity, animManager, health);
            _commandBuffer.DestroyEntity(entity);
            animManager.Play<DieAnimStateComponent>();           
        }

        protected override void OnUpdate()
        {
            Entities.With(_entityQuery).ForEach((Entity entity, AnimStateManager animManager, ref InputData inputData) =>
            {
                if (inputData.isMove)
                {
                    if (inputData.shoot > 0) animManager.Play<ShootAnimStateComponent>();
                    else animManager.Play<WalkAnimStateComponent>();
                }
                else
                {
                    if (inputData.shoot > 0) animManager.Play<StayShootAnimStateComponent>();
                    else animManager.Play<IdleAnimStateComponent>();
                }


            });
        }
    }
}
