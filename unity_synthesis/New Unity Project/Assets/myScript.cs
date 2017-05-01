using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myScript : MonoBehaviour {

    [Range(-1f, 1f)]
    public float offset;            //offset slider with range -1:1 (range defined above)

    public float cutoffOn = 800;
    public float cutoffOff = 100;

    bool engineOn;

    int speed;                      //for rotating the cube at different speeds

    float rotOld;                   //old rotation value
    float rotNew;                   //new rotation value
    float rotDiff;                  //old-new rotation

    Rigidbody rb;
    Vector3 v3Velocity;

    System.Random rand = new System.Random();
    AudioLowPassFilter lowPassFilter;

    void Awake() {                                          //INITIALIZE VARIABLES WHEN WAKING
        rotOld = transform.localEulerAngles.x;              //set rotOld to the x rotation on startup (0)
        rb = GetComponent<Rigidbody>();                     //Rigidbody object
        v3Velocity = rb.velocity;                           //Velocity of the rigidbody object
        lowPassFilter = GetComponent<AudioLowPassFilter>(); //Object of the LowPassFilter component
        Update();
    }

    void OnAudioFilterRead(float[] data, int channels) {                //White noise generation
        for (int i = 0; i < data.Length; i++) {                         
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);  //Fills float array with random floats -1:1
        }
    }

    void Update() {
        rotNew = transform.localEulerAngles.x;              //New angle
        rotDiff = Mathf.Abs(rotOld - rotNew);               //Difference between old and new angle
        if (rotDiff > speed) {                              //Using speed as threshold to avoid too high values
            rotDiff = speed;
        }
        rotOld = rotNew;                                    //Setting new rotation as the old one before reiterating

        print(rotDiff);
        lowPassFilter.cutoffFrequency = 100+rotDiff*80;     //cutoffFrequency changes based on the angular change

        if (Input.GetKey(KeyCode.W)) {                      //Rotate cube by speed = 10
            speed = 10;
            transform.Rotate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.E)) {                      //Rotate cube by speed = 5
            speed = 5;
            transform.Rotate(speed, 0, 0);
        }
    }

    //lowPassFilter.cutoffFrequency = engineOn ? cutoffOn : cutoffOff;
}
