using UnityEngine;
using System.Collections;

public class LaserShoot : MonoBehaviour
{

    GameObject motherShip;
    PublicData data;
    GameObject LilShip;
    GameObject followed;
    public GameObject orbitalCentre;
    public bool isBurst;
    public ParticleSystem charge;
    public GameObject laser;
    //public AudioClip fireSound;
    public float nX;
    public float nY;
    public float ocX;
    public float ocY;
    float delay;
    float radius;
    float damage;
    public bool isFollowing;
    public float angle;
    public float distance;
    bool isShooting;

    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        motherShip = GameObject.Find("MOTHERSHIP");
        LilShip = GameObject.Find("LILSHIP");

        damage = data.laserGunDamage;
        delay = data.laserGunShootDelay;
        radius = data.laserGunRadius;

        isFollowing = false;
        isShooting = false;
    }

    void getAngle(Transform target)
    {
        Vector3 targetDir = target.position - orbitalCentre.transform.position;
        Vector3 forward = orbitalCentre.transform.position;
        angle = Vector3.Angle(targetDir, forward);

    }

    IEnumerator Wait()
    {
        isShooting = true;
        charge.Play();
        yield return new WaitForSeconds(delay);
        if (transform.Find("theShot") == null)
        {
            FireLaser();
        }

    }



    public void FireLaser()
    {
        GameObject shotLaser;
        Quaternion rot = orbitalCentre.transform.rotation;
        if (isBurst)
        {
            rot = transform.rotation;
        }
        shotLaser = Instantiate(laser, transform.position, rot) as GameObject;
        shotLaser.name = "theShot";
        shotLaser.transform.parent = transform;
        shotLaser.GetComponent<CommonBulletStuff>().setDamage(damage);
        if (!isBurst)
        {
            shotLaser.transform.eulerAngles = new Vector3(0, 0, angle);
        }

        isShooting = false;
        charge.Stop();


    }


    public void abort()
    {
        isShooting = false;
        charge.Stop();
        StopCoroutine("Wait");
    }


    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (transform.Find("body").GetComponent<SpriteRenderer>().enabled)
            {
                if (transform.Find("theShot") == null)
                {
                    distance = Vector3.Distance(transform.position, motherShip.transform.position);
                    if (distance <= radius)
                    {
                        angle = Mathf.Atan2(motherShip.transform.position.y - transform.position.y, motherShip.transform.position.x - transform.position.x) * 180 / Mathf.PI;
                        if (!isFollowing && !isShooting)
                        {
                            isFollowing = true;
                            followed = motherShip;
                        }
                    }
                    else if (Vector3.Distance(transform.position, LilShip.transform.position) <= radius)
                    {
                        angle = Mathf.Atan2(LilShip.transform.position.y - transform.position.y, LilShip.transform.position.x - transform.position.x) * 180 / Mathf.PI;
                        if (!isFollowing && !isShooting)
                        {
                            isFollowing = true;
                            followed = LilShip;
                        }
                    }

                    if (isFollowing)
                    {
                        StartCoroutine("Wait");
                        isFollowing = false;
                        //FireLaser();

                    }
                }
            }

        }
    }
}