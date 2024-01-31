using LostInSin.Signals;
using LostInSin.Signals.Abilities;
using LostInSin.Signals.Characters;
using LostInSin.Signals.Combat;
using LostInSin.Signals.UI;
using Zenject;

namespace LostInSin.Context
{
    public class SignalsInstaller : MonoInstaller<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<SelectCharactersSignal>();
            Container.DeclareSignal<CharacterSelectSignal>();
            Container.DeclareSignal<SelectedAbilityChangedSignal>();
            Container.DeclareSignal<PlayableCharactersSpawnedSignal>();
            Container.DeclareSignal<CharacterPortraitClickedSignal>();
            Container.DeclareSignal<CombatStartedSignal>();
            Container.DeclareSignal<SetupInitiativePanelSignal>();
            Container.DeclareSignal<EndTurnSignal>();
        }
    }
}