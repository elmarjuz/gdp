using UnityEngine;
using System.Collections;

public class LilShipArrive : MonoBehaviour {

	public GameObject orbitalCenter;
		PublicData data;
	GameObject leftArm;
	GameObject rightArm;
	Vector3 orbitalPosition;
	public float radius = 15;
	public float angle;
	public float nX;
	public float nY;
	public float ocX;
	public float ocY;
	public float bottomLimit;
	public float topLimit;

	public float beg;
	public float end;

	// Use this for initialization
	void Start () {

		
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();

		angle = data.motherShipAngle;

		
		topLimit = data.topLimit;
		bottomLimit = data.bottomLimit;

		radius = topLimit + 9;



		orbitalPosition = orbitalCenter.transform.position;
		ocX = orbitalPosition.x;
		ocY = orbitalPosition.y;

		beg = radius; 
		end = topLimit;

	}

	// Update is called once per frame
	void Update () {

		data.setMotherShipRadius (radius);
		if(radius>topLimit){
			arrive ();
		} 
		if (end  >= radius){ 

			GetComponent<LilShipRotation>().enabled = true;
			GetComponent<LilShipShoot>().enabled = true;
			orbitalCenter.GetComponent<RotateAndShoot>().enabled = true;
			orbitalCenter.GetComponent<RotateAndShoot>().isTut1 = true;

			GetComponent<LilShipArrive>().enabled = false;
		}

	}

	public void arrive(){
		if(end > beg) radius += 1*Time.deltaTime*2;
		if(end < beg) radius -= 1*Time.deltaTime*2;
		Vector3 newPosition = transform.position;
		nX = ocX + radius * Mathf.Cos (Mathf.PI * angle / 180);
		nY = ocY + radius * Mathf.Sin (Mathf.PI * angle / 180);
		newPosition.x = nX;
		newPosition.y = nY;
		transform.position = newPosition;
		
		transform.eulerAngles = new Vector3 (0, 0, angle-180);
	}

}
