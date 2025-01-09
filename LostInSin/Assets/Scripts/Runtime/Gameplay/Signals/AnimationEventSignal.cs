using Animancer;
using LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;

namespace LostInSin.Runtime.Gameplay.Signals
{
	/// <summary>
	/// Listened by <see cref="AbilityPlayer"/>> <br/>
	/// Fired by <see cref="CharacterAnimationPlayer"/>>
	/// </summary>
	public readonly struct AnimationEventSignal
	{
		public StringAsset stringAsset { get; }

		public AnimationEventSignal(StringAsset stringAsset)
		{
			this.stringAsset = stringAsset;
		}
	}
}