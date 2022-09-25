using Features.Customers.Scripts.Factory;
using Features.StaticData.LevelArea;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
  public class NpcSceneBootstrapp : MonoInstaller
  {
    [SerializeField] private LevelStaticData levelStaticData;

    private NPCFactory npcFactory;

    public override void Start()
    {
      base.Start();
      ResolveNPCObserver();
    }

    public override void InstallBindings()
    {
      BindNPCFactory();
      BindNPCObserver();
    }


    private void BindNPCFactory() => 
      Container.Bind<NPCFactory>().To<NPCFactory>().FromNew().AsSingle();

    private void BindNPCObserver() => 
      Container.Bind<NPCObserver>().To<NPCObserver>().FromNew().AsSingle().WithArguments(levelStaticData.NPCs);

    private void ResolveNPCObserver() => 
      Container.Resolve<NPCObserver>();
  }
}