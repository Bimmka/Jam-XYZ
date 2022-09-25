using Features.Services.Assets;
using Features.Services.Coroutine;
using Zenject;

namespace Features.Bootstrapp
{
    public class GameSceneBootstrapp : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
        }
        private void BindAssetProvider() => 
            Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();
    }
}
