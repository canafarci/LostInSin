using System;
using DG.Tweening;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.Projectiles
{
	public class Arrow : MonoBehaviour, IPoolable
	{
		private Vector3 _originalPos;
		private Quaternion _originalRot;
		private Vector3 _originalScale;
		private int _damage;
		private CharacterFacade _target;
		public bool reachedTarget => Vector3.SqrMagnitude(transform.position - _target.transform.position) < 0.1f;

		public void Shoot(int damage, CharacterFacade target)
		{
			Debug.Log("Called");
			transform.parent = null;

			_damage = damage;
			_target = target;

			transform.DOMove(target.ProjectileHitPoint.position, 10f).SetSpeedBased();
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
		}

		public void OnGetFromPool()
		{
		}
	}
}