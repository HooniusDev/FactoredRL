using Factored.ECS.Interfaces;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Component
{
	public class RenderComponent : IComponent
	{
		public int OwnerID { get; private set; }
		public int GetOwner()
		{
			return OwnerID;
		}
		public CellAppearance Appearance;
		public ICellEffect Explored { get; set; }

		public bool Changed = true;
		public int Layer = 0;

		public RenderComponent( int owner, CellAppearance appearance, int layer = 0 )
		{
			OwnerID = owner;
			Appearance = appearance;
			Layer = layer;

			Explored = new Recolor()
			{
				Foreground = Color.LightGray * 0.3f,
				Background = Color.LightGray * 0.3f,
				DoForeground = true,
				DoBackground = true,
				CloneOnApply = false
			};
		}

		public void SetCellAppearance( CellAppearance appearance )
		{
			Appearance = appearance;
			Changed = true;
		}

		public void Render( Cell sadConsoleCell )
		{
			//if ( Changed == false )
			//	return;
			Appearance.CopyAppearanceTo( sadConsoleCell );
			Changed = false;
		}

		//public void RenderExplored( Cell sadConsoleCell )
		//{
		//	// Clear out the old effect if there was one
		//	if ( sadConsoleCell.Effect != null )
		//	{
		//		sadConsoleCell.Effect.Clear( sadConsoleCell );
		//		sadConsoleCell.Effect = null;
		//	}

		//	sadConsoleCell.Effect = Explored;
		//	sadConsoleCell.Effect.Apply( sadConsoleCell );
		//}

		//public void RenderInFov( Cell sadConsoleCell )
		//{
		//	// Clear out the old effect if there was one
		//	if ( sadConsoleCell.Effect != null )
		//	{
		//		sadConsoleCell.Effect.Clear( sadConsoleCell );
		//		sadConsoleCell.Effect = null;
		//	}
		//	//sadConsoleCell.Effect.Apply( sadConsoleCell );
		//}
	}
}