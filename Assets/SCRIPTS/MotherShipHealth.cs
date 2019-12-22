using UnityEngine;
using System.Collections;

public class MotherShipHealth : MonoBehaviour {
	public float health;
	PublicData data;
	
	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		health = data.motherShipHealth;
	}

	public void setHealth(float setter){
		health = setter;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
