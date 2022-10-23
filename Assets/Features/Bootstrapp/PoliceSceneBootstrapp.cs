using Features.Police.Scripts.Factory;
using Features.Police.Scripts.Observer;
using Features.StaticData.LevelArea;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
  public class PoliceSceneBootstrapp : MonoInstaller
  {
    [SerializeField] private LevelStaticData levelStaticData;
    [SerializeField] private Transform policeSpawnParent;

    public override void Start()
    {
      base.Start();
      Container.Resolve<PoliceObserver>().Spawn(levelStaticData.Polices);
    }

    public override void InstallBindings()
    {
      BindPoliceFactory();
      BindPoliceObserver();
    }

    private void BindPoliceFactory() => 
      Container.Bind<PoliceFactory>().ToSelf().FromNew().AsSingle().WithArguments(policeSpawnParent);
    
    private void BindPoliceObserver() => 
      Container.Bind<PoliceObserver>().ToSelf().FromNew().AsSingle();
  }
}