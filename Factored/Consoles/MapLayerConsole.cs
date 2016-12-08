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
			Fill( null, Colors.DefaultBG, null );
		}

		public void SetTileAppearance( int x, int y, TileType type )
		{
			switch ( type )
			{
				case ( TileType.Floor ):
					{
						SetCellAppearance( x, y, CellAppearances.FloorFov );
						break;
					}
				case ( TileType.Wall ):
					{
						SetCellAppearance( x, y, CellAppearances.WallFov );
						break;
					}
				case ( TileType.Corridor ):
					{
						SetCellAppearance( x, y, CellAppearances.CorridorFov );
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

		public override void Render()
		{
			base.Render();
			//System.Console.WriteLine( "MAp LAyer Render!" );
			//for ( int x = 0; x < map.Width(); x++ )
			//	for ( int y = 0; y < map.Width(); y++ )
			foreach ( Point tile in map.GetTilesToDraw() )
				{
					if ( map.IsFov( tile.X, tile.Y) == true )
					{
						SetTileAppearance( tile.X, tile.Y, map.GetTileType( tile.X, tile.Y ) );
						if ( this[tile.X, tile.Y].Effect != null )
						{

							this[tile.X, tile.Y].Effect.Clear( this[tile.X, tile.Y] );
							//map.TileChanged( x, y );
							//canvas[x, y].Effect.Apply( canvas[x, y] );
						}
					}
					else if ( map.IsExplored( tile.X, tile.Y ) == true )
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
		}

		public void CenterViewOn( Point point )
		{
			TextSurface.RenderArea = new Rectangle( point.X - ( TextSurface.RenderArea.Width / 2 ), point.Y - ( TextSurface.RenderArea.Height / 2 ), TextSurface.RenderArea.Width, TextSurface.RenderArea.Height );
		}
	}
}