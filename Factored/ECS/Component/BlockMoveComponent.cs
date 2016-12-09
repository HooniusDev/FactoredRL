using Factored.Consoles;
using Factored.ECS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Component
{
	public class BlockMoveComponent : IComponent
	{

		public int OwnerID;
		public int GetOwner()
		{
			return OwnerID;
		}

		public BlockMoveComponent( int owner )
		{
			OwnerID = owner;
			//System.Console.WriteLine( "Block Move Component Added TO: " + ComponentManager.GetComponent<PositionComponent>( OwnerID ).Position.ToString() );
			GameConstants.Map.SetBlockMoveComponent( OwnerID, ComponentManager.GetComponent<PositionComponent>( OwnerID ).Position );
		}

		public void RemoveBlockComponent()
		{
			GameConstants.Map.SetBlockMoveComponent( -1, ComponentManager.GetComponent<PositionComponent>( OwnerID ).Position );
			ComponentManager.RemoveComponent( OwnerID, this );
		}
	}
}
