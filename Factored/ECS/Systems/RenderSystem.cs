using System;
using Console = SadConsole.Consoles.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factored.ECS.Interfaces;

namespace Factored.ECS.Systems
{
	public class RenderSystem
	{
		private Console MapLayer;
		private Console EntityLayer;
		private Console GuiLayer;

		private Console[] layers;

		public RenderSystem( int width, int height )
		{
		}




	}

	public enum RenderLayer
	{
		Map = 0,
		Entity = 1,
		Gui = 2
	}
}
