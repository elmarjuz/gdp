using UnityEngine;
using System.Collections;

public class RotateAndShoot : MonoBehaviour
{

	GameObject lilShip;
	PublicData data;
	public GameObject mothership;
	public float angle;
	public float currAngle = 0;
	float n;
	public float rotationSpeed = 1;
	Vector3 targetPosition;
	bool isRotating;
	float bot;
	float rad;

	public GUISkin skin;

	public bool isTut1;
	public bool isTut2;

	public GameObject tut1;
	public GameObject tut2;
	public GameObject camera;


	GameObject tut;
	// Use this for initialization
	void Start ()
	{
		lilShip = GameObject.Find ("LILSHIP");
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		angle = transform.rotation.z;
		bot = data.bottomLimit;
		rad = data.heightMultiplier;
		InvokeRepeating ("rotate", 3, 3);
	}

	void OnGUI () {
		if(skin){
			GUI.skin = skin;
		}


		if(isTut1){
			if(Screen.width == 960){
				if(Time.timeScale != 0){
					tut = Instantiate(tut1, transform.position,  camera.transform.rotation) as GameObject;
				}
				Time.timeScale = 0;
				if (GUI.Button(new Rect(Screen.width/2-45, Screen.height-130, 90, 40), "OK"))
				{
					isTut1 = false;
					Destroy(GameObject.Find("tut1(Clone)"));
					Time.timeScale = 1;
				}
			} else {
				if(Time.timeScale != 0){
					tut = Instantiate(tut1, transform.position,  camera.transform.rotation) as GameObject;
				}
				Time.timeScale = 0;
				if (GUI.Button(new Rect(Screen.width/2-50, Screen.height-150, 100, 60), "OK"))
				{
					isTut1 = false;
					Destroy(GameObject.Find("tut1(Clone)"));
					Time.timeScale = 1;
				}
			}

		}

		else if(isTut2){
			if(Screen.width == 960){
				if(Time.timeScale != 0){
					tut = Instantiate(tut2, transform.position,  camera.transform.rotation) as GameObject;
				}
				Time.timeScale = 0;
				if (GUI.Button(new Rect(Screen.width/2 + 110, Screen.height/2 + 150, 100, 40),"OK")){
					isTut2 = false;
					Destroy(GameObject.Find("tut2(Clone)"));
					mothership.GetComponent<MotherShipRise> ().enabled = false;
					
					Time.timeScale = 1;
					GetComponent<RotateAndShoot>().enabled = false;
					
					
				}
			} else {
				if(Time.timeScale != 0){
					tut = Instantiate(tut2, transform.position,  camera.transform.rotation) as GameObject;
				}
				Time.timeScale = 0;
				if (GUI.Button(new Rect(Screen.width/2 + 130, Screen.height/2 + 190, 120, 60),"OK")){
					isTut2 = false;
					Destroy(GameObject.Find("tut2(Clone)"));
					mothership.GetComponent<MotherShipRise> ().enabled = false;
					
					Time.timeScale = 1;
					GetComponent<RotateAndShoot>().enabled = false;
					
					
				}
			}

		} else {
		}
	}

	void getAngle ()
	{
		Vector3 targetDir = lilShip.transform.position - transform.position;
		Vector3 forward = transform.position;
		angle = Vector3.Angle (targetDir, forward);
		
	}

	void rotate ()
	{
		isRotating = !isRotating;
	}


	// Update is called once per frame
	void Update ()
	{
		if(isTut1){
			if(Input.GetKeyDown ("return") || Input.GetKeyDown ("enter")){
				isTut1 = false;
				Destroy(GameObject.Find("tut1(Clone)"));
				Time.timeScale = 1;
			}

		} else if(isTut2) {
			if(Input.GetKeyDown ("return") || Input.GetKeyDown ("enter")){
				isTut2 = false;
				Destroy(GameObject.Find("tut2(Clone)"));
				mothership.GetComponent<MotherShipRise> ().enabled = false;
				
				Time.timeScale = 1;
				GetComponent<RotateAndShoot>().enabled = false;
			}

		}

		if (Time.timeScale != 0) {
			n = 0;
			foreach (Transform child in transform.Find("WEAPONS")) {
				if (child.tag == "EnemyShield")
					n++;			
			}

			if (n == 0) {
				angle = transform.rotation.z * 180 / Mathf.PI;;
				print (angle);

				//lilShip.GetComponent<LilShipInvasion>().enabled = true;

				lilShip.GetComponent<LilShipControls> ().enabled = true;
				lilShip.GetComponent<LilShipRotation> ().enabled = false;

				data.setMotherShipAngle(angle+145);
				data.setMotherShipRadius(2.9f);

				mothership.GetComponent<MotherShipRise> ().enabled = true;

				mothership.GetComponent<MotherShipRotation> ().passiveAngleChange = 0.01f;
				mothership.GetComponent<MotherShipRotation> ().speedFactor = 15;


				mothership.transform.parent = null;



				//gameObject.transform.FindChild ("ROCKET SILO(Clone)").GetComponent<RocketLaunch> ().enabled = false;

			}

			if (isRotating) {
				targetPosition = lilShip.transform.position;
				angle = Mathf.Atan2 (targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * 180 / Mathf.PI;
				if ((int)currAngle > (int)angle + 35) {
					transform.eulerAngles = new Vector3 (0, 0, currAngle - rotationSpeed);
					currAngle -= rotationSpeed*Time.deltaTime*50; 
					//gameObject.transform.FindChild ("ROCKET SILO(Clone)").GetComponent<RocketLaunch> ().enabled = false;
				} else if ((int)currAngle < (int)angle + 35) {
					transform.eulerAngles = new Vector3 (0, 0, currAngle + rotationSpeed);
					currAngle += rotationSpeed*Time.deltaTime*50;
					//gameObject.transform.FindChild ("ROCKET SILO(Clone)").GetComponent<RocketLaunch> ().enabled = false;
				} else if ((int)currAngle == (int)angle + 35) {
					transform.eulerAngles = new Vector3 (0, 0, currAngle);
				} 

				if (Mathf.Abs (currAngle - (angle + 35)) <= 25) {
					gameObject.transform.Find ("WEAPONS").Find("SLAVER GUN(Clone)").GetComponent<ShootInRadius> ().enabled = true;
				} else {
					gameObject.transform.Find ("WEAPONS").Find("SLAVER GUN(Clone)").GetComponent<ShootInRadius> ().enabled = false;
				}
			} else {
				gameObject.transform.Find ("WEAPONS").Find("SLAVER GUN(Clone)").GetComponent<ShootInRadius> ().enabled = false;
			} 
		}


			
	}
}
