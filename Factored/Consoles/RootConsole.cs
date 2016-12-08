
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

		public static int ScreenWidth { get; set; }
		public static int ScreenHeight { get; set; }

		public static MapSystem Map;

		public static Random random;

	}

	public class RootConsole : ConsoleList
	{

		public static MapViewport mapViewport { get; private set; }

		public static MapLayerConsole MapLayer { get; private set; }
		public static EntityLayerConsole EntityLayer { get; private set; }
		public static Console GuiLayer { get; private set; }
		public static GuiConsole guiConsole;

		public RootConsole( int width, int height )
		{
			GameConstants.ScreenWidth = width;
			GameConstants.ScreenHeight = height;

			GameConstants.random = new Random();

			EntityManager.Init();

			MapLayer = new MapLayerConsole( width, height, GameConstants.Map, GameConstants.MapWidth, GameConstants.MapHeight );
			EntityLayer = new EntityLayerConsole( width, height, GameConstants.Map, GameConstants.MapWidth, GameConstants.MapHeight );

			Add( MapLayer );
			Add( EntityLayer );


		}

		public override void Render()
		{
			base.Render();
			//EntityLayer.RenderEntities();
		}
	}
}
