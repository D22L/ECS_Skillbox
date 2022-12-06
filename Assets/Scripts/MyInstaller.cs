using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("INJECT");
        Container.Bind<GreetMe>().AsSingle().NonLazy();
    }
}

public class GreetMe
{

    public GreetMe(string message)
    {
        Debug.Log(message);
    }
}