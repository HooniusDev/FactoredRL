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

		bool[,] isWalkable { get; set; }
		bool[,] isExplored { get; set; }
		bool[,] isTransparent { get; set; }
		bool[,] isFov { get; set; }
		int[,] blockintEntity { get; set; }
		TileType[,] tileType { get; set; }
		int[,] dynamicLight { get; set; }
		int[,] staticLight { get; set; }
		int[,] permanentLight { get; set; }

		public List<Point> tilesToUpdate;
		public List<Point> GetTilesToDraw()
		{
			return tilesToUpdate;
		}

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

		public void TileChanged( int x, int y )
		{
			tilesToUpdate.Add( new Point(x,y) );
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
					SetTileType( new Point( x, y ), type );
				}

			rooms = b.GetRooms();
			
		}

		private void init()
		{
			isWalkable = new bool[mapRect.Width, mapRect.Height];
			isExplored = new bool[mapRect.Width, mapRect.Height];
			isTransparent = new bool[mapRect.Width, mapRect.Height];
			isFov = new bool[mapRect.Width, mapRect.Height];
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
					isFov[x, y] = true;
					dynamicLight[x, y] = 0;
					staticLight[x, y] = 0;
					permanentLight[x, y] = 0;
				}
		}

		#region  ############## IMAP ##############

		public bool IsValid( Point tile )
		{
			return IsValid( tile.X, tile.Y );
		}
		public bool IsValid( int x, int y )
		{
			return mapRect.Contains( new Point(x,y) );
		}

		public bool IsWalkable( Point tile )
		{
				return IsWalkable(tile.X, tile.Y);
		}
		public bool IsWalkable( int x, int y )
		{
			// Blocking Entity should be setting this flag through a system
			if ( IsValid( x, y ) )
				return isWalkable[x, y];
			else
				return false;
		}

		public bool IsExplored( Point tile )
		{
			return IsExplored( tile.X, tile.Y );
		}
		public bool IsExplored( int x, int y )
		{
			// Blocking Entity should be setting this flag through a system
			if ( IsValid( x, y ) )
				return isExplored[x, y];
			else
				return false;
		}

		public bool IsTransparent( Point tile )
		{
			return IsTransparent( tile.X, tile.Y );
		}
		public bool IsTransparent( int x, int y )
		{
			if ( IsValid( x, y ) )
				return isTransparent[x, y];
			else
				return false;
		}

		public bool IsFov( Point tile )
		{
			return IsFov( tile.X, tile.Y );
		}
		public bool IsFov( int x, int y )
		{
			if ( IsValid( x, y ) )
				return isFov[x, y];
			else
				return false;
		}

		public void SetTileType( Point tile, TileType type )
		{
			if ( !IsValid( tile ) )
				return;
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
		public void SetTileType( int x, int y, TileType type )
		{
			SetTileType( new Point( x, y), type );
		}

		public TileType GetTileType( Point tile )
		{
			return GetTileType( tile.X, tile.Y );
		}
		public TileType GetTileType( int x, int y )
		{
			if ( IsValid( x, y ) )
				return tileType[x, y];
			else
				return TileType.None;
		}

		public void SetFov( Point tile, bool inFov = true )
		{
			if ( !IsValid( tile ) )
				return;
			isFov[tile.X, tile.Y] = inFov;
			tilesToUpdate.Add( tile );
			if ( inFov == false )
				isExplored[tile.X, tile.Y] = true;
		}
		public void SetFov( int x, int y, bool inFov = true )
		{
			if ( !IsValid( x,y ) )
				return;
			Point tile = new Point( x, y );
			SetFov( tile, inFov );
		}
		public void SetFov( List<Point> tiles, bool inFov = true )
		{
			foreach ( Point tile in tiles )
			{
				SetFov( tile, inFov );
			}

		}

		public bool HasLos( Point tile1, Point tile2 )
		{
			throw new NotImplementedException();
		}

		public int Height()
		{
			return mapRect.Height;
		}
		public int Width()
		{
			return mapRect.Width;
		}

		public void ResetFov()
		{
			for ( int x = 0; x < mapRect.Width; x++ )
			{
				for ( int y = 0; y < mapRect.Height; y++ )
				{
					if ( isFov[x, y] )
					{
						isFov[x, y] = false;
						isExplored[x, y] = true;
						tilesToUpdate.Add( new Point( x, y ) );
					}
				}
			}
		}

		#endregion


		public int getBlockinEntityID( Point tile )
		{
				return blockintEntity[tile.X, tile.Y];
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
