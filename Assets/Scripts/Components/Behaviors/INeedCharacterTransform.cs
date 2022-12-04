using Unity.Entities;
using UnityEngine;

namespace ECS_Project
{
    internal interface INeedCharacterTransform
    {
        public Transform CharacterTransform { get; set; }
    }
}