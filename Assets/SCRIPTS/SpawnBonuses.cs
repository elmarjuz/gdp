using UnityEngine;
using System.Collections;

public class SpawnBonuses : MonoBehaviour {

	public GameObject[] pickUps;
	PublicData data;
	float delay;
	float incDelay = 20;
	float topLimit;
	float bottomLimit;

	float radius;
	float angle;

	bool isSpawning = true;

	float nX;
	float nY;
	float ocX;
	float ocY;


	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		delay = data.pickUpDelay;
		topLimit = data.topLimit;
		bottomLimit = data.bottomLimit;
		ocX = transform.position.x;
		ocY = transform.position.y;
		InvokeRepeating ("increaseDelay", incDelay, incDelay);
		StartCoroutine(Wait());
	}

	public void renewLimits(){
		topLimit = data.topLimit;
		bottomLimit = data.bottomLimit;
	}


	IEnumerator Wait() {
		isSpawning = true;
		yield return new WaitForSeconds(delay);
		spawnPickUps();

	}

	void spawnPickUps (){
		isSpawning = false;

		Vector3 newPosition = transform.position;
		radius = Random.Range(bottomLimit, topLimit);
		angle = Random.Range(0, 360);
		nX = ocX + radius * Mathf.Cos (Mathf.PI * angle / 180);
		nY = ocY + radius * Mathf.Sin (Mathf.PI * angle / 180);
		newPosition.x = nX;
		newPosition.y = nY;
		int i = Random.Range(0, pickUps.Length);
		Instantiate (pickUps[i], newPosition, transform.rotation);
	}

	void increaseDelay(){
		delay+=1f;
	}

	public void setDelay(float value){
		delay = value;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isSpawning) StartCoroutine(Wait());
	}
}
