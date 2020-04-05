using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CoinFactory>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerControl>().FromComponentInHierarchy().AsSingle();
           
        Debug.Log("Success");
    }
}
