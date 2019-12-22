using UnityEngine;
using System.Collections;

public class moveTo : MonoBehaviour {
	
	//public GameObject target;
	public Vector3 moveDirection;
	
	void Start () {
		moveDirection = transform.position;
	}
	

	void Update () {
		
		
		//transform.rigidbody.velocity = target.transform.position-moveDirection;
		transform.GetComponent<Rigidbody>().velocity = -moveDirection;
		
	}
}
