using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Tilemaps;

namespace ECS_Project
{
    public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
    {
        [field: SerializeField] public Collider collider { get; private set; }
        public List<Collider> Collisions { get; set; } = new List<Collider>();
        [field: SerializeField] public List<MonoBehaviour> collisionAction { get; set; } = new List<MonoBehaviour>();
        private List<IAbility> collisionAbilities = new List<IAbility>();
        private void Start()
        {
            foreach (MonoBehaviour a in collisionAction)
            {
                if (a is IAbility ability) collisionAbilities.Add(ability);
                else Debug.LogError("collision action must derive from IAbilityTarget");
            }
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            float3 position = gameObject.transform.position;
            switch (collider)
            {
                case SphereCollider sphere:
                    sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData()
                    {
                        colliderType = ColliderType.Sphere,
                        SphereCenter = sphereCenter - position,
                        SphereRadius = sphereRadius,
                        initialTakeOff = true
                    });
                    break;

                case CapsuleCollider capsule:
                    capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData()
                    {
                        colliderType = ColliderType.Capsule,
                        CapsuleStart = capsuleStart - position,
                        CapsuleEnd = capsuleEnd - position,
                        CapsuleRadius = capsuleRadius,
                        initialTakeOff = true
                    });
                    break;
                case BoxCollider box:
                    box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtannts, out var boxOrientation);
                    dstManager.AddComponentData(entity, new ActorColliderData()
                    {
                        colliderType = ColliderType.Box,
                        BoxCenter = boxCenter - position,
                        BoxHalfExtents = boxHalfExtannts,
                        BoxOrientation = boxOrientation,
                        initialTakeOff = true
                    });
                    break;
            }
            collider.enabled = false;
        }

        public void Execute()
        {
            foreach (var a in collisionAbilities)
            {
                if (a is IAbilityTarget abilityTarget)
                {
                    abilityTarget.Targets = new List<GameObject>();
                    Collisions.ForEach(x => abilityTarget.Targets.Add(x.gameObject));
                }
                a.Execute();
            }
        }
  
    }

    public struct ActorColliderData : IComponentData
    {
        public ColliderType colliderType;
        public float3 SphereCenter;
        public float SphereRadius;
        public float3 CapsuleStart;
        public float3 CapsuleEnd;
        public float CapsuleRadius;
        public float3 BoxCenter;
        public float3 BoxHalfExtents;
        public quaternion BoxOrientation;
        public bool initialTakeOff;
    }
    public enum ColliderType
    { 
        Sphere =0,
        Capsule =1,
        Box = 2
    }
}
