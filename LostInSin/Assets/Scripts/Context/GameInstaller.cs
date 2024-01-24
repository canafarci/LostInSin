using Cinemachine;
using LostInSin.AbilitySystem;
using LostInSin.Animation.Data;
using LostInSin.Cameras;
using LostInSin.Characters;
using LostInSin.Characters.PersistentData;
using LostInSin.Control;
using LostInSin.Core;
using LostInSin.Grid;
using LostInSin.Grid.Visual;
using LostInSin.Input;
using LostInSin.Raycast;
using LostInSin.UI;
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

            Container.BindFactory<Vector3, CharacterPersistentData, Character, Character.Factory>()
                     .FromSubContainerResolve()
                     .ByNewPrefabInstaller<CharacterInstaller>(_characterPrefab);

            Container.BindInterfacesAndSelfTo<CharacterSpawner>()
                     .AsSingle();

            Container.Bind<AnimationHashes>().AsSingle();

            BindCharacterSelection();
            BindRaycasters();
            BindGrid();
            BindCamera();
            BindUI();
            BindAbilitySystem();
            BindCore();
        }

        private void BindCharacterSelection()
        {
            Container.BindInterfacesAndSelfTo<CharacterSelector>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterStateTicker>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<CameraRotator>().AsSingle().NonLazy();
        }

        private void InitExecutionOrder()
        {
            Container.BindExecutionOrder<GameInput>(-100);
            Container.BindExecutionOrder<CameraInitializer>(-10);
            Container.BindExecutionOrder<GridMeshDisplayService>(2000);
        }

        private void BindUI()
        {
            Container.Bind<AbilityPanelIconView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<AbilityPanelVM>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AbilityPanelModel>().AsSingle().NonLazy();
        }

        private void BindCore()
        {
            Container.BindInterfacesAndSelfTo<PointerOverUIChecker>().AsSingle().NonLazy();
        }

        private void BindAbilitySystem()
        {
            Container.BindInterfacesAndSelfTo<AbilitySystemManager>().AsSingle();
        }
    }
}