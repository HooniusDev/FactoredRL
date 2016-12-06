using Factored.ECS.Component;
using Factored.ECS.Interfaces;
using Factored.Utils;
using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS
{
	public static class ComponentManager
	{

		private static Dictionary<int, List<IComponent>> _components;

		static ComponentManager()
		{
			_components = new Dictionary<int, List<IComponent>>();
		}

		public static void AddComponent( int id, IComponent component ) 
		{
			if( !_components.ContainsKey( id ) )
			{
				List<IComponent> components = new List<IComponent>();
				components.Add( component ); 
				_components.Add( id, components );
			}
			else
			{
				foreach ( IComponent c in _components[id] )
					if ( component.GetType() == c.GetType() )
					{
						System.Console.WriteLine( "Component already exists in entity!" );
						return;
					}
				List<IComponent> list = _components[id];
				list.Add( component );
				_components[id] = list;
			}	
		}

		public static T GetComponent<T>( int id ) 
		{

			foreach ( int i in _components.Keys )
			{
				if ( i == id )
				{
					foreach ( IComponent component in _components[id] )
					{
						if ( component is T )
							return (T) component;
					}
				}
			}
			return default(T);
		}

		public static List<T> GetComponents<T>()
		{
			List<T> list = new List<T>();
			foreach ( int id in _components.Keys )
			{
				foreach ( IComponent component in _components[id] )
				{
					if ( component is T )
						list.Add( (T) component );
				}
			}
			return list;
		}

		public static List<T> GetComponentsAtTile<T>( Point tile ) 
		{
			List<int> entitiesAtTile = new List<int>();
			List<T> componentsAtTile = new List<T>();
			foreach ( PositionComponent pc in GetComponents<PositionComponent>() )
			{
				if ( pc.Position == tile )
				{
					entitiesAtTile.Add( pc.GetOwner() );
				}
			}
			foreach ( int e in entitiesAtTile )
			{
				T Component = GetComponent<T>( e );
				if ( Component != null )
					componentsAtTile.Add( GetComponent<T>( e ));
			}

			return componentsAtTile;
		}

		public static List<RenderComponent> GetRenderComponents()
		{
			List<RenderComponent> list = new List<RenderComponent>();
			foreach ( int id in _components.Keys )
			{
				foreach ( IComponent component in _components[id] )
				{
					if ( component is RenderComponent )
						list.Add( component as RenderComponent );
				}
			}
			return list;
		}

		public static List<PositionComponent> GetPositionComponents()
		{
			List<PositionComponent> list = new List<PositionComponent>();
			foreach ( int id in _components.Keys )
			{
				foreach ( IComponent component in _components[id] )
				{
					if ( component is PositionComponent )
						list.Add( component as PositionComponent );
				}
			}
			return list;
		}

	}
}
