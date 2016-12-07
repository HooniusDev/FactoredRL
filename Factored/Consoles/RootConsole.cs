
using Console = SadConsole.Consoles.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole.Consoles;
using Microsoft.Xna.Framework;
using Factored.Systems;

namespace Factored.Consoles
{
	public static class GameConstants
	{
		public static readonly int MapWidth = 40;
		public static readonly int MapHeight = 40;

		public static MapSystem Map;

		public static Random random;

	}

	public class RootConsole : ConsoleList
	{

		public static MapViewport mapViewport { get; private set; }

		public static GuiConsole guiConsole;

		public RootConsole()
		{
			EntityManager.Init();
			GameConstants.random = new Random();
			mapViewport = new MapViewport( Program.screenWidth, Program.screenHeight );
			//Player p = GameConstants.player;
			//System.Console.WriteLine( "RootConsole" );
			guiConsole = new GuiConsole( Program.screenWidth, Program.screenHeight );
			
			
			Add( guiConsole );
			Add( mapViewport );


		}
	}
}
