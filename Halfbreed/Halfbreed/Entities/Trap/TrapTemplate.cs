namespace Halfbreed.Entities
{
	public struct TrapTemplate
	{
		private string _trapName;
		private EntityTraits[] _traits;
		private string _moveOnFunction;
		// TODO: there will likely need to be some attack related stuff here later on.

		public TrapTemplate(string trapName, EntityTraits[] traits, string moveOnFunctionName)
		{
			_trapName = trapName;
			_traits = traits;
			_moveOnFunction = moveOnFunctionName;
		}

		public string TrapName
		{
			get { return _trapName; }
		}

		public string MoveOnFunction
		{
			get { return _moveOnFunction; }
		}

		public EntityTraits[] Traits
		{
			get { return _traits; }
		}
	}
}
