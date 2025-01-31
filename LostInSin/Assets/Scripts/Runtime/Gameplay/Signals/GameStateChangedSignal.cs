using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;

namespace LostInSin.Runtime.Gameplay.Signals
{
	public readonly struct GameStateChangedSignal
	{
		private readonly GameState _newState;
		private readonly GameState _oldState;

		public GameState newState => _newState;
		public GameState oldState => _oldState;

		public GameStateChangedSignal(GameState newState, GameState oldState)
		{
			_newState = newState;
			_oldState = oldState;
		}
	}
}