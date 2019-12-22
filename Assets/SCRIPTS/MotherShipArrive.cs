using UnityEngine;
using System.Collections;

public class MotherShipArrive : MonoBehaviour
{


    public GameObject orbitalCenter;
    PublicData data;
    GameObject leftArm;
    GameObject rightArm;
    Vector3 orbitalPosition;
    public float radius;
    public float angle;
    public float nX;
    public float nY;
    public float ocX;
    public float ocY;
    public float bottomLimit;
    public float topLimit;

    public float beg;
    public float end;

    // Use this for initialization
    void Start()
    {


        data = GameObject.Find("DataHolder").GetComponent<PublicData>();

        angle = data.motherShipAngle;

        radius = data.motherShipRadius;
        topLimit = data.topLimit;
        bottomLimit = data.bottomLimit;

        orbitalPosition = orbitalCenter.transform.position;
        ocX = orbitalPosition.x;
        ocY = orbitalPosition.y;

        beg = radius;
        end = topLimit;

    }

    public void setBegEnd(float val1, float val2)
    {
        beg = val1;
        end = val2;
    }

    // Update is called once per frame
    void Update()
    {

        data.setMotherShipRadius(radius);
        if (radius > topLimit)
        {
            arrive();
        }
        if (end >= radius)
        {

            GetComponent<MotherShipRotation>().enabled = true;
            GetComponent<Invasion>().enabled = true;
            data.setMotherShipRadius(radius);
            data.setMotherShipAngle(angle);
            GetComponent<MotherShipArrive>().enabled = false;
        }

    }

    public void arrive()
    {
        if (end > beg) radius += 0.01f;
        if (end < beg) radius -= 0.01f;
        Vector3 newPosition = transform.position;
        nX = ocX + radius * Mathf.Cos(Mathf.PI * angle / 180);
        nY = ocY + radius * Mathf.Sin(Mathf.PI * angle / 180);
        newPosition.x = nX;
        newPosition.y = nY;
        transform.position = newPosition;

        transform.eulerAngles = new Vector3(0, 0, angle - 90);
    }

}
