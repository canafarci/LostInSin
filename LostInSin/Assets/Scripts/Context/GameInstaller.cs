using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Control;
using LostInSin.Grid;
using LostInSin.Input;
using LostInSin.Raycast;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private GameObject _characterPrefab;

        public override void InstallBindings()
        {
            InitExecutionOrder();

            Container.BindInterfacesAndSelfTo<GameInput>()
                .AsSingle().NonLazy();

            Container.BindFactory<Vector3, Character, Character.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<CharacterInstaller>(_characterPrefab);

            Container.BindInterfacesAndSelfTo<CharacterSpawner>()
                .AsSingle();

            Container.Bind<AnimationHashes>().AsSingle();

            Container.BindInterfacesAndSelfTo<CharacterStateTicker>().AsSingle().NonLazy();

            Container.Bind<IComponentRaycaster<Character>>().To<ComponentRaycaster<Character>>().AsSingle();

            Container.Bind<IRayDrawer>().To<MousePositionRayDrawer>().AsSingle();
            Container.Bind<IPositionRaycaster>().To<MousePositionRaycaster>().AsSingle();

            BindGrid();
        }

        private void BindGrid()
        {
            Container.Bind<GridModel>().AsSingle();
            Container.Bind<GridPositionConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridGenerator>().AsSingle().NonLazy();
            //bind visuals
            Container.Bind<GridMeshGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridMeshDisplayService>().AsSingle().NonLazy();
        }

        void InitExecutionOrder()
        {
            Container.BindExecutionOrder<GridMeshDisplayService>(2000);
        }
    }
}