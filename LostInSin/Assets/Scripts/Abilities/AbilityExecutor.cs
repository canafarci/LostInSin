using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LostInSin.Abilities.AbilityData.Abstract;
using LostInSin.Characters;
using LostInSin.Identifiers;
using LostInSin.Signals.Abilities;
using UniRx;
using Zenject;

namespace LostInSin.Abilities
{
    public class AbilityExecutor : IInitializable
    {
        [Inject] private Character _character;
        [Inject] private SignalBus _signalBus;

        private CompositeDisposable _disposables = new();

        private AbilityInfo _currentAbility;
        private List<AbilityTarget> _targets = null;

        public AbilityCastResult Tick()
        {
            if (_targets == null)
            {
                AbilityBlueprint abilityBlueprint = _currentAbility.AbilityBlueprint;

                _targets = abilityBlueprint.AbilityTargetSelector.GetTarget(_character);
                abilityBlueprint.AbilityTargetSelectorVisual.TickVisual(_character);
            }

            return default;
        }

        public void Initialize()
        {
            _signalBus.GetStream<SelectedAbilityChangedSignal>().Subscribe(OnSelectedAbilityChangedSignal)
                      .AddTo(_disposables);
        }

        private void OnSelectedAbilityChangedSignal(SelectedAbilityChangedSignal signal)
        {
            _currentAbility = signal.Ability;
        }

        public async void StartExecutingAbility()
        {
            if (CanCastAbility())
            {
                ShowCastingStartVisuals();
                await UniTask.WaitWhile(() => _targets == null);
            }
        }

        private void ShowCastingStartVisuals()
        {
            foreach (AbilityCastingStarter castingStarter in _currentAbility.AbilityBlueprint.CastingStarters)
                castingStarter.StartCasting(_character);
        }

        private bool CanCastAbility()
        {
            bool canCast = true;

            foreach (AbilityRequirements requirement in _currentAbility.AbilityBlueprint.AbilityRequirements)
            {
                if (!requirement.CanCast(_character))
                {
                    canCast = false;
                    break;
                }
            }

            return canCast;
        }
    }
}