using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Object = UnityEngine.Object;

namespace LostInSin.Runtime.BehaviourTree
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "GetBTReferences", story: "Get [BTReference]", category: "Action",
		id: "a5d4fff787f3481e0521a0ee70c4dbb1")]
	public partial class GetBtReferencesAction : Action
	{
		[SerializeReference] public BlackboardVariable BTReference;

		protected override Status OnStart()
		{
			if (BTReference.ObjectValue == null)
			{
				GetBTReference();
			}

			return Status.Success;
		}

		private void GetBTReference()
		{
			BTReferences obj = (BTReferences)Object.FindAnyObjectByType(typeof(BTReferences));
			BTReference.ObjectValue = obj;
		}
	}
}