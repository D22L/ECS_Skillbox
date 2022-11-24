using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    public class LeapForwardAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _countdown;

        private float _currentTime = float.MinValue;
        public void Execute()
        {
            if (Time.time < _currentTime + _countdown) return;
            transform.position = transform.position + transform.forward.normalized * _distance;
            _currentTime = Time.time;
        }
    }
}
