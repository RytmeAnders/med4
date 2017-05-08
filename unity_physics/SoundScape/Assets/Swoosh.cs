using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swoosh : MonoBehaviour {
    //TODO Removed orientationNew, using just ard.orientation, should work

    public bool dynamicSound;

    //---- Parameters inside Unity
    [Range(0f, 10f)] public float staticThreshold;          //Threshold determining when the static sample will play
    [Range(0,2000)] public int staticFreq;                  //The frequency cut off for the static sample
    [Range(-1f, 1f)] public float offset;                   //offset slider with range -1:1 (range defined above)
    public int lowCut, scalar;
    //----------------------------

    float orientationOld, orientationDiff, dynamicDiff;     //Orientation values to measure a difference over time
    float accelerationOld, accelerationDiff;                //Acceleration values from GY-85

    System.Random rand = new System.Random();               //A class with a method for generating random values
    AudioLowPassFilter lowPassFilter;
    ReadingArduino ard;                                     //Object of the Arduino board from the ReadingArduino script

    void OnAudioFilterRead(float[] data, int channels) {                //White noise generation
        for (int i = 0; i < data.Length; i++) {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);  //Fills float array with random floats -1:1
        }
    }

    void Start() {
        ard = GetComponent<ReadingArduino>();               //Object of the ReadingArduino script
        lowPassFilter = GetComponent<AudioLowPassFilter>(); //Object of the LowPassFilter component

        orientationOld = ard.orientation;                   //Initial orientation value
        accelerationOld = ard.acceleration;                 //Initial acceleration value
    }

    void Update() {
        //Reading the difference in orientation over time
        orientationDiff = Mathf.Abs(ard.orientation - orientationOld);
        orientationOld = ard.orientation;

        //Reading the difference in acceleration over time
        accelerationDiff = Mathf.Abs(ard.acceleration - accelerationOld);
        accelerationOld = ard.acceleration;

        if (dynamicSound) {
            /*TODO Maybe this prevents clipping?
            if(orientationDiff > ard.orientation) {
                orientationDiff = Mathf.Abs(ard.orientation);
            }*/
            dynamicDiff = Mathf.Max(orientationDiff, accelerationDiff);                 //Find the biggest change

            print("Diff Ori: " + orientationDiff + " Raw Ori: " + ard.orientation
                + " | Diff Acc: " + accelerationDiff + " Raw Acc: " + ard.acceleration);
            lowPassFilter.cutoffFrequency = lowCut + dynamicDiff * scalar;              //LPF freq changing based on change
        }
        else {
            if (orientationDiff > staticThreshold) {        //If difference is > threshold, play a static sound sample
                print("Sound Activated!");
                lowPassFilter.cutoffFrequency = staticFreq; //Static sample plays at a set freq cut off
            }
            print("Sound Inactive!");
            lowPassFilter.cutoffFrequency = 0;              //If no difference over time, filter out all freqs (play no sound)
        }
    }
}
