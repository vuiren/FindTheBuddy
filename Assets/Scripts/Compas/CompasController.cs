using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CompasController : MonoBehaviour
{
    LocationInfo guyInfo1;
    string guyInfo2;

    [SerializeField]
    Transform compasSprite;

    [SerializeField]
    string fakeSelfData;

    bool data1Received;
    bool data2Received;

    [SerializeField]
    bool fake;

    float targetAngle;

    [SerializeField]
    float smoothSpeed;

	private void Update()
	{
        SmoothAngle();

        if (!data1Received || !data2Received) return;

        if (fake)
		{
            SetFakeAngle();
            return;
		}

        if (!data1Received || !data2Received) return;
        SetAngle();
	}

	private void SmoothAngle()
	{
        var euler = compasSprite.rotation.eulerAngles;

        compasSprite.rotation = Quaternion.Lerp(compasSprite.rotation, Quaternion.Euler(euler.x, euler.y, targetAngle), smoothSpeed);
    }

    private void SetFakeAngle()
	{
        var euler = compasSprite.rotation.eulerAngles;
        var angle = GetFakeAngle();
        targetAngle = (float)angle;
       // compasSprite.rotation = Quaternion.Euler(euler.x, euler.y, (float)angle);
    }

	private void SetAngle()
	{
        var euler = compasSprite.rotation.eulerAngles;
        var angle = GetFakeAngle();
        targetAngle = (float)angle;
        //compasSprite.rotation = Quaternion.Euler(euler.x, euler.y, (float)angle);
	}

    public double GetFakeAngle()
    {
        var buddyCords1 = fakeSelfData.Split(' ');
        var buddy1Latitude = float.Parse(buddyCords1[0], CultureInfo.InvariantCulture.NumberFormat);
        var buddy1Longitude = float.Parse(buddyCords1[1], CultureInfo.InvariantCulture.NumberFormat);

        var buddyCords2 = guyInfo2.Split(' ');
        var buddy2Latitude = float.Parse(buddyCords2[0], CultureInfo.InvariantCulture.NumberFormat);
        var buddy2Longitude = float.Parse(buddyCords2[1], CultureInfo.InvariantCulture.NumberFormat);

        var angle = AngleFromCoordinate(buddy1Latitude, buddy1Longitude, buddy2Latitude, buddy2Longitude);
        return angle;
    }

    public void GetOwnDataInfo(LocationInfo guyInfo)
	{
        guyInfo1 = guyInfo;
        data1Received = true;
	}

    public void GetOwnDataInfo(string guyInfo)
    {
        fakeSelfData = guyInfo;
        data1Received = true;
    }

    public void GetSecondGuyInfo(string info)
	{
        guyInfo2 = info;
        data2Received = true;

    }

    public double GetAngle()
    {
        var buddyCords = guyInfo2.Split(' ');
        var buddyLatitude = float.Parse(buddyCords[0], CultureInfo.InvariantCulture.NumberFormat);
        var buddyLongitude = float.Parse(buddyCords[1], CultureInfo.InvariantCulture.NumberFormat);

        var angle = AngleFromCoordinate(guyInfo1.latitude, guyInfo1.longitude, buddyLatitude, buddyLongitude);
        return angle;
    }

    private double AngleFromCoordinate(float lat1, float long1, float lat2,
        float long2)
    {

        double dLon = (long2 - long1);

        double y = Math.Sin(dLon) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1)
                * Math.Cos(lat2) * Math.Cos(dLon);

        var brng = Math.Atan2(y, x);

        brng *= 180f / Math.PI;
        brng = (brng + 360) % 360;
        brng = 360 - brng; // count degrees counter-clockwise - remove to make clockwise

        return brng;
    }
}
