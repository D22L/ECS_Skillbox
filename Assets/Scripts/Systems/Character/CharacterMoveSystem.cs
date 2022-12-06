using Unity.Entities;
using UnityEngine;

namespace ECS_Project
{
    public class CharacterMoveSystem : ComponentSystem
    {
        private EntityQuery _query;
        protected override void OnCreate()
        {
            _query = GetEntityQuery(
                ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<MoveData>(),                
                ComponentType.ReadOnly<Transform>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_query).ForEach((Entity entity, Transform transform, ref InputData inputData, ref MoveData moveData) =>
            {
                var pos = transform.position;               
                pos += new Vector3(inputData.move.x, 0, inputData.move.y) * moveData.Speed * Time.DeltaTime;
                transform.position = pos;              
            });
            
        }

    }
}
