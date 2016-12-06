using Factored.Utils;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.MapObjects
{
	class Wall : MapObject
	{

		public Wall() : base ( Colors.Wall, Colors.WallBackground, 35 )
		{
			BlocksLight = true;
			BlocksMove = true;
			InFOV = false;
			Explored = false;
			Desctiption = "There's a wall";
		}

	}
}
