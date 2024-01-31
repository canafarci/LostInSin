using LostInSin.Characters;
using LostInSin.UI;
using LostInSin.UI.AbilityPanel;
using LostInSin.UI.CharacterSelectPanel;
using LostInSin.UI.InitiativePanel;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    public class DungeonUIInstaller : MonoInstaller<DungeonUIInstaller>
    {
        [SerializeField] private GameObject _initiativePanelIconViewPrefab;

        public override void InstallBindings()
        {
            BindAbilityPanel();
            BindCharacterSelectPanel();
            BindInitiativePanel();
        }

        private void BindInitiativePanel()
        {
            Container.Bind<InitiativePanelView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<InitiativePanelVM>().AsSingle().NonLazy();

            Container.BindMemoryPool<InitiativePanelIconView, InitiativePanelIconView.Pool>()
                     .WithInitialSize(9)
                     .FromComponentInNewPrefab(_initiativePanelIconViewPrefab)
                     .UnderTransformGroup("UI_OBJECT_POOL");
        }

        private void BindCharacterSelectPanel()
        {
            Container.Bind<CharacterSelectPanelView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterSelectPanelVM>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterSelectPanelModel>().AsSingle().NonLazy();

            Container.BindFactory<Object,
                         RectTransform,
                         CharacterSelectPanelVM,
                         Character,
                         CharacterSelectPanelIconView, CharacterSelectPanelIconView.Factory>()
                     .FromFactory<
                         PrefabFactory<RectTransform, CharacterSelectPanelVM, Character, CharacterSelectPanelIconView>>();
        }

        private void BindAbilityPanel()
        {
            Container.Bind<AbilityPanelIconView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<AbilityPanelVM>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AbilityPanelModel>().AsSingle().NonLazy();
        }
    }
}