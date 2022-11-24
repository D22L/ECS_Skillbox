using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using System;

namespace ECS_Project
{
    public class CollisionSystem : ComponentSystem
    {
        private EntityQuery _query;
        private Collider[] _results = new Collider[50]; 
        protected override void OnCreate()
        {
            _query = GetEntityQuery(
                ComponentType.ReadOnly<ActorColliderData>(),
                ComponentType.ReadOnly<Transform>()
                );
        }
        protected override void OnUpdate()
        {
            Entities.With(_query).ForEach((Entity entity, CollisionAbility ability, ref ActorColliderData colliderData) => {

                var go = ability.gameObject;
                float3 position = ability.transform.position;
                var rotation = ability.transform.rotation;

                int size = 0;

                switch (colliderData.colliderType)
                {
                    case ColliderType.Sphere:
                        size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, colliderData.SphereRadius, _results);
                        break;
                    case ColliderType.Capsule:
                        var center = (colliderData.CapsuleStart + position + colliderData.CapsuleEnd + position) / 2f;
                        var point1 = colliderData.CapsuleStart + position;
                        var point2 = colliderData.CapsuleEnd + position;
                        point1 = (float3)(rotation * (point1 - center)) + center;
                        point2 = (float3)(rotation * (point2 - center)) + center;
                        size = Physics.OverlapCapsuleNonAlloc(point1, point2,colliderData.CapsuleRadius, _results);
                        break;

                    case ColliderType.Box:
                        size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtents, _results, colliderData.BoxOrientation * rotation);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (size > 0)
                {
                    ability.Execute();
                }

            });
        }
    }
}
