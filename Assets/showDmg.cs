using UnityEngine;
using System.Collections;

public class showDmg : MonoBehaviour {

	public float dmg = 0;
	public bool isGood;

	Color color;
	string dmgMsg = "";

	float x;
	float y;

	public GUISkin skin;
	public Texture some;

	float font;
	public float angle;
	float radius;
	float realAngle;

	float oX;
	float oY;

	GameObject camera;

	PublicData data;
	BonusCounter bonus;
	// Use this for initialization
	void Start () {
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		bonus = GameObject.Find("DataHolder").GetComponent<BonusCounter>();
		camera = GameObject.Find("Main Camera");

		Vector3 screenPos = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
		oX = screenPos.x;
		oY = screenPos.y;

		x = Random.Range(30, 60);
		y = Random.Range(20, 30);

		radius = Vector3.Distance(Camera.main.WorldToScreenPoint(transform.position), screenPos);

		angle = Mathf.Atan2 (-transform.position.y, -transform.position.x) * 180 / Mathf.PI;
		}

	public void setMsg(){
		if(isGood){
			color =  new Color (0.9f, 0.7f, 0.7f, 1);
			dmgMsg = dmg.ToString();
			PlayerPrefs.SetFloat("dmgDone", PlayerPrefs.GetFloat("dmgDone") + dmg);
		} else {
			color =  new Color (1,0.2f,0.2f,1);
			dmgMsg = "-" + dmg.ToString();
		}

		StartCoroutine("zoomIn");
	}

	void OnGUI(){
		GUI.skin = skin;
		GUI.color = color;
		GUI.skin.label.fontSize = (int)font;
		Vector3 newPosition = transform.position;

		newPosition.x = oX - radius * Mathf.Cos (Mathf.PI * realAngle / 180);
		newPosition.y = oY - radius * Mathf.Sin (Mathf.PI * realAngle / 180);
		GUI.Label(new Rect(newPosition.x-x, newPosition.y-y, 100, 100), dmgMsg);
	}


	IEnumerator fadeOut(){
		for(float i=1; i>0; i-=0.03f){
			color.a = i;
			yield return new WaitForSeconds(0.05f);
		}
		Destroy(gameObject);
		
	}
	
	IEnumerator zoomIn(){
		for(float i=0; i<1; i+=0.03f){
			font = 15 * i;
			yield return new WaitForSeconds(0.001f);
		}
		StartCoroutine("fadeOut");
	}



	// Update is called once per frame
	void Update () {
		realAngle =   (camera.transform.eulerAngles.z - angle);
		if(realAngle < 0) realAngle = 360 - realAngle;
		else if(realAngle > 360) realAngle-=360;

	}
}



