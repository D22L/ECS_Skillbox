using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public interface IBehave 
    {
        float Evaluate();
        void Behave();
    }
}
