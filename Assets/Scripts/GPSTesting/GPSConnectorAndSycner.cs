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

	private void UpdateText()
	{
		if (!ownDataReceived || !buddyDataReceived) return;
		var buddyCords = buddyGPSData.Split(' ');
		var buddyLatitude = float.Parse(buddyCords[0], CultureInfo.InvariantCulture.NumberFormat);
		var buddyLongitude = float.Parse(buddyCords[1], CultureInfo.InvariantCulture.NumberFormat);
		var distance = DistanceCalculation.Calculate_Distance(buddyLatitude, ownGPSData.latitude, buddyLongitude, ownGPSData.longitude);
		var styledText =
			string.Format
			("Ваши координаты\nDШирота: {0}\nДолгота: {1}\nКоординаты собеседника\nДолгота: {2}\n Широта: {3}\nРасстояние между вами\n{4} метров",
			ownGPSData.latitude, ownGPSData.longitude, buddyLatitude, buddyLongitude, distance);
		text.text = styledText;
	}
}
