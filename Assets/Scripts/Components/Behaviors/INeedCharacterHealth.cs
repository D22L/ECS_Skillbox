using Unity.Entities;

namespace ECS_Project
{
    public interface INeedCharacterHealth
    {
        HealthComponent HealthComponent { get; set; }
    }
}