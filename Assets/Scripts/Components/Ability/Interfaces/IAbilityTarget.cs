using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public interface IAbilityTarget : IAbility
    {
       List<GameObject> Targets { get; set; }
    }
}
