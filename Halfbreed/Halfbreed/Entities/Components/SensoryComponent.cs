using System;

namespace Halfbreed.Entities
{
	public class SensoryComponent:Component
	{
		static readonly int MAXIMUMVIEWDISTANCE = 24;

		int _viewDistanceBase;
		int _viewDistanceModifiers;

		public SensoryComponent(Entity entity, int viewDistance)
			:base(entity)
		{
			_componentType = ComponentType.SENSORY;
			_viewDistanceBase = viewDistance;
			_viewDistanceModifiers = 0;
		}

		public int CurrentViewDistance
		{
			get {if (_entity.HasTrait(EntityTraits.BLIND) && !_entity.HasTrait(EntityTraits.BLINDSIGHT))
					return 0;

				return Math.Min(Math.Max(_viewDistanceBase + _viewDistanceModifiers, 0), MAXIMUMVIEWDISTANCE); }
		}

		public void AddViewDistanceModifier(int modifier)
		{
			_viewDistanceModifiers += modifier;
		}
	}
}
