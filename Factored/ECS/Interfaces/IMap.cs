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
		bool isWalkable( Point tile );
		bool isExplored( Point tile );
		bool isTransparent( Point tile );
		void SetTile( Point tile, TileType type );
		TileType GetTile( Point tile );

		bool IsInFov( int eid, int eid1 );

	}
}
