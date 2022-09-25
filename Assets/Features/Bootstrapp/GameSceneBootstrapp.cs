using Features.Animation;
using Features.Player.Scripts.Base;
using Features.Player.Scripts.HeroMachine;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.Move;
using Features.Player.Scripts.Rotate;
using Features.Services.Coroutine;
using Features.Services.Input;
using Features.StaticData.Hero.AnimationTransitions;
using Features.StaticData.Hero.Move;
using Features.StaticData.Hero.Rotate;
using InputControl;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
    public class GameSceneBootstrapp : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private GameObject hero;
        [SerializeField] private Transform spawnHeroPoint;
        [SerializeField] private HeroMoveStaticData moveData;
        [SerializeField] private HeroRotateStaticData rotateData;
        [SerializeField] private HeroAnimationsTransitionStaticData transitionStaticData;

        public override void InstallBindings()
        {
            BindInput();
            CreateHero();
        }

        private void BindInput()
        {
            Container.Bind<IInputService>().To<InputService>().FromNew().AsSingle().WithArguments(new HeroControl());
        }

        private void CreateHero()
        {
            GameObject spawnedHero = Container.InstantiatePrefab(hero, spawnHeroPoint.position, Quaternion.Euler(0,0,0), null);

            HeroRotate rotate = HeroRotate(spawnedHero);
            HeroMove move = HeroMove(rotate, spawnedHero);
            
            
            ChangeableParametersAnimator animator = spawnedHero.GetComponentInChildren<ChangeableParametersAnimator>(true);
            animator.Initialize();
            

            HeroStatesContainer container = new HeroStatesContainer(spawnedHero.GetComponent<HeroStateMachineObserver>(), 
                move, animator, transitionStaticData);
            
            spawnedHero.GetComponent<Hero>().Construct(container);
        }

        private HeroRotate HeroRotate(GameObject spawnedHero) => 
            new HeroRotate(spawnedHero.transform, rotateData);

        private HeroMove HeroMove(HeroRotate rotate, GameObject spawnedHero) => 
            new HeroMove(spawnedHero.transform, moveData, rotate, spawnedHero.GetComponent<Rigidbody2D>());
        
    }
}
