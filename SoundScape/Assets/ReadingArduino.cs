using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;

public class ReadingArduino : MonoBehaviour {

    SerialPort stream;

	// Use this for initialization
	void Start () {
        stream = new SerialPort("COM8", 9600);
        stream.ReadTimeout = 50;
        stream.Open();
    }
	
	// Update is called once per frame
	void Update () {

        if(stream.IsOpen)
        {
            try
            {
                stream.ReadLine();
            }
            catch
            {

            }
        }
	}

    public void WriteToArduino(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }
}
