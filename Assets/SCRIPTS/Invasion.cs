using UnityEngine;
using System.Collections;

public class Invasion : MonoBehaviour
{
    public float invPerc;

    public GameObject invader;
    public GameObject invaderTroop;
    public GameObject planet;
    public GameObject marker;
    public GameObject placeArrow;
    public GameObject successBar;
    public GameObject placeBar;

    public Texture message1;
    public Texture message2;
    public Texture message3;
    public Texture message4;
    public Texture message5;

    Texture message;

    GameObject rateBar;

    bool isTellinResult = false;
    float width;
    float height;
    Rect size;



    //public GameObject resourceBar;

    GameObject invasionFX;
    GameObject invasionMarker;
    PublicData data;

    float origAngle;
    float origRadius;

    float invLimit;
    float botLimit;
    float startTime;
    float deltaTime;
    float speed;
    float angTilt;
    float radTilt;
    float surfaceRadius;
    float resource;
    float initialResource;

    float n;
    float invRows;
    float invCols;

    float currAngle;

    bool invasion;
    bool isDropping = false;
    float resourceMultiplier;

    int inDngr;

    float angle;

    bool isWorking = false;

    float newX;
    float newY;

    Color color;

    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        invLimit = data.invasionLimit;
        botLimit = data.bottomLimit;
        radTilt = data.invasionRadTilt;
        angTilt = data.invasionAngTilt;
        surfaceRadius = data.heightMultiplier;
        resourceMultiplier = data.invasionResourceMultiplier;

        invasionFX = transform.Find("inwosion").gameObject;

        invasion = false;

        color = GUI.color;
        color.a = 1;
        GUI.color = color;

        invPerc = 0;
        /*angTilt = 8;
		radTilt = invLimit-botLimit;*/

        speed = 0.2f;

        invRows = data.invasionRow / 2;
        invCols = data.invasionColumn;

        resource = data.invasionResource;
        data.SendMessage("setCurrentInvasionResource", resource);
        initialResource = resource;

