using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class MoveComponent : MonoBehaviour,IConvertGameObjectToEntity
    {
        [field: SerializeField] public float Speed { get; private set; }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData<MoveData>(entity, new MoveData(Speed));
        }
    }

    public struct MoveData : IComponentData
    {
        public float Speed { get; private set; }
        public MoveData(float speedValue)
        {
            Speed = speedValue;
        }
    }
}