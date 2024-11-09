using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LostInSin.Runtime.Infrastructure.MemoryPool
{
	[Serializable]
	public class PoolEntry
	{
		[HorizontalGroup("IsMono")] [HideLabel]
		public bool IsMonoBehaviour;

		[HorizontalGroup("Prefab")] [HideLabel] [ShowIf("@this.IsMonoBehaviour")]
		public GameObject Prefab;

		// Store the class type as a string for serialization
		[FormerlySerializedAs("classTypeName")]
		[HorizontalGroup("Class")]
		[HideLabel]
		[ValueDropdown("GetClassTypeNames")]
		[SerializeField]
		private string ClassTypeName;

		// Property to get the actual Type from the string
		public Type classType
		{
			get => Type.GetType(ClassTypeName);
			set => ClassTypeName = value?.Name;
		}

		// Odin dropdown to show available classes
		private static IEnumerable<ValueDropdownItem<string>> GetClassTypeNames()
		{
			IEnumerable<ValueDropdownItem<string>> types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(t => t.IsClass && !t.IsAbstract)
				.Select(type => new ValueDropdownItem<string>(type.FullName, type.AssemblyQualifiedName));

			return types;
		}
	}
}