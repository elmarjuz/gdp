using UnityEngine;
using System.Collections;

public class GoAway : MonoBehaviour {

	public float delay = 3;
	public float particleFade = 0;
	public int randomFactor = 0;
	float health;
	
	void Start () {

		if(randomFactor>1){
			delay = Random.Range(1, randomFactor)* delay;
			
		}

	}

	public void setDelay(float value){
		delay = value;
	}

	void Update () {
		StartCoroutine(WaitAndDie());

	}


	IEnumerator WaitAndDie() {
		if(particleFade==0){
			yield return new WaitForSeconds(delay);
			if(gameObject.tag == "pickUps") PlayerPrefs.SetInt("pickUpsMissed", PlayerPrefs.GetInt("pickUpsMissed") + 1);
			Destroy (gameObject);
		} else {
			yield return new WaitForSeconds(delay-particleFade);
			KillNow();
		}
	}

	void DisableParticles(){
		transform.Find("burstBody").GetComponent<ParticleSystem>().enableEmission = false;
	}

	void KillNow(){
		transform.parent = null;
		StartCoroutine(Kill());
	}

	IEnumerator Kill(){
		DisableParticles();
		yield return new WaitForSeconds(particleFade);
		if(gameObject.tag == "pickUps") PlayerPrefs.SetInt("pickUpsMissed", PlayerPrefs.GetInt("pickUpsMissed") + 1);
		Destroy (gameObject);
	}

	public float getDelay(){
		return delay;
	}


}
