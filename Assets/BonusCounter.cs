using UnityEngine;
using System.Collections;

public class BonusCounter : MonoBehaviour {

	public GameObject planet;
	public int respawned;
	public int lilShipDeaths;
	public float shipHealth;
	public int pickUpsMissed;
	public float invasionAvgPrc;
	public int defencesLeft;
	
	public float dmgDone;
	
	public bool isEnd;
	public bool isWon;
	bool isCountin;
	
	PublicData data;
	
	float origShipHealth;
	
	float finalScore;
	float currScore;
	
	float bonus1;
	float bonus2;
	float bonus3;
	float bonus4;
	float bonus5;
	float bonus6;

	public GUISkin skin; 
	
	public Texture goodEnd;
	public Texture badEnd;
	Texture end;
	
	public Texture message1;
	public Texture message2;
	public Texture message3;
	public Texture message4;
	public Texture message5;
	
	Texture message;
	public string nextLevel;

	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		origShipHealth = data.motherShipHealth;
	}
	
	public void setDmg(float value){
		dmgDone = value;
	}
	
	IEnumerator wait(){
		yield return new WaitForSeconds(0.01f);
	}
	
	void typeNumbers(float bonus){
		for(float i = 0; i <= bonus; i++){
			finalScore = currScore + i;
		}
		currScore = finalScore;
	}
	
	public void countBonuses(){

		
		
		isCountin = true;
		dmgDone = PlayerPrefs.GetFloat("dmgDone");
		lilShipDeaths = PlayerPrefs.GetInt("lilShipDeath");
		pickUpsMissed = PlayerPrefs.GetInt("pickUpsMissed");
		invasionAvgPrc = PlayerPrefs.GetFloat("invasionAvgPrc");
		shipHealth = PlayerPrefs.GetFloat("shipHealth");
		respawned = PlayerPrefs.GetInt("respawned");
		defencesLeft = planet.transform.Find("WEAPONS").childCount;
		
		
		if(lilShipDeaths <= 0) bonus2 = 1000;
		else if(lilShipDeaths <= 2) bonus2 = 800;
		else if(lilShipDeaths <= 4) bonus2 = 600;
		else bonus2 = 400;
		typeNumbers(bonus1);

		if(pickUpsMissed <= 0) bonus4 = 1000;
		else if(pickUpsMissed <= 2) bonus4 = 800;
		else if(pickUpsMissed <= 4) bonus4 = 600;
		else bonus4 = 400;
		typeNumbers(bonus2);
		
		if(invasionAvgPrc >= 90) bonus5 = 1000;
		else if(invasionAvgPrc >= 80) bonus5 = 800;
		else if(invasionAvgPrc >= 60) bonus5 = 600;
		else bonus5 = 400;
		typeNumbers(bonus3);
		
		if(shipHealth >= origShipHealth * 0.8f) bonus3 = 1000;
		else if(shipHealth >= origShipHealth * 0.6f) bonus3 = 800;
		else if(shipHealth >= origShipHealth * 0.4f) bonus3 = 600;
		else bonus3 = 400;
		typeNumbers(bonus4);
		
		if(respawned <= 0) bonus1 = 1000;
		else if(respawned <= 1) bonus1 = 800;
		else if(respawned <= 3) bonus1 = 600;
		else bonus1 = 400;
		typeNumbers(bonus5);
		
		if(defencesLeft <= 0) bonus6 = 1000;
		else if(defencesLeft <= 3) bonus6 = 800;
		else if(defencesLeft <= 5) bonus6 = 600;
		else bonus6 = 400;
		typeNumbers(bonus6);


		if(finalScore / 6000 >= 0.8f) message = message1;
		else if(finalScore / 6000 >= 0.6f) message = message2;
		else if(finalScore / 6000 >= 0.4f) message = message3;
		else if(finalScore / 6000 >= 0.2f) message = message4;
		finalScore += dmgDone;

		if(isWon) {
			end = goodEnd;
		}
		else {
			end = badEnd;
			message = null;
		} 

		PlayerPrefs.SetFloat("highScore", finalScore);
		PlayerPrefs.SetString("highScore", Application.loadedLevelName);
	}
	
	void OnGUI(){
		if(skin){
			GUI.skin = skin;
		}
		if(isEnd){
			if(Screen.width == 960){
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				
				//GUI.Label(new Rect(20,220,200,200), "fin " + finalScore.ToString());
				
				GUI.Label(new Rect(Screen.height/2 - 150,0, 660, Screen.height), end);
				

				
				if(!isWon){
					
					if (GUI.Button (new Rect ((Screen.width - 500) / 2 + 95, (Screen.height - 80), 140, 60), "Retry")) {
						Application.LoadLevel (nextLevel);
					}
					if (GUI.Button (new Rect ((Screen.width - 500) / 2 + 265, (Screen.height - 80), 140, 60), "Exit")) {
						Application.LoadLevel ("Intro");
					}
					
				} else {
					GUI.skin.label.fontSize = 45;
					GUI.Label(new Rect (Screen.width / 2 - 200, Screen.height - 180, 180, 100), finalScore.ToString());

					GUI.skin.label.fontSize = 20;
					GUI.Label(new Rect (Screen.width / 2 - 110, Screen.height - 170, 180, 100), "+dmg");
					GUI.Label(new Rect (Screen.width / 2 - 110, Screen.height - 150, 180, 100), dmgDone.ToString());
					
					if (GUI.Button (new Rect ((Screen.width - 400) / 2 + 133, (Screen.height - 80), 140, 60), "Continue")) {
						Application.LoadLevel (nextLevel);
					}
					GUI.Label(new Rect(Screen.width / 2 + 10, Screen.height - 200, 200, 150), message);
				}
				
				GUI.skin.label.fontSize = 20;
				
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, 170, 250, Screen.height - 210));
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
				GUILayout.Label("RESPAWNED DEF");
				GUILayout.Label("LIL'SHIP DEATH");
				GUILayout.Label("SHIP HEALTH");
				GUILayout.Label("PICK-UPS MISSED");
				GUILayout.Label("INVASION QUALITY");
				GUILayout.Label("DEFENCES LEFT");
				
				GUILayout.EndArea();
				
				
				GUILayout.BeginArea(new Rect(Screen.width / 2 + 27  , 170, 100, Screen.height - 210));
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				
				GUILayout.Label(respawned.ToString());
				GUILayout.Label(lilShipDeaths.ToString());
				GUILayout.Label((shipHealth/origShipHealth * 100).ToString() + "%");
				GUILayout.Label(pickUpsMissed.ToString());
				GUILayout.Label(((int)invasionAvgPrc).ToString() + "%");
				GUILayout.Label(defencesLeft.ToString());
				
				GUILayout.EndArea();
				
				
				GUILayout.BeginArea(new Rect(Screen.width / 2 + 130  , 170, 100, Screen.height - 210));
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				
				GUILayout.Label(bonus1.ToString());
				GUILayout.Label(bonus2.ToString());
				GUILayout.Label(bonus3.ToString());
				GUILayout.Label(bonus4.ToString());
				GUILayout.Label(bonus5.ToString());
				GUILayout.Label(bonus6.ToString());
				
				GUILayout.EndArea();
				

				

			} else {
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				
				//GUI.Label(new Rect(20,220,200,200), "fin " + finalScore.ToString());
				
				GUILayout.BeginArea(new Rect(0,0, Screen.width, Screen.height));
				GUILayout.Label(end);
				GUILayout.EndArea();
				

				
				if(!isWon){
					
					if (GUI.Button (new Rect ((Screen.width - 500) / 2 + 105, (Screen.height - 80), 140, 60), "Retry")) {
						Application.LoadLevel (nextLevel);
					}
					if (GUI.Button (new Rect ((Screen.width - 500) / 2 + 275, (Screen.height - 80), 140, 60), "Exit")) {
						Application.LoadLevel ("Intro");
					}
					
				} else {
					GUI.skin.label.fontSize = 50;
					GUI.Label(new Rect (Screen.width / 2 - 230, Screen.height - 210, 180, 100), finalScore.ToString());
					
					GUI.skin.label.fontSize = 20;
					GUI.Label(new Rect (Screen.width / 2 - 110, Screen.height - 198, 180, 100), "+dmg");
					GUI.Label(new Rect (Screen.width / 2 - 110, Screen.height - 178, 180, 100), dmgDone.ToString());
					
					if (GUI.Button (new Rect ((Screen.width - 400) / 2 + 133, (Screen.height - 80), 140, 60), "Continue")) {
						Application.LoadLevel (nextLevel);
					}
					GUI.Label(new Rect(Screen.width / 2 + 10, Screen.height - 200, 200, 150), message);
				}
				
				GUI.skin.label.fontSize = 25;
				
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, 210, 250, Screen.height - 210));
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
				GUILayout.Label("RESPAWNED DEF");
				GUILayout.Label("LIL'SHIP DEATH");
				GUILayout.Label("SHIP HEALTH");
				GUILayout.Label("PICK-UPS MISSED");
				GUILayout.Label("INVASION QUALITY");
				GUILayout.Label("DEFENCES LEFT");
				
				GUILayout.EndArea();
				
				
				GUILayout.BeginArea(new Rect(Screen.width / 2 + 47  , 210, 100, Screen.height - 210));
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				
				GUILayout.Label(respawned.ToString());
				GUILayout.Label(lilShipDeaths.ToString());
				GUILayout.Label((shipHealth/origShipHealth * 100).ToString() + "%");
				GUILayout.Label(pickUpsMissed.ToString());
				GUILayout.Label(((int)invasionAvgPrc).ToString() + "%");
				GUILayout.Label(defencesLeft.ToString());
				
				GUILayout.EndArea();
				
				
				GUILayout.BeginArea(new Rect(Screen.width / 2 + 180  , 210, 100, Screen.height - 210));
				GUI.skin.label.alignment = TextAnchor.UpperCenter;
				
				GUILayout.Label(bonus1.ToString());
				GUILayout.Label(bonus2.ToString());
				GUILayout.Label(bonus3.ToString());
				GUILayout.Label(bonus4.ToString());
				GUILayout.Label(bonus5.ToString());
				GUILayout.Label(bonus6.ToString());
				
				GUILayout.EndArea();

				

			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!isCountin && isEnd){
			StartCoroutine("countBonuses");
		}
	}
}
