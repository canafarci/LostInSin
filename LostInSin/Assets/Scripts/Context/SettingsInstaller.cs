using LostInSin.Animation.Data;
using LostInSin.Cameras;
using LostInSin.Characters;
using LostInSin.Characters.PersistentData;
using LostInSin.Grid;
using LostInSin.Grid.Visual;
using LostInSin.Movement;
using LostInSin.UI;
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
        public SavedCharacters CharacterSavedData;
        public CharacterSelectPanelVM.Data CharacterPanelData;

        public override void InstallBindings()
        {
            Container.BindInstances(Mover,
                                    AnimationData,
                                    GridGenerationData,
                                    GridVisualData,
                                    CameraData,
                                    CharacterPanelData);

            const string path = "Data/SavedCharacters";

            Container.Bind<SavedCharacters>().FromScriptableObjectResource(path).AsSingle();
        }
    }
}