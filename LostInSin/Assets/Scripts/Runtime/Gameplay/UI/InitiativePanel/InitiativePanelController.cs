using System;
using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Turns;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.UI.InitiativePanel
{
	public class InitiativePanelController : IInitializable, IDisposable
	{
		[Inject] private ITurnModel _turnModel;

		private DisposableBag _disposable;

		public void Initialize()
		{
		}

		private void OnCharacterTurnOrderChanged(LinkedList<CharacterFacade> characterFacades)
		{
			Debug.Log($"reactive {characterFacades.First.Value.characterName}");
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}