using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.Text.RegularExpressions;

public class ReadingArduino : MonoBehaviour {

    SerialPort stream = new SerialPort("COM7", 9600);

    string str, pattern = "_";
    string[] accData = new string[2];
    float acceleration, orientation;

	// Use this for initialization
	void Start () {
        stream.Open();
        stream.ReadTimeout = 100;
    }
	
	// Update is called once per frame
	void Update () {

        if(stream.IsOpen)
        {
            try
            {
                str = stream.ReadLine();
                //print(str);
                accData = str.Split('_');
                acceleration = float.Parse(accData[0]);
                orientation = float.Parse(accData[1]);
                print(orientation);
            }
            catch (TimeoutException){}
        }
	}

    public void WriteToArduino()
    {
        stream.Write("Yo");
        stream.BaseStream.Flush();
    }
}
