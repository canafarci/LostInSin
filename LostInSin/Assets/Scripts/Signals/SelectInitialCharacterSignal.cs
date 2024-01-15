using LostInSin.Characters;

namespace LostInSin.Signals
{
    public struct SelectInitialCharacterSignal
    {
        private Character _initialCharacter;
        public Character InitialCharacter => _initialCharacter;

        public SelectInitialCharacterSignal(Character initialCharacter)
        {
            _initialCharacter = initialCharacter;
        }
    }
}