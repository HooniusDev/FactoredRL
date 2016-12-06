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
	public abstract class FloorCell : TileCell
	{
		public static readonly CellAppearance floor = new CellAppearance( Colors.Floor, Colors.FloorBackground, 46 );

		public FloorCell( int x, int y ) : base( x,y, false, true, false, false )
		{
			this.Appearance = floor;
		}



	}
}
