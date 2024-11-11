using LostInSin.Runtime.Gameplay.Turns;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Controls
{
	public class PlayerCharacterController : ITickable
	{
		private readonly ITurnModel _turnModel;

		public PlayerCharacterController(ITurnModel turnModel)
		{
			_turnModel = turnModel;
		}

		public void Tick()
		{
			if (!_turnModel.activeCharacter.isPlayerCharacter) return;
		}
	}
}