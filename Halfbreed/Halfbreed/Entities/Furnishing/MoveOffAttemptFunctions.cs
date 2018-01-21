using System.Collections.Generic;

namespace Halfbreed.Entities
{
	public partial class MoveOffAttemptComponent
	{
		private static Dictionary<string, MoveOffAttemptFunction> _moveOffAttemptFunctionDictionary =
			new Dictionary<string, MoveOffAttemptFunction>()
			{
			};
	}
}
