using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public abstract class BaseBehavior : MonoBehaviour, IBehave
    {
        public virtual void Behave() { }
        public virtual float Evaluate() { return 0f; }

    }
}
