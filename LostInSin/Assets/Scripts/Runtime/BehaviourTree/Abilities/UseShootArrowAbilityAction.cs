using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Characters;
using System;
using System.Linq;
using LostInSin.Runtime.BehaviourTree.Utility;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UseShootArrowAbility",
                 story: "[Agent] Uses Shoot Arrow [Ability]",
                 category: "Abilities",
                 id: "1b762c9687d812baa1a5e90a3c7aa7d3")]
public partial class UseShootArrowAbilityAction : Action
{
	[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
	[SerializeReference] public BlackboardVariable<Ability> Ability;

	//This ability's Request data only requires a target character.
	//Set it by getting closest target and then send the ability for execution
	protected override Status OnStart()
	{
		CharacterFacade target = BTReferences.instance.charactersInSceneModel.playerCharactersInScene
			.OrderBy(x => Vector3.SqrMagnitude(x.transform.position - Agent.Value.transform.position))
			.FirstOrDefault();

		Ability ability = Ability.Value;

		ability.AbilityRequest.Initialize(); //creates an ability request data inside Initialize func
		AbilityRequestData abilityRequestData = ability.AbilityRequest.data;

		abilityRequestData.User = Agent.Value;
		abilityRequestData.TargetCharacter = target;

		Agent.Value.ReduceActionPoints(abilityRequestData.totalActionPointCost);
		ability.AbilityExecution.Initialize(abilityRequestData);

		BTReferences.instance.turnSystemFacade.AddAbilityForPlaying(ability.AbilityExecution);

		return Status.Success;
	}
}