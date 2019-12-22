using UnityEngine;
using System.Collections;

public class ControlIntroAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void pauseAnim(){
		GetComponent<Animator>().GetComponent<Animation>().Stop();
		GetComponent<Animation>().Stop();
	}
}
