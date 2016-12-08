using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Factored.Systems.MapSystem;

namespace Factored.ECS.Interfaces
{
	public interface IMap
	{
		bool IsValid( Point tile );
		bool IsValid( int x, int y );
		
		bool IsWalkable( Point tile );
		bool IsWalkable( int x, int y );

		bool IsExplored( Point tile );
		bool IsExplored( int x, int y );

		bool IsTransparent( Point tile );
		bool IsTransparent( int x, int y );

		bool IsFov( Point tile );
		bool IsFov( int x, int y ); 

		void SetTileType( Point tile, TileType type );
		void SetTileType( int x, int y, TileType type );

		TileType GetTileType( Point tile );
		TileType GetTileType( int x, int y );

		void SetFov( Point tile, bool fov = false );
		void SetFov( List<Point> tiles, bool fov = false );

		List<Point> GetTilesToDraw();
		void ClearTilesToDraw();
		List<Point> GetTilesToClear();
		void ClearTilesToClear();

		//void TileChanged( int x, int y );



		bool HasLos( Point tile1, Point tile2 );

		int Height();
		int Width();

		void ResetFov();
		

	}
}
