using UnityEngine;
using System.Collections;

public class NewStuff : MonoBehaviour
{
    GameObject sprite;
    PublicData data;
    public float minRange = 1;
    public float maxRange = 1;
    float ocX;
    float ocY;
    float radius;
    float heightMultiplier;
    GameObject[] things;
    int thingsNumber;
    float delay;
    float angle;
    int iThings;
    bool isSpawning;
    public int[] count;
    float[,] angles;
    public GameObject[] objects;
    public GameObject weaponThing;


    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        heightMultiplier = data.heightMultiplier;
        radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.5f;
        angles = data.spawnAngles;

        GameObject weapons = Instantiate(weaponThing, transform.position, transform.rotation) as GameObject;
        weapons.transform.parent = transform;
        weapons.name = "WEAPONS";
        weapons.transform.localScale = new Vector3(1, 1, 1);

        SpawnAll(objects, angles, count);
    }

    public void clearLevel()
    {
        foreach (Transform child in transform.Find("WEAPONS"))
        {

            Destroy(child.gameObject);

        }
    }

    public void SpawnOne(GameObject thing, float[] angles)
    {
        radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.5f;
        for (int i = 0; i < angles.Length; i++)
        {
            angle = angles[i];
            Vector3 newPosition = transform.position;
            newPosition.x = ocX + radius * Mathf.Cos(Mathf.PI * angle / 180);
            newPosition.y = ocY + radius * Mathf.Sin(Mathf.PI * angle / 180);
            GameObject newThing;
            newThing = Instantiate(thing, newPosition, transform.rotation) as GameObject;

            newThing.GetComponent<TakeDamageAndGetDestroyed>().setAngle(angle);
            //if(transform.FindChild("WEAPONS") != null) 
            newThing.transform.parent = transform.Find("WEAPONS").transform;
            //			print (newThing.transform.parent.name);
            /*if(minRange!=1 || maxRange!=1){
				float sizeMultiplier = Random.Range(minRange,maxRange);
				newThing.transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, sizeMultiplier);
			}*/
            /*if(Random.Range(0,3) > 1) {
				print("yes");
				Vector3 scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
				scale.y = 0-(Mathf.Abs(transform.localScale.y));
				transform.localScale = scale;
				newThing.transform.eulerAngles = new Vector3(0, 0, angle);
			} else {
				newThing.transform.eulerAngles = new Vector3(0, 0, angle-90);
			}*/
            newThing.transform.eulerAngles = new Vector3(0, 0, angle - 90);
            newThing.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SpawnAll(GameObject[] things, float[,] angles, int[] count)
    {
        ocX = transform.position.x;
        ocY = transform.position.y;
        for (int j = 0; j < things.Length; j++)
        {
            float num;
            if (count[j] <= angles.GetLength(1))
                num = count[j];
            else
                num = angles.GetLength(1);
            float[] currentAngles = new float[count[j]];
            for (int i = 0; i < num; i++)
            {
                currentAngles[i] = angles[j, i];
            }
            GameObject thing = things[j];
            SpawnOne(thing, currentAngles);

        }
    }


}
