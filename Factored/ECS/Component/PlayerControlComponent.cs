using Factored.Actions;
using Factored.Actions.Interfaces;
using Factored.ECS.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Component
{
	public class PlayerControlComponent : IComponent, IActor
	{
		public int OwnerID;
		public int GetOwner()
		{
			return OwnerID;
		}

		public PlayerControlComponent( int owner )
		{
			OwnerID = owner;
		}

		public bool actionTaken = false;

		IAction _action;
		public IAction GetAction()
		{
			if ( _action == null )
				return null;
			else
			{
				IAction t = _action;
				_action = null;
				return t;
			}
		}

		public void SetAction( IAction action )
		{
			_action = action;
		}

	}
}
