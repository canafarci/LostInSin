using System;
using Cysharp.Threading.Tasks;
using LostInSin.Abilities;
using LostInSin.Characters;
using LostInSin.Identifiers;
using LostInSin.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.AbilitySystem
{
    public class AbilitySystemManager : IInitializable, IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;

        private readonly CompositeDisposable _disposables = new();

        private Character _instigator;
        private AbilityTarget _target;

        private AbilityInfo _ability;

        public AbilityInfo Ability => _ability;

        public void Initialize()
        {
            _signalBus.GetStream<SelectedAbilityChangedSignal>()
                      .Subscribe(OnSelectedAbilityChanged)
                      .AddTo(_disposables);

            _signalBus.GetStream<CharacterSelectedSignal>()
                      .Subscribe(OnCharacterSelectedSignal)
                      .AddTo(_disposables);
        }

        private void OnSelectedAbilityChanged(SelectedAbilityChangedSignal signal)
        {
            _ability = signal.Ability;
        }

        private void OnCharacterSelectedSignal(CharacterSelectedSignal signal)
        {
            _instigator = signal.SelectedCharacter;
        }

        public async UniTask<AbilityCastResult> CastAbility(AbilityTarget target)
        {
            _target = target;
            AbilityCastResult abilityCastResult = AbilityCastResult.Fail;

            if (await _ability.AbilityBlueprint.CanCast(_instigator, _target))
                await _ability.AbilityBlueprint.Cast(_instigator, _target);

            return abilityCastResult;
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}