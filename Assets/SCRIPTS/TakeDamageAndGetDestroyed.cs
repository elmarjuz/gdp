using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TakeDamageAndGetDestroyed : MonoBehaviour
{
	public GameObject xplosion;
	public GameObject xplosionSound;
	public GameObject theBar;
	GameObject sprite;

	public float health = 100;
	public bool isEnemy = true;
	public float angle;

	PublicData data;
	string theTag;
	string theName;
	float delay;
	float initialHealth;
	bool isHealing;
	bool disabled;
	bool isRespawning;

	public float amplitude=0.1f;
	float scaleMod;

	public GameObject littleThing;

	float allDmg;
	float allDmgCheck;
	


	// Use this for initialization
	void Start ()
	{
		//Instantiate(xplosion, transform.position, transform.rotation);

		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		theTag = gameObject.tag;
		theName = gameObject.name;
		if (theTag == "LilShip") {
			health = data.lilShipHealth;
		} else if (theTag == "MotherShip") {
			health = data.motherShipHealth;
			PlayerPrefs.SetFloat("shipHealth", health);
		} else if (theTag == "EnemyShip") {
			health = data.enemyShipHealth;
			sprite = transform.Find("body").gameObject;
		} else if (theTag == "EnemyLaser") {
			health = data.laserGunHealth;
			sprite = transform.Find("body").gameObject;
		} else if (theTag == "EnemyGun") {
			health = data.plainGunHealth;
			sprite = transform.Find("body").gameObject;
		} else if (theTag == "EnemyShield") {
			health = data.enemyShieldHealth;
			sprite = transform.Find("body").gameObject;
		} else if (theTag == "EnemySpawner") {
			health = data.enemySpawnerHealth;
			sprite = transform.Find("body").gameObject;
		} 
		initialHealth = health;
		if(theTag!="MotherShip" && theTag!="LilShip" && theTag!="EnemyGeneric"){
			scaleMod= Random.Range(-amplitude, amplitude);
			initialHealth+=scaleMod;
			transform.localScale += new Vector3(scaleMod, scaleMod, scaleMod);
			if(Random.value<0.5) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

		}
		delay = data.enemyRespawnDelay;
		if(xplosion==null){
			xplosion = data.currentExplosion;
		}

		isHealing = false;

		InvokeRepeating("checkTriggerThing", 2, 0.5f);

	}

	public void setAngle (float setter)
	{
		angle = setter;
	}

	IEnumerator WaitToHeal ()
	{
		float healthNow = health;
		yield return new WaitForSeconds (delay);
		if (healthNow == health) {
			isHealing = true;
		}
	}



	void destroyIt(){
		if (theTag == "EnemyLaser") {
			SendMessage("DisplayOverlay", "dead");
			if(transform.Find("theShot")){
				transform.Find("theShot").SendMessage("KillNow");
			}
		}
		print ("object destroyed");
		Instantiate (xplosion, transform.position, transform.rotation);
		Instantiate (xplosionSound);
		Destroy (gameObject);
	}




	// Update is called once per frame
	void Update ()
	{
		if(theName=="leftarm")data.motherShipLeftHealth = health/initialHealth;
  		else if(theName=="rightarm")data.motherShipRightHealth = health/initialHealth;
		else if(theName=="core")data.motherShipCoreHealth = health/initialHealth;

		if (health <= 0) {
			if (theTag == "LilShip") {
				if(SceneManager.GetActiveScene().name != "Tutorial"){
					Instantiate (xplosion, transform.position, transform.rotation);
					Instantiate (xplosionSound);
					GetComponent<LilShipControls> ().setWasTeleported (true);
					GetComponent<LilShipControls> ().setHealth (0.1f);
					GetComponent<LilShipControls> ().Parking ();

					PlayerPrefs.SetInt("lilShipDeath", PlayerPrefs.GetInt("lilShipDeath") + 1);
					print (PlayerPrefs.GetInt("lilShipDeath"));

				} else {
					data.EndLevel(false);
				}

			} else if (theTag == "MotherShip" && !disabled) {
				Instantiate (xplosion, transform.position, transform.rotation);
				Instantiate (xplosionSound);
				disablePart ();
			} else if (theTag != "MotherShip" && theTag != "EnemyShip" ) {
				if(SceneManager.GetActiveScene().name != "Tutorial"){
					if(!isRespawning){
						sprite.GetComponent<SpriteRenderer>().enabled = false;
						transform.Find("LIFE BAR").GetComponent<SpriteRenderer>().enabled = false;
						transform.Find("LIFE BAR").Find("thebar").GetComponent<SpriteRenderer>().enabled = false;
						gameObject.GetComponent<PolygonCollider2D>().enabled = false;
						theBar.GetComponent<healthDisplay>().Reset();
						disableShooting();
						StartCoroutine(WaitToSpawn());
					}
				} else {
					Instantiate (xplosion, transform.position, transform.rotation);
					Instantiate (xplosionSound);
					Destroy(gameObject);
				}

			}else if(theTag == "EnemyShip"){
				Instantiate (xplosion, transform.position, transform.rotation);
				Instantiate (xplosionSound);
				Destroy(gameObject);
			}

		}

	}

	void disableShooting(){
		if (theTag == "EnemyLaser") {
			gameObject.GetComponent<LaserShoot>().abort ();

		} else if (theTag == "EnemyGun") {
			gameObject.GetComponent<ShootInRadius>().abort ();

		} else if (theTag == "EnemyShield") {


		} else if (theTag == "EnemySpawner") {
			gameObject.GetComponent<SpawnEnemies>().abort ();

		} 
	}


	public void addHealth(float value){
		health += value;}


	IEnumerator WaitToSpawn(){
		isRespawning = true;
		yield return new WaitForSeconds(delay);
		if(data.invaded[(int)angle]) destroyIt();
		else {
			health = initialHealth * 0.7f;
			theBar.GetComponent<healthDisplay>().Heal(0.7f);
			sprite.GetComponent<SpriteRenderer>().enabled = true;
			gameObject.GetComponent<PolygonCollider2D>().enabled = true;
			isRespawning = false;
			PlayerPrefs.SetFloat("respawned", PlayerPrefs.GetFloat("respawned") + 1);
		}
	}


	IEnumerator endGame(){

		transform.parent.GetComponent<MotherShipRotation>().enabled = false;
		transform.parent.GetComponent<Invasion>().enabled = false;
		for (int i=0; i<10; i++){
			yield return new WaitForSeconds(0.3f);
			Vector3 explMod = new Vector3(Random.Range(-3, 3),Random.Range(-3, 3),0);
			Instantiate (xplosion, transform.position+explMod, transform.rotation);
			Instantiate (xplosionSound);
		}

		data.setIsLost(true);
		data.SendMessage("EndLevel", false);

	}
	/*
	 *MovedThe part-specific things here, because, no, this much shit is not required in Update
	 *this stuff is called only once, so, made a binary branch. When a part gets disabled
	 *it's never checked again. Implementing part healing is gonna be a bitch but oh well.
	 */
	public void disablePart ()
	{
		if (theName == "core") {
			StartCoroutine(endGame());
		} else if (theName == "leftarm") {
			gameObject.transform.parent.SendMessage ("disableMovement", true);
		} else if (theName == "rightarm") {
			gameObject.transform.parent.SendMessage ("disableMovement", false);
		}
		Instantiate (xplosion, transform.position, transform.rotation);
		Instantiate (xplosionSound);

		disabled = true;
	}

	public void setHealth (float setter)
	{
		health = setter;
	}


	void checkTriggerThing(){
		if(allDmg == allDmgCheck && allDmg != 0){
			if (isEnemy) {
				
				GameObject thing = Instantiate(littleThing, transform.position, Quaternion.identity) as GameObject;
				thing.GetComponent<showDmg>().isGood = true;
				thing.GetComponent<showDmg>().dmg = allDmg;
				thing.SendMessage("setMsg");
				allDmg = 0;
				
			}  else if (!isEnemy) {
				
				GameObject thing = Instantiate(littleThing, transform.position, Quaternion.identity) as GameObject;
				thing.GetComponent<showDmg>().isGood = false;
				thing.GetComponent<showDmg>().dmg = allDmg;
				thing.SendMessage("setMsg");
				allDmg = 0;
				
				
			}	
		} else {
			allDmgCheck = allDmg;
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if (!disabled && health>0) {
			//print ("duuh");
			//print (other.gameObject.tag);
			if (other.gameObject.tag == "PlayerWeapon" && isEnemy) {

				GameObject thing = Instantiate(littleThing, transform.position, Quaternion.identity) as GameObject;
				thing.GetComponent<showDmg>().isGood = true;
				thing.GetComponent<showDmg>().dmg = allDmg;
				thing.SendMessage("setMsg");

			}  else if (other.gameObject.tag == "EnemyWeapon" && !isEnemy) {
				
				GameObject thing = Instantiate(littleThing, transform.position, Quaternion.identity) as GameObject;
				thing.GetComponent<showDmg>().isGood = false;
				thing.GetComponent<showDmg>().dmg = allDmg;
				thing.SendMessage("setMsg");
								
	
			}	
			
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		allDmg = 0;
	}


	void OnTriggerStay2D (Collider2D other)
	{
		if (!disabled && health>0) {
			//print ("duuh");
			//print (other.gameObject.tag);
			if (other.gameObject.tag == "PlayerWeapon" && isEnemy) {

				float dmg = other.gameObject.GetComponent<CommonBulletStuff> ().damage;
				health -= dmg;
				theBar.SendMessage ("DisplayDamage", dmg / initialHealth);

			
				//print ("laser hit something enemy");
				isHealing = false;
				allDmg +=dmg;
			}  else if (other.gameObject.tag == "EnemyWeapon" && !isEnemy) {

				float dmg = other.gameObject.GetComponent<CommonBulletStuff> ().damage;
				health -= dmg;

				if(theName == "MOTHERSHIP") 
					PlayerPrefs.SetFloat("shipHealth", health);

				if (theName != "LILSHIP" && theTag != "MotherShip") {
					isHealing = false;
				} else 	if (theName=="core" || theName=="leftarm" || theName=="rightarm"){
					data.ShakeDatGUI(0.1f, 0.2f);

				} else if (theName=="LILSHIP"){
					data.ShakeDatGUI(0.1f, 0.1f);
					theBar.SendMessage ("DisplayDamage", dmg / initialHealth);
				} else {
					theBar.SendMessage ("DisplayDamage", dmg / initialHealth);
				}

				allDmg +=dmg;
				if (Random.Range (0, 100) < 1) {
					Instantiate (xplosion, transform.position, transform.rotation);
					Instantiate (xplosionSound);


				}
			}	

		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (!disabled && health>0) {
			//print ("collision between " + theName + "(" + theTag + ") and " + other.gameObject.name + "(" + other.gameObject.tag + ").");
			if (other.gameObject.tag == "PlayerWeapon" && isEnemy) {
				float dmg = other.gameObject.GetComponent<CommonBulletStuff> ().damage;
				health -= dmg;
				//if(transform.FindChild("shieldPulse"))

				theBar.SendMessage ("DisplayDamage", dmg / initialHealth);

				GameObject thing = Instantiate(littleThing, transform.position, Quaternion.identity) as GameObject;
				thing.GetComponent<showDmg>().isGood = true;
				thing.GetComponent<showDmg>().dmg = dmg;
				thing.SendMessage("setMsg");

				//print ("projectile hit something enemy");
				isHealing = false;
			} else if (other.gameObject.tag == "EnemyWeapon" && !isEnemy) {
				float dmg = other.gameObject.GetComponent<CommonBulletStuff> ().damage;
				health -= dmg;

				GameObject thing = Instantiate(littleThing, transform.position, Quaternion.identity) as GameObject;
				thing.GetComponent<showDmg>().isGood = false;
				thing.GetComponent<showDmg>().dmg = dmg;
				thing.SendMessage("setMsg");

			
				if (theName != "LILSHIP" && gameObject.tag != "MotherShip") {
					isHealing = false;
				} else 	if (theName=="core" || theName=="leftarm" || theName=="rightarm"){
					data.ShakeDatGUI(0.2f, 0.4f);
				} else if (theName=="LILSHIP"){
					data.ShakeDatGUI(0.1f, 0.2f);
					theBar.SendMessage ("DisplayDamage", dmg / initialHealth);
				} else {
					theBar.SendMessage ("DisplayDamage", dmg / initialHealth);
				}

			}
		}
	}
}
