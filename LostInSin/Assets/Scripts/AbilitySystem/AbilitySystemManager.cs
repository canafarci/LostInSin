using System;
using System.Collections.Generic;
using System.Threading;
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
        private readonly Stack<AbilityInfo> _abilityStack = new();
        private Character _instigator;
        private AbilityInfo _ability => _abilityStack.Peek();
        private AbilityCastResult _castResult;
        private CancellationTokenSource _cancellationTokenSource;
        public AbilityCastResult CastResult => _castResult;

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
            //if stack only contains move action, add to stack
            AbilityInfo signalAbility = signal.Ability;
            if (signalAbility == _ability) return;

            if (_abilityStack.Count == 1)
            {
                if (signalAbility.AbilityIdentifier != AbilityIdentifiers.Move) //check new ability is not move
                    _abilityStack.Push(signalAbility);
            }
            else //pop last element and add the new element
            {
                AbilityInfo ability = _abilityStack.Pop();
                ability.AbilityBlueprint.OnAbilityDeselected(_instigator);

                _abilityStack.Push(signalAbility);
            }

            _ability.AbilityBlueprint.OnAbilitySelected(_instigator);
            _cancellationTokenSource = CreateCancellationTokenSource();

            CastAbility(_cancellationTokenSource.Token);
        }

        private void OnCharacterSelectedSignal(CharacterSelectedSignal signal)
        {
            _abilityStack.Clear();

            _instigator = signal.SelectedCharacter;
            AbilityInfo moveAbility = signal.SelectedCharacter
                                            .Abilities
                                            .Find(x => x.AbilityIdentifier == AbilityIdentifiers.Move);

            _abilityStack.Push(moveAbility);
            _cancellationTokenSource = CreateCancellationTokenSource();

            CastAbility(_cancellationTokenSource.Token);
        }

        /// <summary>
        /// Casts the ability represented by the current AbilityBlueprint.
        /// </summary>
        /// <returns>Returns the result of the ability cast.</returns>
        private async void CastAbility(CancellationToken cancellationToken)
        {
            if (_castResult == AbilityCastResult.InProgress) return;

            AbilityBlueprint abilityBlueprint = _ability.AbilityBlueprint;
            _castResult = AbilityCastResult.SelectingTarget;

            if (await abilityBlueprint.CanCast(_instigator, cancellationToken))
            {
                (AbilityCastResult castResult, AbilityTarget target) target =
                    await abilityBlueprint.PreCast(_instigator, cancellationToken);

                _castResult = target.castResult;
                if (_castResult is AbilityCastResult.Fail) return;

                _castResult = await abilityBlueprint.Cast(_instigator, target.target);
                _castResult = await abilityBlueprint.PostCast(_instigator);
            }

            if (_abilityStack.Count > 1) _abilityStack.Pop();

            _cancellationTokenSource = CreateCancellationTokenSource();
            CastAbility(_cancellationTokenSource.Token);
        }

        private CancellationTokenSource CreateCancellationTokenSource()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }

            return new CancellationTokenSource();
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}