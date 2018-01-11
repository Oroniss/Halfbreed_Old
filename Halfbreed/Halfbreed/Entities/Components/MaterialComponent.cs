namespace Halfbreed
{
	public class MaterialComponent:Component
	{
		private Materials _material;

		public MaterialComponent(Entity entity, Materials material)
			:base(entity)
		{
			_material = material;
		}

		public Materials Material
		{
			get { return _material; }
		}
	}
}
