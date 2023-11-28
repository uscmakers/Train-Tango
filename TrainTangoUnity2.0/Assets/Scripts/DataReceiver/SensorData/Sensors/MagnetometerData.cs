using System.Collections.Generic;
using SensorData.Receiver;

namespace SensorData.Sensors
{
    public class MagnetometerData : ASensorData
    {
        protected override void ReceiveData(string timestamp, List<string> dataReceived)
        {
            base.RecordTimestamp(timestamp);
        }
    }
}