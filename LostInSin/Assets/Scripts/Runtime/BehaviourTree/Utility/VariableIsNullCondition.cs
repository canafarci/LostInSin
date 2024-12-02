using System;
using Unity.Behavior;
using UnityEngine;

namespace LostInSin.Runtime.BehaviourTree.Utility
{
	[Serializable, Unity.Properties.GeneratePropertyBag]
	[Condition(name: "Variable Is Null",
	           story: "[Variable] Is Null",
	           category: "Variable Conditions",
	           id: "5e1aaf94a9f185ad5555e0cef64c268d")]
	public partial class VariableIsNullCondition : Condition
	{
		[SerializeReference] public BlackboardVariable Variable;

		public override bool IsTrue()
		{
			return Variable == null || Variable.ObjectValue == null;
		}

		public override void OnStart()
		{
		}

		public override void OnEnd()
		{
		}
	}
}