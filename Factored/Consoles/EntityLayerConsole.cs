using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = SadConsole.Consoles.Console;
using Factored.Utils;
using Microsoft.Xna.Framework;
using Factored.ECS.Interfaces;
using static Factored.Systems.MapSystem;
using Factored.MapObjects;
using Factored.ECS.Component;
using Factored.ECS;
using SadConsole;

namespace Factored.Consoles
{
/// <summary>
/// Will be responsible of renderering entities
/// </summary>
	public class EntityLayerConsole : Console
	{

		public IMap map;

		public EntityLayerConsole( int width, int height, IMap map, int mapWidth, int mapHeight ) : base( mapWidth, mapHeight )
		{
			this.map = map;
		}

		public override void Render()
		{
			
			base.Render();

			List<RenderComponent> rcList = ComponentManager.GetComponents<RenderComponent>();


			for ( int i = rcList.Count - 1; i >= 0; i-- )
			{
				PositionComponent
					RenderEntity( i, this[pc.X, pc.Y] );
				
			}
		}

		public void RenderEntity( int e, Cell sadConsoleCell )
		{
			RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( e );
			if ( rc != null && rc.Changed )
			{
				rc.Appearance.CopyAppearanceTo( sadConsoleCell );
				rc.Changed = false;
			}
		}
	}
}
