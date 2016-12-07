using System;
using Console = SadConsole.Consoles.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SadConsole.Input;
using SadConsole;

namespace Factored.Consoles
{
	public class GuiConsole : Console
	{
		
		//public string mouseInfo;


		public GuiConsole( int width, int height ) : base( width, height )
		{
			//Engine.ActiveConsole = this;
			TextSurface.RenderArea = new Rectangle( 0, 0, width, height );
			VirtualCursor.Position = new Point( 1, 1 );
	
			//Engine.UseMouse = true;
			//Engine.UseMouse = true;
		}

		public void Mouse( MouseInfo info )
		{
			//System.Console.WriteLine( "Process mouse Gui" );
			Clear();
			VirtualCursor.Position = new Point( 1, 1 );
			Point mapCell = info.WorldLocation + TextSurface.RenderArea.Location;
			RootConsole.mapViewport.renderer.HighlightTile( mapCell );
			//mouseConsole.VirtualCursor.Position = infoPos;
			VirtualCursor.Print( mapCell.ToString() ).Right( 2 );
			VirtualCursor.Print( GameConstants.Map.GetTileType( mapCell ).ToString() ).CarriageReturn();
			//VirtualCursor.Position = new Point( 40, 40 );
			//VirtualCursor.Print( "AAAAAAAAAAAAAAAAAAAAAAAAAAARGHH!!" );
			//SetCellAppearance( 2, 2, new CellAppearance( Color.Blue, Color.Yellow, 42 ));
			//Appearance.CopyAppearanceTo( sadConsoleCell );

		}

		public override bool ProcessMouse( MouseInfo info )
		{
			//System.Console.WriteLine( "Process ProcessMouse Gui" );
			RootConsole.guiConsole.Mouse( info );

			return base.ProcessMouse( info );
		}
	}
	

}
