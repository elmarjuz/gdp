using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	PublicData data;

	GameObject mothership;
	GameObject lilShip;
	GameObject hitThing;
	
	Vector3 targetPosition;

	bool isLaunching;
	bool isShooting;

	float angle;
	float speed;
	float rad;

	float bulletSpeed;
	float damage;
	float distance;
	

	RaycastHit2D hit;


	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		mothership = GameObject.Find("MOTHERSHIP");
		lilShip = GameObject.Find ("LILSHIP");
		targetPosition = new Vector3(0, 0, 0);
		transform.GetComponent<Rigidbody2D>().velocity = (targetPosition - transform.position).normalized;
		bulletSpeed = data.enemyBulletSpeed;
		damage = data.enemyShipDamage;
		distance = data.enemyShipShootDistance;

	}

	void followShip(){
		transform.GetComponent<Rigidbody2D>().velocity = (new Vector3(mothership.transform.position.x, mothership.transform.position.y, 0) - transform.position).normalized;
		angle = Mathf.Atan2 (mothership.transform.position.y - transform.position.y, mothership.transform.position.x - transform.position.x) * 180 / Mathf.PI;
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, mothership.transform.position) >=distance && 
		   Vector3.Distance(transform.position, lilShip.transform.position) >=distance )
				followShip();
	}
}
