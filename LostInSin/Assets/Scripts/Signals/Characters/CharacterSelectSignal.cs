using LostInSin.Characters;

namespace LostInSin.Signals.Characters
{
    public struct CharacterSelectSignal
    {
        private Character _character;
        public Character Character => _character;

        public CharacterSelectSignal(Character character)
        {
            _character = character;
        }
    }
}