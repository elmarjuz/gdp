using UnityEngine;
using System.Collections;

public class invasionSmoke : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystemRenderer>().GetComponent<Renderer>().material.renderQueue = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
