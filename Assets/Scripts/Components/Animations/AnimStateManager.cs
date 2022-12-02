using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECS_Project
{
    public class AnimStateManager : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<BaseAnimStateComponent> _listAnimStates = new List<BaseAnimStateComponent>();

        private BaseAnimStateComponent _currentAnimState;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new AnimStateData());
        }

        public void Play<T>() where T : BaseAnimStateComponent
        {
            if (_currentAnimState != null && _currentAnimState is T) return;

            foreach (var animState in _listAnimStates)
            {
                if (animState is T)
                {
                    _currentAnimState = animState;
                    _currentAnimState.Play(_animator);
                    return;
                }
            }
        }
    }

    public struct AnimStateData : IComponentData { }
}
