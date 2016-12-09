using System;
using Console = SadConsole.Consoles.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factored.Utils;
using Microsoft.Xna.Framework;
using Factored.ECS.Interfaces;
using static Factored.Systems.MapSystem;
using Factored.MapObjects;

namespace Factored.Consoles
{
	public class MapLayerConsole : Console
	{

		public IMap map;

		/// <summary>
		/// Create Console view with width and height, that pans in a map sized area.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="mapWidth"></param>
		/// <param name="mapHeight"></param>
		/// 
		public MapLayerConsole( int width, int height, IMap map, int mapWidth, int mapHeight ) : base( mapWidth, mapHeight )
		{
			this.map = map;
			//Fill( null, Colors.DefaultBG, null );
		}

		public void Init()
		{
			for ( int x = 0; x < map.Width(); x++ )
				for ( int y = 0; y < map.Height(); y++ )
				{
					SetTileAppearance( x, y, map.GetTileType( x, y ), false, false );
				}
		}

		public void SetTileAppearance( int x, int y, TileType type, bool fov = false, bool explored = false )
		{
	
			switch ( type )
			{
				case ( TileType.Floor ):
					{
						if ( fov )
							SetCellAppearance( x, y, CellAppearances.FloorFov );
						else if ( explored )
							SetForeground( x, y, Colors.Floor );
						//SetCellAppearance( x, y, CellAppearances.FloorExplored );
						else
							Clear( x, y );
						break;
					}
				case ( TileType.Wall ):
					{
						if ( fov )
							SetCellAppearance( x, y, CellAppearances.WallFov );
						else if ( explored )
							SetForeground( x, y, Colors.Floor );
						//SetCellAppearance( x, y, CellAppearances.WallExplored );
						else
							Clear( x, y );
						break;
					}
				case ( TileType.Corridor ):
					{
						if ( fov )
							SetCellAppearance( x, y, CellAppearances.CorridorFov );
						else if ( explored )
							SetForeground( x, y, Colors.Floor );
						//SetCellAppearance( x, y, CellAppearances.CorridorExplored );
						else
							Clear( x, y );
						break;
					}
				case ( TileType.None ):
					{
						break;
					}
				default:
					{
						break;
					}
			}
		}

		public void RenderAppearances()
		{
			foreach ( Point tile in map.GetTilesToDraw() )
			{

				if ( map.IsFov( tile.X, tile.Y ) == true )
				{
					SetTileAppearance( tile.X, tile.Y,  map.GetTileType( tile.X, tile.Y ), true );
				}
				else if ( !map.IsFov( tile.X, tile.Y ) && map.IsExplored( tile.X, tile.Y ) == true )
				{
					SetTileAppearance( tile.X, tile.Y,  map.GetTileType( tile.X, tile.Y ), false, true );
				}
				if ( !map.IsFov( tile.X, tile.Y ) && !map.IsExplored( tile.X, tile.Y ) )
				{
					SetTileAppearance( tile.X, tile.Y,  map.GetTileType( tile.X, tile.Y ), false, false );
				}
			}
			map.ClearTilesToDraw();
		}

		public void RenderMap()
		{
			//System.Console.WriteLine( "MAp LAyer Render!" );
			//for ( int x = 0; x < map.Width(); x++ )
			//	for ( int y = 0; y < map.Width(); y++ )
			//int counter = 0;
			foreach ( Point tile in map.GetTilesToDraw() )
			{
				//counter++;
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
			//if ( counter > 0)
				//System.Console.WriteLine( "Rendered count: " + counter.ToString() );
			map.ClearTilesToDraw();
		}

		public void CenterViewOn( Point point )
		{
			TextSurface.RenderArea = new Rectangle( point.X - ( TextSurface.RenderArea.Width / 2 ), point.Y - ( TextSurface.RenderArea.Height / 2 ), TextSurface.RenderArea.Width, TextSurface.RenderArea.Height );
		}
	}
}