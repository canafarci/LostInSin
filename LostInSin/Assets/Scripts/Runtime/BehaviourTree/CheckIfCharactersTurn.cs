using System;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Turns;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using VContainer;
using Action = Unity.Behavior.Action;

namespace LostInSin.Runtime.BehaviourTree
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "CheckIfCharactersTurn", story: "Check if it is [Agent] 's Turn",
		category: "Flow/Conditional",
		id: "17b92f89dc21c9c463c8f27773fda06f")]
	public partial class CheckIfCharactersTurnAction : Action
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<BTReferences> BTReferences;

		protected override Status OnStart()
		{
			return Status.Running;
		}

		protected override Status OnUpdate()
		{
			ITurnModel btReferencesTurnModel = BTReferences.Value.TurnModel;
			if (btReferencesTurnModel.activeCharacter == Agent.Value)
			{
				return Status.Success;
			}
			else
			{
				return Status.Failure;
			}
		}

		protected override void OnEnd()
		{
		}
	}
}