using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Characters.StateMachine;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Identifiers;
using LostInSin.Movement;
using LostInSin.Raycast;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    public class CharacterInstaller : Installer<CharacterInstaller>
    {
        [Inject] private Vector3 _startPosition;

        public override void InstallBindings()
        {
            Container.Bind<Character>().FromNewComponentOnRoot().AsSingle();

            Container.Bind<Transform>().FromComponentsOnRoot();
            Container.Bind<Vector3>().FromInstance(_startPosition);
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();

            DeclareSignals();

            Container.Bind<IMover>().To<Mover>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimationStateChanger>().AsSingle().NonLazy();
            Container.Bind<CharacterStateRuntimeData>().AsSingle();

            BindStates();
            InitializeStates();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignalWithInterfaces<StateChangeSignal>();
            Container.DeclareSignalWithInterfaces<AnimationChangeSignal>();
            Container.DeclareSignalWithInterfaces<StateAndAnimationChangeSignal>();
        }

        private void BindStates()
        {
            Container.BindInterfacesAndSelfTo<CharacterStateMachine>()
                .AsSingle()
                .NonLazy();

            Container.Bind<IState>()
                .WithId(CharacterStates.InactiveState)
                .To<InactiveState>()
                .AsSingle();

            Container.Bind<IState>()
                .WithId(CharacterStates.MoveState)
                .To<MoveState>()
                .AsSingle();
        }

        private void InitializeStates()
        {
            MoveState moveState = Container.ResolveId<IState>(CharacterStates.MoveState) as MoveState;
            moveState.Initialize();
        }
    }
}

