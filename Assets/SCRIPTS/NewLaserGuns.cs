using UnityEngine;
using System.Collections;

public class NewLaserGuns : MonoBehaviour
{
    public GameObject laserGun;
    GameObject sprite;
    PublicData data;

    public float minRange = 1;
    public float maxRange = 1;
    float ocX;
    float ocY;
    float radius;
    float heightMultiplier;
    float[] angles;
    GameObject[] laserGuns;
    int laserGunNumber;
    float delay;
    float angle;
    int iLaserGun;
    bool isSpawning;


    void spawn(float angle)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = ocX + radius * Mathf.Cos(Mathf.PI * angle / 180);
        newPosition.y = ocY + radius * Mathf.Sin(Mathf.PI * angle / 180);
        GameObject newLaserGun;
        newLaserGun = Instantiate(laserGun, newPosition, transform.rotation) as GameObject;
        newLaserGun.transform.eulerAngles = new Vector3(0, 0, angle - 90);
        newLaserGun.GetComponent<TakeDamageAndGetDestroyed>().setAngle(angle);
        if (minRange != 1 || maxRange != 1)
        {
            float sizeMultiplier = Random.Range(minRange, maxRange);
            newLaserGun.transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, sizeMultiplier);
        }
    }


    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        angles = data.laserGuns;
        delay = data.enemyRespawnDelay;
        laserGunNumber = angles.Length;
        isSpawning = false;

        heightMultiplier = data.heightMultiplier;
        radius = heightMultiplier;

        ocX = transform.position.x;
        ocY = transform.position.y;
        laserGunNumber = angles.Length;
        for (int i = 0; i < laserGunNumber; i++)
        {
            spawn(angles[i]);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        isSpawning = false;
        if (!data.invaded[(int)angles[iLaserGun]])
        {
            spawn(angles[iLaserGun]);
        }
        else
        {
            laserGunNumber -= 1;
            angles[iLaserGun] = angles[angles.Length - 2];
        }
    }

    // Update is called once per frame
    void Update()
    {
        laserGuns = GameObject.FindGameObjectsWithTag("EnemyLaser");
        if (laserGuns.Length < laserGunNumber && !isSpawning)
        {
            bool isThere = false;
            float thisAngle;
            for (int i = 0; i < laserGunNumber; i++)
            {
                isThere = false;
                for (int j = 0; j < laserGuns.Length; j++)
                {
                    thisAngle = laserGuns[j].GetComponent<TakeDamageAndGetDestroyed>().angle;
                    if (thisAngle == angles[i])
                    {
                        isThere = true;
                        break;
                    }
                }
                if (!isThere)
                {
                    iLaserGun = i;
                    isSpawning = true;
                    StartCoroutine(Wait());
                    break;
                }
            }
        }
    }
}
