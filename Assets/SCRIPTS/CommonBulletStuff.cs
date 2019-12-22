using UnityEngine;
using System.Collections;

public class CommonBulletStuff : MonoBehaviour
{

    public float damage = 10;
    public float lifespan = 0.5f;
    public GameObject poofOut;
    public bool isEvent;
    public bool hitShield;
    public bool isRocket;
    public bool isMissile;

    PublicData data;


    // Use this for initialization
    void Start()
    {
        hitShield = false;
        if (!gameObject.GetComponent<Collider2D>().isTrigger && !isRocket && !isMissile)
        {
            StartCoroutine(Die());
        }
        else if (isRocket)
        {
            StartCoroutine(KillRocket());
        }

    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(lifespan);
        poofItOut();
        Destroy(gameObject);
    }

    IEnumerator KillRocket()
    {
        yield return new WaitForSeconds(lifespan);
        GetComponent<RocketBehaviour>().ChangeHeading(-1);
    }

    /*void OnTriggerEnter2D(){
		print("ooooh");
	}*/


    void OnCollisionEnter2D()
    {
        //if(!isBeam){
        poofItOut();
        Destroy(gameObject);
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (!hitShield && ((other.tag == "EnemyShield" && gameObject.tag == "PlayerWeapon") || (other.name == "theShield" && gameObject.tag == "EnemyWeapon")))
        {
            damage = damage / 2;
            hitShield = true;
            print(other.name + " made my damage " + damage);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (hitShield && other.tag == "EnemyShield" && gameObject.tag == "PlayerWeapon")
        {
            damage = damage * 2;
            hitShield = false;
            print(other.name + " made my damage " + damage);
        }

    }

    /*void OnBecameInvisible() {
		Destroy(transform.parent.gameObject);
	}*/

    public void setDamage(float value)
    {
        damage = value;
    }

    public void SetHitShield(bool value)
    {
        hitShield = value;
    }

    public void poofItOut()
    {
        Instantiate(poofOut, transform.position, poofOut.transform.rotation);
    }

    public void setIsMissile(bool value)
    {
        isMissile = value;
    }

}
