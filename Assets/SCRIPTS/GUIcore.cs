using UnityEngine;
using System.Collections;

public class GUIcore : MonoBehaviour
{

	public GameObject target;
	GameObject lilShip;
	PublicData data;
	GameObject currTarget;
	Vector3 targetPosition;
	bool isDocked;
	GameObject mothership;
	// Use this for initialization

	Transform locationBase;
	float scaleMod;
	Vector3 localPos;


	void Start ()
	{
		locationBase = transform.parent.parent.Find("core");
		scaleMod = transform.parent.localScale.x;
		localPos = new Vector3(0,0,-2);


		GetComponent<SpriteRenderer> ().enabled = false;
		lilShip = GameObject.Find ("LILSHIP");
		mothership = GameObject.Find ("MOTHERSHIP");
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		isDocked = data.isDocked;
	}

	void OnMouseDown ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	void OnMouseOver(){
		GetComponent<SpriteRenderer>().enabled = true;
	}
	void OnMouseExit(){
		GetComponent<SpriteRenderer>().enabled = false;
	}
	
	void OnMouseUp ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		lilShip.GetComponent<LilShipControls> ().Parking ();
		if (data.isDocked) lilShip.transform.position = mothership.transform.position;
	}

	void Update ()
	{

		transform.position=locationBase.position+localPos;
		transform.rotation=locationBase.rotation;
		/*
		if(!lilShip.GetComponent<LilShipControls> ().enabled && data.isDocked){
			lilShip.transform.position = transform.parent.position;
		}*/
		
	}
	/*
	void OnMouseDown() {
		if(GetComponent<coreGUI>().enabled){
			print ("OH HOLY SHIT");
			GetComponent<SpriteRenderer>().enabled = true;


		}
		
	}
	
	void OnMouseUp(){
		if(GetComponent<coreGUI>().enabled){
			GetComponent<SpriteRenderer>().enabled = false;
			if(Vector3.Distance (transform.position, lilShip.transform.position) < 1){
				lilShip.GetComponent<LilShipControls>().Parking();

			}
		}
	}
	*/
	// Update is called once per frame

}
