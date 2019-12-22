using UnityEngine;
using System.Collections;

public class LilShipShoot : MonoBehaviour {	
	
	Vector3 position;
	public Vector3 targetPosition;
	Vector3 moveDirection;
	float distance = 0;
	float speed;
	float damage;
	bool isShooting;
	float delay;

	Transform body;

PublicData data;	public GameObject bullet;


	
	
	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		body = transform.Find("body");
		speed = data.lilShipBulletSpeed;
		damage = data.lilShipDamage;
		delay = data.lilShipShootDelay;
		isShooting = false;
	}
	
	
	
	void getMouseClick(){
		
		Plane playerPlane = new Plane(Vector3.forward, transform.position);
	  	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
 
	 
		if (playerPlane.Raycast (ray, out distance)) {
			targetPosition = ray.GetPoint(distance); 	
		}



    	
	}

	IEnumerator Wait() {
		shoot ();
		yield return new WaitForSeconds(delay);
		isShooting = false;
		
	}

	void shoot(){
		GameObject shotBullet;
		shotBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
		shotBullet.transform.GetComponent<Rigidbody2D>().velocity = (targetPosition - transform.position).normalized * speed;
		shotBullet.GetComponent<CommonBulletStuff>().setDamage(damage);
		isShooting = true;
		if(body.GetComponent<Animator>()!=null){
			body.GetComponent<Animator>().Play("elfDudeAttack");
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire2")){
			if(!isShooting){
				getMouseClick();
				StartCoroutine(Wait ());
				
			}
		}
	}
}

 