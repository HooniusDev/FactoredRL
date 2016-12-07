using Factored.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Console = SadConsole.Consoles.Console;
using Factored.MapObjects;
using SadConsole.Input;
using Microsoft.Xna.Framework.Input;
using Factored.ECS;
using Factored.ECS.Component;
using SadConsole;
using Factored.Test;
using Factored.ECS.Systems;
using Factored.Actions;
using Factored.Actions.Interfaces;
using Factored.Systems;

namespace Factored.Consoles
{

	public class MapViewport : Console
	{

		MapSystem map;

		public RenderingSystem renderer { get; private set; }

		public MapViewport( int width, int height ) : base( GameConstants.MapWidth, GameConstants.MapHeight )
		{
			Engine.ActiveConsole = this;
			Engine.UseMouse = true;

			Engine.Keyboard.RepeatDelay = 0.07f;
			Engine.Keyboard.InitialRepeatDelay = 0.1f;

			TextSurface.RenderArea = new Microsoft.Xna.Framework.Rectangle( 0, 0, width, height );

			Fill( null, Colors.DefaultBG, null );

			renderer = new RenderingSystem( this );

			map = new MapSystem( GameConstants.MapWidth, GameConstants.MapHeight );
			//ComponentManager.AddComponent( EntityManager.Map, map );
		}

		public override void Render()
		{
			//centerView( ComponentManager.GetComponent<PositionComponent>( EntityManager.Player ).Position);
			
			base.Render();
			RootConsole.guiConsole.Render();
			renderer.RenderMap( map );
			renderer.RenderEntities();
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
						renderer.RedrawTile( pc.Position );
					action.Perform();
				}

			}

		}


		public override bool ProcessMouse( MouseInfo info )
		{
			//System.Console.WriteLine( "Process mouse Gui" );
			RootConsole.guiConsole.Mouse( info );
			
			

			return base.ProcessMouse( info );
		}

		public override bool ProcessKeyboard( KeyboardInfo info )
		{

			Point newPosition = Point.Zero;

			if ( info.IsKeyReleased( Keys.Up ) )
			{
				PlayerControlComponent p = ComponentManager.GetComponent<PlayerControlComponent>( 0 );
				p.SetAction( new MoveAction( EntityManager.Player, new Point( 0,-1), 10) );
			}
			else if ( info.IsKeyReleased( Keys.Down ) ) 
			{
				PlayerControlComponent p = ComponentManager.GetComponent<PlayerControlComponent>( 0 );
				p.SetAction( new MoveAction( EntityManager.Player, new Point( 0, 1 ), 10 ) );
			}
			else if ( info.IsKeyReleased(Keys.Left ) ) 
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
				p.SetAction( new CloseAction( EntityManager.Player));
			}
			//else if ( info.IsKeyReleased( Keys.Left ) ) 
			//{
			//	newPosition.X -= 1;
			//	keyHit = true;
			//}
			//else if ( info.IsKeyReleased( Keys.Right ) ) 
			//{
			//	newPosition.X += 1;
			//	keyHit = true;
			//}


			//// Test location
			//if ( keyHit )
			//{

			//	Map.MoveActor( GameConstants.player, newPosition );
			//	//UpdatePlayerView();

			//}
			return false;
		}

		private void centerView( Point point )
		{

			//Center view to Hero
			TextSurface.RenderArea = new Rectangle( point.X - ( TextSurface.RenderArea.Width / 2 ),  point.Y - ( TextSurface.RenderArea.Height / 2 ), TextSurface.RenderArea.Width, TextSurface.RenderArea.Height );
			//TextSurface.RenderArea = new Microsoft.Xna.Framework.Rectangle( 50, 50, 100, 100 );
			//System.Console.WriteLine( "renrer area: " + TextSurface.RenderArea.ToString() );
			//System.Console.WriteLine( "Player pos: " + GameConstants.player.Position.ToString() );
		}

	}
}
