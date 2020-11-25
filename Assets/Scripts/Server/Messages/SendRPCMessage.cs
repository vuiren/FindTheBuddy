using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendRPCMessage : MonoBehaviourPun
{
	[SerializeField]
	Text text;

	[SerializeField]
	StringUnityEvent onGPSUpdate;

	[PunRPC]
	public void SendOnlineMessage(string senderName, string message)
	{
		var newLine = string.Format("{0}: {1}\n", senderName, message);
		text.text += newLine;
	}

	[PunRPC]
	public void SendGPSData(string senderName, string locationInfo)
	{
		//if (senderName == PhotonNetwork.NickName) return;
		onGPSUpdate.Invoke(locationInfo);
	}

	public void DoSendOnlineMessage(string message)
	{
		photonView.RPC("SendOnlineMessage", RpcTarget.All, new[] {PhotonNetwork.NickName, message });
	}

	public void DoSendGPSData(LocationInfo locationInfo)
	{
		var gpsData = string.Format("{0} {1}", locationInfo.latitude, locationInfo.longitude);
		photonView.RPC("SendGPSData", RpcTarget.Others, new[] { PhotonNetwork.NickName, gpsData });
	}
}
