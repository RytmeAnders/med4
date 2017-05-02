using UnityEngine;
using System.Collections;

public class ThrowBall : MonoBehaviour {

    Vector3 initialPos;
    GameObject ballPos;
    Rigidbody rb;

    void Start()
    {
        // Getting rigidbody and ball components
        rb = GetComponent<Rigidbody>();
        ballPos = GameObject.Find("Ball");
        initialPos = ballPos.GetComponent<Transform>().position;
    }
    
    void OnCollisionEnter(Collision coll) // Reset position of ball when target is hit
    {
        ballPos.transform.position = initialPos;
        rb.useGravity = false;
        print(initialPos);
    }
}
