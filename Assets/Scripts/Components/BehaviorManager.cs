using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class BehaviorManager : MonoBehaviour, IConvertGameObjectToEntity
    {
        [field: SerializeField] public List<BaseBehavior> Behaviors { get; private set; }

        public IBehave CurrentBehavior { get; set; }
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new AIAgent());
        }
    }

    public struct AIAgent : IComponentData
    {

    }
}
