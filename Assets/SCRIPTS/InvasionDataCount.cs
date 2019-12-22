using UnityEngine;
using System.Collections;

public class InvasionDataCount : MonoBehaviour
{
    public float invaders = 0;
    public float angle = 0;
    float multiplier;
    PublicData data;



    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        multiplier = data.invasionMultiplier;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "planet")
        {
            //	print ("im in! " + invaders);


            int area = (int)(invaders * multiplier);

            int beg = (int)angle - area / 2;
            int end = (int)angle + area / 2;

            SendMessage("DisplayOverlay", area + " degrees covered");
            if (invaders > 0)
            {
                data.PerformInvasion(beg, end, area);
            }

        }
    }




    public void setInvaders(float setter)
    {
        invaders = setter;
    }

    public void setAngle(float setter)
    {
        angle = setter;
    }
}
