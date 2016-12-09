using Factored.ECS.Interfaces;
using HumbleRL.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Systems
{
	public static class Fov
	{

		public static List<Point> GetFov( IMap map, Point position, int SightRadius )
		{
			List<Point> VisibleTiles = new List<Point>();
			////Center view to Hero
			//TextSurface.RenderArea = new Rectangle( position.X - ( TextSurface.RenderArea.Width / 2 ), position.Y - ( TextSurface.RenderArea.Height / 2 ), TextSurface.RenderArea.Width, TextSurface.RenderArea.Height );
			////Move ActorLayer with "parent"
			//ActorLayer.TextSurface.RenderArea = TextSurface.RenderArea;

			//Tiles in previous version of FOV gets cleared
			map.ResetFov();

			//VisibleTiles.Clear();
			//Generate new FOV
			#region CastRays

			Rectangle sightRect = new Rectangle( position.X - 50, position.Y - 50, 100, 100 );

			for ( int x = sightRect.X; x < sightRect.Right; x++ )
			{
				int y = sightRect.Y;  // TOP OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( map, position, new Point( x, y ), SightRadius ) );

				y = sightRect.Bottom; // BOTTOM OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( map, position, new Point( x, y ), SightRadius ) );
			}

			for ( int y = sightRect.Y; y < sightRect.Bottom; y++ )
			{
				int x = sightRect.X; // LEFT OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( map, position, new Point( x, y ), SightRadius ) );

				x = sightRect.Right;  // RIGHT OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( map, position, new Point( x, y ), SightRadius ) );
			}

			//to the right
			int distance = 0;
			int hideOpeningDistance = 3;
			for ( int x = position.X; x < position.X + SightRadius; x++ )
			{
				//check horizontal +-1 tiles
				Point tileUp = new Point( x, position.Y + 1 );
				Point tileDown = new Point( x, position.Y - 1 );
				if ( !map.IsTransparent( new Point( x, position.Y )))
				{
					if ( !map.IsTransparent(tileUp) )
						VisibleTiles.Add( tileUp );
					if ( !map.IsTransparent( tileDown ) )
						VisibleTiles.Add( tileDown );
					break;
				}
				else
				{
					if ( map.IsValid( tileUp.X, tileUp.Y ) )
					{
						if ( distance >= hideOpeningDistance && map.IsTransparent( tileUp ) )
						{
							VisibleTiles.Remove( tileUp );
						}
						else
							VisibleTiles.Add( tileUp );
					}
					if ( map.IsValid( tileDown.X, tileDown.Y ) )
					{
						if ( distance >= hideOpeningDistance && map.IsTransparent( tileDown ) )
						{
							VisibleTiles.Remove( tileDown );
						}
						else
							VisibleTiles.Add( tileDown );
					}
				}
				distance++;
			}
			distance = 0;
			for ( int x = position.X; x > position.X - SightRadius; x-- )
			{
				//check horizontal +-1 tiles
				Point tileUp = new Point( x, position.Y + 1 );
				Point tileDown = new Point( x, position.Y - 1 );
				if ( !map.IsTransparent( new Point( x, position.Y )))
				{
					if ( !map.IsTransparent( tileUp )) 
						VisibleTiles.Add( tileUp );
					if ( !map.IsTransparent( tileDown )) 
						VisibleTiles.Add( tileDown );
					break;
				}
				else
				{
					if ( map.IsValid( tileUp.X, tileUp.Y ) )
					{
						if ( distance >= hideOpeningDistance && map.IsTransparent( tileUp )) 
						{
							VisibleTiles.Remove( tileUp );
						}
						else
							VisibleTiles.Add( tileUp );
					}
					if ( map.IsValid( tileDown.X, tileDown.Y ) )
					{
						if ( distance >= hideOpeningDistance && map.IsTransparent( tileDown )) 
						{
							VisibleTiles.Remove( tileDown );
						}
						else
							VisibleTiles.Add( tileDown );
					}
				}
				distance++;
			}
			//to the up
			distance = 0;
			for ( int y = position.Y; y > position.Y - SightRadius; y-- )
			{
				//check horizontal +-1 tiles
				Point tileRight = new Point( position.X + 1, y );
				Point tileLeft = new Point( position.X - 1, y );
				if ( !map.IsTransparent( new Point( position.X, y )))
				{
					if ( !map.IsTransparent(tileRight) )
						VisibleTiles.Add( tileRight );
					if ( !map.IsTransparent( tileLeft) )
						VisibleTiles.Add( tileLeft );
					break;
				}
				else
				{
					if ( map.IsValid( tileRight.X, tileRight.Y ) )
					{
						if ( distance >= hideOpeningDistance && map.IsTransparent( tileRight ) )
						{
							VisibleTiles.Remove( tileRight );
						}
						else
							VisibleTiles.Add( tileRight );
					}
					if ( map.IsValid( tileLeft.X, tileLeft.Y ) )
					{
						if ( distance >= hideOpeningDistance && map.IsTransparent( tileLeft ) )
						{
							VisibleTiles.Remove( tileLeft );
						}
						else
							VisibleTiles.Add( tileLeft );
					}

				}
				distance++;
			}
			//to the Down
			distance = 0;
			for ( int y = position.Y; y < position.Y + SightRadius; y++ )
			{
				//check horizontal +-1 tiles
				Point tileRight = new Point( position.X + 1, y );
				Point tileLeft = new Point( position.X - 1, y );
				if ( !map.IsTransparent( new Point( position.X, y )))
				{
					if ( !map.IsTransparent(tileRight) )
						VisibleTiles.Add( tileRight );
					if ( !map.IsTransparent( tileLeft) )
						VisibleTiles.Add( tileLeft );
					break;
				}
				else
				{
					if ( map.IsValid( tileRight.X, tileRight.Y ) )
					{
						if ( distance >= hideOpeningDistance && map.IsTransparent(tileRight) )
						{
							VisibleTiles.Remove( tileRight );
						}
						else
							VisibleTiles.Add( tileRight );
					}
					if ( map.IsValid( tileLeft.X, tileLeft.Y ) )
					{
						if ( distance >= hideOpeningDistance && map.IsTransparent( tileLeft ) )
						{
							VisibleTiles.Remove( tileLeft );
						}
						else
							VisibleTiles.Add( tileLeft );
					}

				}
				distance++;
			}

			#endregion

			//Remove duplicate entries from FOV 
			List<Point> uniq = VisibleTiles.Distinct().ToList();
			//VisibleTiles = uniq;

			//Tiles in new FOV set
			System.Console.WriteLine( "Visibletiles: " + uniq.Count.ToString() );
			foreach ( Point tile in uniq )
			{
				map.SetFov( tile, true );

			}

			//Render "Monsters" in FOV
			//foreach ( Monster m in actors )
			//{
			//	if ( !map[m.Position].Info.IsInFOV )
			//	{
			//		m.InFov = false;
			//		m.UnRenderFromCell( ActorLayer[m.Position.X, m.Position.Y] );
			//	}
			//	else
			//	{
			//		m.InFov = true;
			//		m.RenderToCell( ActorLayer[m.Position.X, m.Position.Y] );
			//	}
			//}
			return VisibleTiles;
		}

	}
}
