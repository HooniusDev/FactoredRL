using Factored.Utils;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.MapObjects
{
	public static class CellAppearances
	{
		//public static Tuple<TileTypes, CellAppearance> FloorFov = new Tuple<TileTypes, CellAppearance>( TileTypes.Floor, new CellAppearance( Colors.FloorFov, Colors.FloorBackgroundFov, 46 ));
		//public static Tuple<TileTypes, CellAppearance> Floor = new Tuple<TileTypes, CellAppearance>( TileTypes.Floor, new CellAppearance( Colors.Floor, Colors.FloorBackground, 46 ) );
		//public static Tuple<TileTypes, CellAppearance> WallFov = new Tuple<TileTypes, CellAppearance>( TileTypes.Wall, new CellAppearance( Colors.WallFov, Colors.WallBackgroundFov, 35 ) );
		//public static Tuple<TileTypes, CellAppearance> Wall = new Tuple<TileTypes, CellAppearance>( TileTypes.Wall, new CellAppearance( Colors.Wall, Colors.WallBackground, 35 ));
		//public static Tuple<TileTypes, CellAppearance> Corridor = new Tuple<TileTypes, CellAppearance>( TileTypes.Corridor, new CellAppearance( Colors.Corridor, Colors.CorridorBG, 44 ));
		//public static Tuple<TileTypes, CellAppearance> DoorClosed = new Tuple<TileTypes, CellAppearance>( TileTypes.Door, new CellAppearance( Colors.Gold, Colors.FloorBackground, 43 ));
		//public static Tuple<TileTypes, CellAppearance> StairsUpFov = new Tuple<TileTypes, CellAppearance>( TileTypes.StairsUp, new CellAppearance( Colors.FloorFov, Colors.FloorBackgroundFov, 43 ) );
		//public static Tuple<TileTypes, CellAppearance> StairsUp = new Tuple<TileTypes, CellAppearance>( TileTypes.StairsUp, new CellAppearance( Colors.Floor, Colors.FloorBackground, 43 ) );


		public static readonly CellAppearance FloorFov = new CellAppearance( Colors.FloorFov, Colors.FloorBackground, 46 );
		public static readonly CellAppearance FloorExplored = new CellAppearance( Colors.Floor, Colors.FloorBackground, 46 );
		public static readonly CellAppearance WallFov = new CellAppearance( Colors.WallFov, Colors.WallBackground, 35 );
		public static readonly CellAppearance WallExplored = new CellAppearance( Colors.Wall, Colors.WallBackground, 35 );
		public static readonly CellAppearance CorridorFov = new CellAppearance( Colors.FloorFov, Colors.CorridorBG, 44 );
		public static readonly CellAppearance CorridorExplored = new CellAppearance( Colors.Floor, Colors.CorridorBG, 44 );
		public static readonly CellAppearance StairsUpFov = new CellAppearance( Colors.Gold, Colors.DefaultBG, 60 );
		public static readonly CellAppearance DoorOpenFov = new CellAppearance( Swatch.DbWood, Colors.DefaultBG, 95 );
		public static readonly CellAppearance DoorClosedFov = new CellAppearance( Swatch.DbWood, Colors.DefaultBG, 43 );
		public static readonly CellAppearance OUTOFFov = new CellAppearance( Color.Black, Swatch.AlternateDarkest, 33 );

		public static readonly ICellEffect ExploredEffect = new Recolor()
		{
			Foreground = Color.LightGray * 0.3f,
			Background = Color.LightGray * 0.3f,
			DoForeground = true,
			DoBackground = true,
			CloneOnApply = false
		};

		public static readonly ICellEffect FovEffect = new Recolor()
		{
			Foreground = Color.White,
			Background = Color.White,
			DoForeground = true,
			DoBackground = true,
			CloneOnApply = false
		};

		public static readonly ICellEffect HiddenEffect = new Recolor()
		{
			Foreground = Color.Black,
                Background = Color.Black,
                DoForeground = true,
                DoBackground = true,
                CloneOnApply = false

			};

		public static readonly ICellEffect HighlighEffect = new Recolor()
		{
			Foreground = Color.White,
			Background = Color.Turquoise,
			DoForeground = true,
			DoBackground = true,
			CloneOnApply = false
		};

		//Generic get tile Appearance()
	}
}
