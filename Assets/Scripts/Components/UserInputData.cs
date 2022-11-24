using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace ECS_Project
{    
    public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
    {
        [field: SerializeField] public MonoBehaviour ShootingBehavior { get; private set; }
        [field: SerializeField] public MonoBehaviour LeapForwardBehavior { get; private set; }
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new InputData());
            dstManager.AddComponentData(entity, new MoveData());

            if (ShootingBehavior != null && ShootingBehavior is IAbility) dstManager.AddComponentData(entity, new ShootData());
            else Debug.LogError("shooting Behavior is null");

            if (LeapForwardBehavior != null && LeapForwardBehavior is IAbility) dstManager.AddComponentData(entity, new LeapForwardData());
            else Debug.LogError("LeapForwardBehavior is null");
        }
    }

    public struct InputData : IComponentData
    {
        public float2 input;
        public float shoot;
        public float leapForward;

        public Vector3 inputV3 => new Vector3(input.x,0,input.y);
    }
    public struct ShootData : IComponentData { }
    public struct LeapForwardData : IComponentData { }
}
