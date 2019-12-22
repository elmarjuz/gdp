using UnityEngine;
using System.Collections;

public class NewShields : MonoBehaviour {
	public GameObject shield;
	GameObject sprite;
	PublicData data;
	
	public float minRange=1;
	public float maxRange=1;
	float ocX;
	float ocY;
	float radius;
	float heightMultiplier;
	float[] angles;
	GameObject[] shields;
	int shieldNumber;
	float delay;
	float angle;
	int iShield;
	bool isSpawning;


	void spawn(float angle){
		Vector3 newPosition = transform.position;
		newPosition.x=ocX+radius*Mathf.Cos(Mathf.PI * angle / 180);
		newPosition.y=ocY+radius*Mathf.Sin(Mathf.PI * angle / 180);
		GameObject newShield;
		newShield = Instantiate(shield, newPosition, transform.rotation) as GameObject;

		newShield.transform.eulerAngles = new Vector3(0, 0, angle-90);
		newShield.GetComponent<TakeDamageAndGetDestroyed>().setAngle(angle);
		newShield.transform.parent = transform;
		if(minRange!=1 || maxRange!=1){
			float sizeMultiplier = Random.Range(minRange,maxRange);
			newShield.transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, sizeMultiplier);
		}
	}

	
	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		angles = data.shields;
		delay = data.enemyRespawnDelay;
		shieldNumber = angles.Length;
		isSpawning = false;

		heightMultiplier = data.heightMultiplier;
		radius = heightMultiplier;

		ocX = transform.position.x;
		ocY = transform.position.y;
		shieldNumber = angles.Length;
		for(int i=0; i<shieldNumber; i++){
			spawn(angles[i]);			
		}
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds(delay);
		isSpawning = false;
		if(!data.invaded[(int)angles[iShield]]){
			spawn(angles[iShield]);
		} else {
			shieldNumber -= 1;
			angles[iShield] = angles[angles.Length-2];
		}	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.name!="SLAVER STATION"){
			shields = GameObject.FindGameObjectsWithTag("EnemyShield");
			if(shields.Length < shieldNumber && !isSpawning){
				bool isThere = false;
				float thisAngle;
				for(int i=0; i < shieldNumber; i++){
					isThere = false;
					for(int j = 0; j < shields.Length; j++){
						thisAngle = shields[j].GetComponent<TakeDamageAndGetDestroyed>().angle;
						if(thisAngle == angles[i]){
							isThere = true;
							break;
						}
					}
					if(!isThere){
						iShield = i;
						isSpawning = true;
						StartCoroutine(Wait());
						break;
					}
				}
			}	
		}
	}
}
