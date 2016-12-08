
using Console = SadConsole.Consoles.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole.Consoles;
using Microsoft.Xna.Framework;
using Factored.Systems;
using SadConsole.Input;
using Factored.ECS.Component;
using Factored.ECS;
using Factored.Actions;
using Microsoft.Xna.Framework.Input;
using Factored.Actions.Interfaces;
using Factored.ECS.Systems;
using SadConsole;

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

			CanUseKeyboard = true;
			Engine.ActiveConsole = this;

			EntityManager.Init();

			MapLayer = new MapLayerConsole( width, height, GameConstants.Map, GameConstants.MapWidth, GameConstants.MapHeight );
			EntityLayer = new EntityLayerConsole( width, height, GameConstants.Map, GameConstants.MapWidth, GameConstants.MapHeight );

			Add( MapLayer );
			//MapLayer.Init();
			Add( EntityLayer );


		}

		public override void Render()
		{
			base.Render();
			//EntityLayer.RenderEntities();
		}

		public override void Update()
		{
			base.Update();
			PlayerControlComponent p = ComponentManager.GetComponent<PlayerControlComponent>( 0 );
			PositionComponent pc = ComponentManager.GetComponent<PositionComponent>( 0 );
			IAction action = p.GetAction();
			if ( action != null )
			{
				bool canDo = action.CanPerform();
				if ( canDo )
				{
					if ( action is MoveAction )
					{
						//renderer.RedrawTile( pc.Position );
						if ( p != null )
						{
							action.Perform();
							Fov.GetFov( GameConstants.Map, pc.Position, 20 );
							MapLayer.RenderAppearances();
						}
					}
					
				}

			}
		}

		public override bool ProcessKeyboard( KeyboardInfo info )
		{
			Point newPosition = Point.Zero;

			if ( info.IsKeyReleased( Keys.Up ) )
			{
				PlayerControlComponent p = ComponentManager.GetComponent<PlayerControlComponent>( 0 );
				p.SetAction( new MoveAction( EntityManager.Player, new Point( 0, -1 ), 10 ) );
			}
			else if ( info.IsKeyReleased( Keys.Down ) )
			{
				PlayerControlComponent p = ComponentManager.GetComponent<PlayerControlComponent>( 0 );
				p.SetAction( new MoveAction( EntityManager.Player, new Point( 0, 1 ), 10 ) );
			}
			else if ( info.IsKeyReleased( Keys.Left ) )
			{
				PlayerControlComponent p = ComponentManager.GetComponent<PlayerControlComponent>( 0 );
				p.SetAction( new MoveAction( EntityManager.Player, new Point( -1, 0 ), 10 ) );
			}
			else if ( info.IsKeyReleased( Keys.Right ) )
			{
				PlayerControlComponent p = ComponentManager.GetComponent<PlayerControlComponent>( EntityManager.Player );
				p.SetAction( new MoveAction( EntityManager.Player, new Point( 1, 0 ), 10 ) );
			}
			else if ( info.IsKeyReleased( Keys.C ) )
			{
				PlayerControlComponent p = ComponentManager.GetComponent<PlayerControlComponent>( EntityManager.Player );
				p.SetAction( new CloseAction( EntityManager.Player ) );
			}


			return base.ProcessKeyboard( info );
		}
	}
}
