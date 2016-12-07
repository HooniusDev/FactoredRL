using Factored.ECS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factored.ECS.Component;
using Microsoft.Xna.Framework;
using Factored.ECS;
using Factored.MapObjects;
using Factored.MapBuilder;
using Factored.Consoles;

namespace Factored.Systems
{
	public class MapSystem : IMap
	{

		public Rectangle mapRect { get; private set; }

		public List<Rectangle> rooms { get; private set; }

		public bool[,] isWalkable { get; private set; }
		public bool[,] isExplored { get; private set; }
		public bool[,] isTransparent { get; private set; }
		public bool[,] isInFov { get; private set; }
		public int[,] blockintEntity { get; private set; }
		public TileType[,] tileType { get; private set; }
		public int[,] dynamicLight { get; private set; }
		public int[,] staticLight { get; private set; }
		public int[,] permanentLight { get; private set; }

		public List<Point> tilesToUpdate;

		public MapSystem( int width, int height )
		{
			GameConstants.Map = this;
			mapRect = new Rectangle( 0, 0, width, height );
			tilesToUpdate = new List<Point>();
			init();
			buildDefaultMap();
			EntityManager.CreatePlayer();
			AddPlayer();
		}

		public void SetBlockMoveComponent( int e, Point tile )
		{
			blockintEntity[tile.X, tile.Y] = e;
		}

		private void AddPlayer()
		{
			ComponentManager.GetComponent<PositionComponent>( EntityManager.Player ).Position = GetRandomEmpty();
			Console.WriteLine( "Player at: " + ComponentManager.GetComponent<PositionComponent>( EntityManager.Player ).Position.ToString() );
			//actors.Add( GameConstants.player, startPos );
			//SetTile( startPos, TileTypes.StairsUp );
			//UpdatePlayerFieldOfView();
			//MapViewport.needsRedraw = true;
		}

		private Point GetRandomEmpty( int roomID = -1 )
		{
			int r = roomID;

			if ( roomID == -1 )
				r = GameConstants.random.Next( rooms.Count - 1 );

			Rectangle room = rooms[r];
			//System.Console.WriteLine( "room spawn: " + r.ToString() );


			int x = GameConstants.random.Next( room.X + 1, room.Right - 1 );
			int y = GameConstants.random.Next( room.Y + 1, room.Bottom - 1 );
			return new Point( x, y );

		}

				void buildDefaultMap()
		{
			csMapbuilder b = new csMapbuilder( mapRect.Width, mapRect.Height );
			b.Build_OneStartRoom();

			for ( int x = 0; x < mapRect.Width; x++ )
				for ( int y = 0; y < mapRect.Height; y++ )
				{
					TileType type = ( TileType ) b.GetCell( x, y );
					SetTile( new Point( x, y ), type );
				}

			rooms = b.GetRooms();
			
		}

		private void init()
		{
			isWalkable = new bool[mapRect.Width, mapRect.Height];
			isExplored = new bool[mapRect.Width, mapRect.Height];
			isTransparent = new bool[mapRect.Width, mapRect.Height];
			isInFov = new bool[mapRect.Width, mapRect.Height];
			tileType = new TileType[mapRect.Width, mapRect.Height];
			blockintEntity = new int[mapRect.Width, mapRect.Height];
			dynamicLight = new int[mapRect.Width, mapRect.Height];
			staticLight = new int[mapRect.Width, mapRect.Height];
			permanentLight = new int[mapRect.Width, mapRect.Height];
			for ( int x = 0; x < mapRect.Width; x++ )
				for ( int y = 0; y < mapRect.Height; y++ )
				{
					isWalkable[x, y] = false;
					isExplored[x, y] = false;
					isTransparent[x, y] = false;
					blockintEntity[x, y] = -1;
					isInFov[x, y] = true;
					dynamicLight[x, y] = 0;
					staticLight[x, y] = 0;
					permanentLight[x, y] = 0;
				}
		}

		public void SetExplored( Point tile )
		{
			isExplored[tile.X, tile.Y] = true;
		}

		#region  ##############Tests##############
		public bool IsValid( int x, int y )
		{
			return mapRect.Contains( new Point(x,y) );
		}

		public bool IsValid( Point tile )
		{
			return mapRect.Contains( tile );
		}

