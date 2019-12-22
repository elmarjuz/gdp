using UnityEngine;
using System.Collections;

public class DelayCollider : MonoBehaviour {

	public float delay=1;

	// Use this for initialization
	void Start () {
		GetComponent<CircleCollider2D>().enabled=false;
		StartCoroutine(WaitToEnable());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator WaitToEnable(){
		yield return new WaitForSeconds(delay);
		GetComponent<CircleCollider2D>().enabled=true;
	}
}
