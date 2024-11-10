using UnityEngine;

namespace LostInSin.Runtime.CrossScene.Audio
{
	public class AudioView : MonoBehaviour
	{
		[SerializeField] private AudioSource SoundEffectAudioSource;
		[SerializeField] private AudioSource MusicAudioSource;

		public AudioSource soundEffectAudioSource => SoundEffectAudioSource;
		public AudioSource musicAudioSource => MusicAudioSource;
	}
}