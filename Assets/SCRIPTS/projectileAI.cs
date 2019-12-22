using UnityEngine;
using System.Collections;

public class projectileAI : MonoBehaviour {

	public float speed = 25;
	
	void Start() {
		Vector3 newVelocity = Vector3.zero;
		newVelocity.y = speed;
		GetComponent<Rigidbody>().velocity = newVelocity;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
