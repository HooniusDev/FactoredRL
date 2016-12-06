using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.Actions
{
	public abstract class ActionBase
	{
		public int EnergyCost = 0;
		public int Performer;

		public ActionBase( int performer, int energyCost = 0 )
		{
			Performer = performer;
			EnergyCost = energyCost;
		}
	}
}
