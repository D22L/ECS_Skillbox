using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class SelfDestructionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbilityTarget
    {
        public SelfDestructionData SelfDestructionData;

        public List<GameObject> Targets { get ; set; }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            SelfDestructionData = new SelfDestructionData();
            dstManager.AddComponentData(entity, SelfDestructionData);
        }

        public void Execute()
        {
            foreach (var target in Targets)
            {
                if (target.TryGetComponent(out BulletComponent bulletComponent)) continue;
                SelfDestructionData.IsAwaitsDestruction = true;
            }
        }
    }

    public struct SelfDestructionData: IComponentData
    {
        public bool IsAwaitsDestruction;
    }
}