		bool IMap.isWalkable( Point tile )
		{
			System.Console.WriteLine( "blocked by: " + blockintEntity[tile.X, tile.Y].ToString() );
			if ( blockintEntity[tile.X, tile.Y] != -1 )
				return false;
			else
				return isWalkable[tile.X, tile.Y];
		}

		public int getBlockinEntityID( Point tile )
		{
				return blockintEntity[tile.X, tile.Y];
		}

		public bool IsWalkable( Point tile )
		{
			if ( blockintEntity[tile.X, tile.Y] != -1 )
			{
				System.Console.WriteLine( "blocked by: " + blockintEntity[tile.X, tile.Y].ToString() );
				return false;
			}
			else
			{
				System.Console.WriteLine( "blocked by isWalkable : " + tile.ToString() + ", " + isWalkable[tile.X, tile.Y] .ToString());
				return isWalkable[tile.X, tile.Y];

			}
		}

		bool IMap.isExplored( Point tile )
		{
			return isExplored[tile.X, tile.Y];
		}

		bool IMap.isTransparent( Point tile )
		{
			return isTransparent[tile.X, tile.Y];
		}

		bool IsInFov( Point tile )
		{
			return isInFov[tile.X, tile.Y];
		}

		public bool IsInFov( int eid, int eid1 )
		{
			throw new NotImplementedException();
		}
		#endregion

		public void SetTile( Point tile, TileType type )
		{
			switch ( type )
			{
				case TileType.Floor:
					{
						isWalkable[tile.X, tile.Y] = true;
						isTransparent[tile.X, tile.Y] = true;
						permanentLight[tile.X, tile.Y] = 100;
						tileType[tile.X, tile.Y] = TileType.Floor;
						tilesToUpdate.Add( tile );
						break;
					}
				case TileType.Wall:
					{
						isWalkable[tile.X, tile.Y] = false;
						isTransparent[tile.X, tile.Y] = false;
						tileType[tile.X, tile.Y] = TileType.Wall;
						tilesToUpdate.Add( tile );
						break;
					}
				case TileType.Corridor:
					{
						isWalkable[tile.X, tile.Y] = true;
						isTransparent[tile.X, tile.Y] = true;
						permanentLight[tile.X, tile.Y] = 40;
						tileType[tile.X, tile.Y] = TileType.Corridor;
						tilesToUpdate.Add( tile );
						break;
					}
				case TileType.Door:
					{
						isWalkable[tile.X, tile.Y] = true;
						isTransparent[tile.X, tile.Y] = true;
						permanentLight[tile.X, tile.Y] = 100;
						tileType[tile.X, tile.Y] = TileType.Floor;
						tilesToUpdate.Add( tile );
						int e = EntityManager.CreateEntity();
						ComponentManager.AddComponent( e, new PositionComponent( e, tile ) );
						ComponentManager.AddComponent( e, new RenderComponent( e, CellAppearances.DoorOpenFov, 1 ) );
						ComponentManager.AddComponent( e, new DoorComponent( e ) );
						//map.Add( tile, e );
						break;
					}
				case TileType.StairsUp:
					{
						isWalkable[tile.X, tile.Y] = true;
						isTransparent[tile.X, tile.Y] = true;
						permanentLight[tile.X, tile.Y] = 100;
						tileType[tile.X, tile.Y] = TileType.Floor;
						tilesToUpdate.Add( tile );
						int e = EntityManager.CreateEntity();
						ComponentManager.AddComponent( e, new PositionComponent( e, tile ) );
						ComponentManager.AddComponent( e, new RenderComponent( e, CellAppearances.StairsUpFov, 1 ) );
						break;
					}
				case TileType.None:
					{
						isWalkable[tile.X, tile.Y] = false;
						isTransparent[tile.X, tile.Y] = false;
						permanentLight[tile.X, tile.Y] = 0;
						tileType[tile.X, tile.Y] = TileType.None;
						break;
					}
			}
		}

		public TileType GetTile( Point tile )
		{
			return GetTile(tile.X, tile.Y);
		}

		public TileType GetTile( int x, int y )
		{
			if ( IsValid( x, y ))
				return tileType[x, y];
			else
				return TileType.None;
		}

		public enum TileType
		{
			None = -1,
			Floor = 0,
			Wall = 1,
			Corridor = 2,
			Door = 3,
			StairsUp = 4
		}
	}
}
