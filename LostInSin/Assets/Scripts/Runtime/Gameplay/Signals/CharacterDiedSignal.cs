using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.Signals
{
	public readonly struct CharacterDiedSignal
	{
		public CharacterFacade character { get; }

		public CharacterDiedSignal(CharacterFacade character)
		{
			this.character = character;
		}
	}
}