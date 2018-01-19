using System;
using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class InteractibleComponent:Component
	{
		private static Dictionary<string, InteractionFunction> _interactionFunctionDictionary = new
			Dictionary<string, InteractionFunction>()
		{
			{"NoUse", new InteractionFunction(NoUse)},
			{"UseDoor", new InteractionFunction(UseDoor)}
		};

		private List<string> _interactionFunctions;

		public InteractibleComponent(Entity entity)
			:base(entity)
		{
			_componentType = ComponentType.INTERACTIBLE;

			_interactionFunctions = new List<string>();
		}

		public void AddFunction(string functionName)
		{
			_interactionFunctions.Add(functionName);
		}

		public void RemoveFunction(string functionName)
		{
			_interactionFunctions.Remove(functionName);
		}

		private delegate void InteractionFunction(Entity interactible, Entity actor, int currentTime);

		public void InteractWith(Entity actor, int currentTime)
		{
			if (_interactionFunctions.Count == 0)
			{
				NoUse(_entity, actor, currentTime);
				return;
			}

			foreach (string functionName in _interactionFunctions)
				_interactionFunctionDictionary[functionName](_entity, actor, currentTime);
		}
	}
}
