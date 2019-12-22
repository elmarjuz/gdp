using UnityEngine;
using System.Collections;

public class ManageTheArena : MonoBehaviour {


	PublicData data;
	NewStuff moonStuff;
	NewStuff planetStuff;
	public GameObject planet;
	public GameObject mothership;
	float size = 1;

	GameObject[] things;
	float[,] angles;
	int[] count;

	public bool newWave;

	public Texture win;

	bool showThing;


	public GameObject shield;
	public GameObject gun;
	public GameObject burst;
	public GameObject missile;
	public GameObject snowman;
	public GameObject enemy;

	public GameObject snowStorm;

	TakeDamageAndGetDestroyed core;
	TakeDamageAndGetDestroyed leftarm;
	TakeDamageAndGetDestroyed rightarm;

	float health;


	// Use this for initialization	
	void Start () {
		snowStorm.transform.localScale = new Vector3(0.5f, 0.5f, 1);
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		data.setIsArena(true);
		health = data.motherShipHealth;
		planetStuff = planet.GetComponent<NewStuff>();
		gun.GetComponent<RocketLaunch>().enabled = false;
		core = mothership.transform.Find("core").gameObject.GetComponent<TakeDamageAndGetDestroyed>();
		leftarm = mothership.transform.Find("leftarm").gameObject.GetComponent<TakeDamageAndGetDestroyed>();
		rightarm = mothership.transform.Find("rightarm").gameObject.GetComponent<TakeDamageAndGetDestroyed>();
	}

	void EnlargePlanet(){
		size+=0.3f;
		planet.transform.localScale = new Vector3(size, size, 1); 
		//snowStorm.transform.localScale=planet.transform.localScale;
		//Instantiate(snowStorm);
		data.renewHeightMultiplier();

	}


	void OnGUI(){
		GUI.backgroundColor = Color.clear;
		if(showThing)
			GUI.Box(new Rect(Screen.width/2-400, Screen.height/2-200, 800, 800), win);

	}

	public IEnumerator Wait(int num){
		showThing = true;
		NewWave(num);
		yield return new WaitForSeconds(2.5f);
		showThing = false;
		core.addHealth(health);
		leftarm.addHealth(health);
		rightarm.addHealth(health);
		mothership.SendMessage ("DisplayOverlay", "All damage healed!");
	}


