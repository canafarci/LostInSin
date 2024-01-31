using System.Collections.Generic;
using System.Linq;
using LostInSin.Characters;
using LostInSin.Identifiers;
using LostInSin.Signals.UI;
using UnityEngine;
using Zenject;

namespace LostInSin.Combat
{
    public class TurnManager
    {
        [Inject] private SignalBus _signalBus;

        private List<Character> _combatCharacters;
        private LinkedList<Character> _orderedCombatCharacters;
        public LinkedList<Character> OrderedCombatCharacters => _orderedCombatCharacters;

        public void Initialize(List<Character> characters)
        {
            _combatCharacters = characters;
            // Sort the characters list based on Initiative attribute, in descending order
            IOrderedEnumerable<Character> sortedCharacters = _combatCharacters.OrderByDescending(c => c.AttributeSet
                             .GetAttribute(AttributeIdentifiers.Initiative)
                             .CurrentValue);

            // Initialize the LinkedList with the sorted characters
            _orderedCombatCharacters = new LinkedList<Character>(sortedCharacters);

            SetupInitiativePanelSignal setupInitiativePanelSignal = new(_orderedCombatCharacters);
            _signalBus.Fire(setupInitiativePanelSignal);
        }
    }
}