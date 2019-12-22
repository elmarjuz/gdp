using UnityEngine;
using System.Collections;

public class LilShipRotation : MonoBehaviour
{

	public GameObject orbitalCenter;
	PublicData data;
	Vector3 pos;
	public float nX;
	public float nY;
	public float ocX;
	public float ocY;
	float bottomLimit;
	float topLimit;
	float passiveAngleChange;
	float speedFactor;
	float angle = 90;
	float radius = 6;
	bool isMovin = true;
	// Use this for initialization
	void Start ()
	{
		data = GameObject.Find ("DataHolder").GetComponent<PublicData> ();
		data.isArriving = false;
		bottomLimit = data.bottomLimit;
		topLimit = data.topLimit;
		passiveAngleChange = data.motherShipPassiveSpeed;
		speedFactor = data.motherShipSpeed;
		radius = topLimit;


	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.timeScale != 0) {

			if (angle > 360) {
				angle = 0;
			} else if (angle < 0) {
				angle = 360;
			}	

			if (Input.GetButton ("Fire2")) {
				isMovin = false;

				Plane playerPlane = new Plane (Vector3.forward, transform.position);
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);    
				float hitdist = 0;

				if (playerPlane.Raycast (ray, out hitdist)) {
					pos = ray.GetPoint (hitdist);	
				}

				float angle1 = Mathf.Atan2 (pos.y - transform.position.y, pos.x - transform.position.x) * 180 / Mathf.PI;
				transform.eulerAngles = new Vector3 (0, 0, angle1);


			}
			if (Input.GetButtonUp ("Fire2")) {
				isMovin = true;
			}
		
		
			if (isMovin) {
				Vector3 orbitalPosition = orbitalCenter.transform.position;
				ocX = orbitalPosition.x;
				ocY = orbitalPosition.y;
				transform.parent = null;
				float modifier = Input.GetAxis ("Vertical") / 2 * Time.deltaTime * speedFactor;
				radius += modifier;
				float input = Input.GetAxis ("Horizontal") * Time.deltaTime * speedFactor;
				if (input == 0)
					angle += passiveAngleChange;
				angle -= input;
				if (radius < bottomLimit)
					radius = bottomLimit;
				if (radius > topLimit)
					radius = topLimit;
				if (input > 0)
					transform.eulerAngles = new Vector3 (0, 0, angle - 90);
				if (input <= 0)
					transform.eulerAngles = new Vector3 (0, 0, angle + 90);
			} 

			Vector3 newPosition = transform.position;
			nX = ocX + radius * Mathf.Cos (Mathf.PI * angle / 180);
			nY = ocY + radius * Mathf.Sin (Mathf.PI * angle / 180);
			newPosition.x = nX;
			newPosition.y = nY;
			transform.position = newPosition;
		}
		data.motherShipAngle = angle;
		data.motherShipRadius = radius;
	}
}
