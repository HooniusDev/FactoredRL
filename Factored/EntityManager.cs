using Factored.Consoles;
using Factored.ECS;
using Factored.ECS.Component;
using Factored.Test;
using Factored.Utils;
using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored
{
	public static class EntityManager
	{
		private static List<int> _entities = new List<int>();
		private static int _entityCounter = 0;

		public static int Player { get; private set; }
		private static bool playerCreated = false;
		public static int Map { get; private set; }

		//public static int Player;

		public static void Init()
		{
			Player = CreatePlayer();
		
			Map = CreateEntity(); //Create Map
								  //CreateTestEntities.Create();
			//CreatePlayer();
		}

		public static int CreateEntity( )
		{
			Entity e = new Entity( _entityCounter ); // Create Entity
			int id = _entityCounter;
			 _entities.Add( id );

			_entityCounter++; // Increment id counter
			return id;
		}

		public static List<int> GetEntities()
		{
			return _entities;
		}

		public static int CreatePlayer()
		{
			if ( playerCreated == false )
			{
				Player = CreateEntity(); //Create Player
				playerCreated = true;
				CellAppearance ca = new CellAppearance( Colors.Player, Colors.FloorBackgroundFov, 64 );
				RenderComponent rc = new RenderComponent( Player, ca, 2 );
				ComponentManager.AddComponent( Player, rc );
				PositionComponent pc = new PositionComponent( Player  );
				ComponentManager.AddComponent( Player, pc );
				PlayerControlComponent pcc = new PlayerControlComponent( Player );
				ComponentManager.AddComponent( Player, pcc );
				ComponentManager.AddComponent( Player, new FovComponent( Player ));
			}
			return 0;
		}


	}
}
