using System;
using System.Collections.Generic;
using Console = SadConsole.Consoles.Console;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factored.ECS.Component;
using Factored.ECS.Interfaces;
using Microsoft.Xna.Framework;
using Factored.Systems;
using Factored.MapObjects;
using static Factored.Systems.MapSystem;
using SadConsole.Effects;

namespace Factored.ECS.Systems
{
	public class RenderingSystem
	{

		private Console canvas;

		private Point lastHighlight = new Point(0,0);
		private Point newHighlight;

		public bool _dirty = true;

		public RenderingSystem( Console canvas )
		{
			this.canvas = canvas;
		}

		public void HighlightTile( Point tile )
		{
			if ( tile == lastHighlight )
				return;
			else
			{
				newHighlight = tile;
			}

		}

		public void RedrawTile( Point tile )
		{
			
			//get position components for the tile and set rendercomponents .changed to true
			List<PositionComponent> pclist = ComponentManager.GetComponents<PositionComponent>();
			foreach ( PositionComponent pc in pclist )
			{
				if ( pc.Position == tile )
				{
					RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( pc.GetOwner() );
					if ( rc != null )
						rc.Changed = true;
				}
			}
		}

		public void SetTileAppearance( int x, int y, TileType type )
		{
			switch ( type )
			{
				case ( TileType.Floor ):
					{
						canvas.SetCellAppearance(x,y, CellAppearances.FloorFov );
						break;
					}
				case ( TileType.Wall ):
					{
						canvas.SetCellAppearance( x, y, CellAppearances.WallFov );
						break;
					}
				case ( TileType.Corridor ):
					{
						canvas.SetCellAppearance( x, y, CellAppearances.CorridorFov );
						break;
					}
				case ( TileType.None ):
					{
						break;
					}
				default:
					{
						break;
					}
			}
		}

		/*
		public void RenderMap( MapSystem map )
		{

			foreach ( Point tile in map.tilesToUpdate )
			{
				SetTileAppearance( tile.X, tile.Y, map.GetTileType( tile ) );
			}


			map.tilesToUpdate.Clear();
			
			if ( _dirty )
			{
				System.Console.WriteLine( "render Map ! " );
				for ( int x = 0; x < map.mapRect.Width; x++ )
					for ( int y = 0; y < map.mapRect.Height; y++ )
					{
						if ( map.isInFov[x, y] == true )
						{
							//SetTileAppearance( x, y, map.GetTileType( x, y ) );
							if ( canvas[x, y].Effect != null )
							{

								canvas[x, y].Effect.Clear( canvas[x, y] );
								map.tilesToUpdate.Add( new Point( x, y ) );
								//canvas[x, y].Effect.Apply( canvas[x, y] );
							}
						}
						else if ( map.isExplored[x, y] == true ) 
						{
							
							if ( map.GetTileType( x, y ) != TileType.None )
							{
								canvas[x, y].Effect = CellAppearances.ExploredEffect ;
								canvas[x, y].Effect.Apply( canvas[x, y] );
							}
						}
						//if ( !map.isInFov[x, y] && map.isExplored[x, y] )
						//{
						//	if ( map.GetTileType( x, y ) != TileType.None )
						//	{
						//		canvas.SetEffect( x, y, CellAppearances.ExploredEffect );
						//		canvas[x, y].Effect.Apply( canvas[x, y] );
						//	}
						//}
						if ( !map.isInFov[x, y] && !map.isExplored[x, y] )
						{
							if ( map.GetTileType( x, y ) != TileType.None )
							{
								canvas[x, y].Effect = CellAppearances.HiddenEffect ;
								canvas[x, y].Effect.Apply( canvas[x, y] );
							}
						}

					}
				
				_dirty = false;
			}
			if ( map.IsValid( newHighlight ) )
			{
				if ( lastHighlight != null )
				{
					canvas.Clear( lastHighlight.X, lastHighlight.Y );
					map.tilesToUpdate.Add( lastHighlight );
				}
				canvas[newHighlight.X, newHighlight.Y].Effect = CellAppearances.HighlighEffect;
				canvas[newHighlight.X, newHighlight.Y].Effect.Apply( canvas[newHighlight.X, newHighlight.Y] );
				lastHighlight = newHighlight;
			}
		}
		public void RenderEntities()
		{
			List<RenderComponent> rcList = ComponentManager.GetComponents<RenderComponent>();
			
			for ( int i = rcList.Count-1; i >= 0; i-- )
			{
				RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( rcList[i].OwnerID );
				PositionComponent pc = ComponentManager.GetComponent<PositionComponent>( rcList[i].OwnerID );
				//System.Console.WriteLine( "owner:: " + rcList[i].OwnerID );
				if ( rc != null && pc != null )
				{
					rc.Render( canvas[pc.X, pc.Y] );
				}
			}
		}
		*/
		//public void Process()
		//{

		//	List<RenderComponent> rcList = ComponentManager.GetComponents<RenderComponent>();
		//	List<int> renderLater = new List<int>();
		//	//System.Console.WriteLine( "tc count: " + rcList.Count.ToString() );

		//	for ( int i = 0; i < rcList.Count; i++ )
		//	{
		//		//Render Map tiles
		//		TileComponent tc = ComponentManager.GetComponent<TileComponent>( rcList[i].GetOwner() );
		//		//Render Entities without TileComponent later...
		//		if ( tc == null )
		//			renderLater.Add( rcList[i].GetOwner() );
		//		else
		//		{
		//			RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( tc.OwnerID );
		//			PositionComponent pc = ComponentManager.GetComponent<PositionComponent>( tc.OwnerID );
		//			if ( rc != null && pc != null )
		//			{
		//				rc.Render( canvas[pc.X, pc.Y] );

		//			}
		//		}
		//	}
		//	foreach ( int i in renderLater )
		//	{
		//		RenderComponent rc = ComponentManager.GetComponent<RenderComponent>( i );
		//		PositionComponent pc = ComponentManager.GetComponent<PositionComponent>( i );
		//		if ( rc != null && pc != null && i != 0)
		//		{
		//			rc.Render( canvas[pc.X, pc.Y] );
		//		}
		//	}

		//	RenderComponent RC = ComponentManager.GetComponent<RenderComponent>( 0 );
		//	PositionComponent PC = ComponentManager.GetComponent<PositionComponent>( 0 );
		//	RC.Render( canvas[PC.X, PC.Y] );
		//}


	}


}

