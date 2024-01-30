using System.Collections.Generic;
using System.Linq;
using LostInSin.Characters;
using LostInSin.Identifiers;

namespace LostInSin.Combat
{
    public class TurnManager
    {
        private List<Character> _combatCharacters;
        private LinkedList<Character> _orderedCombatCharacters;
        private LinkedListNode<Character> _currentNode;

        public void Initialize(List<Character> characters)
        {
            _combatCharacters = characters;
            // Sort the characters list based on Initiative attribute, in descending order
            IOrderedEnumerable<Character> sortedCharacters = _combatCharacters.OrderByDescending(c => c.AttributeSet
                             .GetAttribute(AttributeIdentifiers.Initiative)
                             .CurrentValue);

            // Initialize the LinkedList with the sorted characters
            _orderedCombatCharacters = new LinkedList<Character>(sortedCharacters);
            // Set the starting point of the turn order
            _currentNode = _orderedCombatCharacters.First;
        }

        public Character GetNextCharacter()
        {
            // If the list is empty or only has one character
            if (_orderedCombatCharacters.Count <= 1) return _currentNode?.Value;

            // Move to the next character or loop back to the start if at the end
            _currentNode = _currentNode.Next ?? _orderedCombatCharacters.First;

            return _currentNode.Value;
        }
    }
}