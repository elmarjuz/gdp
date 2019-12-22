using UnityEngine;
using System.Collections;

public class LilShipControls : MonoBehaviour

{
	public float moveSpeed = 1;
	float maxSpeed;
	private Vector3 targetPosition;
	private Vector3 motherPosition; //changed the variable because distance does not need to be stored

	public float health;

	PublicData data;	
	GameObject sprite;
	GameObject motherShip;

	public float targetDistance;
	float angle;

	bool isParked;	
	public bool wasTeleported;

	// Use this for initialization
	void Start ()
	{
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		motherShip = GameObject.Find ("MOTHERSHIP");
		sprite = transform.Find("body").gameObject;

		maxSpeed = data.lilShipSpeed;
		health = data.lilShipHealth;

		isParked = data.isDocked;
		wasTeleported = false;
		GetComponent<ParticleSystem>().enableEmission = false;
		if(isParked) sprite.GetComponent<SpriteRenderer>().enabled = false;
		
	}
	public void setHealth(float value){
		health = value;
	}

	public void Parking(){
		if(isParked){
			if(!(wasTeleported && health < data.lilShipHealth)){
				moveShip();
				isParked = false;
				data.setIsDocked(isParked);
				wasTeleported = false;
				GetComponent<EdgeCollider2D>().enabled = true;
			}

		} else { 
			transform.position = motherPosition;
			isParked = true;
			data.setIsDocked(isParked);
			GetComponent<EdgeCollider2D>().enabled = false;
		}

	}

	void moveShip(){
		Plane playerPlane = new Plane (Vector3.forward, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitdist = 0;

		transform.position += (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
		
		
		angle = Mathf.Atan2 (targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * 180 / Mathf.PI;
		transform.eulerAngles = new Vector3 (0, 0, angle);
		
		//added this, because flickering of the ship is annoying and it doesn't move past the point anyway
		if (Vector3.Distance (transform.position, targetPosition) < 0.3) {
			moveSpeed = 0;
		}

		
		
		if (Input.GetKey (KeyCode.Mouse0)) {
			//moved this check under keydown to avoid the constant turning after the mouse.
			if (playerPlane.Raycast (ray, out hitdist)) {
				targetPosition = ray.GetPoint (hitdist);	
			}
					
			//wouldn't be a problem usually but it also keeps moving forward when it turns and it is weird.
			if(!GetComponent<AudioSource>().isPlaying){
				GetComponent<AudioSource>().Play();

			}

			moveSpeed += 0.1f;
			
			//added a maxSpeed check
			if (moveSpeed > maxSpeed) {
				moveSpeed = maxSpeed;
			}
		} else {
			//transform.Translate(targetPosition.x, targetPosition.y, 0);
			moveSpeed -= moveSpeed / 60;
			if (moveSpeed < 0.1f) {
				moveSpeed = 0;
			}

		}		
	}


	void heal () {
		if(health != data.lilShipHealth){
			health +=0.05f;
			GetComponent<TakeDamageAndGetDestroyed>().setHealth(health);
		} else {
			wasTeleported = false;
			data.setWasTeleported(false);
		}
		if(health > data.lilShipHealth){
			health = data.lilShipHealth;
			GetComponent<TakeDamageAndGetDestroyed>().setHealth(health);
		}
	}

	public void setWasTeleported (bool setter){
		wasTeleported = setter;
	}

	// Update is called once per frame
	void Update () {

		if(!isParked) health = GetComponent<TakeDamageAndGetDestroyed>().health;
		data.setWasTeleported(wasTeleported);
		if(isParked) {
			sprite.GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<ParticleSystem>().enableEmission = false;
			transform.GetComponent<EdgeCollider2D>().enabled = false;
		} else {
			sprite.GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<ParticleSystem>().enableEmission = true;
			transform.GetComponent<EdgeCollider2D>().enabled = true;
		}
		motherPosition = motherShip.transform.position;

		if(wasTeleported){
			GetComponent<LilShipShoot>().enabled = false;
		} else {
			GetComponent<LilShipShoot>().enabled = true;

		}

		if (Time.timeScale != 0) {

			if (isParked) {
				transform.position = motherPosition;
				GetComponent<TakeDamageAndGetDestroyed>().enabled = false;
				heal();

			} else {
				moveShip();
				GetComponent<TakeDamageAndGetDestroyed>().enabled = true;
			}

		}
		if (Input.GetMouseButtonUp(0)){
			if(GetComponent<AudioSource>().isPlaying){
				GetComponent<AudioSource>().Stop();
			}
		}
	}
}
