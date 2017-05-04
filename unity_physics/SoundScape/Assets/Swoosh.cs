using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swoosh : MonoBehaviour {

    [Range(-1f, 1f)]
    public float offset;            //offset slider with range -1:1 (range defined above)

    public int lowCut;              //Minimum frequency for the LPF
    public int frequencyScalar;     //A scalar for the changes, because they are too small for the LPF

    float speedRot;                 //for rotating the cube at different speeds
    float speedAcc;                 //for acceleration of the cube

    float rotOld;                   //old rotation value
    float rotNew;                   //new rotation value
    float rotDiff;                  //old-new rotation

    float avgDiff;                  //average between angle and acceleration

    float accOld;                   //old accelerometer value
    float accNew;                   //new accelerometer value
    float accDiff;                  //new-old accelerometer value

    System.Random rand = new System.Random();               //A class with a method for generating random values
    AudioLowPassFilter lowPassFilter;

    void Awake() {                                          //INITIALIZE VARIABLES WHEN WAKING
        rotOld = transform.localEulerAngles.y;              //set rotOld to the x rotation on startup (0)
        accOld = transform.localPosition.x;                 //set accOld to the local x position on startup

        lowPassFilter = GetComponent<AudioLowPassFilter>(); //Object of the LowPassFilter component
        Update();
    }

    void OnAudioFilterRead(float[] data, int channels) {                //White noise generation
        for (int i = 0; i < data.Length; i++) {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);  //Fills float array with random floats -1:1
        }
    }

    void Update() {
        // ROTATIONAL DIFFERENCE --------------------
        rotNew = transform.localEulerAngles.y;              //New angle
        rotDiff = Mathf.Abs(rotOld - rotNew);               //Difference between old and new angle
        if (rotDiff > speedRot) {                           //Using speed as threshold to avoid too high values
            rotDiff = speedRot;
        }
        rotOld = rotNew;                                    //Setting new rotation as the old one before reiterating

        if (Input.GetKey(KeyCode.W)) {                      //Rotate cube by speed = 10
            speedRot = 10;
            transform.Rotate(0, speedRot, 0);
        }
        if (Input.GetKey(KeyCode.E)) {                      //Rotate cube by speed = 5
            speedRot = 5;
            transform.Rotate(0, speedRot, 0);
        }
        // END ROTATIONAL DIFFERENCE -----------------

        // ACCELEROMETER DIFFERENCE ------------------
        accNew = transform.localPosition.x;                 //(See rot example)
        accDiff = Mathf.Abs(accNew - accOld);
        if (accDiff > speedAcc) {
            accDiff = speedAcc;
        }
        accOld = accNew;

        if (Input.GetKey(KeyCode.S)) {                      //Move cube by speed = 2
            speedAcc = 2;
            transform.localPosition += new Vector3(speedAcc, 0, 0);
        }
        if (Input.GetKey(KeyCode.D)) {                      //Move cube by speed = 1
            speedAcc = 1;
            transform.localPosition -= new Vector3(speedAcc, 0, 0);
        }
        // END ACCELEROMTER DIFFERENCE ---------------

        avgDiff = Mathf.Max(rotDiff, accDiff);              //LPF takes the highest value between angle and acceleration
        lowPassFilter.cutoffFrequency = lowCut + avgDiff * frequencyScalar;     //cutoffFrequency changes based on movement
        print(avgDiff);


    }
}
