using UnityEngine;
using System.Collections;

public class controlAlpha : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setAlpha(float value){
		value = value/100;
		Color newColor = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, value);
		GetComponent<SpriteRenderer>().color = newColor;
	}
}
