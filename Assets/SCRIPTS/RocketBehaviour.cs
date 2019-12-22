using UnityEngine;
using System.Collections;

public class RocketBehaviour : MonoBehaviour
{
	public float rocketSpeed = 1;
	float direction;
	float accuracy = 5;
	PublicData data;
	float currentRadius;
	float limitRadius;
	public float currentAngle;
	float rotationMod;
	float limitAngle;
	bool goUp;
	bool goAround;
	bool goDown;

	// Use this for initialization
	void Start ()
	{
		goUp = true;
		data = GameObject.Find ("DataHolder").GetComponent<PublicData> ();
		limitAngle = data.motherShipAngle;
		limitRadius = data.topLimit +2 + Random.Range (-accuracy/5, accuracy);
		currentRadius = data.heightMultiplier;
		currentAngle = Mathf.Atan2 (transform.parent.transform.position.y, transform.parent.transform.position.x) * 180 / Mathf.PI;;
		transform.rotation = transform.parent.rotation;
		ChangeHeading(1);
		if (limitAngle - currentAngle < 0)
			direction = -1;
		else
			direction = 1;

		transform.parent = null;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		limitAngle = data.motherShipAngle;
		if (currentAngle >= 360)
			currentAngle -= 360;
		else if (currentAngle < 0)
			currentAngle += 360;

		if (goUp) {
			GoUp ();
		} else if (goAround) {
			GoAround();
		} else {
			GoDown();
		}

		if (!goDown && Mathf.Abs ((int)currentAngle - (int)limitAngle) < accuracy && data.motherShipRadius < currentRadius) {
			ChangeHeading(-1);
		}

		Vector3 newPosition = transform.position;
		newPosition.x = currentRadius * Mathf.Cos (Mathf.PI * currentAngle / 180);
		newPosition.y = currentRadius * Mathf.Sin (Mathf.PI * currentAngle / 180);
		transform.position = newPosition;
		transform.eulerAngles = new Vector3 (0, 0, currentAngle - rotationMod);

	
	}

	void GoUp ()
	{
		currentRadius += rocketSpeed * Time.deltaTime * currentRadius / 70;
		currentAngle += (0.01f+currentRadius/2)  * direction * Time.deltaTime;
		rotationMod -= 0.05f * rocketSpeed * currentRadius * direction * Time.deltaTime;
		
		if (currentRadius >= limitRadius) {
			ChangeHeading(0);

		}
	} 

	void GoAround ()
	{
		currentAngle += rocketSpeed * 2 * direction * Time.deltaTime;
	}

	void GoDown ()
	{
		currentRadius -= rocketSpeed * Time.deltaTime / currentRadius * 15;
		currentAngle += (0.05f+currentRadius/4.5f) * direction * Time.deltaTime;
		rotationMod -= 0.05f * rocketSpeed * currentRadius * direction * Time.deltaTime;
		
		if (currentRadius <= data.heightMultiplier) {
			GetComponent<CommonBulletStuff> ().poofItOut ();
			Destroy (gameObject);
		}
	}

	public void ChangeHeading (int head)
	{ //direction, as: 1 for up, 0 for around -1 for down
		switch (head) {
		case 1:
			goUp = true;
			goAround = false;
			rotationMod = 90;
			break;
		case 0:
			goUp = false;
			goAround = true;
			if (direction == -1)
				rotationMod = 180;
			else
				rotationMod = 0;
			break;
		default:
			//particleSystem.enableEmission=false;
			goUp = false;
			goAround = false;
			rotationMod = 270;
			break;
		}
	}

	public void ChangeDirection (int dir)
	{ //direction, as: -1 for CCW, 1 for CW
		direction = dir;
	}



}
