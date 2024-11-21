using Animancer;

namespace LostInSin.Runtime.Gameplay.Signals
{
	public readonly struct AnimationEventSignal
	{
		private readonly StringAsset _stringAsset;
		public StringAsset stringAsset => _stringAsset;

		public AnimationEventSignal(StringAsset stringAsset)
		{
			_stringAsset = stringAsset;
		}
	}
}