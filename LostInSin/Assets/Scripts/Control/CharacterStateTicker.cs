using Zenject;
using LostInSin.Characters;
using LostInSin.Raycast;
using LostInSin.Input;
using UnityEngine.InputSystem;

namespace LostInSin.Control
{
    public class CharacterStateTicker : ITickable
    {
        private Character _selectedCharacter = null;

        public void SetTickingCharacter(Character character)
        {
            _selectedCharacter = character;
            _selectedCharacter.SetAsTickingCharacter();
        }

        public void Tick()
        {
            if (_selectedCharacter == null) return;

            _selectedCharacter.TickState();
        }
    }
}