using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ping : MonoBehaviour {

    public int startingPitch = 4;
    public int timeToDecrease = 5;
    AudioSource aud;

    void Start() {
        aud = GetComponent<AudioSource>();
        aud.pitch = startingPitch;
    }

    void Update() {
        if (aud.pitch > 0)
            aud.pitch -= Time.deltaTime * startingPitch / timeToDecrease;
    }
}
