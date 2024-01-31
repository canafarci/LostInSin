using LostInSin.Characters;

namespace LostInSin.Signals.Characters
{
    public readonly struct SelectCharactersSignal
    {
        private readonly Character _initialCharacter;
        public readonly Character InitialCharacter => _initialCharacter;

        public SelectCharactersSignal(Character initialCharacter)
        {
            _initialCharacter = initialCharacter;
        }
    }
}