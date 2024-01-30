using LostInSin.Signals;
using LostInSin.Signals.Abilities;
using LostInSin.Signals.Characters;
using LostInSin.Signals.Combat;
using Zenject;

namespace LostInSin.Context
{
    public class SignalsInstaller : MonoInstaller<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<SelectInitialCharacterSignal>();
            Container.DeclareSignal<CharacterSelectedSignal>();
            Container.DeclareSignal<SelectedAbilityChangedSignal>();
            Container.DeclareSignal<PlayableCharactersSpawnedSignal>();
            Container.DeclareSignal<CharacterPortraitClickedSignal>();
            Container.DeclareSignal<CombatStartedSignal>();
        }
    }
}