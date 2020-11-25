using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationService : MonoBehaviour
{
    [SerializeField] bool isUnityRemote = true;
    [SerializeField] Text infoText;
    [SerializeField] float gpsUpdateTime;
    [SerializeField] LocationInfoUnityEvent onGpsDataUpdate;

    private void Start()
    {
        StartCoroutine(StartLocationService(0, 0));
    }

    private IEnumerator StartLocationService(float desiredAccuracyInMeters, float updateDistanceInMeters)
    {
        // Wait until the editor and unity remote are connected before starting a location service
        if (isUnityRemote)
        {
            infoText.text = "GPS Warm up Started";
            yield return new WaitForSeconds(5);
            infoText.text = "GPS Warm up finished";
        }

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            infoText.text = "No locations enabled in the device";
           // Debug.Log("No locations enabled in the device");
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        if (isUnityRemote)
        {
            yield return new WaitForSeconds(5);
        }

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            infoText.text = "Waiting for gps";
            //Debug.Log("Waiting for gps");
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            infoText.text = "GPS didn't initialize in 20 seconds";
        //    Debug.Log("Service didn't initialize in 20 seconds");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            infoText.text = "Unable to determine device location";
            yield break;
        }
        // Access granted and location value could be retrieved
        else
        {
            float lat = Input.location.lastData.latitude;
            float lon = Input.location.lastData.longitude;
            Debug.Log("Latency = " + lat);
            Debug.Log("Longitude = " + lon);
            StartCoroutine(UpdateGPSLocation());
        }

    }

    private IEnumerator UpdateGPSLocation()
	{
        WaitForSeconds updateTime = new WaitForSeconds(gpsUpdateTime);

        while (true)
        {
            var lastData = Input.location.lastData;
            var styledText = string.Format("Sent data:\nШирота: {0} Долгота: {1}", lastData.latitude, lastData.longitude);
            infoText.text = styledText;
            onGpsDataUpdate.Invoke(lastData);
            yield return updateTime;
        }
    }

    [Button]
    private void SendFakeGPSData()
	{
        var locationInfo = new LocationInfo();
        var styledText = string.Format("Sent data:\nШирота: {0} Долгота: {1}", locationInfo.latitude, locationInfo.longitude);
        infoText.text = styledText;
        onGpsDataUpdate.Invoke(locationInfo);
	}
}