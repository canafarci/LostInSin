using System;
using LostInSin.Runtime.BehaviourTree.Utility;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Signals;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace LostInSin.Runtime.BehaviourTree.Turns
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "PassTurnAction",
	                 story: "[Agent] Passes Its Turn",
	                 category: "Action",
	                 id: "dbe82c406420e967c0db00e475c61894")]
	public partial class PassTurnAction : Action
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;

		protected override Status OnStart()
		{
			BTReferences.instance.signalBus.Fire(new EndCharacterTurnSignal());
			return Status.Success;
		}
	}
}