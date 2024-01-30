using LostInSin.Characters;

namespace LostInSin.Signals.Characters
{
    public struct CharacterSelectedSignal
    {
        private Character _selectedCharacter;
        public Character SelectedCharacter => _selectedCharacter;

        public CharacterSelectedSignal(Character selectedCharacter)
        {
            _selectedCharacter = selectedCharacter;
        }
    }
}