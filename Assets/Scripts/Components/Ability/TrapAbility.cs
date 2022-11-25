using UnityEngine;

namespace ECS_Project
{
    public class TrapAbility : CollisionAbility
    {
        [SerializeField] private int _damage;

        public void Execute()
        {
            foreach (var c in Collisions)
            {
                if (c.TryGetComponent(out HealthComponent healthComponent))
                {
                    healthComponent.TakeDamage(_damage);
                }
            }
        }

    }
}
