using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.Systems
{
	public class Fov
	{

		//public class Fov(  )

		private void Calculate()
		{

			//Tiles in previous version of FOV gets cleared
			foreach ( Point tile in VisibleTiles )
			{
				Map[tile].Info.IsInFOV = false;
				Map[tile].Info.IsExplored = true;
				Map[tile].RenderToCell( this[tile.X, tile.Y] );
			}

			VisibleTiles.Clear();
			//Generate new FOV
			#region CastRays

			Rectangle sightRect = new Rectangle( hero.Position.X - 50, hero.Position.Y - 50, 100, 100 );

			for ( int x = sightRect.X; x < sightRect.Right; x++ )
			{
				int y = sightRect.Y;  // TOP OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( Map, hero.Position, new Point( x, y ), hero.SightRange ) );

				y = sightRect.Bottom; // BOTTOM OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( Map, hero.Position, new Point( x, y ), hero.SightRange ) );
			}

			for ( int y = sightRect.Y; y < sightRect.Bottom; y++ )
			{
				int x = sightRect.X; // LEFT OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( Map, hero.Position, new Point( x, y ), hero.SightRange ) );

				x = sightRect.Right;  // RIGHT OF SIGHT RECT

				VisibleTiles.AddRange( TileLine.GetLosLine( Map, hero.Position, new Point( x, y ), hero.SightRange ) );
			}

			//to the right
			int distance = 0;
			int hideOpeningDistance = 3;
			for ( int x = hero.Position.X; x < hero.Position.X + hero.SightRange; x++ )
			{
				//check horizontal +-1 tiles
				Point tileUp = new Point( x, hero.Position.Y + 1 );
				Point tileDown = new Point( x, hero.Position.Y - 1 );
				if ( !Map.ContainsKey( new Point( x, hero.Position.Y ) ) || !Map[new Point( x, hero.Position.Y )].Info.IsTransparent )
				{
					if ( Map.ContainsKey( tileUp ) && !Map[tileUp].Info.IsTransparent )
						VisibleTiles.Add( tileUp );
					if ( Map.ContainsKey( tileDown ) && !Map[tileDown].Info.IsTransparent )
						VisibleTiles.Add( tileDown );
					break;
				}
				else
				{
					if ( Map.ContainsKey( tileUp ) )
					{
						if ( distance >= hideOpeningDistance && Map[tileUp].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileUp );
						}
						else
							VisibleTiles.Add( tileUp );
					}
					if ( Map.ContainsKey( tileDown ) )
					{
						if ( distance >= hideOpeningDistance && Map[tileDown].Info.IsTransparent )
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
			for ( int x = hero.Position.X; x > hero.Position.X - hero.SightRange; x-- )
			{
				//check horizontal +-1 tiles
				Point tileUp = new Point( x, hero.Position.Y + 1 );
				Point tileDown = new Point( x, hero.Position.Y - 1 );
				if ( !Map.ContainsKey( new Point( x, hero.Position.Y ) ) || !Map[new Point( x, hero.Position.Y )].Info.IsTransparent )
				{
					if ( Map.ContainsKey( tileUp ) && !Map[tileUp].Info.IsTransparent )
						VisibleTiles.Add( tileUp );
					if ( Map.ContainsKey( tileDown ) && !Map[tileDown].Info.IsTransparent )
						VisibleTiles.Add( tileDown );
					break;
				}
				else
				{
					if ( Map.ContainsKey( tileUp ) )
					{
						if ( distance >= hideOpeningDistance && Map[tileUp].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileUp );
						}
						else
							VisibleTiles.Add( tileUp );
					}
					if ( Map.ContainsKey( tileDown ) )
					{
						if ( distance >= hideOpeningDistance && Map[tileDown].Info.IsTransparent )
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
			for ( int y = hero.Position.Y; y > hero.Position.Y - hero.SightRange; y-- )
			{
				//check horizontal +-1 tiles
				Point tileRight = new Point( hero.Position.X + 1, y );
				Point tileLeft = new Point( hero.Position.X - 1, y );
				if ( !Map.ContainsKey( new Point( hero.Position.X, y ) ) || !Map[new Point( hero.Position.X, y )].Info.IsTransparent )
				{
					if ( Map.ContainsKey( tileRight ) && !Map[tileRight].Info.IsTransparent )
						VisibleTiles.Add( tileRight );
					if ( Map.ContainsKey( tileLeft ) && !Map[tileLeft].Info.IsTransparent )
						VisibleTiles.Add( tileLeft );
					break;
				}
				else
				{
					if ( Map.ContainsKey( tileRight ) )
					{
						if ( distance >= hideOpeningDistance && Map[tileRight].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileRight );
						}
						else
							VisibleTiles.Add( tileRight );
					}
					if ( Map.ContainsKey( tileLeft ) )
					{
						if ( distance >= hideOpeningDistance && Map[tileLeft].Info.IsTransparent )
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
			for ( int y = hero.Position.Y; y < hero.Position.Y + hero.SightRange; y++ )
			{
				//check horizontal +-1 tiles
				Point tileRight = new Point( hero.Position.X + 1, y );
				Point tileLeft = new Point( hero.Position.X - 1, y );
				if ( !Map.ContainsKey( new Point( hero.Position.X, y ) ) || !Map[new Point( hero.Position.X, y )].Info.IsTransparent )
				{
					if ( Map.ContainsKey( tileRight ) && !Map[tileRight].Info.IsTransparent )
						VisibleTiles.Add( tileRight );
					if ( Map.ContainsKey( tileLeft ) && !Map[tileLeft].Info.IsTransparent )
						VisibleTiles.Add( tileLeft );
					break;
				}
				else
				{
					if ( Map.ContainsKey( tileRight ) )
					{
						if ( distance >= hideOpeningDistance && Map[tileRight].Info.IsTransparent )
						{
							VisibleTiles.Remove( tileRight );
						}
						else
							VisibleTiles.Add( tileRight );
					}
					if ( Map.ContainsKey( tileLeft ) )
					{
						if ( distance >= hideOpeningDistance && Map[tileLeft].Info.IsTransparent )
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
			//List<Point> uniq = VisibleTiles.Distinct().ToList();
			//VisibleTiles.Clear();
			//VisibleTiles = uniq;

			//Tiles in new FOV set 
		//	foreach ( Point tile in VisibleTiles )
		//	{
		//		Map[tile].Info.IsInFOV = true;
		//		Map[tile].RenderToCell( this[tile.X, tile.Y] );
		//	}

		//	//Render "Monsters" in FOV
		//	foreach ( Monster m in actors )
		//	{
		//		if ( !Map[m.Position].Info.IsInFOV )
		//		{
		//			m.InFov = false;
		//			m.UnRenderFromCell( ActorLayer[m.Position.X, m.Position.Y] );
		//		}
		//		else
		//		{
		//			m.InFov = true;
		//			m.RenderToCell( ActorLayer[m.Position.X, m.Position.Y] );
		//		}
		//	}
		//}


		//internal void Walk( Point point )
		//{
		//	//Move Hero and open doors when bumped on
		//	Point destination = hero.Position + point;
		//	if ( Map.ContainsKey( destination ) )
		//	{
		//		if ( Map[destination].Info.BlocksMove == true )
		//		{
		//			if ( Map[destination] is IOpenable )
		//			{
		//				IOpenable d = Map[destination] as IOpenable;
		//				d.Open();

		//				Map[destination].RemoveCellFromView( this[destination.X, destination.Y] );
		//				Map[destination].RenderToCell( this[destination.X, destination.Y] );
		//				UpdateFov();
		//			}
		//			MessagesConsole.Instance.PrintMessage( "Blocked Move!" );
		//			return;
		//		}
		//		// Handle render to new pos and unrendering from previous pos
		//		if ( new Rectangle( 0, 0, Width - 1, Height - 1 ).Contains( destination ) )
		//		{
		//			hero.UnRenderFromCell( ActorLayer[hero.Position.X, hero.Position.Y] );
		//			hero.Position = destination;
		//			MessagesConsole.Instance.PrintMessage( "You see here: " + Map[destination].Info.Description );
		//			UpdateFov();
		//			hero.RenderToCell( ActorLayer[destination.X, destination.Y] );
		//			// Update Stats screen
		//			hero.OnChangedEvent();

		//		}
		//	}
		}

	}
}
