using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.MapObjects
{
	public class MapObject
	{
		public CellAppearance Appearance { get; internal set; }
		public bool BlocksMove;
		public bool BlocksLight;
		public bool Explored;
		public bool InFOV;

		public string Desctiption;

		public MapObject( Color foreground, Color background, int character )
		{
			Appearance = new CellAppearance( foreground, background, character );
			BlocksLight = true;
			BlocksMove = true;
			Desctiption = "Base MapObject";
		}

		public virtual void RenderToCell( SadConsole.Cell sadConsoleCell )
		{
			Appearance.CopyAppearanceTo( sadConsoleCell );
		}
	}
}
