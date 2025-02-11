using System;
using Cysharp.Threading.Tasks;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Visuals.FX
{
	public class FireballFX : MonoPoolable
	{
		[SerializeField] private ParticleSystem FX;

		public void SetPosition(Vector3 position)
		{
			transform.position = position + Vector3.up * 1.5f;
		}

		public async UniTask Play()
		{
			FX.Play();
			await UniTask.Delay(TimeSpan.FromSeconds(FX.main.duration));
			PoolManager.ReleaseMono(this);
		}
	}
}