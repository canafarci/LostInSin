using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Characters.StateMachine;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Characters.StateMachine.States;
using LostInSin.Identifiers;
using LostInSin.Movement;
using LostInSin.Raycast;
using LostInSin.Visuals;
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
            Container.Bind<Vector3>().FromInstance(_startPosition);

            Container.Bind<Transform>().FromComponentsOnRoot().AsSingle();
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterVisualsVO>().FromComponentsOnRoot().AsSingle();

            DeclareSignals();

            Container.Bind<IMover>().To<Mover>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimationStateChanger>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterVisualChanger>().AsSingle().NonLazy();
            Container.Bind<CharacterStateRuntimeData>().AsSingle();

            BindStates();
            InitializeStates();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignalWithInterfaces<StateChangeSignal>();
            Container.DeclareSignalWithInterfaces<AnimationChangeSignal>();
            Container.DeclareSignalWithInterfaces<StateAndAnimationChangeSignal>();
            Container.DeclareSignalWithInterfaces<SelectionChangeSignal>();
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

            Container.Bind<IState>()
                     .WithId(CharacterStates.IdleState)
                     .To<IdleState>()
                     .AsSingle();

            Container.Bind<IState>()
                     .WithId(CharacterStates.InitialSelectionState)
                     .To<InitialSelectionState>()
                     .AsSingle();
        }

        private void InitializeStates()
        {
            MoveState moveState = Container.ResolveId<IState>(CharacterStates.MoveState) as MoveState;
            moveState.Initialize();
        }
    }
}