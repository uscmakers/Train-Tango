using System.IO.Ports;
using UnityEngine;

public class SerialDataReceiver : MonoBehaviour
{
    public string portName = "COM3"; // The serial port where the Arduino is connected
    public int baudRate = 115200;    // The baud rate must match the Arduino's
    private SerialPort serialPort;

    private void Start()
    {
        OpenConnection();
    }

    private void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string dataString = serialPort.ReadLine();
                HandleData(dataString);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    void OpenConnection()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 50; // Prevents blocking if no data is available to read

        try
        {
            serialPort.Open();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not open serial port: " + e.Message);
        }
    }

    void HandleData(string dataString)
    {
        // Assuming the dataString is in the format "A:x,y,z;G:x,y,z;"
        // We split the data into accelerometer and gyroscope values
        if (dataString.StartsWith("A:") && dataString.Contains("G:"))
        {
            // Parse the data string here
            string[] splitData = dataString.Split(';');
            string[] accData = splitData[0].Split(':')?[1].Split(',');
            string[] gyroData = splitData[1].Split(':')?[1].Split(',');

            // Convert the split string data to float
            Vector3 acceleration = new Vector3(
                float.Parse(accData[0]),
                float.Parse(accData[1]),
                float.Parse(accData[2]));

            Vector3 gyroscope = new Vector3(
                float.Parse(gyroData[0]),
                float.Parse(gyroData[1]),
                float.Parse(gyroData[2]));

            // Now you have your accelerometer and gyroscope data
            // Do something with it like applying it to an object's transform
            // This is where you'd integrate with your game logic
            
            Debug.Log("Accelerometer data received: " + acceleration);
            Debug.Log("Gyroscope data received: " + gyroscope);
        }
    }

    private void OnDestroy()
    {
        // Close the port when the application ends
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
