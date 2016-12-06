using Factored.Utils;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.MapObjects
{
	class Floor : MapObject
	{

		public Floor() : base ( Colors.Floor, Colors.FloorBackground, 46 )
		{
			BlocksLight = false;
			BlocksMove = false;
			InFOV = false;
			Explored = false;
			Desctiption = "Dust covered floor with a bit of moss growing.";
		}

	}
}
