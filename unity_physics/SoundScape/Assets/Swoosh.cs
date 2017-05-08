using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swoosh : MonoBehaviour {
    //TODO Removed orientationNew, using just ard.orientation, should work

    public bool dynamicSound;
    int speedRot, speedAcc;

    [Range(-1f, 1f)]
    public float offset;                                    //offset slider with range -1:1 (range defined above)

    public int lowCut, scalar;
    float orientationOld, orientationDiff;                  //Orientation values to measure a difference over time

    System.Random rand = new System.Random();               //A class with a method for generating random values
    AudioLowPassFilter lowPassFilter;
    ReadingArduino ard;

    void OnAudioFilterRead(float[] data, int channels) {                //White noise generation
        for (int i = 0; i < data.Length; i++) {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);  //Fills float array with random floats -1:1
        }
    }

    void Start() {
        ard = GetComponent<ReadingArduino>();               //Object of the ReadingArduino script
        lowPassFilter = GetComponent<AudioLowPassFilter>(); //Object of the LowPassFilter component

        orientationOld = ard.orientation;                   //Initial orientation value
    }

    void Update() {
        //Reading the difference in orientation over time
        orientationDiff = Mathf.Abs(ard.orientation - orientationOld);
        orientationOld = ard.orientation;

        if (dynamicSound) {
            /*TODO Maybe this prevents clipping?
            if(orientationDiff > ard.orientation) {
                orientationDiff = Mathf.Abs(ard.orientation);
            }*/

            print("Difference: " + orientationDiff + " Raw Value: " + ard.orientation);
            lowPassFilter.cutoffFrequency = lowCut + orientationDiff * scalar;          //LPF freq changing based on change in orientation
        }
        else {
            if (orientationDiff > 2f) {                 //If difference is > 2, play a static sound sample
                print("Sound Activated!");
                lowPassFilter.cutoffFrequency = 900;    //Static because cutOff is always 900
            }
            print("Sound Inactive!");
            lowPassFilter.cutoffFrequency = 0;          //If no difference over time, filter out all freqs (play no sound)
        }
    }
}
