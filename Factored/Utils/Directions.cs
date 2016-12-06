using Factored.Consoles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.Utils
{
	public static class Directions
	{
		public static readonly Point Up = new Point( 0, -1 );
		public static readonly Point Right = new Point( 1, 0 );
		public static readonly Point Down = new Point( 0, 1 );
		public static readonly Point Left = new Point( -1, 0 );

		public static Point Random()
		{
			int r = GameConstants.random.Next( 4 );
			switch ( r )
			{
				case 0:
					return Up;
				case 1:
					return Right;
				case 2:
					return Down;
				case 3:
					return Left;
				default:
					return Point.Zero;
			}
		}
	}
}
