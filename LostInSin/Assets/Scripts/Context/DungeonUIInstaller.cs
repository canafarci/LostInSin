using LostInSin.Characters;
using LostInSin.UI;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    public class DungeonUIInstaller : MonoInstaller<DungeonUIInstaller>
    {
        public override void InstallBindings()
        {
            BindAbilityPanel();
            BindCharacterSelectPanel();

            Container.Bind<InitiativePanelView>().FromComponentsInHierarchy().AsSingle();
        }

        private void BindCharacterSelectPanel()
        {
            Container.Bind<CharacterSelectPanelView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterSelectPanelVM>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterSelectPanelModel>().AsSingle().NonLazy();

            Container
                .BindFactory<Object, RectTransform, CharacterSelectPanelVM, Character, CharacterSelectPanelIconView,
                    CharacterSelectPanelIconView.Factory>()
                .FromFactory<PrefabFactory<RectTransform, CharacterSelectPanelVM, Character, CharacterSelectPanelIconView>>();
        }

        private void BindAbilityPanel()
        {
            Container.Bind<AbilityPanelIconView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<AbilityPanelVM>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AbilityPanelModel>().AsSingle().NonLazy();
        }
    }
}