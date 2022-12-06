using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Project
{
    [CreateAssetMenu(fileName = "GlobalGameSettings", menuName = "Configs/GlobalGameSettings")]
    public class GlobalGameSettingsConfig : ScriptableObject, IGameSettings
    {
        [field: SerializeField] public int DefaultHealthReward { get; private set; }
        [field: SerializeField] public int DefaultCoinReward { get; private set; }
    }

    public class CustomGameSettings: IGameSettings
    {
        public int DefaultHealthReward { get; } = 1;

        public int DefaultCoinReward { get; } = 3;
    }

    public interface IGameSettings
    {
        int DefaultHealthReward { get; }
        int DefaultCoinReward { get; }
    }
}
