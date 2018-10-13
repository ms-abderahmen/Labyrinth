using UnityEngine;
using System.Collections;

public class PillRotation : MonoBehaviour {
	public float rotationSpeed = 0.5f;

	private float y = 0f;

	void Update () {
		y += rotationSpeed;
		transform.rotation = Quaternion.Euler (0f,y,0f);
	}
}
