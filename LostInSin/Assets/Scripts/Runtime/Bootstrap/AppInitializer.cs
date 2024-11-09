using DG.Tweening;
using LostInSin.Runtime.Infrastructure.Data;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.Bootstrap
{
	public class AppInitializer
	{
		[Inject] private ApplicationSettings _applicationSettings;

		public void ApplyAppSettings()
		{
			Application.targetFrameRate = _applicationSettings.TargetFrameRate;
			InitDoTween();
		}

		private void InitDoTween()
		{
			DOTween.Init(_applicationSettings.RecycleAllByDefault, _applicationSettings.UseSafeMode)
				.SetCapacity(_applicationSettings.TweenCapacity, _applicationSettings.SequenceCapacity);

			DOTween.defaultEaseType = _applicationSettings.DefaultEase;
		}
	}
}