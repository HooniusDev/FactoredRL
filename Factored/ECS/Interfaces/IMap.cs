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
		bool IsValid( int x, int y );
		bool IsWalkable( Point tile );
		bool IsExplored( Point tile );
		bool IsTransparent( Point tile );
		void SetTile( Point tile, TileType type );
		TileType GetTileType( Point tile );
		TileType GetTileType( int x, int y );

		bool IsInFov( int eid, int eid1 );
		void SetFov( Point tile);
		int Height();
		int Width();
		void ResetFov();
		

	}
}
