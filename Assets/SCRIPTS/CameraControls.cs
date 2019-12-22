using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraControls : MonoBehaviour {
	
	float angle;
	float radius;
	public float camRadius;
	float topCamLimit;
	float currentCamLimit;
	float bottomCamLimit;
	float camMod;
	float heightMod;
	float radiusMod;
	float speedFactor;

	public bool advancedZoom;
	public bool cameraRotation;
	PublicData data;

	Vector3 origPos;
	bool isShaking;
	float quakeAmt;
	float amplitude;

	// Use this for initialization
	void Start () {
		if(SceneManager.GetActiveScene().name!="Intro"){
			data = GameObject.Find("DataHolder").GetComponent<PublicData>();
			heightMod= data.topLimit/12;
			topCamLimit = data.topLimit+data.topLimit/12+heightMod*2 + 5;
			bottomCamLimit = data.bottomLimit+heightMod+5;
		
			GetComponent<Camera>().orthographicSize = topCamLimit;
			speedFactor = data.motherShipSpeed;

		}
	}

	public void renewLimits(){
		heightMod= data.topLimit/12;
		topCamLimit = data.topLimit+data.topLimit/12+heightMod*2 + 5;
		bottomCamLimit = data.bottomLimit+heightMod+5;
		
		GetComponent<Camera>().orthographicSize = topCamLimit;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale!=0 && !data.isArriving && SceneManager.GetActiveScene().name!="Intro"){

			angle = data.motherShipAngle;
			radius = data.motherShipRadius;
			if(radius > data.topLimit) radius = data.topLimit;
			if(radius < data.bottomLimit) radius = data.bottomLimit;
			camMod = (Input.GetAxis("Mouse ScrollWheel") - Input.GetAxis ("Vertical")/(topCamLimit*heightMod*1.5f))*1000*Time.deltaTime;
			if(radius+heightMod>topCamLimit) currentCamLimit = topCamLimit;
			else if(radius+heightMod<bottomCamLimit) currentCamLimit = bottomCamLimit;
			else currentCamLimit = radius+heightMod;
	
			GetComponent<Camera>().orthographicSize-=camMod;
			if(GetComponent<Camera>().orthographicSize>topCamLimit) 
				GetComponent<Camera>().orthographicSize=topCamLimit;
			else if(GetComponent<Camera>().orthographicSize<bottomCamLimit)
				GetComponent<Camera>().orthographicSize=bottomCamLimit;
			else if(GetComponent<Camera>().orthographicSize<currentCamLimit && !advancedZoom)
				GetComponent<Camera>().orthographicSize=currentCamLimit;

			/*radiusMod = Input.GetAxis ("Vertical") / 2 * Time.deltaTime * speedFactor;
			if (radiusMod!=0 && camera.orthographicSize <= topCamLimit) {
				if(advancedZoom){
					radiusMod*=1.5f;
				}
				camera.orthographicSize += radiusMod*10*radius*Time.deltaTime;
			}*/



			if (cameraRotation){
				transform.eulerAngles = new Vector3(0, 0, angle-90);
				if(advancedZoom){
					Vector3 newPosition = transform.position;
					float advancedPos = 4+GetComponent<Camera>().orthographicSize/heightMod + radiusMod*radius/15;
					newPosition.x = advancedPos * Mathf.Cos (Mathf.PI * angle / 180);
					newPosition.y = advancedPos * Mathf.Sin (Mathf.PI * angle / 180);
					transform.position = newPosition;
					
					if (GetComponent<Camera>().orthographicSize<heightMod*2){
						GetComponent<Camera>().orthographicSize=heightMod*2;
						
					}
				}
			}
		}
		if(isShaking){
			Vector3 jigglePos = transform.position;
			quakeAmt = Random.Range(-amplitude/2, amplitude/2);
			jigglePos.x+= quakeAmt; 
			quakeAmt = Random.Range(-amplitude/2, amplitude/2);
			jigglePos.y+= quakeAmt; 
			transform.position = jigglePos;
		}
	}

	void LateUpdate(){
		if(Input.GetKeyDown("z") && cameraRotation){
			//advancedZoom=!advancedZoom;
		}
	}

	public void toggleRotation(bool value){
		angle=90;
		Vector3 newPosition = transform.position;
		newPosition.x = 0;
		newPosition.y = 0;
		transform.position = newPosition;

		cameraRotation = value;
		if(!cameraRotation){
			advancedZoom=false;
		}
	}

	public void ShakeDat(float delay, float value){
		if(!isShaking){
			origPos = transform.position;
			isShaking = true;
			amplitude = value;
			StartCoroutine(WaitToStopShaking(delay));
		}
	}

	IEnumerator WaitToStopShaking(float delay){
		yield return new WaitForSeconds(delay);
		isShaking = false;
		transform.position = origPos;
	}
}
