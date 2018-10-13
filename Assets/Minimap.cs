using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public Transform target;
    public float distance = 15f;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.position.x, distance, target.position.z);
    }
}
