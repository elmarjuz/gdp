using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

    PublicData data;
    float health;
    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        health = data.enemyShipHealth;
    }

    public void setHealth(float setter)
    {
        health = setter;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
