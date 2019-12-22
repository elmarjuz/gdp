using UnityEngine;
using System.Collections;

public class ShowGUI : MonoBehaviour {

	PublicData data;

	float alphaFadeValue = 1;

	public Texture2D black;

	public GameObject gui;
	GameObject currentGui;

	Transform res;
	Transform left;
	Transform right;
	Transform core;
	Transform lowL;
	Transform lowR;
	Transform tinyL;
	Transform tinyR;

	float guiRadius;

	Vector3 origPos;
	bool isShaking;
	float quakeAmt;
	float amplitude;

	/*public Texture2D BGtxt;
	public Texture2D FGtxt;
	public Texture2D resTxt;

	public Texture2D leftTxt;
	public Texture2D rightTxt;
	public Texture2D coreTxt;
	public Texture2D defenseTxt;
	public Texture2D offenseTxt;
	public Texture2D indicatorRTxt;
	public Texture2D indicatorLTxt;

	float mod = 300;
	float topBarHeight = 50;
*/
	// Use this for initialization
	void Start () {
		data = GetComponent<PublicData>();
	}
	
	// Update is called once per frame
	void Update () {
		if(data.GUIOverlay && !data.isArriving){

			/*Vector3 guiPos = currentGui.transform.position;
			Vector3 guiScale = new Vector3(guiRadius*guiRadius/(2*guiRadius+70), guiRadius*guiRadius/(2*guiRadius+70), 1);
			if(Camera.main.GetComponent<CameraControls>().cameraRotation){
				guiRadius = Camera.main.camera.orthographicSize-guiRadius*guiRadius/(2*guiRadius+100);
				guiPos.x = guiRadius * Mathf.Cos (Mathf.PI * data.motherShipAngle / 180);
				guiPos.y = guiRadius * Mathf.Sin (Mathf.PI * data.motherShipAngle / 180);
				currentGui.transform.rotation = Camera.main.transform.rotation;
			} else {
				guiRadius = Camera.main.orthographicSize-4;
				//guiPos.x = guiRadius * Mathf.Cos (Mathf.PI * currentGui.transform.position.z / 180);
				//guiPos.y = guiRadius * Mathf.Sin (Mathf.PI * currentGui.transform.position.z / 180);
			}

			if(isShaking){
				quakeAmt = Random.Range(-amplitude/2, amplitude/2);
				guiPos.x += quakeAmt; 
				quakeAmt = Random.Range(-amplitude/2, amplitude/2);
				guiPos.y += quakeAmt; 
			}*/



			Vector3 guiScale = new Vector3(Camera.main.GetComponent<Camera>().orthographicSize/6, Camera.main.GetComponent<Camera>().orthographicSize/6, 1);
			Vector3 guiPos = new Vector3(0,Camera.main.GetComponent<Camera>().orthographicSize/1.24f, 30);
			if(isShaking){
				quakeAmt = Random.Range(-amplitude/2, amplitude/2);
				guiPos.x += quakeAmt; 
				quakeAmt = Random.Range(-amplitude/2, amplitude/2);
				guiPos.y += quakeAmt; 
			}
			currentGui.transform.localPosition = guiPos;
			currentGui.transform.localScale = guiScale;
			res.localScale = new Vector3(1*data.currentInvasionResource,1,1);
			left.localScale = new Vector3(1*data.motherShipLeftHealth,1,1);
			right.localScale = new Vector3(1*data.motherShipRightHealth,1,1);
			core.localScale = new Vector3(1*data.motherShipCoreHealth,1,1);
			lowL.localScale = new Vector3(1*data.shipBurstChargePercent,1,1);
			lowR.localScale = new Vector3(1*data.shipShieldChargePercent,1,1);
			tinyR.gameObject.GetComponent<SpriteRenderer>().enabled = data.isDocked;
			if(!data.wasTeleported)
				tinyL.gameObject.GetComponent<SpriteRenderer>().color = new Color (0,1,0,1);
			else 
				tinyL.gameObject.GetComponent<SpriteRenderer>().color = new Color (1,0,0,1);
		}  
	}

	public void ShakeDat(float delay, float value){
		if(!isShaking){
			isShaking = true;
			amplitude = value;
			StartCoroutine(WaitToStopShaking(delay));
		}
	}
	
	IEnumerator WaitToStopShaking(float delay){
		yield return new WaitForSeconds(delay);
		isShaking = false;
	}

