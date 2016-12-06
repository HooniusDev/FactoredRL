using Factored.Utils;
using Microsoft.Xna.Framework;
using RogueSharp;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.MapObjects
{
	public abstract class WallCell : TileCell
	{
		public static readonly CellAppearance wall = new CellAppearance( Colors.Wall, Colors.WallBackground, 35 );

		public WallCell( int x, int y ) : base( x,y, false, true, false, false )
		{
			this.Appearance = wall;
		}



	}
}
