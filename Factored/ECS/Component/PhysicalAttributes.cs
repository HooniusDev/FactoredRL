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

		public EntitySizes Size { get; set; }

		public int Weight { get; set; }

		public int GetOwner()
		{
			return OwnerID;
		}

		public PhysicalAttributes(int owner, EntitySizes size = EntitySizes.None )
		{
			OwnerID = owner;
			Size = size;
			Weight = 0;
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
