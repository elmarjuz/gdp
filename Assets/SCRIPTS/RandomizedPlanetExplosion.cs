using UnityEngine;
using System.Collections;

public class RandomizedPlanetExplosion : MonoBehaviour {

	public float val;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		transform.Find("burstBody").GetComponent<ParticleSystem>().Play ();
		val = GetComponent<GoAway>().getDelay();
		if(val>8){
			Instantiate(explosion, transform.position, transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
