using System.Collections.Generic;
using LostInSin.Characters;

namespace LostInSin.Signals.UI
{
    public readonly struct SetupInitiativePanelSignal
    {
        private readonly LinkedList<Character> _characters;

        public LinkedList<Character> Characters => _characters;

        public SetupInitiativePanelSignal(LinkedList<Character> characters)
        {
            _characters = characters;
        }
    }
}