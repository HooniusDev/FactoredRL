using Factored.Actions.Interfaces;
using Factored.ECS;
using Factored.ECS.Component;
using Factored.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.Actions
{
	public class CloseAction : ActionBase, IAction
	{

		private List<DoorComponent> doors;

		public CloseAction( int performer ) : base( performer, 4 )
		{
			doors = new List<DoorComponent>();
		}

		public bool CanPerform()
		{
			// scan for a door in all directions 
			Point tile = ComponentManager.GetComponent<PositionComponent>( Performer ).Position;

			doors.AddRange( ComponentManager.GetComponentsAtTile<DoorComponent>( tile + Directions.Up ));
			doors.AddRange( ComponentManager.GetComponentsAtTile<DoorComponent>( tile + Directions.Left ) );
			doors.AddRange( ComponentManager.GetComponentsAtTile<DoorComponent>( tile + Directions.Down ) );
			doors.AddRange( ComponentManager.GetComponentsAtTile<DoorComponent>( tile + Directions.Right ) );

			System.Console.WriteLine( "Doors found: " + doors.Count.ToString() );  

			if ( doors.Count > 0 )
				return true;
			else
				return false;
		}

		public void Perform()
		{
			foreach ( DoorComponent c in doors )
				c.Close( Performer );
		}
	}
}
