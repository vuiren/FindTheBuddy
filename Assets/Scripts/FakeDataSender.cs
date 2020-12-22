using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDataSender : MonoBehaviour
{
	[SerializeField]
	CompasController compasController;

	[SerializeField]
	GPSConnectorAndSycner GPSConnectorAndSycner;

	public void DoSend()
	{
		var selfFake = string.Format("{0} {1}", Random.Range(-100f, 100f), Random.Range(-100f, 100f)); 
		var buddyFake = string.Format("{0} {1}", Random.Range(-100f, 100f), Random.Range(-100f, 100f)); 
		compasController.GetOwnDataInfo(selfFake);
		compasController.GetSecondGuyInfo(buddyFake);

		GPSConnectorAndSycner.UpdateOwnFakeGPSData(selfFake);
		GPSConnectorAndSycner.ReceiveBuddyGPSData(buddyFake);
	}
}
