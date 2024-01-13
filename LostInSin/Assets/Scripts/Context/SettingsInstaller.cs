using LostInSin.Animation;
using LostInSin.Camera;
using LostInSin.Characters;
using LostInSin.Grid;
using LostInSin.Grid.Visual;
using LostInSin.Movement;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public Mover.Settings Mover;
        public AnimationHashes.Data AnimationData;
        public GridModel.Data GridGenerationData;
        public GridMeshDisplayService.Data GridVisualData;
        public CameraModel.CameraData CameraData;
        public CharacterSpawner.Data CharacterSpawnerData;

        public override void InstallBindings()
        {
            Container.BindInstances(Mover,
                                    AnimationData,
                                    GridGenerationData,
                                    GridVisualData,
                                    CameraData,
                                    CharacterSpawnerData);
        }
    }
}