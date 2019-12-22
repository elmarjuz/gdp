using UnityEngine;
using System.Collections;

public class RocketLaunch : MonoBehaviour
{

    // Use this for initialization

    public GameObject rocket;
    public float damage = 50;
    bool wasDisabled;

    void Start()
    {
        InvokeRepeating("LaunchRocket", Random.Range(1.0f, 10.0f), Random.Range(10, 15));

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.Find("body").GetComponent<SpriteRenderer>().enabled)
        {
            CancelInvoke();
            wasDisabled = true;
        }
        else
        {
            if (wasDisabled)
            {
                InvokeRepeating("LaunchRocket", Random.Range(1.0f, 10.0f), Random.Range(10, 15));
                wasDisabled = false;
            }
        }
    }

    public void LaunchRocket()
    {
        GameObject theRocket;

        theRocket = Instantiate(rocket, transform.localPosition, transform.rotation) as GameObject;
        theRocket.name = "theRocket";
        theRocket.transform.parent = transform;
        theRocket.GetComponent<CommonBulletStuff>().setDamage(damage);


    }
}
