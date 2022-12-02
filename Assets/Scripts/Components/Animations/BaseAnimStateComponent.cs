using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public abstract class BaseAnimStateComponent : MonoBehaviour
    {
        [field: SerializeField] public string AnimStateName { get; private set; }
        [SerializeField] protected float transitionDuration = 0.1f;
        public void Play(Animator animator)
        {
            animator.CrossFade(AnimStateName, transitionDuration);
        }
    }

   
}
