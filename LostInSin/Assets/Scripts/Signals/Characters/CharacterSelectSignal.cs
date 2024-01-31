using LostInSin.Characters;

namespace LostInSin.Signals.Characters
{
    public struct CharacterSelectSignal
    {
        private Character _selectedCharacter;
        public Character SelectedCharacter => _selectedCharacter;

        public CharacterSelectSignal(Character selectedCharacter)
        {
            _selectedCharacter = selectedCharacter;
        }
    }
}