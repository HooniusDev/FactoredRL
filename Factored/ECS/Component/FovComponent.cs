using Factored.ECS.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Component
{
	public class FovComponent : IComponent
	{
		public int OwnerID;
		public int GetOwner()
		{
			return OwnerID;
		}

		public bool changed;

		public FovComponent( int owner )
		{
			OwnerID = owner;
		}

	}
}
