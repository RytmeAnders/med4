using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;

public class ReadingArduino : MonoBehaviour {

    SerialPort stream = new SerialPort("COM7", 9600);

	// Use this for initialization
	void Start () {
        stream.Open();
        stream.ReadTimeout = 50;
    }
	
	// Update is called once per frame
	void Update () {

        if(stream.IsOpen)
        {
            try
            {
                //stream.ReadByte();
                print(stream.ReadLine());
            }
            catch (System.Exception){}
        }
	}

    public void WriteToArduino()
    {
        stream.Write("Yo");
        stream.BaseStream.Flush();
    }
}
