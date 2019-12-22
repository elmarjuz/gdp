using UnityEngine;
using System.Collections;

public class ShootInRadius : MonoBehaviour
{
	
	Vector3 targetPosition;
	public GameObject bullet;
	GameObject LilShip;
	GameObject mothership;
	PublicData data;
	GameObject followed;
	float radius;
	float speed;
	float delay;
	float pause;
	float duration;
	float damage;	
	bool isShooting;
	float distance;
	bool isWaiting;
	bool isWaitingToShoot;

	void Start ()
	{
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		LilShip = GameObject.Find ("LILSHIP");
		mothership = GameObject.Find ("MOTHERSHIP");
		speed = data.plainGunBulletSpeed;
		delay = data.plainGunShootDelay;
		pause = data.plainGunPause;
		duration = data.plainGunDuration;
		radius = data.plainGunRadius;
		damage = data.plainGunDamage;

		isShooting = false;

	}

	IEnumerator WaitAndShoot() {
		isWaiting = true;
		yield return new WaitForSeconds(duration);
		isShooting = true;
		//print ("wait and shoot");
		
	}

	public void abort(){
		StopAllCoroutines();
		isShooting = false;
		isWaiting = false;
		isWaitingToShoot = false;
	}
	
	IEnumerator Wait() {
		StartCoroutine("WaitAndShoot");
		yield return new WaitForSeconds(pause);
		//print ("wait");
		isShooting = false;
		isWaiting = false;

	}


	IEnumerator Shoot(){
		isWaitingToShoot = true;
		yield return new WaitForSeconds(delay);
		GameObject shotBullet;
		targetPosition = followed.transform.position;
		shotBullet = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
		shotBullet.layer = 9;
		shotBullet.transform.GetComponent<Rigidbody2D>().velocity = (targetPosition - transform.position).normalized * speed;
		shotBullet.GetComponent<CommonBulletStuff>().setDamage(damage);
		isWaitingToShoot = false;

	}


	void Update (){
		if (Time.timeScale != 0) {
			if(transform.Find("body").GetComponent<SpriteRenderer>().enabled){
				LilShip = GameObject.Find("LILSHIP");
				distance = Vector3.Distance (transform.position, LilShip.transform.position);
				if ((distance <= radius) && !isShooting) {
					followed = LilShip;
					if(!isWaitingToShoot) StartCoroutine(Shoot());
					if(!isWaiting) StartCoroutine("Wait");
				} else if(Vector3.Distance (transform.position, mothership.transform.position) <= radius && !isShooting){
					followed = mothership;
					if(!isWaitingToShoot) StartCoroutine(Shoot());
					if(!isWaiting) StartCoroutine("Wait");
				}
			} 
		}
	}
}
