using System;
using Console = SadConsole.Consoles.Console;
using SadConsole.Consoles;
using Microsoft.Xna.Framework;
using Factored.Consoles;

namespace Factored
{
	public class Program
	{
		public static readonly int screenWidth = 60;
		public static readonly int screenHeight = 50;

		public static ConsoleList _rootConsole { get; private set; }

		static void Main( string[] args )
		{
			// Setup the engine and creat the main window.
			SadConsole.Engine.Initialize( "Cheepicus12.font", screenWidth, screenHeight );

			// Hook the start event so we can add consoles to the system.
			SadConsole.Engine.EngineStart += Engine_EngineStart;

			// Hook the update event that happens each frame so we can trap keys and respond.
			SadConsole.Engine.EngineUpdated += Engine_EngineUpdated;

			// Start the game.
			SadConsole.Engine.Run();
		}

		private static void Engine_EngineStart( object sender, EventArgs e )
		{
			_rootConsole = new RootConsole();
			SadConsole.Engine.ConsoleRenderStack.Add( _rootConsole );
			System.Console.WriteLine( "Welcome to the RL..." );
			//_rootConsole.Print( 1, 1, "Welcome to SadConsole", Color.Aqua, Color.Black );
		}

		private static void Engine_EngineUpdated( object sender, EventArgs e )
		{
			//System.Console.WriteLine( "Welcome to the RL..." );
		}
	}
}

