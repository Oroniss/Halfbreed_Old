using System;

namespace Halfbreed.Entities
{
	public class SensoryComponent:Component
	{
		static readonly int MAXIMUMVIEWDISTANCE = 24;

		int _viewDistanceBase;
		int _viewDistanceModifiers;
		int _detectionLevel;

		public SensoryComponent(Entity entity, int viewDistance)
			:base(entity)
		{
			_componentType = ComponentType.SENSORY;
			_viewDistanceBase = viewDistance;
			_viewDistanceModifiers = 0;
			// TODO: Figure this out a bit more carefully
			_detectionLevel = 2;
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

		public int DetectionLevel
		{
			get { return _detectionLevel; }
		}
	}
}
