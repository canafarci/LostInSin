using System;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Characters;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.Abilities
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "UseMeleeAttackAbility",
	                 story: "[Agent] Uses [MeleeAttackAbility]",
	                 category: "Action",
	                 id: "49b31ec2e6b0fe633abeec194be26cdf")]
	public partial class UseMeleeAttackAbilityAction : Action
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> MeleeAttackAbility;

		protected override Status OnStart()
		{
			return Status.Running;
		}

		protected override Status OnUpdate()
		{
			return Status.Success;
		}

		protected override void OnEnd()
		{
		}
	}
}