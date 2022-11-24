using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace ECS_Project
{
    public static class Utils
    {
        public static List<Collider> GetAllColliders(this GameObject go)
        {
            return go == null ? null : go.GetComponents<Collider>().ToList();
        }

        public static void ToWorldSpaceBox(this BoxCollider box, out float3 center,out float3 halfExtents, out quaternion orientation)
        {
            Transform transform = box.transform;
            orientation = transform.rotation;
            center = transform.TransformPoint(box.center);
            var lossyScale = transform.localScale;
            var scale = Abs(lossyScale);
            halfExtents = Vector3.Scale(scale,box.size);
        }

        public static void ToWorldSpaceCapsule(this CapsuleCollider capsule, out float3 point0, out float3 point1, out float radius)
        {
            Transform transform = capsule.transform;
            var center = (float3)transform.TransformPoint(capsule.center);
            radius = 0f;
            float height = 0f;
            float3 lossyScale = transform.lossyScale;
            float3 dir = float3.zero;

            switch (capsule.direction)
            {
                case 0://x
                    radius = Mathf.Max(lossyScale.y,lossyScale.z)* capsule.radius;
                    height = lossyScale.x * capsule.height;
                    dir = capsule.transform.TransformDirection(Vector3.right);
                    break;
                case 1://y
                    radius = Mathf.Max(lossyScale.x, lossyScale.z) * capsule.radius;
                    height = lossyScale.y * capsule.height;
                    dir = capsule.transform.TransformDirection(Vector3.up);
                    break;
                case 2://z
                    radius = Mathf.Max(lossyScale.x, lossyScale.y) * capsule.radius;
                    height = lossyScale.z * capsule.height;
                    dir = capsule.transform.TransformDirection(Vector3.forward);
                    break;
            }

            if (height < radius * 2f)
            {
                dir = Vector3.zero;
            }

            point0 = center + dir *(height *0.5f - radius);
            point1 = center - dir *(height *0.5f - radius);
        }

        public static void ToWorldSpaceSphere(this SphereCollider sphere, out float3 center, out float radius)
        {
            Transform transform = sphere.transform;
            center = transform.TransformPoint(sphere.center);
            radius = sphere.radius * Max(Abs(transform.lossyScale));
        }
        public static float Max(float3 value) => Mathf.Max(value.x,Mathf.Max(value.y,value.z));
        public static float3 Abs(float3 value) => new float3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
    }
}
