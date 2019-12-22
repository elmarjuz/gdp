using UnityEngine;
using System.Collections;

public class PickUpInvasion : MonoBehaviour
{
    PublicData data;
    GameObject motherShip;
    float modifier;
    float top;
    float bot;

    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        motherShip = GameObject.Find("MOTHERSHIP");
        top = data.topInvasion;
        bot = data.botInvasion;
    }

    void OnCollisionEnter2D()
    {
        float adder = Random.Range(bot, top);

        data.gameObject.GetComponent<PublicData>().addInvasion(adder);
        motherShip.SendMessage("DisplayOverlay", "+ " + System.Math.Round(adder, 2) + "% invasion success!");

        //SendMessage ("DisplayOverlay", "+" + System.Math.Round(adder,2) + "% invasion success!");
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
