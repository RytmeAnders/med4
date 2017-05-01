using UnityEngine;
using System.Collections;

public class MoveTarget : MonoBehaviour {

    private int posCounter;

    private GameObject target;
    private Vector3[] targetPos = new Vector3[6];

	// Use this for initialization
	void Start () {
        MoveTargetOnArray();
    }

    void MoveTargetOnArray() // Sets positions target is to be moved to
    {
        posCounter = -1;
        target = GameObject.Find("Target");
        target.transform.position = new Vector3(0f, 0f, 0f);
        targetPos[0] = new Vector3(10f, 10f, 10f);
        targetPos[1] = new Vector3(20f, 20f, 20f);
        targetPos[2] = new Vector3(30f, 30f, 30f);
        targetPos[3] = new Vector3(40f, 40f, 40f);
        targetPos[4] = new Vector3(50f, 50f, 50f);
        targetPos[5] = new Vector3(60f, 60f, 60f);
    }

    void OnCollisionEnter(Collision collision) // Moves target when hit
    {
        posCounter++;
        if (posCounter >= 6)
        {
            posCounter = 0;
        }
        target.transform.position = targetPos[posCounter];
        print(posCounter);
    }
}
