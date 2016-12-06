using Factored.Utils;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.MapObjects
{
	class Corridor : MapObject
	{

		public Corridor() : base ( Colors.Corridor, Colors.CorridorBG, 44 )
		{
			BlocksLight = false;
			BlocksMove = false;
			InFOV = false;
			Explored = false;
			Desctiption = "Narrow and dark corridor.";
		}

	}
}
