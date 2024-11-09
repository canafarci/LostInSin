using System.Collections.Generic;
using LostInSin.Runtime.CrossScene.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace LostInSin.Runtime.CrossScene.Data
{
	[CreateAssetMenu(fileName = "Audio Data", menuName = "Infrastructure/Sounds Data", order = 0)]
	public class AudioDataSo : SerializedScriptableObject
	{
		[FormerlySerializedAs("_audioMixer")] [SerializeField]
		private AudioMixer AudioMixer;

		[SerializeField] private Dictionary<AudioClipID, AudioClip> _audioClips = new();

		public AudioMixer audioMixer => AudioMixer;
		public Dictionary<AudioClipID, AudioClip> audioClips => _audioClips;
	}
}