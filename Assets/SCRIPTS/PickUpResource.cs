using UnityEngine;
using System.Collections;

public class PickUpResource : MonoBehaviour
{
    PublicData data;
    GameObject mothership;
    float modifier;
    float top;
    float bot;

    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        mothership = GameObject.Find("MOTHERSHIP");

        top = data.topResource;
        bot = data.botResource;
    }

    void OnCollisionEnter2D()
    {
        float adder = Random.Range(bot, top);

        mothership.GetComponent<Invasion>().addResource();
        mothership.SendMessage("DisplayOverlay", "Resources restored!");
        //SendMessage ("DisplayOverlay", adder + " invasion resource has been regenerated..");
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
