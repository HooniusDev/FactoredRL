using Factored.Actions;
using Factored.Actions.Interfaces;
using Factored.ECS.Interfaces;
using Factored.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.ECS.Component
{
	public class BasicAIComponent : IComponent, IActor
	{
		public int OwnerID;
		public int GetOwner()
		{
			return OwnerID;
		}

		IAction _action;
		public IAction GetAction()
		{
			if ( _action == null )
			{
				//Process AI
				return ProcessAI();
			}
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

		public IAction ProcessAI()
		{
			return new MoveAction( OwnerID, Directions.Random() );
		}
	}
}
