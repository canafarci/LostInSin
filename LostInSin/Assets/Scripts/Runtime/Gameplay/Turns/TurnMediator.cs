using System;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.UI.AbilityPanel;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Turns
{
	public class TurnMediator : IInitializable, IDisposable
	{
		private readonly TurnView _view;
		private readonly IAbilityPanelMediator _abilityPanelMediator;

		public event Action OnEndTurnButtonClicked;

		public TurnMediator(TurnView view, IAbilityPanelMediator abilityPanelMediator)
		{
			_view = view;
			_abilityPanelMediator = abilityPanelMediator;
		}

		public void Initialize()
		{
			_view.endTurnButton.onClick.AddListener(() => OnEndTurnButtonClicked?.Invoke());
		}


		public void Dispose()
		{
			_view.endTurnButton.onClick.RemoveAllListeners();
		}

		public void SetUpUI(CharacterFacade characterToPlay)
		{
			_abilityPanelMediator.SetAbilityUI(characterToPlay.abilities);
		}
	}
}