using System;
using System.IO.Ports;
using UnityEngine;

public class SerialDataReceiver : MonoBehaviour
{
    SerialPort serialPort;
    public string portName = "COM9"; // Example port name
    public int baudRate = 115200;      // Example baud rate

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }
    private void Update()
    {
        string dataString = serialPort.ReadLine();
        Debug.Log(dataString);
        if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            try
            {
                //HandleData(dataString);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error reading from serial port: " + e.Message);
            }
        }
    }

    void OpenConnection()
    {
        serialPort = new SerialPort(portName, baudRate)
        {
            ReadTimeout = 5000 // Prevents blocking if no data is available to read
        };

        try
        {
            serialPort.Open();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not open serial port: " + e.Message);
        }
    }

    void HandleData(string dataString)
    {
        try
        {
            // Assuming the dataString is in the format "A:x,y,z;G:x,y,z;"
            // We split the data into accelerometer and gyroscope values
            if (!string.IsNullOrEmpty(dataString) && dataString.Contains("A:") && dataString.Contains("G:"))
            {
                string[] splitData = dataString.Split(';');
                if (splitData.Length >= 2)
                {
                    string[] accData = splitData[0].Split(':')[1].Split(',');
                    string[] gyroData = splitData[1].Split(':')[1].Split(',');

                    if (accData.Length == 3 && gyroData.Length == 3)
                    {
                        Vector3 acceleration = new Vector3(
                            float.Parse(accData[0]),
                            float.Parse(accData[1]),
                            float.Parse(accData[2]));

                        Vector3 gyroscope = new Vector3(
                            float.Parse(gyroData[0]),
                            float.Parse(gyroData[1]),
                            float.Parse(gyroData[2]));

                        // Use the parsed data
                        Debug.Log("Accelerometer data received: " + acceleration);
                        Debug.Log("Gyroscope data received: " + gyroscope);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error handling data: " + e.Message);
        }
    }

    private void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
