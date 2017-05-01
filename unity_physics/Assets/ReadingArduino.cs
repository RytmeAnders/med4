using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.Text.RegularExpressions;

public class ReadingArduino : MonoBehaviour {

    SerialPort stream = new SerialPort("COM7", 9600); // Sets streaming port arduino communicates through

    string str, pattern = "_";
    string[] accData = new string[2];
    float acceleration, orientation;

	// Use this for initialization
	void Start () {
        //Opens stream
        stream.Open();
        stream.ReadTimeout = 100; // Sets timeout to 100 milliseconds, so it doesn't overload
    }
	
	// Update is called once per frame
	void Update () {

        if(stream.IsOpen)
        {
            try
            {
                // Reads data to string
                str = stream.ReadLine();
                accData = str.Split('_'); // Splits string
                acceleration = float.Parse(accData[0]); //Parsing the split string into floats
                orientation = float.Parse(accData[1]);
                RotatePlayer(orientation); // Calls for rotations based off orientation received from arduino
            }
            catch (TimeoutException){}
        }
	}

    public void RotatePlayer(float yOrientation)
    {
        transform.localEulerAngles = new Vector3(0, yOrientation, 0); // Rotate player
    }
}
