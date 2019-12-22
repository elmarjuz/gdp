using UnityEngine;
using System.Collections;

public class AutoRotation : MonoBehaviour {

	public bool fullAuto;
	public float direction=1;
	PublicData data; 
	float angle;
	float radius;
	float radiusMod;
	float topLimit;
	float bottomLimit;
	Vector3 currentSize;

	// Use this for initialization
	void Start () {
		if(!fullAuto){
			data = GameObject.Find ("DataHolder").GetComponent<PublicData>();
			currentSize = transform.localScale;
		}

	
	}
	
	// Update is called once per frame
	void Update () {
		if(!fullAuto){


			radius = Camera.main.orthographicSize/2.5f;
			angle = Input.GetAxis("Horizontal")*Time.deltaTime*data.motherShipSpeed/radius;

			currentSize.x=radius*direction;
			currentSize.y=radius*direction;
			transform.localScale = currentSize;

			if(data.isArriving){
				if(angle==0){
					angle=0.5f*Time.deltaTime*radius;
					transform.Rotate(new Vector3(0, 0, angle*direction));
				}
			} else {
				if(angle==0){
					angle=0.5f*Time.deltaTime*radius;
				}
				transform.Rotate(new Vector3(0, 0, angle*direction));
				
			}
		} else {
			transform.Rotate(new Vector3(0, 0, 0.005f*direction));
		}
	}
}
