using Factored.Consoles;
using Factored.ECS.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Component
{
	public class PositionComponent : IComponent
	{
		public int OwnerID { get; private set; }
		public int GetOwner()
		{
			return OwnerID;
		}

		private Point _position;
		public Point Position 
		{
		 get
			{
				return _position;
			}
		 set
			{
				if ( GameConstants.Map.IsValid( value ) )
				{
					_position = value;
					RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( OwnerID );
					if ( rc != null )
						rc.Changed = true;
				}
			}		
		  }


		public int X { get { return Position.X; } }
		public int Y { get { return Position.Y; } }

		public PositionComponent( int owner, Point position )
		{
			OwnerID = owner;
			Position = position;
		}
	}
}
