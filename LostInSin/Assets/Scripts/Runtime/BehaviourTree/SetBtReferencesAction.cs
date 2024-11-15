using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Object = UnityEngine.Object;

namespace LostInSin.Runtime.BehaviourTree
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "SetBTReferences", 
		story: "Set [BTReference]",
		category: "Action/Blackboard",
		id: "c53b8ca60ca30ba5b13367fb88222971")]
	public partial class SetBtReferencesAction : Action
	{
		[SerializeReference] public BlackboardVariable<BTReferences> BTReference;

		protected override Status OnStart()
		{
			BTReferences obj = (BTReferences)Object.FindFirstObjectByType(typeof(BTReferences));
			BTReference.ObjectValue = obj;

			return Status.Success;
		}
	}
}