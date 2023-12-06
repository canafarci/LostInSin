using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Input;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private GameObject _characterPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameInput>()
                .AsSingle().NonLazy();

            Container.BindFactory<Vector3, Character, Character.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<CharacterInstaller>(_characterPrefab);

            Container.BindInterfacesAndSelfTo<CharacterSpawner>()
                .AsSingle();

            Container.Bind<AnimationHashes>().AsSingle();
        }
    }
}