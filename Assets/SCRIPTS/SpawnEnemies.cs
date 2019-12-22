using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
	public GameObject enemy;
	PublicData data;
	GameObject sprite;
	float delay;
	float radius;

	bool isLaunching;
	bool wasInvasion;

	float count;
	float enemyCount;



	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		delay = data.enemySpawnDelay;
		enemyCount = data.enemyShipCount;

		StartCoroutine("Wait");
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds(delay);
		isLaunching = true;
	}

	public void abort(){
		StopCoroutine("Wait");
		isLaunching = false;

	}



	void spawnALot(){
		Instantiate(enemy, transform.position, transform.rotation);
		count++;
		if(count >= enemyCount){
			print (count);
			CancelInvoke();
			count = 0;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0) {
			if(transform.Find("body")!=null && transform.Find("body").GetComponent<SpriteRenderer>().enabled){
				if(isLaunching){
					Instantiate(enemy, transform.position, transform.rotation);
					isLaunching = false;
					StartCoroutine("Wait");
				} 

				if(data.wasInvaded) {
					InvokeRepeating ("spawnALot", 1, 1);
					data.setWasInvaded(false);
					
				}
			} else if(transform.Find("body") == null){
				if(isLaunching){
					Instantiate(enemy, transform.position, transform.rotation);
					isLaunching = false;
					StartCoroutine("Wait");
				} 

				if(data.wasInvaded) {
					InvokeRepeating ("spawnALot", 1, 1);
					data.setWasInvaded(false);
					
				}
			}

		}
	}
}
