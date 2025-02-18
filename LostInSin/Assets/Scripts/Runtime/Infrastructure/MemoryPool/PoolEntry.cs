using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using LostInSin.Runtime.Infrastructure.ApplicationState;

namespace LostInSin.Runtime.Infrastructure.MemoryPool
{
	[Serializable]
	public class PoolEntry
	{
		[FoldoutGroup("Pool Entry Data")] public bool IsMonoBehaviour;

		[FoldoutGroup("Pool Entry Data")] [ShowIf(nameof(IsMonoBehaviour))]
		public GameObject Prefab;

		// Store the class type as a string for serialization
		[FoldoutGroup("Pool Entry Data")] [ValueDropdown("GetClassTypeNames")] [SerializeField]
		private string ClassTypeName;

		[FoldoutGroup("Pool Entry Data")] public int InitialSize = 5;
		[FoldoutGroup("Pool Entry Data")] public int DefaultCapacity = 10;
		[FoldoutGroup("Pool Entry Data")] public int MaximumSize = 100;
		[FoldoutGroup("Pool Entry Data")] public bool ManagePoolOnSceneChange = true;

		[FoldoutGroup("Pool Entry Data")] [ShowIf(nameof(ManagePoolOnSceneChange))]
		public AppStateID LifetimeSceneID;

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
				.Where(assembly => assembly.GetName().Name == "LostInSin.Runtime")
				.SelectMany(assembly => assembly.GetTypes())
				.Where(t => t.IsClass && !t.IsAbstract &&
				            (typeof(IPoolable).IsAssignableFrom(t) || (t.BaseType != null && typeof(Poolable).IsAssignableFrom(t.BaseType))))
				.Select(type => new ValueDropdownItem<string>(type.Name, type.AssemblyQualifiedName));


			return types;
		}
	}
}