using Cysharp.Threading.Tasks;
using LostInSin.Abilities;
using LostInSin.AbilitySystem;
using LostInSin.Animation;
using LostInSin.Attributes;
using LostInSin.Characters;
using LostInSin.Characters.PersistentData;
using LostInSin.Characters.StateMachine;
using LostInSin.Characters.StateMachine.States;
using LostInSin.Identifiers;
using LostInSin.Movement;
using LostInSin.Signals;
using LostInSin.Signals.Animations;
using LostInSin.Signals.Characters.States;
using LostInSin.Signals.Characters.Visuals;
using LostInSin.Visuals;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    public class CharacterInstaller : Installer<CharacterInstaller>
    {
        private Vector3 _startPosition;
        private CharacterPersistentData _characterPersistentData;

        private CharacterInstaller(CharacterPersistentData characterPersistentData, Vector3 startPosition)
        {
            _characterPersistentData = characterPersistentData;
            _startPosition = startPosition;
        }

        public override void InstallBindings()
        {
            Container.InstantiatePrefab(_characterPersistentData.CharacterPrefab);

            Container.Bind<Character>().FromNewComponentOnRoot().AsSingle();
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterVisualsVO>().FromComponentInHierarchy().AsSingle();

            Container.Bind<Vector3>().FromInstance(_startPosition);
            Container.Bind<CharacterPersistentData>().FromInstance(_characterPersistentData);

            DeclareSignals();

            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IMover>().To<Mover>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterVisualChanger>().AsSingle().NonLazy();
            Container.Bind<CharacterStateRuntimeData>().AsSingle();

            BindAnimations();
            BindStates();
            BindAttributeSystem();
        }

        private void BindAnimations()
        {
            Container.BindInterfacesAndSelfTo<AnimationStateChanger>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<AnimationReference>()
                     .FromComponentsInHierarchy()
                     .AsSingle()
                     .NonLazy();
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
                     .WithId(CharacterStates.UseAbilityState)
                     .To<UseAbilityState>()
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

        private void BindAttributeSystem()
        {
            Container.Bind<AbilitySet>().AsSingle();
            Container.Bind<AttributeSet>().AsSingle();

            Container.Bind<IAttribute>()
                     .WithId(AttributeIdentifiers.Health)
                     .To<HealthAttribute>()
                     .AsSingle();

            Container.Bind<IAttribute>()
                     .WithId(AttributeIdentifiers.Power)
                     .To<PowerAttribute>()
                     .AsSingle();

            Container.Bind<IAttribute>()
                     .WithId(AttributeIdentifiers.Resilience)
                     .To<ResilienceAttribute>()
                     .AsSingle();

            Container.Bind<IAttribute>()
                     .WithId(AttributeIdentifiers.Luck)
                     .To<LuckAttribute>()
                     .AsSingle();

            Container.Bind<IAttribute>()
                     .WithId(AttributeIdentifiers.ActionPoints)
                     .To<ActionPointAttribute>()
                     .AsSingle();
        }
    }
}