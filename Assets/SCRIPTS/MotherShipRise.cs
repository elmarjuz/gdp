using UnityEngine;
using System.Collections;

public class MotherShipRise : MonoBehaviour {
	
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
		

		bottomLimit = data.bottomLimit;
		
		radius = 2.9f;
		
		
		
		orbitalPosition = orbitalCenter.transform.position;
		ocX = orbitalPosition.x;
		ocY = orbitalPosition.y;
		
		beg = 2.9f; 
		end = bottomLimit;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		data.setMotherShipRadius (radius);
		if(radius<bottomLimit){
			arrive ();
		} 
		if (end  <= radius){ 
			transform.Find("core").GetComponent<SpriteManager>().PickSprite(1);
			transform.Find("leftarm").GetComponent<SpriteManager>().PickSprite(1);
			transform.Find("rightarm").GetComponent<SpriteManager>().PickSprite(1);
			GetComponent<MotherShipRotation>().enabled = true;
			GetComponent<Invasion>().enabled = true;
			data.setMotherShipRadius(end);

			orbitalCenter.GetComponent<RotateAndShoot> ().isTut2 = true;

			data.setCore(true);
			GetComponent<MotherShipRise>().enabled = false;
		}
		
	}
	
	public void arrive(){
		Camera.main.orthographicSize = 27.5f;
		radius += 1*Time.deltaTime*2;
		Vector3 newPosition = transform.position;
		nX = ocX + radius * Mathf.Cos (Mathf.PI * angle / 180);
		nY = ocY + radius * Mathf.Sin (Mathf.PI * angle / 180);
		newPosition.x = nX;
		newPosition.y = nY;
		transform.position = newPosition;
		
		transform.eulerAngles = new Vector3 (0, 0, angle-90);
	}

}
