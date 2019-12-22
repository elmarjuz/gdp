using UnityEngine;
using System.Collections;

public class PickUpHealth : MonoBehaviour {
	PublicData data;
	GameObject mothership;
	GameObject core;
	float modifier;
	float top;
	float bot;
	
	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		mothership = GameObject.Find("MOTHERSHIP");

		core = mothership.transform.Find("core").gameObject;

		top = data.topHealth;
		bot = data.botHealth;
	}

	void OnCollisionEnter2D(){
		float adder = Random.Range(bot, top);

		core.gameObject.GetComponent<TakeDamageAndGetDestroyed>().addHealth(adder);

		mothership.SendMessage ("DisplayOverlay", "+ " + System.Math.Round(adder,2) + " HP!");
		PlayerPrefs.SetFloat("shipHealth", PlayerPrefs.GetFloat("shipHealth") + adder);
		Destroy(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
