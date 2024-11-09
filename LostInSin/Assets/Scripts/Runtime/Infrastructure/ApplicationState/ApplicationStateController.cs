using LostInSin.Runtime.Infrastructure.ApplicationState.Signals;
using LostInSin.Runtime.Infrastructure.Templates;

namespace LostInSin.Runtime.Infrastructure.ApplicationState
{
	public class ApplicationStateController : SignalListener
	{
		private AppStateID _currentStateID = AppStateID.Initializing;

		protected override void SubscribeToEvents()
		{
			SignalBus.Subscribe<ChangeAppStateSignal>(OnAppStateChangeSignal);
		}

		private void OnAppStateChangeSignal(ChangeAppStateSignal signal)
		{
			AppStateID oldState = _currentStateID;
			_currentStateID = signal.newState;

			SignalBus.Fire(new AppStateChangedSignal(_currentStateID, oldState));
		}

		protected override void UnsubscribeFromEvents()
		{
			SignalBus.Unsubscribe<ChangeAppStateSignal>(OnAppStateChangeSignal);
		}
	}
}