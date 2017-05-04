using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swoosh : MonoBehaviour {
    [Range(-1f, 1f)]
    public float offset;                                    //offset slider with range -1:1 (range defined above)

    public int lowCut, scalar;
    float orientationOld, orientationNew, orientationDiff;  //Orientation values to measure a difference over time

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
        orientationNew = ard.orientation;
        orientationDiff = Mathf.Abs(orientationNew - orientationOld);
        orientationOld = orientationNew;

        print("Me: " + orientationDiff + " Andreas: " + ard.orientation);

        lowPassFilter.cutoffFrequency = lowCut + orientationDiff * scalar;
    }
}
