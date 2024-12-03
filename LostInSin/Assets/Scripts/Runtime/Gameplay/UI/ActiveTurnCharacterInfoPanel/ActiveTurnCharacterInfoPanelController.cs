using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;
using LostInSin.Runtime.Infrastructure.Templates;

namespace LostInSin.Runtime.Gameplay.UI.ActiveTurnCharacterInfoPanel
{
	public class ActiveTurnCharacterInfoPanelController : SignalListener
	{
		private readonly ActiveTurnCharacterInfoPanelView _view;
		private readonly ITurnModel _turnModel;

		public ActiveTurnCharacterInfoPanelController(ActiveTurnCharacterInfoPanelView view, ITurnModel turnModel)
		{
			_view = view;
			_turnModel = turnModel;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<CharacterAPChangedSignal>(OnCharacterAPChangedSignal);
			_signalBus.Subscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignal);
		}

		private void OnActiveTurnCharacterChangedSignal(ActiveTurnCharacterChangedSignal signal)
		{
			string activeCharacterCharacterName = _turnModel.activeCharacter.characterName;
			_view.characterNameText.SetText(activeCharacterCharacterName);
			UpdateAPDisplay();
		}

		private void UpdateAPDisplay()
		{
			_view.characterAPText.SetText($"AP: {_turnModel.activeCharacter.actionPoints}");
		}

		private void OnCharacterAPChangedSignal(CharacterAPChangedSignal signal)
		{
			UpdateAPDisplay();
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<CharacterAPChangedSignal>(OnCharacterAPChangedSignal);
			_signalBus.Unsubscribe<ActiveTurnCharacterChangedSignal>(OnActiveTurnCharacterChangedSignal);
		}
	}
}