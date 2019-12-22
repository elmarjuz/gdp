using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MotherShipRotation : MonoBehaviour
{

    public GameObject orbitalCenter;
    PublicData data;
    GameObject leftArm;
    GameObject rightArm;
    public float radius;
    public float angle;
    public float ocX;
    public float ocY;
    public float bottomLimit;
    public float topLimit;
    public float passiveAngleChange;
    public float speedFactor;
    public bool isInvading;
    public bool isArriving;
    public bool left = true;
    public bool right = true;
    Vector3 orbitalPosition;
    float rotDir;

    public Texture newThing;
    bool isShowin;
    public GUISkin skin;
    string text;

    float scaleMod;

    Transform fxL;
    Transform fxR;


    // Use this for initialization
    void Start()
    {
        scaleMod = transform.localScale.x;
        leftArm = transform.Find("leftarm").gameObject;
        rightArm = transform.Find("rightarm").gameObject;

        //fxL = leftArm.transform.FindChild ("FXL");
        //fxR = rightArm.transform.FindChild ("FXR");


        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        angle = data.motherShipAngle;
        speedFactor = data.motherShipSpeed;
        passiveAngleChange = data.motherShipPassiveSpeed;


        topLimit = data.topLimit;
        bottomLimit = data.bottomLimit;

        radius = data.motherShipRadius;

        orbitalPosition = orbitalCenter.transform.position;
        ocX = orbitalPosition.x;
        ocY = orbitalPosition.y;

        if (radius > topLimit)
            isArriving = true;



    }



    public void setRadius(float value)
    {
        radius = value;
    }

    public void disableMovement(bool isLeft)
    {
        if (isLeft)
        {
            if (fxL == null)
            {
                fxL = leftArm.transform.Find("disabledFX");
                fxL.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0);
                fxL.GetComponent<ParticleSystem>().startSize = 1.5f;
                fxL.GetComponent<ParticleSystem>().startLifetime = 1;
                fxL.GetComponent<ParticleSystem>().maxParticles = 15;
            }

            left = false;
        }
        else
        {
            if (fxR == null)
            {
                fxR = rightArm.transform.Find("disabledFX");
                fxR.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0);
                fxR.GetComponent<ParticleSystem>().startSize = 1.5f;
                fxR.GetComponent<ParticleSystem>().startLifetime = 1;
                fxR.GetComponent<ParticleSystem>().maxParticles = 15;
            }

            right = false;


        }
    }

    void OnGUI()
    {
        if (isShowin)
        {
            if (skin != null)
            {
                GUI.skin = skin;
            }

            switch (SceneManager.GetActiveScene().name)
            {
                case "TechnoLevel":
                    text = "left";
                    break;
                case "NatureLevel":
                    text = "right";
                    break;
            }
            GUI.Label(new Rect((Screen.width - 300) / 2, (Screen.height - 400) / 2, 300, 400), newThing);
            GUI.Label(new Rect((Screen.width - 260) / 2, (Screen.height - 300) / 2, 260, 400),
                      "There's a new weapon available on the mothership! " +
                      "Click-and-drag the " + text + " wing to use it.");
            if (GUI.Button(new Rect((Screen.width - 100) / 2, (Screen.height - 20) / 2, 100, 60), "OK"))
            {
                Time.timeScale = 1;
                isShowin = false;
            }
        }
    }

    public void Arrive()
    {
        data.setMotherShipRadius(radius);
        data.setMotherShipAngle(angle);
        //if(end > beg) radius += 0.01f;
        if (topLimit < radius)
        {
            radius -= 1 * Time.deltaTime * 2;
            Vector3 newPosition = transform.position;
            newPosition.x = ocX + radius * Mathf.Cos(Mathf.PI * angle / 180);
            newPosition.y = ocY + radius * Mathf.Sin(Mathf.PI * angle / 180);
            transform.position = newPosition;
            transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }
        else
        {
            if (!isShowin)
            {
                isArriving = false;
                GetComponent<Invasion>().enabled = true;
                data.isArriving = false;
                if (SceneManager.GetActiveScene().name == "TechnoLevel" || SceneManager.GetActiveScene().name == "NatureLevel")
                {
                    isShowin = true;
                    Time.timeScale = 0;
                }

            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale != 0)
        {
            if (isArriving)
            {
                if (orbitalCenter.name != "SLAVER STATION")
                {
                    Arrive();
                }
                else
                {
                    isArriving = false;
                }
            }
            else
            {



                if (angle > 360)
                {
                    angle = 0;
                }
                else if (angle < 0)
                {
                    angle = 360;
                }
                data.setMotherShipRadius(radius);
                data.setMotherShipAngle(angle);
                isInvading = data.ongoingInvasion;
                float radiusMod = Input.GetAxis("Vertical") / 2 * Time.deltaTime * speedFactor;
                float angleMod = Input.GetAxis("Horizontal") * Time.deltaTime * speedFactor;
                //if (Input.GetAxis ("Horizontal") * Time.deltaTime * speedFactor < 0) {
                if (!left && angleMod > 0)
                {
                    angleMod = -passiveAngleChange * Time.deltaTime * 2 + angleMod / 7;
                }
                else if (!right && angleMod < 0)
                {
                    angleMod = -passiveAngleChange * Time.deltaTime * 2 + angleMod / 7;
                }
                else if (isInvading)
                {
                    angleMod /= 2;
                    radiusMod /= 2;
                }
                if (angleMod == 0)
                {
                    angleMod = -passiveAngleChange * Time.deltaTime * 2;
                }


                angle -= angleMod;
                radius += radiusMod;

                if (radius < bottomLimit)
                    radius = bottomLimit;
                if (radius > topLimit)
                    radius = topLimit;

                //radius=controlRadius;

                Vector3 newPosition = transform.position;

                newPosition.x = ocX + radius * Mathf.Cos(Mathf.PI * angle / 180);
                newPosition.y = ocY + radius * Mathf.Sin(Mathf.PI * angle / 180);
                transform.position = newPosition;

                transform.eulerAngles = new Vector3(0, 0, angle - 90);

                /*if(SceneManager.GetActiveScene().name=="ChristmasArena"){
					Vector3 turnAround;
					if(Input.GetAxis("Horizontal")>0){
						rotDir=-1;
					} else {
						rotDir=1;

					}
					turnAround = new Vector3(rotDir*Mathf.Abs(transform.localScale.x), transform.localScale.y, 3);

					transform.localScale = turnAround;
					
				}*/
                if (right)
                    leftArm.transform.eulerAngles = new Vector3(0, 0, angle - 55 - radius * 2 / scaleMod);
                if (left)
                    rightArm.transform.eulerAngles = new Vector3(0, 0, angle - 125 + radius * 2 / scaleMod);

            }
        }

    }

    public void setAngle(float value)
    {
        angle = value;
    }

    public float getRadius()
    {
        return this.radius;
    }

    public float getBottomLimit()
    {
        return this.bottomLimit;
    }
    public void renewLimits()
    {
        topLimit = data.topLimit;
        bottomLimit = data.bottomLimit;
    }
}
