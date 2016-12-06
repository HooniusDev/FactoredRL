using Factored.ECS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Component
{
	public class PhysicalAttributes : IComponent
	{
		public int OwnerID { get; private set; }

		public bool BlockMove { get; set; }
		public bool BlockLight { get; set; }

		public EntitySizes Size { get; set; }

		public int GetOwner()
		{
			return OwnerID;
		}

		public PhysicalAttributes(int owner, bool blockMove, bool blockLight, EntitySizes size = EntitySizes.None )
		{
			OwnerID = owner;
			BlockMove = blockMove;
			BlockLight = blockLight;
			Size = size;
		}
	}

	public enum EntitySizes
	{
		None,
		Tiny,
		Medium,
		Big,
		OccupiesCell,
	}
}
