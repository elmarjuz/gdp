using UnityEngine;
using System.Collections;

public class MissileAI : MonoBehaviour {

	GameObject mothership;
	PublicData data;
	float angle;
	float speed;


	// Use this for initialization
	void Start () {
		mothership = GameObject.Find("MOTHERSHIP");
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		speed = data.missileSpeed;
	}


	// Update is called once per frame
	void Update () {
		transform.GetComponent<Rigidbody2D>().velocity = (new Vector3(mothership.transform.position.x, mothership.transform.position.y, 0) - transform.position).normalized * speed * Time.deltaTime*100;
		angle = Mathf.Atan2 (mothership.transform.position.y - transform.position.y, mothership.transform.position.x - transform.position.x) * 180 / Mathf.PI;
		transform.eulerAngles = new Vector3 (0, 0, angle-90);
	}
}
