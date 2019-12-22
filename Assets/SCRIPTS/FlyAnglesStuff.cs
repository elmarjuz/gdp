using UnityEngine;
using System.Collections;

public class FlyAnglesStuff : MonoBehaviour {
	GameObject thing;
	GameObject other;
	PublicData data;

	Vector3 lDirection;
	Vector3 MDirection;

	float rad = 2;
	float dist;

	Vector3 LPoint;
	Vector3 RPoint;
	Vector3 point;

	bool isDoin;

	float speed;

	// Use this for initialization
	void Start () {
		speed = 10;
		isDoin = false;
		thing = GameObject.Find("GoalTypeThing");
		lDirection = (thing.transform.position- transform.position);
		transform.GetComponent<Rigidbody2D>().velocity = lDirection.normalized * speed;

	}

	void doShit(){
		print("doin shit");
		Quaternion rot = Quaternion.AngleAxis(-10,Vector3.forward);
		MDirection = rot * (thing.transform.position - transform.position);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, MDirection, 100, 1 << 10);
		if(hit != null) {
			LPoint = new Vector3(hit.point.x, hit.point.y, 0);
		}

		rot = Quaternion.AngleAxis(10,Vector3.forward);
		MDirection = rot * (thing.transform.position - transform.position);

		hit = Physics2D.Raycast(transform.position, MDirection, 100, 1 << 10);
		if(hit != null) {
			RPoint = new Vector3(hit.point.x, hit.point.y, 0);
		}

		if(Vector3.Distance(RPoint, transform.position) > Vector3.Distance(LPoint, transform.position)){
			point = RPoint;
			print (RPoint);
			print ("R");
		} else {
			point = LPoint;
			print (LPoint);
			print ("L");
		}
		print (point);

		dist = rad + Vector3.Distance(other.transform.position, point);
		RaycastHit2D hit2 = Physics2D.Raycast(other.transform.position, 
		                                    point-other.transform.position, 
		                                    dist, 
		                                    1 << 10);
		lDirection.x = hit2.point.x;
		lDirection.y = hit2.point.y;

		transform.GetComponent<Rigidbody2D>().velocity = lDirection.normalized * speed;
		isDoin = false;

	}

	void seeIfInWay(){
		RaycastHit2D hit3 = Physics2D.Raycast(transform.position, thing.transform.position-transform.position, 1000, 1 << 10);
		if(hit3 != null) {
			if(hit3.collider.gameObject.name == "GoalTypeThing" 
			   && Vector3.Distance(other.transform.position, transform.position) > 10){

				isDoin = false;
				print ("false");
			} else {

				isDoin = true;
				print (hit3.point.x);
				if(hit3.collider.gameObject.name != "GoalTypeThing" ){
					other = hit3.collider.gameObject;
					if(Vector3.Distance(other.transform.position, transform.position) > 30) {
						isDoin = false;
					}
				}

			}


		}
	}
	
	// Update is called once per frame
	void Update () {
		seeIfInWay();

		if(isDoin){
			doShit ();
		} else {
			lDirection = (thing.transform.position- transform.position );
			transform.GetComponent<Rigidbody2D>().velocity = lDirection.normalized * speed;
		}

		if(Input.GetKeyDown(KeyCode.Mouse0)){

		}
	}
}
