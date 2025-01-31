using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;

namespace LostInSin.Runtime.Gameplay.Signals
{
	public readonly struct ChangeGameStateSignal
	{
		private readonly GameState _newState;

		public GameState newState => _newState;

		public ChangeGameStateSignal(GameState newState)
		{
			_newState = newState;
		}
	}
}