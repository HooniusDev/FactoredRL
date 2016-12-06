using Factored.Actions.Interfaces;
using Factored.ECS.Component;
using Factored.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			// Check The Target Tile for any PhysicalComponent.BlocksMove
			//if ( MapComponent.IsWalkable( Target ) )
			//	return true;
			//else
			//{
			//	List<DoorComponent> dc = ComponentManager.GetComponentsAtTile<DoorComponent>( Target );
			//	foreach ( DoorComponent c in dc )
			//	{
			//		PhysicalAttributes pa = ComponentManager.GetComponent<PhysicalAttributes>( c.OwnerID );
			//		System.Console.WriteLine( "Door: " + c.IsOpen.ToString() );
			//		System.Console.WriteLine( "BlockMove: " + pa.BlockMove.ToString());
			//	}
					
			//}
			
			return false;
		}

		public void Perform()
		{
			PositionComponent pc = ComponentManager.GetComponent<PositionComponent>( Performer );

			pc.Position = Target;
			//throw new NotImplementedException();
		}
	}
}
