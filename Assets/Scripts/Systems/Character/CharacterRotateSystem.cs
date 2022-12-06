using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class CharacterRotateSystem : ComponentSystem
    {
        private EntityQuery _query;

        protected override void OnStartRunning()
        {
            _query = GetEntityQuery(
                ComponentType.ReadOnly<RotateData>(),
                ComponentType.ReadOnly<InputData>()
                );
        }

        protected override void OnUpdate()
        {
            Entities.With(_query).ForEach((Entity entity, Transform transform, ref InputData inputData, ref RotateData rotateData ) => {

                if (inputData.isMove)
                {
                    var targetRotate = Quaternion.LookRotation(inputData.moveV3, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, rotateData.TurnSpeed * Time.DeltaTime);
                }
                 
            });
        }
    }
}
