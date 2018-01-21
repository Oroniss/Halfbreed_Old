using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public abstract class MovementTriggerComponent:Component
	{
		protected List<string> _functions;

		protected MovementTriggerComponent(Entity entity)
			:base(entity)
		{
			_functions = new List<string>();
		}

		public void AddFunction(string functionName)
		{
			_functions.Add(functionName);
		}

		public void RemoveFunction(string functionName)
		{
			_functions.Remove(functionName);
		}
	}
}
