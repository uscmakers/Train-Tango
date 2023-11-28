using System.Collections.Generic;
using SensorData.Receiver;
using UnityEngine;

namespace SensorData.Sensors
{
    public class AccelerometerData : ASensorData
    {
        protected override void ReceiveData(string timestamp, List<string> dataReceived)
        {
            base.RecordTimestamp(timestamp);
            Debug.Log("Accelerometer data has been received");
            Debug.Log(dataReceived);
        }
    }
}