using Unity.Entities;
using UnityEngine;

namespace ECS_Project
{
    public class CharacterShootSystem : ComponentSystem
    {
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(
                ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<ShootData>(),                
                ComponentType.ReadOnly<UserInputData>());      
        }

        protected override void OnUpdate()
        {   
            Entities.With(_query).ForEach((Entity entity, UserInputData userInputData, ref InputData inputData) =>
            {
                if (inputData.shoot > 0f && userInputData.ShootingBehavior != null && userInputData.ShootingBehavior is IAbility ability)
                {
                    ability.Execute();                    
                }
            });
            
        }

    }
}
