using UnityEngine;
using UnityEngine.UI;

public class SyncGPSText : MonoBehaviour
{
	[SerializeField] Text text;

	public void DoSyncText(string newText)
	{
		text.text = newText;
	}

	public void DoSyncGPSText(LocationInfo lastData)
	{
		var styledText = string.Format("Долгота: {0}/n Широта: {1}/n Высота: {2}/n Дата: {3}", 
			lastData.longitude, lastData.latitude, lastData.altitude, lastData.timestamp);
		text.text = styledText;
	}
}
