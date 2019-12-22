using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour
{

    public GameObject intro;

    public GUISkin skin;
    public GUISkin slideSkin;
    public GUISkin webSkin;

    public Texture startTexture;
    public Texture tutorialTexture;
    public Texture creditsTexture;
    public Texture exitTexture;
    public Texture settingsTexture;
    public Texture titleTexture;
    public Texture muteTexture;
    public Texture volumeTexture;
    public Texture backTexture;
    public Texture daytonaTexture;
    public Texture indieTexture;
    public Texture fbTexture;
    public Texture continueTexture;

    public GUISkin pretty;

    public Texture creditSheet;

    public GameObject slide1;
    public GameObject slide2;
    public GameObject slide3;

    public Texture textBack;

    public AudioClip sound;

    bool isTypin;

    string currText;
    int storyCount;

    public GameObject camera;

    public bool isStart;
    public bool isTut;
    public bool isSet;
    public bool isCredits;
    public bool isStory;
    public bool isLevelSelect;


    public GameObject tut;
    public GameObject credits;
    public GameObject inwo;
    public GameObject back;

    Color color;



    string message;

    bool muteMusic;

    float volume;


    bool GUIEnabled = false;


    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevelName == "Intro")
        {
            /*PreventDemo ();
			GUIEnabled = false;
			GameObject curr = Instantiate (intro) as GameObject;
			curr.name = "INTRO";
			StartCoroutine (WaitAndRemoveDemo ());*/
            GUIEnabled = true;
            print(PlayerPrefs.GetInt("story"));
            if (PlayerPrefs.HasKey("story"))
            {
                if (PlayerPrefs.GetInt("story") == 1)
                {
                    isStory = false;
                    isStart = true;
                }
                else
                {
                    isStory = true;
                }
            }
            else
            {
                isStory = true;
            }


            volume = camera.GetComponent<MetaControl>().getVolume();
            message = "";
        }
    }


    IEnumerator TypeText()
    {
        isTypin = true;
        foreach (char letter in message.ToCharArray())
        {
            currText += letter;
            if (sound)
                GetComponent<AudioSource>().PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(0.09f);
        }
        isTypin = false;
    }

    void PreventDemo()
    {
        if (GameObject.Find("INTRO") != null)
        {
            Destroy(GameObject.Find("INTRO"));
        }
        else
        {
            StopAllCoroutines();
        }
        GUIEnabled = true;
        StartCoroutine(WaitAndRunDemo());
    }

    IEnumerator WaitAndRemoveDemo()
    {
        yield return new WaitForSeconds(10);
        PreventDemo();
    }

    IEnumerator WaitAndRunDemo()
    {
        yield return new WaitForSeconds(30);
        GameObject curr = Instantiate(intro) as GameObject;
        curr.name = "INTRO";
        GUIEnabled = false;
        yield return new WaitForSeconds(10);
        PreventDemo();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
        {
            if (isCredits)
            {
                isCredits = false;
                inwo.GetComponent<SpriteRenderer>().enabled = true;
                credits.GetComponent<SpriteRenderer>().enabled = false;
                back.GetComponent<SpriteRenderer>().enabled = true;

                isStart = true;
                isCredits = false;
            }
            else if (isTut)
            {
                isStart = true;
                isCredits = false;
                tut.GetComponent<SpriteRenderer>().enabled = false;
                back.GetComponent<SpriteRenderer>().enabled = true;
                inwo.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (isSet)
            {
                isSet = false;
                isStart = true;
            }
            else if (isStory)
            {
                if (isTypin)
                {
                    currText = message;
                    StopCoroutine("TypeText");
                    isTypin = false;
                }
                else storyCount++;
            }
            else if (isStart)
            {
                isLevelSelect = true;
                isStart = false;
                back.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCredits)
            {
                isCredits = false;
                inwo.GetComponent<SpriteRenderer>().enabled = true;
                credits.GetComponent<SpriteRenderer>().enabled = false;

                isStart = true;
                isCredits = false;
            }
            else if (isTut)
            {
                isStart = true;
                isTut = false;
                tut.GetComponent<SpriteRenderer>().enabled = false;
                inwo.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (isSet)
            {
                isSet = false;
                isStart = true;
            }
            else if (isStory)
            {
                storyCount = 6;
                StopCoroutine("TypeText");
                isTypin = false;
            }
            else if (isLevelSelect)
            {
                isLevelSelect = false;
                isStart = true;
            }
        }
    }


    void OnGUI()
    {
        if (GUIEnabled)
        {
            if (!isStory)
            {
                if (Screen.width == 960)
                {
                    GUI.skin = webSkin;
                    //GUI.skin.label.fontSize = 
                    GUI.Label(new Rect(Screen.width / 4, Screen.height - 48, Screen.width / 2, 20), "Version 0.0.0.0.0.1");
                }
                else
                {
                    GUI.skin = skin;
                    GUI.Label(new Rect(Screen.width / 3, Screen.height - 54, Screen.width / 3, 40), "Version 0.0.0.0.0.1");
                }

            }


            if (isStory)
            {
                if (slideSkin != null)
                {
                    GUI.skin = slideSkin;
                }

                switch (storyCount)
                {
                    case 0:
                        slide1.GetComponent<SpriteRenderer>().enabled = true;
                        message = "Somewhere in the vast spaciousiousness of space existed a planet, Inwo. It was a beautiful, peaceful, secluded planet, having never been subject to alien contact. Until...";
                        //currText = ;
                        break;
                    case 1:
                        message = "A huge mechanical monstrosity approached the planet from outer space, and, as friendly and tolerant as the inhabitans of the INWO were, it didn't seem like very nice company.";
                        break;
                    case 2:
                        slide1.GetComponent<SpriteRenderer>().enabled = false;
                        slide2.GetComponent<SpriteRenderer>().enabled = true;
                        message = "The giant ship turned out to be trouble indeed. It was a slaver mothership, a grave danger recognized with fear and despised all across the universe.";
                        break;
                    case 3:
                        message = "It brought horrid destruction upon the planet and its inhabitants, enslaving thousands and leaving the rest to die on the uninhabitable piece of rock.";
                        break;
                    case 4:
                        slide2.GetComponent<SpriteRenderer>().enabled = false;
                        slide3.GetComponent<SpriteRenderer>().enabled = true;
                        message = "Lil'ship, the only surviving inwonian vessel, was out on a mission, and had the chance to witness the atrocity.";
                        break;

                    case 5:
                        message = "The small crew of the spaceship, having no home to return to, decided to follow the slavers, with little aim and hope, driven by desperation alone.";
                        break;

                    case 6:
                        slide1.GetComponent<SpriteRenderer>().enabled = false;
                        slide2.GetComponent<SpriteRenderer>().enabled = false;
                        slide3.GetComponent<SpriteRenderer>().enabled = false;
                        storyCount = 0;
                        currText = "";
                        PlayerPrefs.SetInt("story", 1);
                        print(PlayerPrefs.GetInt("story"));

                        StopAllCoroutines();
                        isTypin = false;
                        isStory = false;
                        isStart = true;

                        break;
                }

                if (!isTypin && message != currText && isStory)
                {
                    currText = "";
                    StartCoroutine("TypeText");
                }
                color = GUI.color;
                color.a = 0.5f;
                GUI.color = color;
                GUI.Label(new Rect((Screen.width - 700) / 2, Screen.height - 150, 700, 400), textBack);
                GUI.color = Color.black;
                GUI.Label(new Rect((Screen.width - 620) / 2 - 1, Screen.height - 110 - 1, 620, 200), currText);
                GUI.color = Color.white;
                color.a = 1;
                GUI.color = color;
                GUI.Label(new Rect((Screen.width - 620) / 2, Screen.height - 110, 620, 200), currText);


                if (GUI.Button(new Rect((Screen.width + 500) / 2, Screen.height - 50, 140, 55), "Continue"))
                {
                    if (isTypin)
                    {
                        currText = message;
                        StopCoroutine("TypeText");
                        isTypin = false;
                    }
                    else storyCount++;
                }
                if (GUI.Button(new Rect((Screen.width - 500) / 2 - 125, Screen.height - 50, 140, 55), "Skip"))
                {
                    storyCount = 6;
                    isTypin = false;
                    StopCoroutine("TypeText");

                }

            }
            else
            {
                if (skin != null)
                {
                    GUI.skin = skin;
                }


            }


            if (isStart)
            {
                inwo.GetComponent<SpriteRenderer>().enabled = true;
                back.GetComponent<SpriteRenderer>().enabled = true;

                if (Screen.width == 960)
                {
                    GUI.skin = webSkin;
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 60, Screen.height / 2, 200, 1000));
                }
                else
                {
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 10, 200, 1000));
                }
                if (GUILayout.Button("Start Game"))
                {
                    isLevelSelect = true;
                    isStart = false;
                    back.GetComponent<SpriteRenderer>().enabled = true;
                    //SceneManager.LoadScene();
                }



                if (GUILayout.Button("Tutorial"))
                {
                    inwo.GetComponent<SpriteRenderer>().enabled = false;
                    back.GetComponent<SpriteRenderer>().enabled = false;
                    tut.GetComponent<SpriteRenderer>().enabled = true;
                    isStart = false;
                    isTut = true;
                }
                if (GUILayout.Button("Story"))
                {
                    isStart = false;
                    isStory = true;
                }
                if (GUILayout.Button("Audio"))
                {
                    isStart = false;
                    isSet = true;
                }
                if (GUILayout.Button("Credits"))
                {
                    inwo.GetComponent<SpriteRenderer>().enabled = false;
                    back.GetComponent<SpriteRenderer>().enabled = false;
                    isStart = false;
                    isCredits = true;

                }

                if (GUILayout.Button("Exit Game"))
                    Application.Quit();

                GUI.EndGroup();



            }
            else if (isSet)
            {
                if (Screen.width == 960)
                {
                    GUI.skin = webSkin;
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 110, Screen.height / 2 + 20, 250, 500));
                }
                else
                {
                    GUI.skin = skin;
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 110, Screen.height / 2 + 70, 250, 500));
                }

                GUILayout.Label(volumeTexture);
                volume = GUILayout.HorizontalSlider(volume, 0, 1);
                muteMusic = GUILayout.Toggle(muteMusic, muteTexture);
                camera.GetComponent<MetaControl>().muteMusic = muteMusic;
                camera.GetComponent<MetaControl>().setVolume(volume);

                GUI.EndGroup();
                if (Screen.width == 960)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 35, Screen.height / 2 + 180, 70, 40), "Back"))
                    {
                        isSet = false;
                        isStart = true;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 200, 100, 60), "Back"))
                    {
                        isSet = false;
                        isStart = true;
                    }
                }


                /*GUI.Label(new Rect(Screen.width/2-60, Screen.height/2+40, 120, 60), volumeTexture);
				volume = GUI.HorizontalSlider(new Rect(Screen.width/2-60, Screen.height/2+94, 120, 10), volume, 0, 1);
				muteMusic = GUI.Toggle(
					new Rect(Screen.width/2-60, Screen.height/2+117, 120, 60), 
					muteMusic, 
					muteTexture);
				camera.GetComponent<MetaControl>().muteMusic = muteMusic;
				camera.GetComponent<MetaControl>().setVolume(volume);

				if (GUI.Button(new Rect(Screen.width/2-60, Screen.height/2+155, 120, 60), backTexture))
				{
					isSet = false;
					isStart = true;
				}*/
            }
            else if (isLevelSelect)
            {
                if (Screen.width == 960)
                {
                    GUI.skin = webSkin;
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 70, Screen.height / 2, 200, 1000));
                }
                else
                {
                    GUI.skin = skin;
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 110, Screen.height / 2, 500, 1000));
                }


                if (GUILayout.Button("Slaver Station"))
                    SceneManager.LoadScene("Slaver Station");
                if (PlayerPrefs.GetInt("level") > 0 && GUILayout.Button("Fire Planet"))
                    SceneManager.LoadScene("Fire Planet");
                if (PlayerPrefs.GetInt("level") > 1 && GUILayout.Button("Techno Planet"))
                    SceneManager.LoadScene("Techno Planet");
                if (PlayerPrefs.GetInt("level") > 2 && GUILayout.Button("Jungle Planet"))
                    SceneManager.LoadScene("Jungle Planet");
                GUI.EndGroup();
                if (Screen.width == 960)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 130, Screen.height - 90, 70, 40), "Back"))
                    {
                        isLevelSelect = false;
                        isStart = true;
                    }

                    if (GUI.Button(new Rect(Screen.width / 2, Screen.height - 90, 140, 40), "Clear Save"))
                    {
                        PlayerPrefs.DeleteKey("level");
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height - 100, 100, 60), "Back"))
                    {
                        isLevelSelect = false;
                        isStart = true;
                    }

                    if (GUI.Button(new Rect(Screen.width / 2, Screen.height - 100, 170, 60), "Clear Save"))
                    {
                        PlayerPrefs.DeleteKey("level");
                    }
                }




            }
            else if (isCredits)
            {
                GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height), creditSheet);
                GUI.EndGroup();
                if (Screen.width == 960)
                {
                    GUI.skin.button.fontSize = 25;
                    if (GUI.Button(new Rect(Screen.width / 2 - 94, Screen.height / 2 - 106, 200, 50), "Daytona White"))
                    {
                        /* TODO: verify
						if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
							Application.ExternalEval("window.open('https://www.facebook.com/daytonawhiteband')");
						} else {
							
						}
						*/
                        Application.OpenURL("https://www.facebook.com/daytonawhiteband");
                    }

                    GUI.skin = pretty;
                    if (GUI.Button(new Rect(Screen.width / 2 - 170, Screen.height / 2 + 180, 170, 65), fbTexture))
                    {
                        /* TODO: verify
						if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
							Application.ExternalEval("window.open('https://www.facebook.com/itneverworksout')");
						} else {
							
							
						}
						*/
                        Application.OpenURL("https://www.facebook.com/itneverworksout");
                    }

                    if (GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 180, 170, 65), indieTexture))
                    {
                        /* TODO: verify
						if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
							Application.ExternalEval("window.open('http://indiedb.com/games/inwo')");
						} else {
						}
						*/
                        Application.OpenURL("http://indiedb.com/games/inwo");

                    }
                    GUI.skin = skin;
                    if (GUI.Button(new Rect(Screen.width / 2 - 28, Screen.height / 2 + 250, 80, 48), "Back"))
                    {
                        inwo.GetComponent<SpriteRenderer>().enabled = true;
                        back.GetComponent<SpriteRenderer>().enabled = true;

                        isStart = true;
                        isCredits = false;
                    }
                }
                else
                {
                    GUI.skin = skin;
                    if (GUI.Button(new Rect(Screen.width / 2 - 116, Screen.height / 2 - 144, 250, 65), "Daytona White"))
                    {
                        /* TODO: verify
						if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
							Application.ExternalEval("window.open('https://www.facebook.com/daytonawhiteband')");
						} else {
						}
						*/
                        Application.OpenURL("https://www.facebook.com/daytonawhiteband");
                    }

                    GUI.skin = pretty;
                    if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 220, 200, 65), fbTexture))
                    {
                        /* TODO: verify
						if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
							Application.ExternalEval("window.open('https://www.facebook.com/itneverworksout')");
						} else {
							
						}
						*/
                        Application.OpenURL("https://www.facebook.com/itneverworksout");
                    }

                    if (GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 220, 200, 65), indieTexture))
                    {
                        /* TODO: verify
						if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
							Application.ExternalEval("window.open('http://indiedb.com/games/inwo')");
						} else {}
						*/
                        Application.OpenURL("http://indiedb.com/games/inwo");
                    }
                    GUI.skin = skin;
                    if (GUI.Button(new Rect(Screen.width / 2 - 36, Screen.height / 2 + 300, 100, 66), "Back"))
                    {
                        inwo.GetComponent<SpriteRenderer>().enabled = true;
                        back.GetComponent<SpriteRenderer>().enabled = true;

                        isStart = true;
                        isCredits = false;
                    }
                }


            }
            else if (isTut)
            {
                if (Screen.width == 960)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 + 130, Screen.height / 2 + 150, 100, 60), "Back"))
                    {
                        isStart = true;
                        isCredits = false;
                        isTut = false;
                        credits.GetComponent<SpriteRenderer>().enabled = false;
                        tut.GetComponent<SpriteRenderer>().enabled = false;
                        inwo.GetComponent<SpriteRenderer>().enabled = true;
                        back.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width / 2 + 130, Screen.height / 2 + 200, 200, 60), "Back"))
                    {
                        isStart = true;
                        isCredits = false;
                        isTut = false;
                        credits.GetComponent<SpriteRenderer>().enabled = false;
                        tut.GetComponent<SpriteRenderer>().enabled = false;
                        inwo.GetComponent<SpriteRenderer>().enabled = true;
                        back.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }

            }





            /*GUI.BeginGroup (new Rect (Screen.width / 2 - 80, Screen.height / 2, 150, 190));
			GUI.Box (new Rect (0, 0, 150, 190), "");
			if (GUI.Button (new Rect (10, 10, 130, 20), "Tutorial")) {
				Application.LoadLevel ("Tutorial");
			}
			if (GUI.Button (new Rect (10, 40, 130, 20), "Fire Level")) {
				Application.LoadLevel ("FireLevel");
			}
			if (GUI.Button (new Rect (10, 70, 130, 20), "Techno Level")) {
				Application.LoadLevel ("TechnoLevel");
			}
			if (GUI.Button (new Rect (10, 100, 130, 20), "Nature Level")) {
				Application.LoadLevel ("NatureLevel");
			}

			if (GUI.Button(new Rect (10, 130, 130, 20), "Settings")) {
				GameObject.Find ("THE CAMERA").GetComponent<MetaControl>().ActivateMenu();
			}
			if (GUI.Button(new Rect (10, 160, 130, 20), "Exit Game")) {

				Application.Quit();
				
			}
			GUI.EndGroup ();*/

        }
    }

}
