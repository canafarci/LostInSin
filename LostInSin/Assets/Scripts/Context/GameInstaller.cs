using Cinemachine;
using LostInSin.Animation;
using LostInSin.Camera;
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
        [SerializeField] private CinemachineVirtualCamera _camera;

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

            BindRaycasters();
            BindGrid();
            BindCamera();
        }

        private void BindRaycasters()
        {
            Container.Bind<IComponentRaycaster<Character>>().To<ComponentRaycaster<Character>>().AsSingle();
            Container.Bind<IRayDrawer>().To<MousePositionRayDrawer>().AsSingle();
            Container.Bind<IPositionRaycaster>().To<MousePositionRaycaster>().AsSingle();
            Container.Bind<IGridRaycaster>().To<GridRaycaster>().AsSingle();
        }

        private void BindGrid()
        {
            Container.Bind<GridModel>().AsSingle();
            Container.Bind<IGridPositionConverter>().To<GridPositionConverter>().AsSingle();
            Container.Bind<IGridCellGenerator>().To<GridCellGenerator>().AsSingle();
            Container.Bind<IGridPointsGenerator>().To<GridPointsGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridGenerator>().AsSingle().NonLazy();
            //bind visuals
            Container.Bind<GridMeshGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridMeshDisplayService>().AsSingle().NonLazy();
        }

        private void BindCamera()
        {
            Container.Bind<CinemachineVirtualCamera>().FromInstance(_camera);
            Container.BindInterfacesAndSelfTo<CameraInitializer>().AsSingle().NonLazy();
            Container.Bind<CameraModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraMover>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraZoomer>().AsSingle().NonLazy();
        }

        void InitExecutionOrder()
        {
            Container.BindExecutionOrder<GridMeshDisplayService>(2000);
        }
    }
}