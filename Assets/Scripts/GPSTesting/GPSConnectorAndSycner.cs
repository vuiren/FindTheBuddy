using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GPSConnectorAndSycner : MonoBehaviour
{
	[SerializeField] Text text;
	LocationInfo ownGPSData;

	[SerializeField]
	string ownFakeData;

	[SerializeField]
	Text gpsWorkerText;

	[SerializeField]
	bool fake;

	[ReadOnly] [SerializeField] string ownGPSDataString;
	[ReadOnly] [SerializeField] string buddyGPSData;

	[ReadOnly] [SerializeField] bool ownDataReceived;
	[ReadOnly] [SerializeField] bool buddyDataReceived;

	public void UpdateOwnGPSData(LocationInfo locationInfo)
	{
		ownGPSData = locationInfo;
		ownGPSDataString = locationInfo.latitude.ToString() + " " + locationInfo.longitude.ToString();
		ownDataReceived = true;
		UpdateText();
	}

	public void ReceiveBuddyGPSData(string data)
	{
		buddyGPSData = data;
		buddyDataReceived = true;
		UpdateText();
	}

	public void UpdateOwnFakeGPSData(string data)
	{
		ownFakeData = data;
	}

	private void UpdateText()
	{
		if (!fake && (!ownDataReceived || !buddyDataReceived)) return;
		var buddyCords = buddyGPSData.Split(' ');
		var buddyLatitude = float.Parse(buddyCords[0], CultureInfo.InvariantCulture.NumberFormat);
		var buddyLongitude = float.Parse(buddyCords[1], CultureInfo.InvariantCulture.NumberFormat);
		var distance = DistanceCalculation.Calculate_Distance(buddyLatitude, ownGPSData.latitude, buddyLongitude, ownGPSData.longitude);

		var styledText =
	string.Format
	("Ваши координаты\nШирота: {0}\nДолгота: {1}\nКоординаты собеседника\nДолгота: {2}\n Широта: {3}\nРасстояние между вами\n{4} метров",
	ownGPSData.latitude, ownGPSData.longitude, buddyLatitude, buddyLongitude, distance);

		if (fake)
		{
			var buddyCords2 = ownFakeData.Split(' ');
			var buddy2Latitude = float.Parse(buddyCords2[0], CultureInfo.InvariantCulture.NumberFormat);
			var buddy2Longitude = float.Parse(buddyCords2[1], CultureInfo.InvariantCulture.NumberFormat);

			distance = DistanceCalculation.Calculate_Distance(buddyLatitude, buddy2Latitude, buddyLongitude, buddy2Longitude);

			gpsWorkerText.text = string.Format
	("Ваши координаты\nШирота: {0}\nДолгота: {1}\nКоординаты собеседника\nДолгота: {2}\n Широта: {3}\n",
	buddy2Latitude, buddy2Longitude, buddyLatitude, buddyLongitude);

		}
		text.text = distance.ToString() + " метров";
	}
}