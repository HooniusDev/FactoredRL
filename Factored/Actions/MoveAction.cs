using Factored.Actions.Interfaces;
using Factored.ECS.Component;
using Factored.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factored.Consoles;

namespace Factored.Actions
{
	public class MoveAction : ActionBase, IAction
	{

		Point Direction;
		Point Target;

		public MoveAction( int perfomer, Point direction, int energyCost = 10 ) : base ( perfomer, energyCost )
		{
			Direction = direction;
			Target = ComponentManager.GetComponent<PositionComponent>(perfomer).Position + Direction;
		}

		public bool CanPerform()
		{
			//System.Console.WriteLine( "Can=: " + Target.ToString() );
			//Check The Target Tile 
			if ( !GameConstants.Map.IsWalkable( Target ) )
			{
				//System.Console.WriteLine( "Blocked: " + Target.ToString() );
				//find blocking entity
				int e = GameConstants.Map.getBlockinEntityID( Target );
				List<DoorComponent> dc = ComponentManager.GetComponentsAtTile<DoorComponent>( Target );
				foreach ( DoorComponent c in dc )
				{
					c.Open(Performer);
					//System.Console.WriteLine( "Door: " + c.IsOpen.ToString() );
					
				}
				return false;
			}
			
			//else
			//{
			//	//its a door so open it...
			//	// TODO implement a open door action..
			//	List<DoorComponent> dc = ComponentManager.GetComponentsAtTile<DoorComponent>( Target );
			//	foreach ( DoorComponent c in dc )
			//	{
			//		PhysicalAttributes pa = ComponentManager.GetComponent<PhysicalAttributes>( c.OwnerID );
			//		System.Console.WriteLine( "Door: " + c.IsOpen.ToString() );
			//	}

			//}

			return true;
		}

		public void Perform()
		{
			PositionComponent pc = ComponentManager.GetComponent<PositionComponent>( Performer );
			GameConstants.Map.OnEntityMoved( Performer, pc.Position, Target );
			pc.Position = Target;
			//throw new NotImplementedException();
		}
	}
}
