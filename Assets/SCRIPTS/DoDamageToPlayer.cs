using UnityEngine;
using System.Collections;

public class DoDamageToPlayer : MonoBehaviour {

	public float damage;
	float health;

	void Start () {

	}
	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.name == "LILSHIP"){
			health = collision.gameObject.GetComponent<LilShipControls>().health -= damage;
			collision.gameObject.GetComponent<LilShipControls>().setHealth(health);
			print (health);
		} else if(collision.gameObject.name == "MOTHERSHIP"){
			health = collision.gameObject.GetComponent<MotherShipHealth>().health -= damage;
			collision.gameObject.GetComponent<MotherShipHealth>().setHealth(health);
		}
		
	}


	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.name == "LILSHIP"){
			health = other.gameObject.GetComponent<LilShipControls>().health -= damage;
			other.gameObject.GetComponent<LilShipControls>().setHealth(health);
			print (health);
		} else if(other.gameObject.name == "MOTHERSHIP"){
			health = other.gameObject.GetComponent<MotherShipHealth>().health -= damage;
			other.gameObject.GetComponent<MotherShipHealth>().setHealth(health);
			//print (damage);
		}
	}
	public void setDamage(float setter){
		damage = setter;
	}
}
