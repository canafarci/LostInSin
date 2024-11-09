using UnityEngine;
using UnityEngine.UI;

namespace LostInSin.Runtime.CrossScene.LoadingScreen
{
	public class LoadingScreenView : MonoBehaviour
	{
		[SerializeField] private CanvasGroup CanvasGroup;
		[SerializeField] private Image FillImage;

		public CanvasGroup canvasGroup => CanvasGroup;
		public Image fillImage => FillImage;
	}
}