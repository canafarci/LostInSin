using System;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Turns;
using Unity.Behavior;
using UnityEngine;

namespace LostInSin.Runtime.BehaviourTree
{
	[Serializable, Unity.Properties.GeneratePropertyBag]
	[Condition(name: "AgentsTurn",
		story: "Current [Agent] 's Turn [BTReferences]",
		category: "TurnConditions",
		id: "287f38d2ff2a28b4576bdd05e7d965a1")]
	public partial class AgentsTurnCondition : Condition
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<BTReferences> BTReferences;

		public override bool IsTrue()
		{
			TurnSystemFacade turnSystemFacade = BTReferences.Value.turnSystemFacade;

			return turnSystemFacade.activeCharacter == Agent.Value;
		}
	}
}