        //resourceBar.SendMessage("DisplayValue", resource/initialResource);


    }

    public void renewLimits()
    {
        invLimit = data.invasionLimit;
        botLimit = data.bottomLimit;
        surfaceRadius = data.heightMultiplier;
    }




    public void addResource()
    {
        //resource += value;
        resource = initialResource;
        data.SendMessage("setCurrentInvasionResource", resource / initialResource);
    }


    IEnumerator fadeTextureOut()
    {
        color = GUI.color;
        for (float i = 1; i > 0; i -= 0.03f)
        {
            color.a = i;
            yield return new WaitForSeconds(0.05f);
        }
        isTellinResult = false;
        color.a = 1;

    }

    IEnumerator zoomTextureIn()
    {
        isTellinResult = true;
        for (float i = 0; i < 1; i += 0.02f)
        {
            width = 270 * i;
            height = 140 * i;
            yield return new WaitForSeconds(0.003f);
        }
        StartCoroutine("fadeTextureOut");
    }

    void OnGUI()
    {
        if (isTellinResult)
        {
            GUI.color = color;
            GUI.Label(new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height), message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        data.SendMessage("setOngoingInvasion", invasion);
        angle = data.motherShipAngle;
        if (Input.GetButtonDown("Jump"))
        {
            startInvasionLoad();
        }
        else if (Input.GetButton("Jump"))
        {
            doInvasionLoad();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (invasion)
            {
                stopInvasionLoad();
            }
        }

        /*if(isWorking){
			showPosition();
		}*/

        if (resource < initialResource && !invasion)
        {
            resource += 0.02f * Time.deltaTime * resourceMultiplier;
            //resourceBar.SendMessage("DisplayValue", resource/100);
            data.SendMessage("setCurrentInvasionResource", resource / initialResource);

        }
    }


    void startInvasionLoad()
    {


        //rateBar.transform.Rotate(0,0,90);
        origRadius = data.motherShipRadius;
        if (!invasion)
        {
            if (origRadius <= invLimit)
            {

                Vector3 newPos = transform.position;
                rateBar = Instantiate(successBar, newPos, transform.rotation) as GameObject;
                rateBar.transform.localScale = new Vector3(2, 2);
                rateBar.transform.parent = transform;
                rateBar.transform.localPosition = new Vector3(0, -2f, 0);

                isWorking = true;
                placeBar.GetComponent<SpriteRenderer>().enabled = true;
                placeArrow.GetComponent<SpriteRenderer>().enabled = true;

                origAngle = data.motherShipAngle;
                startTime = Time.time;
                invasion = true;
                invPerc = 0;

                Vector3 newPosition = transform.position;
                newPosition.x = surfaceRadius * Mathf.Cos(Mathf.PI * angle / 180);
                newPosition.y = surfaceRadius * Mathf.Sin(Mathf.PI * angle / 180);

                invasionMarker = Instantiate(marker, newPosition, transform.rotation) as GameObject;

                invasionFX.SendMessage("StartParticles");
            }
        }
    }

    void doInvasionLoad()
    {
        if (invasion)
        {
            data.SendMessage("setInvasionTroopPrc", invPerc);

            currAngle = data.motherShipAngle;
            float currRadius = data.motherShipRadius;
            if (!isDropping)
            {
                invPerc = (Time.time - startTime) * 30;
                if (invPerc >= 100)
                {
                    isDropping = true;
                }
            }
            else if (invPerc >= 100 || isDropping)
            {
                invPerc = 200 - (Time.time - startTime) * 30;
                if (invPerc <= 0)
                {
                    isDropping = false;
                    startTime = Time.time;
                }
            }
            rateBar.SendMessage("DisplayValue", invPerc / 100);
            showPosition();
            if (Mathf.Abs(currAngle - origAngle) > angTilt || Mathf.Abs(currRadius - origRadius) > radTilt)
            {
                stopInvasionLoad();
            }
        }
    }

    void showPosition()
    {
        currAngle = data.motherShipAngle;
        if (origAngle > currAngle)
        {
            newX = -((currAngle - origAngle) * 2.024f) / angTilt;
            newY = 0.5f - ((currAngle - origAngle) * (-0.59398f)) / angTilt;
        }
        else
        {
            newX = -((currAngle - origAngle) * 1.759f) / angTilt;
            newY = 0.5f - ((currAngle - origAngle) * (0.5819f)) / angTilt;
        }

        //print(placeArrow.transform.localPosition.y);
        placeArrow.transform.localPosition = new Vector3(newX, newY, 0);
    }


    float random()
    {
        return Random.value;
    }

    void stopInvasionLoad()
    {
        Destroy(rateBar);
        invPerc -= Mathf.Abs(data.motherShipAngle - origAngle);
        invPerc -= Mathf.Abs(data.motherShipRadius - origRadius);
        if (PlayerPrefs.GetFloat("invasionAvgPrc") != 0) PlayerPrefs.SetFloat("invasionAvgPrc", (PlayerPrefs.GetFloat("invasionAvgPrc") + invPerc) / 2);
        else PlayerPrefs.SetFloat("invasionAvgPrc", invPerc);
        if (invPerc >= 80) message = message1;
        else if (invPerc >= 60) message = message2;
        else if (invPerc >= 40) message = message3;
        else if (invPerc >= 20) message = message4;
        else if (invPerc >= 0) message = message5;
        StopAllCoroutines();
        StartCoroutine("zoomTextureIn");

        invPerc *= resource / initialResource;

        inDngr = Mathf.FloorToInt((100 - invPerc) * (invRows * invCols) / 100);
        drawTroops();




        if (invPerc < 50) invPerc = 50;
        resource -= resource * invPerc / 100;
        invPerc = 0;
        data.SendMessage("setInvasionTroopPrc", invPerc);
        isDropping = false;
        data.SendMessage("setCurrentInvasionResource", resource);
        Destroy(invasionMarker);
        invasionFX.SendMessage("StopParticles");
        invasion = false;

        isWorking = false;
        placeBar.GetComponent<SpriteRenderer>().enabled = false;
        placeArrow.GetComponent<SpriteRenderer>().enabled = false;


    }

    void drawTroops()
    {

        GameObject troop;
        troop = Instantiate(invaderTroop, transform.position, invaderTroop.transform.rotation) as GameObject;
        n = 0;
        int m = 0;
        for (float j = -invRows; j < invRows; j += 1)
        {
            for (float i = -invCols; i <= invCols; i += 1)
            {
                m++;
                if (random() < invPerc / 100)
                {
                    Vector3 position = troop.transform.position;
                    position.y += -1 + i * 0.3f;
                    position.x += j * 0.38f;
                    GameObject clone;
                    clone = Instantiate(invader, position, troop.transform.rotation) as GameObject;
                    clone.transform.parent = troop.transform;
                    clone.transform.eulerAngles = new Vector3(0, 0, 0);
                    clone.transform.GetComponent<Rigidbody2D>().velocity = (planet.transform.position - troop.transform.position) * speed;
                    string num = n.ToString();
                    clone.name = "invader" + num;
                    n++;
                }
            }
        }


        troop.transform.GetComponent<Rigidbody2D>().velocity = (planet.transform.position - troop.transform.position) * speed;
        troop.GetComponent<InvasionDataCount>().setInvaders(n);


        troop.GetComponent<InvasionDataCount>().setAngle(angle);
        troop.transform.eulerAngles = new Vector3(0, 0, angle);
    }

}
