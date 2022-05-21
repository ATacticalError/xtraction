namespace Mapbox.Examples
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using UnityEngine.UI;

	public class ImmediatePositionWithLocationProvider : MonoBehaviour
	{
		bool _isInitialized;
		public float smoothTime = 0.5f;
		public bool smooth = true;
		public Vector3 velocity = Vector3.zero;

		ILocationProvider _locationProvider;
		ILocationProvider LocationProvider
		{
			get
			{
				if (_locationProvider == null)
				{
					_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
				}

				return _locationProvider;
			}
		}

		Vector3 _targetPosition;

		void Start()
		{
			LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
		}

		void LateUpdate()
		{
			if (_isInitialized)
			{
				var map = LocationProviderFactory.Instance.mapManager;
				if(smooth)
					transform.localPosition = Vector3.SmoothDamp(transform.localPosition, 
					map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude),
					ref velocity,
					smoothTime);
				else
					transform.localPosition = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);
			}
		}
	}
}