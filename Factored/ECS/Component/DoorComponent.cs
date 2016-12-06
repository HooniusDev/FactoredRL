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

		public void Open()
		{
			if ( OwnerID == EntityManager.Player )
				System.Console.WriteLine( "You manage to open the door." );
			IsOpen = true;
			RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( OwnerID );
			if ( rc != null )
				rc.SetCellAppearance( CellAppearances.DoorOpenFov );
			PhysicalAttributes pa = ComponentManager.GetComponent<PhysicalAttributes>( OwnerID );
			if ( pa == null )
				pa = new PhysicalAttributes( OwnerID, false, false, EntitySizes.Big );
			else
			{
				pa.BlockLight = false;
				pa.BlockMove = false;
			}

		}

		public void Close()
		{
			if ( OwnerID == EntityManager.Player )
				System.Console.WriteLine( "You gently slam the door shut." );
			IsOpen = false;
			RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( OwnerID );
			if ( rc != null )
				rc.SetCellAppearance( CellAppearances.DoorClosedFov);
			PhysicalAttributes pa = ComponentManager.GetComponent<PhysicalAttributes>( OwnerID );
			if ( pa == null )
				pa = new PhysicalAttributes( OwnerID, true, true, EntitySizes.Big );
			else
			{
				pa.BlockLight = true;
				pa.BlockMove = true;
			}

		}	
	}
}
