using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PublicData : MonoBehaviour
{

    /*//objects
	public GameObject lilShip;
	public GameObject motherShip;
	public GameObject enemyShip;
	public GameObject planet;
	public GameObject moon;*/

    //GUI Stuff
    public bool isStarting = true;
    public bool isEnding;
    public bool GUIOverlay;
    public bool debugOverlay = false;

    public bool pressed;

    public bool isLost = false;

    public Texture badEnd;
    public Texture retryBtn;
    public Texture goodEnd;

    public GUISkin skin;

    Texture end;

    string endMessage;
    string buttonMessage;
    //public GUIText percentageDisplay;
    GameObject invasionDisplay;
    GUIText percentageDisplay;
    GameObject OUI;
    public Texture2D BGtxt;
    float alphaFadeValue = 1;
    string nextLevel;
    int waveNum = 0;
    public bool isArena;
    public bool isAddin = true;


    GameObject cover;
    public GameObject border;
    public GameObject flag;
    public GameObject marker;
    public GameObject boomBoom;
    public GameObject currentExplosion;
    public GameObject planet;
    public GameObject theEvent;
    public float eventDelay = 30;
    public float[,] spawnAngles;

    public float[] laserGuns;
    public float[] shields;
    public float[] plainGuns;
    public float moonSpeed = 0.05f;
    public float moonRadius = 20;
    public float moonAngle = 180;
    public float heightMultiplier;


    //Mothership Stuff
    public bool isArriving = true;
    public float bottomLimit;
    public float topLimit;
    public float motherShipRadius = 20;
    public float motherShipSpeed = 20;
    public float motherShipPassiveSpeed = 5;
    public float motherShipAngle = 90;
    public float motherShipHealth = 300;
    public float motherShipCoreHealth;
    public float motherShipLeftHealth;
    public float motherShipRightHealth;
    public float motherShipDamage = 1;
    public float motherShipLaserDelay = 15;

    //mothership weapons
    public bool left;
    public bool core;
    public bool right;

    public float shipShieldChargePercent;
    public float shipShieldMaxCharge = 3;
    public float shipShieldRecharge = 0.05f;
    public float shipBurstChargePercent;
    public float shipBurstMaxCharge = 3;
    public float shipBurstRecharge = 0.05f;

    //Lilship Stuff
    public bool isDocked;
    public bool wasTeleported;
    public float lilShipHealth = 100;
    public float lilShipSpeed = 15;
    public float lilShipDamage = 10;
    public float lilShipAngle;
    public float lilShipShootDelay = 0.3f;
    public float lilShipBulletSpeed = 20;

    //Invasion Stuff
    public bool ongoingInvasion;

    //resources
    public float invasionResource = 1;
    public float currentInvasionResource;
    public float pickUpDelay = 30;
    public float topHealth = 100;
    public float botHealth = 30;
    public float topResource = 70;
    public float botResource = 20;
    public float topInvasion = 1.5f;
    public float botInvasion = 0.7f;

    //invasion
    public float snowmanSpeed = 20;
    public float invasionLimit;
    public float invasionRadTilt;
    public float invasionAngTilt = 8;
    public float invasionPercent;
    public float invasionRow = 3;
    public float invasionColumn = 8;
    public float invasionDoingGuys;
    public float invasionTroopPrc;
    public float invasionResourceMultiplier = 1;
    public bool[] invaded;
    public float invasionMultiplier = 1;
    public bool isBoomin;
    public float uninvadeDelay = 5;
    public bool wasInvaded;

    //Enemy Stuff
    //enemy ships
    public float enemyShipAngle;
    public float enemyShipSpeed = 7;
    public float enemyShipHealth = 90;
    public float enemyShipDamage = 10;
    public float enemyShipShootDelay = 0.3f;
    public float enemyRespawnDelay = 50;
    public float enemySpawnDelay = 1;
    public float enemyHealDelay = 5;
    public float enemyBulletSpeed = 20;
    public float enemyShipShootDistance = 7;
    public float enemyShipCount = 5;
    //guns
    public float plainGunHealth = 200;
    public float plainGunRadius = 12;
    public float plainGunDamage = 5;
    public float plainGunBulletSpeed = 20;
    public float plainGunShootDelay = 0.2f;
    public float plainGunPause = 2;
    public float plainGunDuration = 1.5f;
    //lasers
    public float laserGunHealth = 200;
    public float laserGunRadius = 9;
    public float laserGunDamage = 1;
    public float laserGunShootDelay = 4;
    //shields
    public float enemyShieldHealth = 200;

    //spawner
    public float enemySpawnerHealth = 200;

    //missile
    public float missileDelay = 5;
    public float missileSpeed = 1;
    public float missileDamage = 50;





    // Use this for initialization
    void Start()
    {

        PlayerPrefs.SetFloat("dmgDone", 0);
        PlayerPrefs.SetInt("lilShipDeath", 0);
        PlayerPrefs.SetInt("pickUpsMissed", 0);
        PlayerPrefs.SetFloat("invasionAvgPrc", 0);

        spawnAngles = new float[,]{
            {30, 115, 145, 175, 205, 310, 0, 0, 0},
            {20, 60, 110, 140, 170, 210, 260, 320, 0},
            {1, 40, 75, 150, 190, 230, 280, 300, 330},
            {50, 100, 250, 0, 0, 0, 0, 0, 0}
        };

        if (SceneManager.GetActiveScene().name == "Tutorial")
            spawnAngles = new float[,]{
                {195, 75},
                {326, 0}
            };
        else if (SceneManager.GetActiveScene().name == "TechnoLevel")
            spawnAngles = new float[,]{
                {15, 55, 110, 160, 240, 250, 310},
                {1, 90, 180, 270, 0, 0, 0},
                {40, 80, 135, 180, 210, 320, 0}
            };
        else if (SceneManager.GetActiveScene().name == "FireLevel")
            spawnAngles = new float[,]{
                {20, 70, 180, 230, 0, 0},
                {75, 220, 310, 0, 0, 0},
                {45, 85, 120, 205, 250, 340}
            };
        else if (SceneManager.GetActiveScene().name == "NatureLevel")
            spawnAngles = new float[,]{
                {30, 115, 145, 175, 205, 310, 0, 0, 0},
                {20, 60, 110, 140, 170, 210, 260, 320, 0},
                {1, 40, 75, 150, 190, 230, 280, 300, 330},
                {50, 100, 250, 0, 0, 0, 0, 0, 0}
            };

        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            cover = planet.transform.Find("COVER").gameObject;
            //InvokeRepeating ("ExplodeStuff", 2, 2);
        }

        InvokeRepeating("Uninvade", uninvadeDelay, uninvadeDelay);

        //all the radiuses and related stuff.
        heightMultiplier = planet.GetComponent<CircleCollider2D>().radius * planet.transform.localScale.x + 0.5f;

        //this is a game object with a sphere collider. it's only here to collide with particles.
        GameObject particleCollider = new GameObject("particle collider");
        particleCollider.transform.parent = planet.transform;
        particleCollider.layer = 22;
        SphereCollider sc = particleCollider.AddComponent<SphereCollider>();
        sc.radius = heightMultiplier;

        bottomLimit = heightMultiplier + 5;
        invasionLimit = bottomLimit + 3;
        topLimit = bottomLimit + 10;
        invasionRadTilt = (invasionLimit - bottomLimit) * 0.8f;
        motherShipRadius = topLimit + 6;

        //textStyle.normal.textColor = Color.cyan;
        invaded = new bool[360];
        for (int i = 0; i < 360; i++)
        {
            invaded[i] = false;
        }

        if (theEvent != null)
        {
            InvokeRepeating("ShakeDatCam", eventDelay - 1, eventDelay);
            InvokeRepeating("createEvent", eventDelay, eventDelay);
        }
        //PopulateLevel();
        if (GUIOverlay) DrawOUI();

    }

    void ShakeDatCam()
    {
        Camera.main.GetComponent<CameraControls>().ShakeDat(3, 0.6f);
    }

    public void ShakeDatGUI(float delay, float amplitude)
    {
        GetComponent<ShowGUI>().ShakeDat(delay, amplitude);
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown("o"))
        {
            /*if (OUI == null) {
				DrawOUI ();				
			} else { 
				ClearOUI ();
			}*/
        }
        else if (Input.GetKeyDown("p"))
        {
            //debugOverlay=!debugOverlay;
        }
        else if (Input.GetKeyDown("i"))
        {
            //GUIOverlay=!GUIOverlay;
        }
    }





    void OnGUI()
    {
        /*/ STUFFFFF TO DELEEEEETE
		GUI.Label(new Rect(20,20,200,200), "dmg " + PlayerPrefs.GetFloat("dmgDone").ToString());
		GUI.Label(new Rect(20,70,200,200),"lil " + PlayerPrefs.GetInt("lilShipDeath").ToString());
		GUI.Label(new Rect(20,120,200,200), "pick " + PlayerPrefs.GetInt("pickUpsMissed").ToString());
		GUI.Label(new Rect(20,170,200,200), "inv " + PlayerPrefs.GetFloat("invasionAvgPrc").ToString());

		//END OF THE STUFFFFFFF
		*/

        if (isEnding && !isArena)
        {

            if (skin)
            {
                GUI.skin = skin;
            }
            GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));
            //GUILayout.Label(end);

            GUILayout.EndArea();

            if (isLost)
            {
                /*if (GUI.Button (new Rect ((Screen.width - 400) / 2 + 75, (Screen.height - 300)/2 + 180, 120, 60), "Retry")) {
					SceneManager.LoadScene(nextLevel);
				}
				if (GUI.Button (new Rect ((Screen.width - 400) / 2 + 205, (Screen.height - 300)/2 + 180, 120, 60), "Exit")) {
					SceneManager.LoadScene("Intro");
				}*/
            }
            else
            {
                /*if (GUI.Button (new Rect ((Screen.width - 400) / 2 + 133, (Screen.height - 300)/2 + 170, 140, 60), "Continue")) {
					SceneManager.LoadScene(nextLevel);
				}*/
            }

            /*alphaFadeValue += Mathf.Clamp01 (Time.deltaTime / 5);
			GUI.depth = -1000;
			GUI.color = new Color (0, 0, 0, alphaFadeValue);
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), BGtxt);
			
			float endW = Screen.width / 5;
			if (endW < 400)
				endW = 400;
			if (endW > 700)
				endW = 700;
			float endH = Screen.height / 5;
			if (endH < 100)
				endH = 100;
			GUI.depth = 1000;
			if(alphaFadeValue>=0.99f){
				alphaFadeValue=1;
				Time.timeScale = 0;
			}
			GUI.color = new Color(1,1,1,1);
			GUI.BeginGroup (new Rect(Screen.width/2-endW/2, Screen.height/2- endH / 2, endW, endH));
			GUI.Box (new Rect (0, 0, endW, endH), "");
			GUI.Label (new Rect (10, 10, endW / 2, endH / 2), endMessage); 
			
			if (GUI.Button (new Rect (10, endH / 2 + 10, endW- 20, endH/2- 20), buttonMessage)) {
				SceneManager.LoadScene(nextLevel);
			}
			GUI.EndGroup ();*/
        }
        else if (isEnding && isArena && isLost)
        {
            alphaFadeValue += Mathf.Clamp01(Time.deltaTime / 5);
            GUI.depth = -1000;
            GUI.backgroundColor = Color.clear;

            float endW = Screen.width / 5;
            if (endW < 400)
                endW = 400;
            if (endW > 700)
                endW = 700;
            float endH = Screen.height / 5;
            if (endH < 100)
                endH = 100;
            GUI.depth = 1000;
            if (alphaFadeValue >= 0.99f)
            {
                alphaFadeValue = 1;
                Time.timeScale = 0;
            }
            GUI.color = new Color(1, 1, 1, 1);
            GUI.BeginGroup(new Rect(Screen.width / 2 - endW / 2, Screen.height / 2 - endH, endW, endH + 200));
            GUI.Box(new Rect(0, 0, endW, endH), badEnd);

            if (GUI.Button(new Rect(15, endH / 2 + 70, endW - 20, endH / 2), retryBtn))
            {
                SceneManager.LoadScene(nextLevel);
            }
            GUI.EndGroup();

        }

    }
    public void DrawOUI()
    {
        OUI = new GameObject("OUI");
        //the radius borders
        border.GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.2f, 0.15f);
        border.transform.localScale = new Vector3(1.3f, 0.7f);
        MakeACircle(topLimit, 18);

        border.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.3f, 0.9f, 0.15f);
        border.transform.localScale = new Vector3(0.5f, 0.5f);
        MakeACircle(invasionLimit, 9);

        //border.transform.localScale = new Vector3 (0.3f, 0.4f);
        MakeACircle(bottomLimit - 0.4f, 12);

        GameObject[] uiElements = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject element in uiElements)
        {
            element.GetComponent<SpriteRenderer>().enabled = true;
        }
        GetComponent<ShowGUI>().ToggleBar(true);
        GUIOverlay = true;
        for (int i = 0; i < invaded.Length; i++)
        {
            if (i % 4 == 0)
            {
                if (invaded[i])
                {
                    placeInvasionMarker((float)i, heightMultiplier, marker);
                }
            }
        }
    }

    public void ClearOUI()
    {
        GetComponent<ShowGUI>().ToggleBar(false);
        Destroy(GameObject.Find("OUI"));
        GameObject[] uiElements = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject element in uiElements)
        {
            element.GetComponent<SpriteRenderer>().enabled = false;
        }
        GUIOverlay = false;
    }

    void MakeACircle(float radius, float spacing)
    {
        for (int a = 0; a < 360; a++)
        {
            if (a % spacing == 0)
            {
                if (GameObject.Find("OUI") != null)
                    placeMarker(a, radius, border);
            }
        }
    }

    public void placeInvasionMarker(float pos, float rad, GameObject markerToPlace)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = rad * Mathf.Cos(Mathf.PI * pos / 180);
        newPosition.y = rad * Mathf.Sin(Mathf.PI * pos / 180);
        GameObject currMarker;
        currMarker = Instantiate(markerToPlace, newPosition, transform.rotation) as GameObject;
        currMarker.transform.parent = OUI.transform;
        currMarker.transform.eulerAngles = new Vector3(0, 0, pos - 90);
        currMarker.name = pos.ToString();
    }

    public void placeMarker(float pos, float rad, GameObject markerToPlace)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = rad * Mathf.Cos(Mathf.PI * pos / 180);
        newPosition.y = rad * Mathf.Sin(Mathf.PI * pos / 180);
        GameObject currMarker;
        currMarker = Instantiate(markerToPlace, newPosition, transform.rotation) as GameObject;
        currMarker.transform.parent = OUI.transform;
        currMarker.transform.eulerAngles = new Vector3(0, 0, pos - 90);
    }

    IEnumerator WaitToEnd()
    {

        pressed = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextLevel);
    }

    void Update()
    {

        /*if (isEnding && !pressed){
			if(Input.anyKey){
				StartCoroutine(WaitToEnd());
			}
		}*/
        if (Input.GetButtonDown("invasion"))
        {
            /*for(int i = 0; i<360; i++){
				invaded[i] = true;
			}*/
        }

    }


    IEnumerator doWave()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(nullInvasion());
        StartCoroutine(GetComponent<ManageTheArena>().Wait(waveNum));
        waveNum++;
    }


    void SaveLevel(int i)
    {
        if (i > PlayerPrefs.GetInt("level"))
            PlayerPrefs.SetInt("level", i);
    }

    public void EndLevel(bool isWon)
    {
        GUIOverlay = false;


        if (isWon)
        {
            if (!isArena)
            {
                isLost = false;
                isEnding = true;
                string currentName = SceneManager.GetActiveScene().name;
                endMessage = "CONGRATULATIONS!\nYOU HAVE WON!";
                if (currentName == "Tutorial")
                {
                    nextLevel = "FireLevel";
                    SaveLevel(1);
                }

                else if (currentName == "FireLevel")
                {
                    nextLevel = "TechnoLevel";
                    SaveLevel(2);
                }
                else if (currentName == "TechnoLevel")
                {
                    nextLevel = "NatureLevel";
                    SaveLevel(3);
                }

                else if (currentName == "NatureLevel")
                {
                    nextLevel = "Intro";
                    SaveLevel(4);
                }

                else
                {
                    nextLevel = "Intro";
                }

                buttonMessage = "Continue to Next Level!";
                end = goodEnd;

                GetComponent<BonusCounter>().isEnd = true;
                GetComponent<BonusCounter>().isWon = true;
                GetComponent<BonusCounter>().nextLevel = nextLevel;
                GetComponent<BonusCounter>().countBonuses();

            }
            else
            {
                isEnding = false;
                GUIOverlay = true;
                if (isAddin)
                {
                    StartCoroutine(doWave());
                }

            }

        }
        else
        {
            isLost = true;
            isEnding = true;
            if (!isArena)
            {
                endMessage = "TOO BAD.\nYOU LOST.";
                nextLevel = SceneManager.GetActiveScene().name;
                buttonMessage = "Retry.";
                end = badEnd;

                GetComponent<BonusCounter>().isEnd = true;
                GetComponent<BonusCounter>().isWon = false;
                GetComponent<BonusCounter>().nextLevel = nextLevel;
                GetComponent<BonusCounter>().countBonuses();
            }
            else
            {
                endMessage = "TOO BAD.\nYOU LOST.";
                nextLevel = SceneManager.GetActiveScene().name;
                buttonMessage = "Retry.";
                //SceneManager.LoadScene("ChristmasIntro");
            }

        }
        //Time.timeScale = 0;
    }

    void createEvent()
    {
        Instantiate(theEvent);
    }

    void Uninvade()
    {
        for (int i = 1; i < 359; i++)
        {
            if (invaded[i])
            {
                if (!invaded[i - 1] || !invaded[i + 1])
                {
                    if (Random.Range(0, 10) < 2)
                    {

                        invaded[i] = false;
                    }
                }
            }
        }
    }

    public void addInvasion(float value)
    {
        invasionMultiplier *= value;
    }

    void MakeABoomBoom(int angle)
    {

        Vector3 newPosition = transform.position;
        float multY = Random.Range(0.9f - (0.9f * invasionPercent / 100), 0.9f);
        float multX = Random.Range(0.9f - (0.9f * invasionPercent / 100), 0.9f);
        newPosition.x = heightMultiplier * multX * Mathf.Cos(Mathf.PI * angle / 180);
        newPosition.y = heightMultiplier * multY * Mathf.Sin(Mathf.PI * angle / 180);
        Instantiate(boomBoom, newPosition, transform.rotation);


    }

    void ExplodeStuff()
    {
        for (int i = 0; i < 360; i++)
        {
            if (invaded[i])
            {
                if (Random.Range(0, 40) < 1)
                {
                    MakeABoomBoom(i);
                    //WaitToMakeBoomBoom(i);
                }
            }
        }
    }

    void Invade(int beg, int end)
    {
        wasInvaded = true;
        for (int i = beg; i < end; i++)
        {
            if (i >= 360)
            {
                invaded[i - 360] = true;
            }
            else if (i < 0)
            {
                invaded[360 + i] = true;
            }
            else if (i < 360 && i >= 0)
            {
                invaded[i] = true;
            }
        }
    }

    public IEnumerator nullInvasion()
    {
        isAddin = false;
        invaded = new bool[360];
        yield return new WaitForSeconds(2);
        invasionPercent = 0;
        cover.SendMessage("setAlpha", 0f);

        foreach (Transform child in GameObject.Find("OUI").transform)
        {
            if (child.gameObject.tag == "invMarker")
            {
                Destroy(child.gameObject);
            }
        }

        /* markers = GameObject.Find ("OUI").transform.FindGameObjectsWithTag ("invMarker");
		for (int i = 0; i<markers.Length; i++) {
			Destroy(markers[i]);
		}*/
        isAddin = true;
    }

    public float checkInvasion()
    {
        float percentage = 0;
        float n = 0;
        GameObject[] markers;
        markers = GameObject.FindGameObjectsWithTag("invMarker");

        foreach (GameObject currMarker in markers)
        {
            Destroy(currMarker);
        }

        for (int i = 0; i < invaded.Length; i++)
        {

            if (invaded[i])
            {
                n++;
                if (i % 4 == 0)
                {
                    if (GameObject.Find("OUI"))
                        placeInvasionMarker((float)i, heightMultiplier, marker);
                }
            }
        }
        percentage = n / 360 * 100;

        //makes the planets darker 
        if (planet.name != "SLAVER STATION")
            if (cover != null) cover.SendMessage("setAlpha", percentage);
        return percentage;
    }

    public void PerformInvasion(int beg, int end, int area)
    {

        //parsing the input into 0-360 standard
        if (beg < 0)
            beg += 360;
        if (end < 0)
            end += 360;
        if (end >= 360)
            end -= 360;
        if (beg >= 360)
            beg -= 360;
        int counter = area;



        if (invaded[beg] == true)
        {
            for (int i = beg; i > beg - 360; i--)
            {
                int dev = beg - i;
                if (i < 0)
                {
                    if (invaded[i + 360] == false)
                    {
                        beg = i - counter / dev;
                        break;
                    }
                }
                else
                {
                    if (invaded[i] == false)
                    {
                        beg = i - counter / dev;
                        break;
                    }
                }
            }
        }

        if (invaded[end] == true)
        {

            for (int i = end; i < end + 360; i++)
            {
                int dev = end - i;
                if (i >= 360)
                {
                    if (invaded[i - 360] == false)
                    {
                        end = i + counter / dev;
                        break;
                    }
                }
                else
                {
                    if (invaded[i] == false)
                    {
                        end = i + counter / dev;
                        break;
                    }
                }
            }
        }

        if (end != beg)
        {
            if (beg > end)
            {
                Invade(beg, 359);
                Invade(0, end);
            }
            else
            {
                Invade(beg, end);
            }
        }

        //ClearFlags(beg, end);

        //place the flags if the level isn't won
        invasionPercent = checkInvasion();

        if (invasionPercent >= 85 + waveNum * 2 || invasionPercent > 99)
        {
            invasionPercent = 100;
            EndLevel(true);
        }
        else
        {
            /*PlaceFlag (beg);
			PlaceFlag (end);*/
        }
    }

    void ClearFlags(float beg, float end)
    {
        GameObject[] flags;
        flags = GameObject.FindGameObjectsWithTag("invFlag");

        //removes all flags including the ones on the current beg/end positions
        for (int i = 0; i < flags.Length; i++)
        {
            int angle = flags[i].GetComponent<flagInfo>().angle;
            if (angle == 0)
            {
                if ((invaded[359] && invaded[1]) || angle == (int)beg || angle == (int)end)
                {
                    Destroy(flags[i]);
                }
            }
            else if (angle == 359)
            {
                if ((invaded[358] && invaded[0]) || angle == (int)beg || angle == (int)end)
                {
                    Destroy(flags[i]);
                }
            }
            else
            {

                if ((invaded[angle - 1] && invaded[angle + 1]) || angle == (int)beg || angle == (int)end)
                {
                    Destroy(flags[i]);
                }
            }
        }
    }

    public void PlaceFlag(float pos)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = (heightMultiplier + 0.1f) * Mathf.Cos(Mathf.PI * pos / 180);
        newPosition.y = (heightMultiplier + 0.1f) * Mathf.Sin(Mathf.PI * pos / 180);
        GameObject currFlag;
        currFlag = Instantiate(flag, newPosition, transform.rotation) as GameObject;
        currFlag.transform.eulerAngles = new Vector3(0, 0, pos - 90);

        if (pos < 0)
            pos += 360;
        if (pos >= 360)
            pos -= 360;

        currFlag.SendMessage("setAngle", pos);
    }




    //resources
    public void setCurrentInvasionResource(float value)
    {
        currentInvasionResource = value;
    }

    //delay
    public void setPlainGunShootDelay(float value)
    {
        plainGunShootDelay = value;
    }

    public void setLaserGunShootDelay(float value)
    {
        laserGunShootDelay = value;
    }

    public void setEnemySpawnDelay(float value)
    {
        enemySpawnDelay = value;
    }

    //speed
    public void setLilShipSpeed(float value)
    {
        lilShipSpeed = value;
    }

    public void setEnemyShipSpeed(float value)
    {
        enemyShipSpeed = value;
    }

    public void setMotherShipSpeed(float value)
    {
        motherShipSpeed = value;
    }

    public void setMotherShipPassiveSpeed(float value)
    {
        motherShipPassiveSpeed = value;
    }

    public void setPlayerBulletSpeed(float value)
    {
        lilShipBulletSpeed = value;
    }

    public void setEnemyBulletSpeed(float value)
    {
        enemyBulletSpeed = value;
    }

    public void setPlainGunBulletSpeed(float value)
    {
        plainGunBulletSpeed = value;
    }

    public void setMoonSpeed(float value)
    {
        moonSpeed = value;
    }

    //health
    public void setLilShipHealth(float value)
    {
        lilShipHealth = value;
    }

    public void setEnemyShipHealth(float value)
    {
        enemyShipHealth = value;
    }

    public void setMotherShipHealth(float value)
    {
        motherShipHealth = value;
    }

    public void setPlainGunHealth(float value)
    {
        plainGunHealth = value;
    }

    public void setLaserGunHealth(float value)
    {
        laserGunHealth = value;
    }

    //radius
    public void setLaserGunRadius(float value)
    {
        laserGunRadius = value;
    }

    public void setPlainGunRadius(float value)
    {
        plainGunRadius = value;
    }

    public void setMoonRadius(float value)
    {
        moonRadius = value;
    }

    public void setTopLimit(float value)
    {
        topLimit = value;
    }

    public void setBottomLimit(float value)
    {
        bottomLimit = value;
    }

    public void setMotherShipRadius(float value)
    {
        motherShipRadius = value;
    }

    //damage
    public void setLilShipDamage(float value)
    {
        lilShipDamage = value;
    }

    public void setEnemyShipDamage(float value)
    {
        enemyShipDamage = value;
    }

    public void setMotherShipDamage(float value)
    {
        motherShipDamage = value;
    }

    public void setPlainGunDamage(float value)
    {
        plainGunDamage = value;
    }

    public void setLaserGunDamage(float value)
    {
        laserGunDamage = value;
    }

    //angles
    public void setLilShipAngle(float value)
    {
        lilShipAngle = value;
    }

    public void setEnemyShipAngle(float value)
    {
        enemyShipAngle = value;
    }

    public void setMotherShipAngle(float value)
    {
        motherShipAngle = value;
    }

    public void setMoonAngle(float value)
    {
        moonAngle = value;
    }

    //invasion
    public void setInvasionDoingGuys(float value)
    {
        invasionDoingGuys = value;
    }

    public void setInvasionTroopPrc(float value)
    {
        invasionTroopPrc = value;
    }

    public void setInvasionRow(float value)
    {
        invasionRow = value;
    }

    public void setInvasionColumn(float value)
    {
        invasionColumn = value;
    }

    public void setOngoingInvasion(bool value)
    {
        ongoingInvasion = value;
    }


    public void setCore(bool value)
    {
        core = value;
        GameObject.Find("MOTHERSHIP OUI").GetComponent<OUImothership>().enableCore();
    }


    public void setIsArena(bool value)
    {
        isArena = value;
    }

    public void redoBorders()
    {
        Destroy(GameObject.Find("OUI"));
        OUI = new GameObject("OUI");


        border.GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.2f, 0.1f);
        border.transform.localScale = new Vector3(1.3f, 0.7f);
        MakeACircle(topLimit, 18);

        border.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.15f);
        border.transform.localScale = new Vector3(0.5f, 0.5f);
        MakeACircle(invasionLimit, 9);

        //border.transform.localScale = new Vector3 (0.3f, 0.4f);
        MakeACircle(bottomLimit - 0.4f, 12);
    }

    public void renewHeightMultiplier()
    {
        heightMultiplier = planet.GetComponent<CircleCollider2D>().radius * planet.transform.localScale.x + 0.5f;
        bottomLimit = heightMultiplier + 5;
        invasionLimit = bottomLimit + 3;
        topLimit = bottomLimit + 10;
        invasionRadTilt = (invasionLimit - bottomLimit) * 0.8f;
        planet.transform.Find("particle collider").GetComponent<SphereCollider>().radius = planet.GetComponent<CircleCollider2D>().radius / 2;


        redoBorders();

        ShakeDatCam();
        GameObject.Find("MOTHERSHIP").GetComponent<MotherShipRotation>().renewLimits();
        GameObject.Find("MOTHERSHIP").GetComponent<Invasion>().renewLimits();
        GameObject.Find("CHRISTMAS CAMERA").GetComponent<CameraControls>().renewLimits();
        GameObject.Find("SNOW PLANET").GetComponent<SpawnBonuses>().renewLimits();
    }


    public void setIsDocked(bool value)
    {
        isDocked = value;
    }

    public void setWasInvaded(bool value)
    {
        wasInvaded = value;
    }

    public void setIsLost(bool value)
    {
        isLost = value;
    }

    public void setWasTeleported(bool value)
    {
        wasTeleported = value;
    }


    /*
	void PopulateLevel(){
		if(shields.Length!=0){
			for(int i=0; i<shields.Length; i++)
				Spawn(shields[i], theShield);
		}
		if(plainGuns.Length!=0){
			for(int i=0; i<plainGuns.Length; i++)
				Spawn(plainGuns[i], theGun);
		}
		if(laserGuns.Length!=0){
			for(int i=0; i<laserGuns.Length; i++)
				Spawn(laserGuns[i], theBurst);
		}
		InvokeRepeating ("RespawnAll", 1, 1);

	}

	void RespawnAll(){
		Respawn (shields, theShield, "EnemyShield", shields.Length);
		Respawn (laserGuns, theBurst, "EnemyLaser", laserGuns.Length);
		Respawn (plainGuns, theGun, "EnemyGun", plainGuns.Length);
	}

	void Spawn(float angle, GameObject item){
		Vector3 newPosition = transform.position;
		newPosition.x=heightMultiplier*Mathf.Cos(Mathf.PI * angle / 180);
		newPosition.y=heightMultiplier*Mathf.Sin(Mathf.PI * angle / 180);
		GameObject newItem;
		newItem = Instantiate(item, newPosition, transform.rotation) as GameObject;
		float sizeMultiplier = Random.Range(0.9f, 1.4f);
		newItem.transform.eulerAngles = new Vector3(0, 0, angle-90);
		newItem.GetComponent<TakeDamageAndGetDestroyed>().setAngle(angle);
		newItem.transform.parent = transform;
		newItem.transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, 1);
	}



	void Respawn(float[] items, GameObject item, string itemTag, float itemsLength){
		GameObject[] currentItems = GameObject.FindGameObjectsWithTag(itemTag);
		if(currentItems.Length < itemsLength && !isSpawning){
			bool isThere = false;
			float thisAngle;
			for(int i=0; i < itemsLength; i++){
				isThere = false;
				for(int j = 0; j < currentItems.Length; j++){
					thisAngle = currentItems[j].GetComponent<TakeDamageAndGetDestroyed>().angle;
					if(thisAngle == items[i]){
						isThere = true;
						break;
					}
				}
				if(!isThere){
					itemNum = i;
					isSpawning = true;
					StartCoroutine(WaitToRespawn(items, item, itemsLength));
					break;
				}
			}
		}	
	}

	IEnumerator WaitToRespawn(float[] items, GameObject item, float itemsLength) {
		yield return new WaitForSeconds(enemyRespawnDelay);
		isSpawning = false;
		if(!invaded[(int)items[itemNum]]){
			Spawn(items[itemNum], item);
		} else {
			itemsLength -= 1;
			items[itemNum] = items[items.Length-2];
		}
	}*/


}


