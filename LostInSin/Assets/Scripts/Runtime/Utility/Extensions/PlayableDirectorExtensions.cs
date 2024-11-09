using System;
using System.Collections;
using System.Collections.Generic;
using LostInSin.Runtime.Infrastructure;
using UnityEngine;
using UnityEngine.Playables;

namespace LostInSin.Runtime.Utility.Extensions
{
	public static class TimelineExtension
	{
		private static readonly WaitForEndOfFrame FrameWait = new();
		private static readonly List<PlayableDirector> CurrentlyPlayingTimelines = new();

		//Reverses playback of the given PlayableDirector timeline.
		public static void ReversePlay(this PlayableDirector timeline, Action onCompleted = null)
		{
			CoroutineRunner.instance.StartCoroutine(Reverse(timeline, onCompleted));
		}

		private static IEnumerator Reverse(PlayableDirector timeline, Action onCompleted = null)
		{
			if (IsTimelineAlreadyPlaying(timeline) || timeline == null)
				yield break;

			CurrentlyPlayingTimelines.Add(timeline);

			DirectorUpdateMode originalUpdateMode = SetTimelineToManualUpdateMode(timeline);

			if (IsTimelineAtStartOrEnd(timeline)) StartAtEnd(timeline);

			yield return ReversePlayback(timeline);

			StopAndCleanup(timeline, originalUpdateMode);

			onCompleted?.Invoke();
		}

		private static bool IsTimelineAlreadyPlaying(PlayableDirector timeline)
		{
			return CurrentlyPlayingTimelines.Contains(timeline);
		}

		private static DirectorUpdateMode SetTimelineToManualUpdateMode(PlayableDirector timeline)
		{
			DirectorUpdateMode originalUpdateMode = timeline.timeUpdateMode;
			timeline.timeUpdateMode = DirectorUpdateMode.Manual;
			return originalUpdateMode;
		}

		private static bool IsTimelineAtStartOrEnd(PlayableDirector timeline)
		{
			return Mathf.Approximately((float)timeline.time, (float)timeline.duration) ||
			       Mathf.Approximately((float)timeline.time, 0);
		}

		private static void StartAtEnd(PlayableDirector timeline)
		{
			timeline.time = timeline.duration;
			timeline.Evaluate();
		}

		private static IEnumerator ReversePlayback(PlayableDirector timeline)
		{
			var timeDelta = (float)timeline.duration;
			while (timeDelta > 0)
			{
				timeDelta -= Time.deltaTime / (float)timeline.duration;
				timeline.time = Mathf.Max(timeDelta, 0);
				timeline.Evaluate();

				yield return FrameWait;
			}

			timeline.time = 0;
			timeline.Evaluate();
		}

		private static void StopAndCleanup(PlayableDirector timeline, DirectorUpdateMode originalUpdateMode)
		{
			timeline.timeUpdateMode = originalUpdateMode;
			timeline.Stop();
			CurrentlyPlayingTimelines.Remove(timeline);
		}

		/// Sets the PlayableDirector timeline to its first frame.
		public static void SetFirstFrame(this PlayableDirector playableDirector)
		{
			playableDirector.time = 0;
			playableDirector.Evaluate();
		}

		// Helper methods for approximate equality checking
		private static bool EqualsApproximately(this double num, float other)
		{
			return Mathf.Approximately((float)num, other);
		}

		private static bool EqualsApproximately(this double num, double other)
		{
			return Mathf.Approximately((float)num, (float)other);
		}
	}
}