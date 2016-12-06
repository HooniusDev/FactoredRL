using Factored.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole;

namespace Factored.MapObjects.Features
{
	class Door : MapObject
	{
		public bool Closed{ get; private set; }

		public Door( bool closed ) : base ( Colors.Floor, Colors.FloorBackground, 43 )
		{
			Closed = closed;
			if ( closed )
			{
				BlocksLight = true;
				BlocksMove = true;
				Desctiption = "Firmly closed door with a tiny keyhole.";
			}
			else
			{
				Appearance = new CellAppearance( Colors.Floor, Colors.FloorBackground, 46 );
				BlocksLight = false;
				BlocksMove = false;
				Desctiption = "There is a open door.";
			}
			InFOV = false;
			Explored = false;	
		}

		public override void RenderToCell( Cell sadConsoleCell )
		{
			//if (Closed) //Render only closed doors...
				base.RenderToCell( sadConsoleCell );

		}
	}
}
