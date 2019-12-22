using UnityEngine;
using System.Collections;

public class SpawnNatureEnemies : MonoBehaviour {
	public GameObject enemy;
	PublicData data;
	float delay;
	bool isLaunching;

	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		delay = data.enemySpawnDelay;
		isLaunching = true;
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds(delay);
		isLaunching = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0) {
			if(isLaunching){
				print("s");
				Vector3 position = new Vector3(0,0,0);
				Instantiate(enemy, position, transform.rotation);
				isLaunching = false;
				StartCoroutine(Wait());
			}
		}
	}
}
