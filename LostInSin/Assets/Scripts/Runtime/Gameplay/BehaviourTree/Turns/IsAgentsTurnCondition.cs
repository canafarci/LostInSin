using System;
using LostInSin.Runtime.Gameplay.BehaviourTree.Utility;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;
using Unity.Behavior;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.Turns
{
	[Serializable, Unity.Properties.GeneratePropertyBag]
	[Condition(name: "AgentsTurn",
	           story: "Is Current [Agent] 's Turn",
	           category: "TurnConditions",
	           id: "287f38d2ff2a28b4576bdd05e7d965a1")]
	public partial class IsAgentsTurnCondition : Condition
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;

		public override bool IsTrue()
		{
			TurnSystemFacade turnSystemFacade = BTReferences.instance.turnSystemFacade;

			return turnSystemFacade.activeCharacter == Agent.Value;
		}
	}
}