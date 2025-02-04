using LostInSin.Runtime.Gameplay.Abilities.RequestFilling;
using LostInSin.Runtime.Gameplay.Grid.Data;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.Visuals.Decals;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using LostInSin.Runtime.Infrastructure.Templates;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Visuals
{
	public class AreaTargetedAbilityVisualDisplayer : SignalListener, IFixedTickable
	{
		[Inject] private PoolManager _poolManager;
		[Inject] private readonly PlayerRaycaster _playerRaycaster;

		protected AbilityRequest _abilityRequest;

		private const float UPDATE_INTERVAL = 0.05f;
		private float _updateTimer;
		private CircularAreaTargetedAbilityDecal _decal;

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<AbilityRequestCreatedSignal>(OnAbilityRequestCreatedSignalHandler);
			_signalBus.Subscribe<AbilityRequestCancelledSignal>(OnAbilityRequestCancelledSignalHandler);
		}

		public void FixedTick()
		{
			if (ShouldNotDraw()) return;

			if (_playerRaycaster.TryRaycastForGridCell(out GridCell targetCell))
			{
				_decal.transform.position = targetCell.centerPosition;

				if (!_decal.gameObject.activeSelf)
				{
					_decal.gameObject.SetActive(true);
				}
			}
			else
			{
				_decal.gameObject.SetActive(false);
			}
		}

		private bool ShouldNotDraw()
		{
			if (_abilityRequest == null) return true;

			if (_abilityRequest.state == AbilityRequestState.Complete)
			{
				ClearDecal();
				return true;
			}

			if (EventSystem.current.IsPointerOverGameObject()) return true;

			_updateTimer += Time.deltaTime;
			if (_updateTimer < UPDATE_INTERVAL) return true;
			_updateTimer = 0f;

			return false;
		}

		private void ClearDecal()
		{
			PoolManager.ReleaseMono(_decal);
			_decal = null;
		}

		private void OnAbilityRequestCreatedSignalHandler(AbilityRequestCreatedSignal signal)
		{
			AbilityRequest request = signal.request;
			if (!IsRequestRelevant(request)) return;

			_decal = PoolManager.GetMono<CircularAreaTargetedAbilityDecal>();
			_abilityRequest = request;
			_decal.transform.localScale = Vector3.one * _abilityRequest.Radius;
		}

		private bool IsRequestRelevant(AbilityRequest request)
		{
			// Only consider if user is the player, request uses AreaTargeted 
			if (!request.data.User.isPlayerCharacter) return false;
			if (!request.RequestType.HasFlag(AbilityRequestType.CircularAreaTargeted)) return false;
			return true;
		}

		private void OnAbilityRequestCancelledSignalHandler(AbilityRequestCancelledSignal signal)
		{
			_abilityRequest = null;
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<AbilityRequestCreatedSignal>(OnAbilityRequestCreatedSignalHandler);
			_signalBus.Unsubscribe<AbilityRequestCancelledSignal>(OnAbilityRequestCancelledSignalHandler);
		}
	}
}