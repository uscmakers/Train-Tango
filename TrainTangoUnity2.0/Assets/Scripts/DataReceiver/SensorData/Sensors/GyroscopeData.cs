using System.Collections.Generic;
using SensorData.Receiver;
using UnityEngine;

namespace SensorData.Sensors
{
    public class GyroscopeData : ASensorData
    {
        protected override void ReceiveData(string timestamp, List<string> dataReceived)
        {
            base.RecordTimestamp(timestamp);
            Debug.Log("Gyroscope data has been received");
            Debug.Log(dataReceived);
        }
    }
}