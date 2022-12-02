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
        public float2 move;
        public float shoot;
        public float leapForward;

        public Vector3 moveV3 => new Vector3(move.x,0,move.y);
        public bool isMove => move.x != 0 || move.y != 0;
    }
    public struct ShootData : IComponentData { }
    public struct LeapForwardData : IComponentData { }
}
