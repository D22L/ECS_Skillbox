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