	public void ToggleBar(bool value){
		if(!value && currentGui!=null){
			Destroy (currentGui);
		} else {
			Vector3 guiPos = new Vector3(0, 40, 30);
			currentGui = Instantiate(gui, guiPos, transform.rotation) as GameObject;
			currentGui.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
			res=currentGui.transform.Find("UIres/indicator");
			left=currentGui.transform.Find("UIcenter/UIleftArm");
			right=currentGui.transform.Find("UIcenter/UIrightArm");
//			left=currentGui.transform.FindChild("UIL/UIleftArm");
//			right=currentGui.transform.FindChild("UIR/UIrightArm");
			core=currentGui.transform.Find("UIcenter/UIcore");
			lowL=currentGui.transform.Find("UIlowL/indicator");
			lowR=currentGui.transform.Find("UIlowR/indicator");
			tinyL=currentGui.transform.Find("UItiny/UItinyL");
			tinyR=currentGui.transform.Find("UItiny/UItinyR");
		}
	}

	void OnGUI(){
		if (data.isStarting) {
			alphaFadeValue -= Mathf.Clamp01 (Time.deltaTime / 5);
			GUI.color = new Color (0, 0, 0, alphaFadeValue);
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), black);
			if (alphaFadeValue < 0.001) {
				alphaFadeValue = 0;
				data.isStarting = false;
			}
		}
		
		/*
			GUI.BeginGroup(new Rect (Screen.width/2-mod/2, mod/24, mod, mod/4.8f));
			GUI.depth = 0;
			GUI.DrawTexture (new Rect (mod/60, mod/80, mod/1.03f, mod/5.58f), BGtxt);

			GUI.depth = 10;
			GUI.DrawTexture (new Rect (mod/8, mod/80, (mod/1.33f)*data.currentInvasionResource, mod/30), resTxt);
			GUI.DrawTexture (new Rect (mod/80, mod/24, (mod/3)*data.motherShipLeftHealth, mod/16), leftTxt);
			GUI.DrawTexture (new Rect (mod/1.52f, mod/24, (mod/3)*data.motherShipRightHealth, mod/16), rightTxt);
			GUI.DrawTexture (new Rect (mod/3.2f, mod/20, (mod/2.6f)*data.motherShipCoreHealth, mod/10.43f), coreTxt);
			GUI.DrawTexture (new Rect (mod/120, mod/8, (mod/2.5f), mod/16), offenseTxt);
			GUI.DrawTexture (new Rect (mod/1.69f, mod/8, (mod/2.5f), mod/16), defenseTxt);
			GUI.DrawTexture (new Rect (mod/2.19f, mod/6, (mod/20), mod/26.6f), indicatorLTxt);
			GUI.DrawTexture (new Rect (mod/2, mod/6, (mod/20), mod/26.6f), indicatorRTxt);

			
			GUI.depth = -100;
			GUI.DrawTexture (new Rect (0, 0, mod, mod/4.8f), FGtxt);
			GUI.EndGroup();*/

		if (data.debugOverlay) {
			float invDataW = Screen.width / 5;
			if (invDataW < 150)
				invDataW = 150;
			if (invDataW > 200)
				invDataW = 200;
			float invDataH = Screen.height / 5;
			if (invDataH < 500)
				invDataH = 500;
			GUI.BeginGroup (new Rect (10, 10, invDataW, invDataH));
			GUI.Box (new Rect (0, 0, invDataW, invDataH), "INFO HOLDER:");
			GUI.Label (new Rect (10, 20, invDataW - 20, invDataH - 10), 
			           "Invasion: " + System.Math.Round(data.invasionPercent,2) + "%\n" +
			           "Prepared: " + System.Math.Round(data.currentInvasionResource*100,2) + "%\n" +
			           "Chance: " + System.Math.Round(data.invasionTroopPrc,2) + "\n" +
			           "----------------\nCONTROLS:\n" +
			           "ESC - Menu\n" +
			           "WASD/arrow keys - Mothership movement\n" +
			           "Spacebar - Invasion\n" +
			           "LMB - Little ship movement\n" +
			           "RMB - Little ship fire\n" +
			           "Q - Little ship recall\n" +
			           "O - Toggle overlay\n" +
			           "P - Toggle this menu\n" +
			           "M - Toggle music\n" +
			           "C - Toggle rotation\n" +
			           "Hover over Mothership limbs for weapons and lilship docking-undocking." +
			           "Use the lilship as a turret while docked."); 
			if(GUI.Button (new Rect(10,invDataH-40,invDataW - 20,15), "Win Level"))data.EndLevel(true);
			if(GUI.Button (new Rect(10,invDataH-20,invDataW - 20,15), "Lose Level"))data.EndLevel(false);
			GUI.EndGroup ();
		} 


	

	}
}