using System;
using DG.Tweening;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.Projectiles
{
	public class Arrow : MonoBehaviour, IPoolable
	{
		public bool ReachedTarget = false;

		private Vector3 _originalPos;
		private Quaternion _originalRot;
		private Vector3 _originalScale;
		private CharacterFacade _target;

		public void Shoot(CharacterFacade target)
		{
			transform.parent = null;

			_target = target;

			transform.DOMove(target.visualReferences.ProjectileHitPoint.position, 10f)
				.SetSpeedBased()
				.OnComplete(() =>
				{
					ReachedTarget = true;
					transform.parent = _target.visualReferences.ProjectileHitPoint;
				});
		}

		public void ResetPosition()
		{
			transform.localRotation = _originalRot;
			transform.localPosition = _originalPos;
			transform.localScale = _originalScale;
		}

		public void OnCreated()
		{
			_originalPos = transform.position;
			_originalRot = transform.rotation;
			_originalScale = transform.lossyScale;
		}

		public void OnDestroyed()
		{
		}

		public void OnReturnToPool()
		{
			transform.parent = null;
		}

		public void OnGetFromPool()
		{
			_target = null;
			ReachedTarget = false;
		}
	}
}