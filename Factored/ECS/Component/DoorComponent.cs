using Factored.Consoles;
using Factored.ECS.Interfaces;
using Factored.MapObjects;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Component
{
	public class DoorComponent : IComponent
	{

		public int OwnerID;
		public int GetOwner()
		{
			return OwnerID;
		}

		public bool IsOpen { get; private set; }

		public DoorComponent( int owner )
		{
			OwnerID = owner;
			IsOpen = true;
			int r = GameConstants.random.Next( 100 );
			if ( r < 50 )
				Open();
			else
				Close();
		}

		public void Open( int e = -1)
		{
			if ( e == EntityManager.Player )
				System.Console.WriteLine( "You manage to open the door." );
			IsOpen = true;
			RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( OwnerID );
			BlockMoveComponent bmc = ComponentManager.GetComponent<BlockMoveComponent>( OwnerID );
			if (bmc != null)
				bmc.RemoveBlockComponent();
			if ( rc != null )
				rc.SetCellAppearance( CellAppearances.DoorOpenFov );
		
		}

		public void Close( int e = -1)
		{
			if ( e == EntityManager.Player )
				System.Console.WriteLine( "You gently slam the door shut." );
			IsOpen = false;
			ComponentManager.AddComponent( OwnerID, new BlockMoveComponent( OwnerID ) );
			RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( OwnerID );
			if ( rc != null )
				rc.SetCellAppearance( CellAppearances.DoorClosedFov );
		}
	}
}
