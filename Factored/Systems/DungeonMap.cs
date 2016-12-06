using Factored.Consoles;
using RogueSharp;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.MapCreation;
using Factored.MapBuilder;
using Factored.Actors;
using Point = Microsoft.Xna.Framework.Point;

namespace Factored.Systems
{
	public class DungeonMap
	{

		private Dictionary<Point, TileFeature> features;
		private Dictionary<Actor, Point> actors;

		public static Map map { get; private set; }
		public List<Rectangle> Rooms { get; private set; }

		public DungeonMap(  )
		{
			//map = RogueSharp.Map.Create( mapCreationStrategy );
			features = new Dictionary<Point, TileFeature>();
			actors = new Dictionary<Actor, Point>();

			csMapbuilder b = new csMapbuilder( GameConstants.MapWidth, GameConstants.MapHeight );
			b.Build_OneStartRoom();
			Rooms = b.GetRooms();
			map = new Map( GameConstants.MapWidth, GameConstants.MapHeight );
			CopyMap( b );
			AddPlayer();
		}


		private void AddPlayer()
		{
			Point startPos = GetRandomEmpty();
			actors.Add( GameConstants.player, startPos );
			SetTile( startPos, TileTypes.StairsUp );
			UpdatePlayerFieldOfView();
			MapViewport.needsRedraw = true;
		}

		public Point GetActorPosition( Actor actor )
		{
			if ( actors.ContainsKey( actor ) )
				return actors[actor];
			else
				return new Point(-1,-1);
		}

		public void MoveActor( Actor actor, Point dir )
		{
			if ( actors.ContainsKey( actor ) )
			{
			Point destination = new Point( actors[actor].X + dir.X, actors[actor].Y + dir.Y);
				if ( map.IsWalkable( destination.X, destination.Y ))
				{
					System.Console.WriteLine( "walkable (" + destination.ToString() + "): " + DungeonMap.map.IsWalkable( destination.X, destination.Y ) );
					actors[actor] = destination;
					if ( actor == GameConstants.player )
					{
						UpdatePlayerFieldOfView();
						MapViewport.needsRedraw = true;
					}
				}
			}
		}

		private void CopyMap( csMapbuilder b )
		{
			for ( int x = 0; x < GameConstants.MapWidth; x++ )
				for ( int y = 0; y < GameConstants.MapHeight; y++ )
				{
					TileTypes type = ( TileTypes ) b.GetCell( x, y );
					SetTile( new Point( x, y ), type );
				}
			
		}

		public static void UpdatePlayerFieldOfView()
		{
			Player player = GameConstants.player;
			// Compute the field-of-view based on the player's location and awareness
			map.ComputeFov( GameConstants.player.Position.X, GameConstants.player.Position.Y, player.Awareness, true );
			// Mark all cells in field-of-view as having been explored
			foreach ( Cell cell in map.GetAllCells() )
			{
				if ( map.IsInFov( cell.X, cell.Y ) )
				{
					map.SetCellProperties( cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true );
				}
			}
		}

		public void SetTile( Point tile, TileTypes tileType )
		{
			switch ( tileType )
			{
				case TileTypes.Floor:
					{
						map.SetCellProperties( tile.X, tile.Y, true, true, false );
						break;
					}
				case TileTypes.Wall:
					{
						map.SetCellProperties( tile.X, tile.Y, false, false, false );
						break;
					}
				case TileTypes.Corridor:
					{
						map.SetCellProperties( tile.X, tile.Y, true, true, false );
						break;
					}
				case TileTypes.Door:
					{
						features.Add( tile, TileFeature.DoorClosed );
						map.SetCellProperties( tile.X, tile.Y, false, false, false );
						break;
					}
				case TileTypes.StairsUp:
					{
						features.Add( tile, TileFeature.StairsUp );
						map.SetCellProperties( tile.X, tile.Y, true, true, false );
						break;
					}
				case TileTypes.None:
					{
						break;
					}
			}
		}

		public Dictionary<Point, TileFeature> GetTileFeatures()
		{
			return features;
		}
		public Dictionary<Actor, Point> GetActors()
		{
			return actors;
		}
		

		public bool IsWalkable( int x, int y )
		{
			return map.GetCell( x, y ).IsWalkable;
		}

		public IEnumerable<Cell> GetAllCells()
		{
			return map.GetAllCells();
		}

		private Point GetRandomEmpty( int roomID = -1 )
		{
				

			Point p;
			int r = roomID;

			if ( roomID == -1 )
				r = GameConstants.random.Next( Rooms.Count - 1 );

			Rectangle room = Rooms[r];
			//System.Console.WriteLine( "room spawn: " + r.ToString() );


			while ( true )
			{
				int x = GameConstants.random.Next( room.X + 1, room.Right - 1 );
				int y = GameConstants.random.Next( room.Y + 1, room.Bottom - 1 );
				p = new Point( x, y );

				//foreach ( ActorBase m in ActorBase.actors )
				//{
				//	if ( m.Position == p )
				//		break;
				//}

				break;
			}

			//System.Console.WriteLine( "spawn point:" + p.ToString() );
			return p;
		}

	}

	public enum TileFeature
	{
		DoorClosed,
		DoorOpen,
		StairsDown,
		StairsUp
	}

	public enum TileTypes
	{
		None = -1,
		Floor = 0,
		Wall = 1,
		Corridor = 2,
		Door = 3,
		StairsUp = 4
	}

}

