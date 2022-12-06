using UnityEngine;
using Zenject;
using ECS_Project;

public class GameSettingsInstaller : MonoInstaller
{
    [SerializeField] private bool _useCustomSettings;
    [SerializeField] private GlobalGameSettingsConfig _gameSettingsConfig;
    
    public override void InstallBindings()
    {
        if (!_useCustomSettings) Container.Bind<IGameSettings>().To<GlobalGameSettingsConfig>().FromScriptableObject(_gameSettingsConfig).AsSingle().NonLazy();
        else Container.Bind<IGameSettings>().To<CustomGameSettings>().AsSingle().NonLazy();
    }
}