using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using LostInSin.Characters;
using LostInSin.Extensions;
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
        private List<Character> _selectableCharacters = new();
        private List<Character> _charactersCompletedTurns = new();
        private LinkedListNode<Character> _characterPlayingTurn;

        public List<Character> SelectableCharacters => _selectableCharacters;
        public LinkedList<Character> OrderedCombatCharacters => _orderedCombatCharacters;

        public void Initialize()
        {
            _signalBus.GetStream<EndTurnSignal>().Subscribe(OnEndTurnSignal).AddTo(_disposables);
            _signalBus.GetStream<CharacterSelectSignal>().Subscribe(OnCharacterSelectSignal).AddTo(_disposables);
        }

        private void OnEndTurnSignal(EndTurnSignal signal)
        {
            LinkedListNode<Character> nextCharacter;
            CharacterTeam lastCharacterTeam = _characterPlayingTurn.Value.CharacterPersistentData.CharacterTeam;

            if (lastCharacterTeam == CharacterTeam.Friendly)
            {
                _charactersCompletedTurns.Add(_characterPlayingTurn.Value);
                int selectableCharactersCount = _selectableCharacters.Count;

                if (_charactersCompletedTurns.Count == selectableCharactersCount)
                {
                    int currentCharacterSelectableIndex = _selectableCharacters.IndexOf(_characterPlayingTurn.Value);
                    int selectableIndexDifference = selectableCharactersCount - currentCharacterSelectableIndex;

                    nextCharacter = GetNextCharacter(selectableIndexDifference);

                    _charactersCompletedTurns.Clear();
                }
                else
                {
                    nextCharacter = _orderedCombatCharacters
                                    .EnumerateNodes()
                                    .First(x =>
                                               _selectableCharacters.Contains(x.Value) &&
                                               !_charactersCompletedTurns.Contains(x.Value));
                }
            }
            else
            {
                nextCharacter = GetNextCharacter(1);
            }

            SetSelectedCharacter(nextCharacter);

            CharacterTeam nextCharacterTeam = nextCharacter.Value.CharacterPersistentData.CharacterTeam;
            if (lastCharacterTeam == CharacterTeam.Enemy && nextCharacterTeam == CharacterTeam.Friendly)
                SetSelectableCharacters();
        }

        private async void OnCharacterSelectSignal(CharacterSelectSignal signal)
        {
            await UniTask.DelayFrame(1);

            if (_characterPlayingTurn.Value == signal.Character) return;

            _characterPlayingTurn = _orderedCombatCharacters.EnumerateNodes()
                                                            .First(x =>
                                                                       x.Value == signal.Character);
        }

        private LinkedListNode<Character> GetNextCharacter(int count)
        {
            LinkedListNode<Character> nextCharacter = _characterPlayingTurn;

            while (count > 0)
            {
                nextCharacter = nextCharacter.Next ?? _orderedCombatCharacters.First;
                count--;
            }

            return nextCharacter;
        }

        public void InitializeTurns(List<Character> characters)
        {
            InitializeCombatOrder(characters);
            FireSetupInitiativePanelSignal();

            SetSelectedCharacter(_orderedCombatCharacters.First);
            SetSelectableCharacters();
        }

        private void InitializeCombatOrder(List<Character> characters)
        {
            _combatCharacters = characters;

            // Sort the characters list based on Initiative attribute, in descending order
            IOrderedEnumerable<Character> sortedCharacters = _combatCharacters.OrderByDescending(c => c.AttributeSet
                             .GetAttribute(AttributeIdentifiers.Initiative)
                             .CurrentValue);

            // Initialize the LinkedList with the sorted characters
            _orderedCombatCharacters = new LinkedList<Character>(sortedCharacters);
        }

        private void SetSelectableCharacters()
        {
            _selectableCharacters.Clear();

            LinkedListNode<Character> currentNode = _characterPlayingTurn;

            while (currentNode.Value.CharacterPersistentData.CharacterTeam == CharacterTeam.Friendly)
            {
                _selectableCharacters.Add(currentNode.Value);
                currentNode = currentNode.Next;

                if (currentNode == null)
                    currentNode = _orderedCombatCharacters.First;
            }
        }

        private void FireSetupInitiativePanelSignal()
        {
            SetupInitiativePanelSignal setupInitiativePanelSignal = new(_orderedCombatCharacters);
            _signalBus.Fire(setupInitiativePanelSignal);
        }

        private void SetSelectedCharacter(LinkedListNode<Character> character)
        {
            CharacterSelectSignal characterSelectSignal = new(character.Value);
            _signalBus.Fire(characterSelectSignal);

            _characterPlayingTurn = character;
        }
    }
}