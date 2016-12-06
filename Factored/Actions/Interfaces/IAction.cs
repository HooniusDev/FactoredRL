using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factored.Actions.Interfaces
{
	public interface IAction
	{
		bool CanPerform();
		void Perform();
	}
}