	public void NewWave(int num){
		newWave = true;
		planetStuff.clearLevel();

		switch(num){
		case 0:
			EnlargePlanet();
			shield.transform.localScale = new Vector3(0.5f, 0.5f, 1);
			gun.GetComponent<RocketLaunch>().enabled = false;
			things = new GameObject[]{gun, shield};
			angles = new float[,]{
				{1, 14, 170, 250, 270, 90, 110, 0, 0, 0},
				{10, 170, 260, 80, 0, 0, 0, 0, 0, 0}
			};
			count = new int[] {7, 4};
			planetStuff.SpawnAll(things, angles, count);
			break;
		case 1:
			EnlargePlanet();
			angles = new float[,]{
				{72, 152, 168, 278, 291, 200, 0, 0, 0, 0},
				{80, 160, 270, 310, 0, 0, 0, 0, 0, 0},
				{87, 267, 300, 220, 0, 0, 0, 0, 0, 0}

			};
			gun.GetComponent<RocketLaunch>().enabled = false;
			things = new GameObject[]{gun, shield, burst};
			count = new int[] {8, 4, 5};
			planetStuff.SpawnAll(things, angles, count);
			break;
		case 2:
			EnlargePlanet();
			angles = new float[,]{
				{145, 175, 185, 65, 300, 0, 100, 0, 0, 0},
				{170, 60, 300, 60, 200, 0, 0, 0, 0, 0},
				{165, 180, 55, 310, 0, 0, 0, 0, 0, 0},
				{170, 0, 0, 0, 0, 0, 0, 0, 0, 0}
			};
			gun.GetComponent<RocketLaunch>().enabled = false;
			things = new GameObject[]{gun, shield, burst, enemy};
			count = new int[] {7,5,4,1};
			planetStuff.SpawnAll(things, angles, count);

			break;
		case 3:
			EnlargePlanet();
			angles = new float[,]{
				{355, 212, 125, 20, 200, 130, 0, 0, 0, 0},
				{350, 210, 125, 85, 270, 0, 20, 0, 0, 0},
				{340, 120, 10, 0, 0, 0, 0, 0, 0, 0},
				{350, 210, 0, 0, 0, 0, 0, 0, 0, 0}
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy};
			count = new int[] {6,6,4,2};
			planetStuff.SpawnAll(things, angles, count);

			break;
		case 4:
			EnlargePlanet();
			angles = new float[,]{
				{105, 95, 25, 265, 275, 160, 0, 0, 0, 0},
				{100, 30, 270, 170, 263, 190, 0, 0, 0, 0},
				{90, 260, 180, 0, 0, 0, 0, 0, 0, 0},
				{100, 30, 270, 170, 0, 0, 0, 0, 0, 0},
				{95, 35, 40, 45, 268, 0, 0, 0, 0, 0}
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy, missile};
			count = new int[] {6,7,3,4,4};
			planetStuff.SpawnAll(things, angles, count);
			break;

		case 5:
			EnlargePlanet();
			angles = new float[,]{
				{167, 310, 221, 103, 75, 7, 340, 0, 0, 0},
				{160, 330, 220, 110, 10, 0, 0, 0, 0, 0},
				{156, 201, 5, 80, 0, 0, 0, 0, 0, 0},
				{160, 330, 10, 0, 0, 0, 0, 0, 0, 0},
				{70, 90, 150, 190, 0, 0, 0, 0, 0, 0}, 
				{1, 180, 250, 0, 0, 0, 0, 0, 0, 0}
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy, missile, snowman};
			count = new int[] {7,5,4,3,4,3};
			planetStuff.SpawnAll(things, angles, count);
			break;
			
		
		case 6:
			EnlargePlanet();
			angles = new float[,]{
				{30, 50, 70, 120, 130, 190, 330, 0, 0, 0},
				{37, 230, 100, 320, 160, 0, 0, 0, 0, 0},
				{41, 97, 315, 231, 160, 0, 0, 0, 0, 0},
				{25, 100, 320, 0, 0, 0, 0, 0, 0, 0},
				{35, 40, 225, 235, 0, 0, 0, 0, 0, 0}, 
				{90, 220, 310, 0, 0, 0, 0, 0, 0, 0}
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy, missile, snowman};
			count = new int[] {7,5,5,3,4,3};
			planetStuff.SpawnAll(things, angles, count);
			break;
		case 7:
			EnlargePlanet();
			angles = new float[,]{
				{30, 100, 110, 115, 155, 165, 210, 0, 0},
				{25, 105, 160, 180, 330, 355, 0, 0, 0, 0},
				{350, 335, 340, 97, 175, 3, 0, 0, 0, 0},
				{325, 40, 120, 205, 0, 0, 0, 0, 0, 0},
				{27, 157, 215, 345, 0, 0, 0, 0, 0, 0}, 
				{140, 270, 50, 0, 0, 0, 0, 0, 0, 0}
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy, missile, snowman};
			count = new int[] {8,6,6,4,4,3};
			planetStuff.SpawnAll(things, angles, count);
			break;
		case 8:
			EnlargePlanet();
			angles = new float[,]{
				{30, 92, 110, 115, 155, 165, 200, 320, 0},
				{31, 105, 150, 170, 330, 355, 85, 0, 0, 0},
				{355, 340, 345, 103, 155, 3, 200, 0, 0, 0},
				{325, 35, 130, 85, 0, 0, 0, 0, 0, 0},
				{27, 157, 215, 345, 80, 355, 0, 0, 0, 0}, 
				{10, 100, 190, 260, 0, 0, 0, 0, 0, 0}
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy, missile, snowman};
			count = new int[] {9,7,7,4,6,4};
			planetStuff.SpawnAll(things, angles, count);
			break;
		case 9:
			EnlargePlanet();
			angles = new float[,]{
				{30, 100, 110, 115, 155, 165, 200, 320, 5},
				{31, 105, 150, 170, 330, 355, 85, 0, 0, 0},
				{350, 335, 340, 97, 155, 3, 200, 12, 240, 0},
				{325, 35, 130, 85, 240, 0, 0, 0, 0, 0},
				{27, 157, 215, 345, 80, 355, 90, 0, 0, 0}, 
				{140, 270, 50, 39, 0, 0, 0, 0, 0, 0}
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy, missile, snowman};
			count = new int[] {10,7,9,5,7,4};
			planetStuff.SpawnAll(things, angles, count);
			break;
		case 10:
			EnlargePlanet();
			angles = new float[,]{
				{07, 47, 85, 122, 169, 205, 252, 280, 330, 345},
				{330, 50, 90, 130, 170, 210, 250, 290, 0, 0},
				{07, 47, 85, 122, 169, 205, 290, 340, 0, 0},
				{10, 50, 90, 130, 210, 290, 325, 0, 0, 0},
				{07, 47, 85, 122, 169, 290, 252, 280, 0, 0},
				{330, 41, 280, 120, 170, 200, 0, 0, 0, 0},
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy, missile, snowman};
			count = new int[] {10,8,8,7,8,6};
			planetStuff.SpawnAll(things, angles, count);
			break;
		case 11:
			EnlargePlanet();
			angles = new float[,]{
				{07, 47, 85, 122, 169, 205, 252, 280, 330, 345},
				{10, 50, 90, 130, 170, 210, 250, 290, 325, 350},
				{07, 47, 85, 122, 169, 205, 252, 280, 330, 345},
				{10, 50, 90, 130, 170, 210, 250, 290, 325, 350},
				{07, 47, 85, 122, 169, 205, 252, 280, 330, 345},
				{09, 41, 90, 120, 170, 200, 240, 280, 340, 348},
			};
			gun.GetComponent<RocketLaunch>().enabled = true;
			things = new GameObject[]{gun, shield, burst, enemy, missile, snowman};
			count = new int[] {10,10,10,10,10,10};
			planetStuff.SpawnAll(things, angles, count);
			break;

		}



	}
	
	// Update is called once per frame
	void Update () {
		if(newWave) {
			data.invaded = new bool[360];
			foreach(Transform child in GameObject.Find ("OUI").transform){
				if(child.gameObject.tag == "invMarker"){
					print ("waat");
					Destroy (child.gameObject);
				}		
			}
			newWave = false;
		}
	}
}
