using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using LostInSin.Characters;
using LostInSin.Identifiers;
using LostInSin.Signals.Characters;
using LostInSin.Signals.Combat;
using LostInSin.Signals.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.Combat
{
    public class TurnManager : IInitializable
    {
        [Inject] private SignalBus _signalBus;

        private readonly CompositeDisposable _disposables = new();

        private List<Character> _combatCharacters;
        private LinkedList<Character> _orderedCombatCharacters;
        public LinkedList<Character> OrderedCombatCharacters => _orderedCombatCharacters;

        public void Initialize()
        {
            _signalBus.GetStream<EndTurnSignal>()
                      .Subscribe(OnEndTurnSignal)
                      .AddTo(_disposables);
        }

        private void OnEndTurnSignal(EndTurnSignal signal)
        {
        }

        public void InitializeTurns(List<Character> characters)
        {
            _combatCharacters = characters;
            // Sort the characters list based on Initiative attribute, in descending order
            IOrderedEnumerable<Character> sortedCharacters = _combatCharacters.OrderByDescending(c => c.AttributeSet
                             .GetAttribute(AttributeIdentifiers.Initiative)
                             .CurrentValue);

            // Initialize the LinkedList with the sorted characters
            _orderedCombatCharacters = new LinkedList<Character>(sortedCharacters);

            FireSetupInitiativePanelSignal();
            FireCharacterSelectSignal(_orderedCombatCharacters.First.Value);
        }

        private void FireSetupInitiativePanelSignal()
        {
            SetupInitiativePanelSignal setupInitiativePanelSignal = new(_orderedCombatCharacters);
            _signalBus.Fire(setupInitiativePanelSignal);
        }

        private void FireCharacterSelectSignal(Character character)
        {
            CharacterTeam characterTeam = character.CharacterPersistentData.CharacterTeam;

            if (characterTeam is CharacterTeam.Friendly)
            {
                CharacterSelectSignal characterSelectSignal = new(character);
                _signalBus.Fire(characterSelectSignal);
            }
        }
    }
}