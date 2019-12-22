using UnityEngine;
using System.Collections;

public class SnowManStuff : MonoBehaviour
{
    GameObject planet;
    PublicData data;
    float speed;
    float ocX;
    float ocY;
    float radius;
    public float angle = 0;

    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        planet = GameObject.Find("SNOW PLANET");
        radius = planet.GetComponent<CircleCollider2D>().radius * planet.transform.localScale.x + 0.5f;
        speed = data.snowmanSpeed;

        ocY = planet.transform.position.y;
        ocX = planet.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (angle <= 0) angle = 359;
        Vector3 newPosition = transform.position;
        newPosition.x = ocX + radius * Mathf.Cos(Mathf.PI * angle / 180);
        newPosition.y = ocY + radius * Mathf.Sin(Mathf.PI * angle / 180);
        transform.position = newPosition;
        transform.eulerAngles = new Vector3(0, 0, angle + 270);
        data.invaded[(int)angle] = false;
        if (GameObject.Find("OUI").transform.Find(((int)angle).ToString()) != null)
            Destroy(GameObject.Find("OUI").transform.Find(((int)angle).ToString()).gameObject);
        angle -= speed * Time.deltaTime;
    }

    public void setAngle(float value)
    {
        angle = value;
    }
}
