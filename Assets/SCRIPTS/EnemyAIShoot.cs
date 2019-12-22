using UnityEngine;
using System.Collections;

public class EnemyAIShoot : MonoBehaviour {
	public GameObject bullet;
	GameObject mothership; 
	GameObject LilShip;
	PublicData data;
	GameObject followed;

	bool isShooting;

	float speed;
	float damage;
	float delay;
	float distance;

	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		mothership = GameObject.Find("MOTHERSHIP");
		LilShip = GameObject.Find("LILSHIP");
		speed = data.enemyBulletSpeed;
		damage = data.enemyShipDamage;
		delay = data.enemyShipShootDelay;
		distance = data.enemyShipShootDistance;
		isShooting = false;
	}

	void Update(){


			if(!isShooting && Vector3.Distance(transform.position, mothership.transform.position) <= distance){
				followed = mothership;
				shoot ();
				StartCoroutine(Wait());
			} else if (!isShooting && Vector3.Distance(transform.position, LilShip.transform.position) <= distance){
				followed = LilShip;
				shoot ();
				StartCoroutine(Wait());
				
			}


	}

	

	IEnumerator Wait() {

		yield return new WaitForSeconds(delay);
		isShooting = false;

		//isShooting = false;
	}
	
	void shoot(){
		GameObject shotBullet;
		shotBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
		shotBullet.transform.GetComponent<Rigidbody2D>().velocity = (followed.transform.position - transform.position).normalized * speed;
		shotBullet.GetComponent<CommonBulletStuff>().setDamage(damage);
		isShooting = true;
	}
}
