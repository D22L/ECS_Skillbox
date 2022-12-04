using Unity.Entities;

namespace ECS_Project
{
    public class AIEvaluateSystem : ComponentSystem
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
                
                float evaluate = float.MinValue;
                foreach (var behave in behaviorManager.Behaviors)
                {
                    var ev = behave.Evaluate();
                    if (ev > evaluate)
                    {
                        behaviorManager.CurrentBehavior = behave;
                        evaluate = ev;
                    }
                }

                
            });
        }
    }
}
