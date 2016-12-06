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
	public abstract class TileCell : RogueSharp.Cell
	{
		public CellAppearance Appearance { get; internal set; }

		public TileCell( int x, int y, bool isTransparent, bool isWalkable, bool isFov, bool isExplored ) : base( x, y, isTransparent, isWalkable, isFov, isExplored )
		{
		}

		public virtual void RenderToCell( SadConsole.Cell sadConsoleCell )
		{
			Appearance.CopyAppearanceTo( sadConsoleCell );
		}

	}
}
