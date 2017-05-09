using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.Text.RegularExpressions;

public class ReadingArduino : MonoBehaviour {

    SerialPort stream; // Sets streaming port arduino communicates through

    Rigidbody rb;

    string str, pattern = "_";
    string[] accData = new string[3];
<<<<<<< HEAD
    float acceleration, accHigh;
    public float orientation, state, state1;
=======
    public float acceleration, orientation, state, state1;
>>>>>>> c5c29f5081772e835c09c5c7ad9024e93b0a1bb9
    int u, angle; //Initial Velocity u (Science notation) and angle

    char[] strm = new char[20];

	// Use this for initialization
	void Start () {
        string[] ports = SerialPort.GetPortNames();
        for (int i = 0; i < ports.Length; i++)
        {
            Debug.Log(ports[i]);
        }
        stream = new SerialPort("COM9", 9600, Parity.None, 8, StopBits.One);
        accHigh = 0;
        state1 = 0;
        //Opens stream
        stream.Open();
        stream.ReadTimeout = 100; // Sets timeout to 100 milliseconds, so it doesn't overload

        rb = GameObject.Find("Ball").GetComponent<Rigidbody>(); // Getting the rigidbody component of the ball

        u = 100; // Initial velocity in m/s
        angle = 30; // Angle in degrees
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
                if (acceleration > accHigh)
                {
                    accHigh = acceleration;
                    print(accHigh);
                }
                orientation = float.Parse(accData[1]);
                state = float.Parse(accData[2]);
                RotatePlayer(orientation); // Calls for rotations based off orientation received from arduino
                if (state == 1 && state1 == 0)
                {
                    state1 = 1;
                }
                if (state == 0 && state1 == 1)
                {
                    LaunchBall(accHigh*100f);
                }
                //print(acceleration);
            }
            catch (TimeoutException){
            }
        }
	}

    public void RotatePlayer(float yOrientation)
    {
        transform.localEulerAngles = new Vector3(0, yOrientation, 0); // Rotate player
    }

    void LaunchBall(float acceleration)
    {
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        rb.AddForce(direction * (u + (acceleration * Time.deltaTime)));
        print(direction * (u + (acceleration * Time.deltaTime)));
        rb.useGravity = true;
        state1 = 0;
        accHigh = 0;
    }
}
