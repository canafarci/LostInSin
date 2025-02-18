using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;
using UnityEngine.Assertions;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.GameStates
{
	public class GameStateController : SignalListener
	{
		private readonly IGameStateModel _gameStateModel;
		private GameState _currentState;

		public GameStateController(IGameStateModel gameStateModel)
		{
			_gameStateModel = gameStateModel;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<ChangeGameStateSignal>(OnChangeGameStateSignal);
			_signalBus.Subscribe<TriggerLevelEndSignal>(OnTriggerLevelEndSignal);
		}

		private void OnTriggerLevelEndSignal(TriggerLevelEndSignal signal)
		{
			_gameStateModel.SetGameIsWon(signal.isGameWon);
			ChangeGameState(GameState.GameOver);
		}

		private void OnChangeGameStateSignal(ChangeGameStateSignal signal)
		{
			Assert.IsFalse(signal.newState == GameState.GameOver,
			               "Game Over change should be triggered with use of TriggerLevelEndSignal");

			ChangeGameState(signal.newState);
		}

		private void ChangeGameState(GameState newState)
		{
			GameState oldState = _currentState;
			_currentState = newState;

			_signalBus.Fire(new GameStateChangedSignal(_currentState, oldState));
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<ChangeGameStateSignal>(OnChangeGameStateSignal);
			_signalBus.Unsubscribe<TriggerLevelEndSignal>(OnTriggerLevelEndSignal);
		}
	}
}