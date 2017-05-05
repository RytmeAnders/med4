using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiopan : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.panStereo += 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
