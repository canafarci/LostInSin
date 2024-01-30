using LostInSin.Characters;

namespace LostInSin.Signals
{
    public readonly struct CharacterPortraitClickedSignal
    {
        private readonly Character _character;

        public Character Character => _character;

        public CharacterPortraitClickedSignal(Character character)
        {
            _character = character;
        }
    }
}