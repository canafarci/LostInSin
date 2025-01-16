using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; // if you prefer using Odin
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Infrastructure.MemoryPool;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions
{
	[CreateAssetMenu(fileName = "Composite Ability Execution",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Composite Ability Execution")]
	public class CompositeAbilityExecution : AbilityExecution
	{
		[SerializeField, ListDrawerSettings(DraggableItems = false)]
		private List<AbilityExecution> LeafAbilityExecutions;

		private int _currentIndex;

		public override void Initialize(AbilityRequestData data)
		{
			base.Initialize(data);
			_currentIndex = 0;

			// Initialize all child abilities
			foreach (AbilityExecution leafAbility in LeafAbilityExecutions)
			{
				leafAbility.Initialize(data);
				leafAbility.executionData.AbilityTriggers = executionData.AbilityTriggers;
			}
		}

		public override void StartAbility()
		{
			if (LeafAbilityExecutions.Count == 0)
			{
				// No children to run; we can immediately mark ourselves as complete
				executionStage = AbilityExecutionStage.Complete;
				return;
			}

			// Start the first sub-ability
			LeafAbilityExecutions[_currentIndex].StartAbility();
			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			AbilityExecution currentChild = LeafAbilityExecutions[_currentIndex];

			if (currentChild.executionStage == AbilityExecutionStage.Updating)
			{
				currentChild.UpdateAbility();
			}
			else if (currentChild.executionStage == AbilityExecutionStage.Finishing)
			{
				currentChild.FinishAbility();
			}
			else if (currentChild.executionStage == AbilityExecutionStage.Complete)
			{
				currentChild.EndAbility();

				// Move to the next child (if any)
				_currentIndex++;
				if (_currentIndex < LeafAbilityExecutions.Count)
				{
					LeafAbilityExecutions[_currentIndex].StartAbility();
				}
				else
				{
					// All children completed
					executionStage = AbilityExecutionStage.Finishing;
				}
			}
		}

		public override void FinishAbility()
		{
			executionStage = AbilityExecutionStage.Complete;
		}
	}
}