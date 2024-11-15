using UnityEngine;

namespace LostInSin.Runtime.Infrastructure
{
	public class CoroutineRunner : MonoBehaviour
	{
		public static CoroutineRunner instance { get; private set; }

		private void Awake()
		{
			if (instance == null) instance = this;
		}
	}
}