using UnityEngine;
using System.Collections;

public class ThrowBall : MonoBehaviour {

    Vector3 initialPos;
    GameObject ballPos;
    Rigidbody rb;

    ReadingArduino arduino;

    float orientation;

    void Start()
    {
        arduino = GetComponent<ReadingArduino>();
        // Getting rigidbody and ball components
        rb = GetComponent<Rigidbody>();
        ballPos = GameObject.Find("Ball");
        initialPos = ballPos.GetComponent<Transform>().position;
    }

    void Update()
    {
        //orientation = arduino.orientation;
        //print(orientation);
    }

    public void RotateBall(float yOrientation)
    {
        transform.localEulerAngles = new Vector3(0, yOrientation, 0); // Rotate player
    }

    void OnCollisionEnter(Collision coll) // Reset position of ball when target is hit
    {
        ballPos.transform.position = initialPos;
        rb.useGravity = false;
        print(initialPos);
    }
}
