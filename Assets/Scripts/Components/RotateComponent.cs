using Unity.Entities;
using UnityEngine;

namespace ECS_Project
{
    public class RotateComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float _turnSpeed;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData<RotateData>(entity, new RotateData(_turnSpeed));
        }
    }

    public struct RotateData : IComponentData
    {
        public float TurnSpeed { get; private set; }
        public RotateData(float turnSpeed)
        {
            TurnSpeed = turnSpeed;
        }

    }
}
