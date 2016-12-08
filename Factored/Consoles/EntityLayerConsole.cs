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
				PositionComponent pc = ComponentManager.GetComponent<PositionComponent>( rcList[i].OwnerID );
					RenderEntity( rcList[i].OwnerID, pc.Position );
			}
		}

		public void RenderEntity( int e, Point tile )
		{
			foreach ( Point p in map.GetTilesToClear() )
				this.Clear( p.X, p.Y );
				

			RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( e );
			if ( rc != null )
			{
				rc.Appearance.CopyAppearanceTo( this[tile.X, tile.Y] );
				if ( map.IsFov( tile.X, tile.Y ) == true )
				{
					if ( this[tile.X, tile.Y].Effect != null )
					{
						//this[tile.X, tile.Y].Effect = CellAppearances.FovEffect;
						//this[tile.X, tile.Y].Effect.Apply( this[tile.X, tile.Y] );
						this[tile.X, tile.Y].Effect.Clear( this[tile.X, tile.Y] );
						this[tile.X, tile.Y].Effect = null;
						return;
					}
				}
				else if ( !map.IsFov( tile.X, tile.Y ) && map.IsExplored( tile.X, tile.Y ) == true )
				{

					if ( map.GetTileType( tile.X, tile.Y ) != TileType.None )
					{
						this[tile.X, tile.Y].Effect = CellAppearances.ExploredEffect;
						this[tile.X, tile.Y].Effect.Apply( this[tile.X, tile.Y] );
					}
				}
				if ( !map.IsFov( tile.X, tile.Y ) && !map.IsExplored( tile.X, tile.Y ) )
				{
					if ( map.GetTileType( tile.X, tile.Y ) != TileType.None )
					{
						this[tile.X, tile.Y].Effect = CellAppearances.HiddenEffect;
						this[tile.X, tile.Y].Effect.Apply( this[tile.X, tile.Y] );
					}
				}
			}
			map.ClearTilesToClear();
		}
	}
}
