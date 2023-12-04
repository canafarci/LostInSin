using LostInSin.Characters;
using LostInSin.Characters.StateMachine;
using LostInSin.Movement;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    public class CharacterInstaller : Installer<CharacterInstaller>
    {
        [Inject] private Vector3 _startPosition;

        public override void InstallBindings()
        {
            Container.Bind<Character>().AsSingle();

            Container.Bind<Transform>().FromComponentsOnRoot();
            Container.Bind<Vector3>().FromInstance(_startPosition);

            Container.Bind<IMover>().To<Mover>().AsSingle();

            BindStates();
        }

        private void BindStates()
        {
            Container.BindInterfacesAndSelfTo<CharacterStateMachine>()
                .AsSingle()
                .NonLazy();

            Container.Bind<IState>()
                .WithId(CharacterState.WaitState)
                .To<WaitState>()
                .AsSingle();

            Container.Bind<IState>()
                .WithId(CharacterState.MoveState)
                .To<MoveState>()
                .AsSingle();
        }
    }
}

