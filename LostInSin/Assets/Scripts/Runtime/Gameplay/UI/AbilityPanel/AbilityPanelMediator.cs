using System;
using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.UI.AbilityPanel
{
	public class AbilityPanelMediator : SignalListener, IAbilityPanelMediator
	{
		[Inject] private List<AbilityView> _abilityViews;

		public event Action<Ability> OnAbilityClicked;

		public void SetAbilityUI(List<Ability> abilities)
		{
			RemoveViewClickListeners();

			for (int i = 0; i < abilities.Count; i++)
			{
				SetupView(_abilityViews[i], abilities[i]);
			}

			for (int i = abilities.Count; i < _abilityViews.Count; i++)
			{
				_abilityViews[i].gameObject.SetActive(false);
			}

			DisableAllSelectedBG();
		}

		private void SetupView(AbilityView abilityView, Ability ability)
		{
			abilityView.gameObject.SetActive(true);

			abilityView.abilityIcon.sprite = ability.Icon;
			abilityView.abilityText.text = ability.AbilityName;

			abilityView.abilityButton.onClick.AddListener(() => OnButtonClicked(ability, abilityView));
		}

		private void OnButtonClicked(Ability ability, AbilityView abilityView)
		{
			OnAbilityClicked?.Invoke(ability);

			DisableAllSelectedBG();

			abilityView.selectedBG.SetActive(true);
		}

		private void DisableAllSelectedBG()
		{
			foreach (AbilityView view in _abilityViews)
			{
				view.selectedBG.SetActive(false);
			}
		}

		private void RemoveViewClickListeners()
		{
			foreach (AbilityView view in _abilityViews)
			{
				view.abilityButton.onClick.RemoveAllListeners();
			}
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<AbilityRequestCancelledSignal>(OnAbilityRequestCancelledSignalHandler);
			_signalBus.Subscribe<AbilityExecutionCompletedSignal>(OnAbilityExecutionCompletedSignalHandler);
		}

		private void OnAbilityExecutionCompletedSignalHandler(AbilityExecutionCompletedSignal signal) => DisableAllSelectedBG();
		private void OnAbilityRequestCancelledSignalHandler(AbilityRequestCancelledSignal signal) => DisableAllSelectedBG();

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<AbilityRequestCancelledSignal>(OnAbilityRequestCancelledSignalHandler);
			_signalBus.Unsubscribe<AbilityExecutionCompletedSignal>(OnAbilityExecutionCompletedSignalHandler);
		}

		public override void Dispose()
		{
			RemoveViewClickListeners();
			base.Dispose();
		}
	}
}