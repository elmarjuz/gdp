using UnityEngine;
using System.Collections;

public class FirinMaLazers : MonoBehaviour
{
	public float delay;

	// Use this for initialization
	void Start ()
	{

		if (delay > 0) {
			StartCoroutine (WaitAndFire ());
		} else {
			FireAllDaLazerz ();
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	IEnumerator WaitAndFire ()
	{
		yield return new WaitForSeconds (delay);
		FireAllDaLazerz ();
	}

	void FireAllDaLazerz ()
	{
	

		bool temp;
		/*GameObject[] allDaOldLazorz = GameObject.FindGameObjectsWithTag ("EnemyWeapon");
		foreach (GameObject lazor in allDaOldLazorz) {
			Destroy(lazor);
		}*/

		GameObject[] allDaLazorz = GameObject.FindGameObjectsWithTag ("EnemyLaser");
		foreach (GameObject lazor in allDaLazorz) {
			temp = lazor.GetComponent<LaserShoot> ().isBurst;
			lazor.GetComponent<LaserShoot> ().isBurst = true;
			lazor.GetComponent<LaserShoot> ().FireLaser();
			lazor.GetComponent<LaserShoot> ().isBurst = temp;
		}
	}
}