using UnityEngine;
using System.Collections;

public class LaunchMissile : MonoBehaviour
{
    PublicData data;
    float delay;
    public GameObject missile;
    bool isLaunching;
    float damage;

    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        delay = data.missileDelay;
        damage = data.missileDamage;

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        isLaunching = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (transform.Find("body").GetComponent<SpriteRenderer>().enabled)
            {
                if (isLaunching)
                {
                    GameObject newMissile;
                    newMissile = Instantiate(missile, transform.position, transform.rotation) as GameObject;
                    newMissile.GetComponent<CommonBulletStuff>().setDamage(damage);
                    newMissile.GetComponent<CommonBulletStuff>().setIsMissile(true);
                    isLaunching = false;
                    StartCoroutine(Wait());
                }
            }

        }
    }
}
