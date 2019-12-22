using UnityEngine;
using System.Collections;

public class lookAtCenter : MonoBehaviour {
	private float angle;
	Transform parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		parent = transform.parent;
		angle = parent.rotation.z;
		transform.eulerAngles = new Vector3(0, 0, angle-90);

	}
}
