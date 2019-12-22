using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	
	public GameObject orbitalCenter;
	PublicData data;
	public float radius=20;
	public float angle;
	
	public bool controllable=true;
	
	public float nX;
	public float nY;
	public float ocX;
	public float ocY;

	float passiveAngleChange = 1;
	float speedFactor;
	float passiveFactor;
	
	
	// Use this for initialization
	void Start () {
		
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		angle=data.moonAngle;
		speedFactor = data.moonSpeed;
		passiveFactor = data.moonSpeed;
		radius = data.topLimit + 3;		
	}

	public void renewLimits(){
		radius = data.topLimit + 3;	
	}
	
	// Update is called once per frame
	void Update () {
		if(angle > 360 ){
			angle = 0;
		} else if(angle < 0) {
			angle = 360;
		}
		data.setMoonAngle(angle);

		
		transform.eulerAngles = new Vector3(0, 0, angle-90);
		if(Time.timeScale!=0){
			Vector3 orbitalPosition = orbitalCenter.transform.position;
			ocX = orbitalPosition.x;
			ocY = orbitalPosition.y;
			angle+=passiveAngleChange*passiveFactor;
			
			Vector3 newPosition = transform.position;
			nX=ocX+radius*Mathf.Cos(Mathf.PI * angle / 180);
			nY=ocY+radius*Mathf.Sin(Mathf.PI * angle / 180);
			newPosition.x=nX;
			newPosition.y=nY;
			transform.position=newPosition;
			
			Vector3 relativePos = orbitalPosition - newPosition;
			Vector3 upwards = Vector3.right;

			transform.eulerAngles = new Vector3(0, 0, angle-90);
			
			
		}
		
	}
	
	public float getAngle(){
		return this.angle;
	}
	
	public float getRadius(){
		return this.radius;
	}

	
}
