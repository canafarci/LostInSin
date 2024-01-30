using LostInSin.Characters;

namespace LostInSin.Signals.Characters
{
    public readonly struct SelectInitialCharacterSignal
    {
        private readonly Character _initialCharacter;
        public readonly Character InitialCharacter => _initialCharacter;

        public SelectInitialCharacterSignal(Character initialCharacter)
        {
            _initialCharacter = initialCharacter;
        }
    }
}