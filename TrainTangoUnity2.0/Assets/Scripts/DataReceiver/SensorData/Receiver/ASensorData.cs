using System.Collections.Generic;

namespace SensorData.Receiver
{
    public abstract class ASensorData
    {
        public string Timestamp;
        
        // Process data received here
        protected abstract void ReceiveData(string timestamp, List<string> dataReceived);

        protected void RecordTimestamp(string timestamp)
        {
            Timestamp = timestamp;
        }
    }
}