namespace Mapbox.Examples
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using UnityEngine.UI;

	public class ImmediatePositionWithLocationProvider : MonoBehaviour
	{
		public Text latLongText;
		bool _isInitialized;

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
				transform.localPosition = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);
				print(LocationProvider.CurrentLocation.LatitudeLongitude);
				latLongText.text = LocationProvider.CurrentLocation.LatitudeLongitude.ToString();
			}
		}
	}
}