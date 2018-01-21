using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class MoveOnAttemptComponent:MovementTriggerComponent
	{
		private static Dictionary<string, MoveOnAttemptFunction> _moveOnAttemptFunctionDictionary =
			new Dictionary<string, MoveOnAttemptFunction>()
			{
			};
	}
}
