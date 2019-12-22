using UnityEngine;
using System.Collections;

public class NewPlainGuns : MonoBehaviour {
	public GameObject plainGun;
	GameObject sprite;
	PublicData data;

	public float minRange=1;
	public float maxRange=1;
	float ocX;
	float ocY;
	float radius;
	float heightMultiplier;
	float[] angles;
	GameObject[] plainGuns;
	int plainGunNumber;
	float delay;
	float angle;
	int iPlainGun;
	public bool isSpawning;
	
	
	void spawn(float angle){
		Vector3 newPosition = transform.position;
		newPosition.x=ocX+radius*Mathf.Cos(Mathf.PI * angle / 180);
		newPosition.y=ocY+radius*Mathf.Sin(Mathf.PI * angle / 180);
		GameObject newPlainGun;
		newPlainGun = Instantiate(plainGun, newPosition, transform.rotation) as GameObject;
		newPlainGun.transform.eulerAngles = new Vector3(0, 0, angle-90);
		newPlainGun.GetComponent<TakeDamageAndGetDestroyed>().setAngle(angle);
		newPlainGun.transform.parent = transform;
		if(minRange!=1 || maxRange!=1){
			float sizeMultiplier = Random.Range(minRange,maxRange);
			newPlainGun.transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, sizeMultiplier);
		}
	}
	
	
	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		angles = data.plainGuns;
		delay = data.enemyRespawnDelay;
		plainGunNumber = angles.Length;
		isSpawning = false;
		
		heightMultiplier = data.heightMultiplier;
		radius = heightMultiplier;
		
		ocX = transform.position.x;
		ocY = transform.position.y;
		plainGunNumber = angles.Length;
		for(int i=0; i<plainGunNumber; i++){
			spawn(angles[i]);			
		}
	}
	
	IEnumerator Wait() {
		yield return new WaitForSeconds(delay);
		isSpawning = false;
		if(!data.invaded[(int)angles[iPlainGun]]){
			spawn(angles[iPlainGun]);
		} else {
			plainGunNumber -= 1;
			angles[iPlainGun] = angles[angles.Length-2];
		}	
	}
	
	// Update is called once per frame
	void Update () {
		plainGuns = GameObject.FindGameObjectsWithTag("EnemyGun");
		if(plainGuns.Length < plainGunNumber && !isSpawning){
			bool isThere = false;
			float thisAngle;
			for(int i=0; i < plainGunNumber; i++){
				isThere = false;
				for(int j = 0; j < plainGuns.Length; j++){
					thisAngle = plainGuns[j].GetComponent<TakeDamageAndGetDestroyed>().angle;
					if(thisAngle == angles[i]){
						isThere = true;
						break;
					}
				}
				if(!isThere){
					iPlainGun = i;
					isSpawning = true;
					StartCoroutine(Wait());
					break;
				}
			}
		}	
	}
}
