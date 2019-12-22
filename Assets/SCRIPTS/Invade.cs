using UnityEngine;
using System.Collections;

public class Invade : MonoBehaviour {
	bool isHit;
	public GameObject poof;
	public AudioClip[] sounds = new AudioClip[8];
	 
	// Use this for initialization
	void Start () {
		Instantiate (poof, transform.position, transform.rotation);

			
	}

	void OnCollisionEnter2D(Collision2D collision){
		GetComponent<AudioSource>().clip = sounds[(int)Random.Range(0,7)];
		GetComponent<AudioSource>().Play();
		if(collision.collider.gameObject.tag != "planet"){

			if(!isHit){
				float invaders = transform.parent.GetComponent<InvasionDataCount>().invaders-1;
				transform.parent.GetComponent<InvasionDataCount>().setInvaders(invaders);
				StartCoroutine(Die());
				isHit=true;
			}

		} else {
			isHit=true;
			GameObject aChild = transform.Find("invaderSprite").gameObject;
			aChild.GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<BoxCollider2D>().enabled = false;
			StartCoroutine(Die());
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag=="EnemyWeapon" && !isHit){
			GetComponent<AudioSource>().clip = sounds[(int)Random.Range(0,7)];
			GetComponent<AudioSource>().Play();
			isHit=true;
			StartCoroutine(Die());	
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Die() {
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}


}
