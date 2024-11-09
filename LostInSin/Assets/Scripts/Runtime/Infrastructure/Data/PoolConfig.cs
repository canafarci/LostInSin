using System.Collections.Generic;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Infrastructure.Data
{
	[CreateAssetMenu(fileName = "Pool Config", menuName = "Infrastructure/Pool Config")]
	public class PoolConfig : SerializedScriptableObject
	{
		[TableList]
		public List<PoolEntry> PoolEntries = new();
	}
}