using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MetaControl : MonoBehaviour
{
    public GUISkin skin;


    public bool cameraRotation = true;
    public bool muteMusic;
    public AudioClip[] music = new AudioClip[2];

    private GameObject theShip;
    private float gldepth = -0.5f;
    private float startTime = 0.1f;
    private long tris = 0;
    private long verts = 0;
    private float savedTimeScale;

    private bool showfps;
    private bool showtris;
    private bool showvtx;
    private bool showfpsgraph;
    //private bool isGameStarted;


    public Texture continueTexture;
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
    public Texture retryTexture;

    public GUISkin pretty;


    public Texture creditSheet;
    public Texture tutorialSheet;
    public Texture backSheet;
    public Texture storyBack;

    private Color lowFPSColor = Color.red;
    private Color highFPSColor = Color.green;

    private int lowFPS = 30;
    private int highFPS = 50;

    private GameObject start;

    private string url = "unity.html";

    private Color statColor = Color.white;

    string[] credits = {
        "Sex, Drugs and Game Development team:",
        "Anne Mirjam Kraav, Jana Levitina, Elmar Juzar",
        "Music by Swift Feed",
        "Copyright (c) 2013- thisgamesgonnabeawesome ltd."};
    public Texture[] crediticons;

    public enum Page
    {
        None, Main, Options, Credits, Scene, Tutorial
    }

    private Page currentPage;

    private float[] fpsarray;
    private float fps;

    private int toolbarInt = 0;
    private string[] toolbarstrings = { "Audio", "Stats", "System", "Game" };

    string text;

    bool isBegin;


    void Start()
    {
        //isGameStarted=false;
        if (SceneManager.GetActiveScene().name == "Intro")
        {
            GetComponent<CameraControls>().enabled = false;
            GetComponent<AudioSource>().clip = music[0];
            GetComponent<AudioSource>().Play();
        }
        else if (SceneManager.GetActiveScene().name == "ChristmasArena")
        {
            GetComponent<CameraControls>().enabled = false;
            GetComponent<AudioSource>().clip = music[2];
            GetComponent<AudioSource>().Play();

        }
        else
        {
            GetComponent<CameraControls>().enabled = true;
            GetComponent<AudioSource>().clip = music[1];
            GetComponent<AudioSource>().Play();
        }

        fpsarray = new float[Screen.width];
        AudioListener.pause = false;
        Time.timeScale = 1;
        //PauseGame ();
        isBegin = true;

        switch (SceneManager.GetActiveScene().name)
        {
            case "FireLevel":
                text = "Gaining control of the machine it quickly became obvious that the ship wouldn't be able to house the surviving Inwonians for a long time. " +
                    "Having nowhere to go, yet desperate to survive, the nomads decided to try and seek help at the nearest inhabited planet the mothership's navigation could locate. " +
                    "Arriving there, however, they quickly discovered that their presence was not welcome. " +
                    "Having no way to communicate with an alien species and not enough resources to survive another voyage, they were left with only one option: " +
                        "to force themselves onto the Fire Planet.";
                break;
            case "TechnoLevel":
                text = "Inwonian biology turned out to be too fragile for the raw and furious Fire Planet. " +
                    "A few years of immeasurable death and injury provided by the planet's environment forced the homeless species to refuel the ship, " +
                    "restock the supplies as best they could and leave for the next closest neighbour planet, seemingly a much more refined place. " +
                    "The slaver ship was immediately recognized as a threat and, being unable to resolve the misunderstanding through the radio chatter " +
                    "of incomprehensible alien gibberish coming from the Techno Planet, once again the Inwonians found themselves battling for survival.";
                break;
            case "NatureLevel":
                text = "Techno planet was a very organized and civilized place. " +
                    "Its inhabitants did not, however, welcome the alien invaders and did everything they could to make the Inwonians leave. " +
                    "The string of large-scale conflicts lasted for over a decade, slowly destroying the planet and exahusting its resources. " +
                    "The fatigued nomad species, now hardened by nearly constant war and conflict, having lost much of its innate naivete, " +
                    "had given up and left the planet, picking a more innocent, more basic place to try and claim as its own." +
                    "The Nature Planet's local flora, however, was not too eager to welcome the intruders.";
                break;
            case "Tutorial":
                isBegin = false;
                text = "Having followed the slaver ship back to its resting station, the Lil'ship's crew " +
                    "decided to act upon the almost completely defenseless mechanical monstrosity.";
                break;
            default:
                isBegin = false;
                break;
        }


    }


    /*void OnPostRender() {
	    if (showfpsgraph && mat != null) {
	        GL.PushMatrix ();
	        GL.LoadPixelMatrix();
	        for (var i = 0; i < mat.passCount; ++i)
	        {
	            mat.SetPass(i);
	            GL.Begin( GL.LINES );
	            for (int x=0; x < fpsarray.Length; ++x) {
	                GL.Vertex3(x, fpsarray[x], gldepth);
	            }
	        GL.End();
	        }
	        GL.PopMatrix();
	        ScrollFPS();
	    }
	}*/

    void ScrollFPS()
    {
        for (int x = 1; x < fpsarray.Length; ++x)
        {
            fpsarray[x - 1] = fpsarray[x];
        }
        if (fps < 1000)
        {
            fpsarray[fpsarray.Length - 1] = fps;
        }
    }


    void Update()
    {
        if (isBegin)
        {
            if (Input.GetKeyDown("return") || Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.Escape))
            {
                isBegin = false;
                Time.timeScale = 1;
            }
        }
    }


    void LateUpdate()
    {
        if (showfps || showfpsgraph)
        {
            FPSUpdate();
        }

        if (Input.GetKeyDown("escape"))
        {
            if (SceneManager.GetActiveScene().name != "Intro")
            {
                ActivateMenu();
            }

        }
        else if (Input.GetKeyDown("m"))
        {
            muteMusic = !muteMusic;
        }
        else if (Input.GetKeyDown("c"))
        {
            //cameraRotation=!cameraRotation;
        }
        else if ((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) && currentPage != Page.Main && currentPage != Page.None)
        {
            currentPage = Page.Main;
        }
    }

    public void ActivateMenu()
    {
        switch (currentPage)
        {
            case Page.None:
                if (Time.timeScale != 0) PauseGame();
                break;

            case Page.Main:
                if (!IsBeginning())
                    UnPauseGame();
                break;

            default:
                currentPage = Page.Main;
                break;
        }
    }

    void OnGUI()
    {
        if (skin != null)
        {
            GUI.skin = skin;

        }
        GUI.skin.label.fontSize = 18;
        ShowStatNums();
        /*ShowLegal();*/
        CameraControls cr = GetComponent<CameraControls>();
        cr.toggleRotation(cameraRotation);
        GetComponent<AudioSource>().mute = muteMusic;
        GUI.color = statColor;
        switch (currentPage)
        {
            case Page.Main: MainPauseMenu(); break;
            case Page.Options: VolumeControl(); break;
            case Page.Scene: LoadScene(); break;
            case Page.Credits: ShowCredits(); break;
            case Page.Tutorial: ShowTutorial(); break;
        }

        if (isBegin)
        {
            Time.timeScale = 0;
            //GUI.Label(new Rect((Screen.width-600)/2, (Screen.height-500)/2, 600, 500), storyBack);
            GUI.Label(new Rect((Screen.width - 550) / 2, (Screen.height - 430) / 2, 550, 430), text);


            if (GUI.Button(new Rect((Screen.width - 220) / 2, (Screen.height + 250) / 2, 220, 50), "Start"))
            {
                isBegin = false;
                Time.timeScale = 1;
            };
        }
        /*if(!isGameStarted) { 

			GUI.color = statColor;
	        switch (currentPage) {
	            case Page.Main: MainStartMenu(); break;
	            case Page.Options: ShowToolbar(); break;
	            case Page.Credits: ShowCredits(); break;
			}
			isGameStarted = true;
		} else if (IsGamePaused()) {*/

        //}   
    }



    void ShowToolbar()
    {
        BeginPage(300, 300);
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarstrings);
        switch (toolbarInt)
        {
            case 0: VolumeControl(); break;
            case 1: StatControl(); break;
            case 2: ShowDevice(); break;
            case 3: ShowGameOptions(); break;

        }
        EndPage();
    }

    void LoadScene()
    {
        BeginPage(300, 300);
        if (GUILayout.Button("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (GUILayout.Button("Intro"))
        {
            SceneManager.LoadScene("Intro");
        }
        if (GUILayout.Button("Tutorial"))
        {
            SceneManager.LoadScene("Tutorial");
        }
        if (GUILayout.Button("Fire Level"))
        {
            SceneManager.LoadScene("FireLevel");
        }
        if (GUILayout.Button("Techno Level"))
        {
            SceneManager.LoadScene("TechnoLevel");
        }
        if (GUILayout.Button("Nature Level"))
        {
            SceneManager.LoadScene("NatureLevel");
        }
        if (GUILayout.Button("TEST SCENE"))
        {
            SceneManager.LoadScene("testscene");
        }

        EndPage();
    }

    void ShowCredits()
    {
        BeginPage(550, 700);
        GUILayout.Label(creditSheet);
        EndPage();
        int size = GUI.skin.button.fontSize;
        GUI.skin.button.fontSize = 27;
        if (GUI.Button(new Rect(Screen.width / 2 - 102, Screen.height / 2 - 135, 220, 50), "Daytona White"))
        {
            /* TODO: verify
			if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
				Application.ExternalEval("window.open('https://www.facebook.com/daytonawhiteband')");
			} else {
				
			}*/

            Application.OpenURL("https://www.facebook.com/daytonawhiteband");
            GUI.skin.button.fontSize = size;
        }
        GUI.skin.button.fontSize = size;
        GUI.skin = pretty;
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 185, 200, 65), fbTexture))
        {
            /* TODO: verify
			if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
				Application.ExternalEval("window.open('http://www.facebook.com/itneverworksout')");
			} else {
				
			}
			*/
            Application.OpenURL("http://www.facebook.com/itneverworksout");
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 185, 200, 65), indieTexture))
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

        if (GUI.Button(new Rect(Screen.width / 2 - 29, Screen.height / 2 + 260, 80, 45), "Back"))
        {
            currentPage = Page.Main;
        }

    }

    void ShowTutorial()
    {
        BeginPage(500, 500);
        GUILayout.Label(tutorialSheet);
        if (GUI.Button(new Rect(340, 380, 120, 60), "Back"))
        {
            currentPage = Page.Main;
        }
        EndPage();
    }

    /*void ShowBackButton() {
		if (GUI.Button(new Rect(Screen.width/2-24, Screen.height/2+154, 60, 30),"Back")) {
	        currentPage = Page.Main;
	    }
	}*/



    void ShowDevice()
    {
        GUILayout.Label("Unity player version " + Application.unityVersion);
        GUILayout.Label("Graphics: " + SystemInfo.graphicsDeviceName + " " +
        SystemInfo.graphicsMemorySize + "MB\n" +
        SystemInfo.graphicsDeviceVersion + "\n" +
        SystemInfo.graphicsDeviceVendor);
        GUILayout.Label("Shadows: " + SystemInfo.supportsShadows);
        GUILayout.Label("Image Effects: " + SystemInfo.supportsImageEffects);
        GUILayout.Label("Render Textures: " + SystemInfo.supportsRenderTextures);
    }


    public float getVolume()
    {
        return AudioListener.volume;
    }

    public void setVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void VolumeControl()
    {
        GUI.Label(new Rect((Screen.width - 340) / 2, (Screen.height - 440) / 2, 340, 440), backSheet);
        BeginPage(120, 100);
        GUILayout.Label(volumeTexture);
        AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
        muteMusic = GUILayout.Toggle(muteMusic, muteTexture);

        EndPage();
        int size = GUI.skin.button.fontSize;
        GUI.skin.button.fontSize = 20;
        if (GUI.Button(new Rect(Screen.width / 2 - 23, Screen.height / 2 + 159, 58, 30), "Back"))
        {
            currentPage = Page.Main;
            GUI.skin.button.fontSize = size;
        }
        GUI.skin.button.fontSize = size;

    }

    void ShowGameOptions()
    {
        GUILayout.BeginVertical();
        cameraRotation = GUILayout.Toggle(cameraRotation, "Camera Rotation");
        GUILayout.EndVertical();
    }


    void StatControl()
    {
        GUILayout.BeginHorizontal();
        showfps = GUILayout.Toggle(showfps, "FPS");
        showtris = GUILayout.Toggle(showtris, "Triangles");
        showvtx = GUILayout.Toggle(showvtx, "Vertices");
        showfpsgraph = GUILayout.Toggle(showfpsgraph, "FPS Graph");
        GUILayout.EndHorizontal();
    }

    void FPSUpdate()
    {
        float delta = Time.smoothDeltaTime;
        if (!IsGamePaused() && delta != 0.0)
        {
            fps = 1 / delta;
        }
    }



    void ShowStatNums()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 100, 10, 100, 200));
        if (showfps)
        {
            string fpsstring = fps.ToString("#,##0 fps");
            GUI.color = Color.Lerp(lowFPSColor, highFPSColor, (fps - lowFPS) / (highFPS - lowFPS));
            GUILayout.Label(fpsstring);
        }
        if (showtris || showvtx)
        {
            GetObjectStats();
            GUI.color = statColor;
        }
        if (showtris)
        {
            GUILayout.Label(tris + "tri");
        }
        if (showvtx)
        {
            GUILayout.Label(verts + "vtx");
        }
        GUILayout.EndArea();
    }

    void BeginPage(int width, int height)
    {
        GUILayout.BeginArea(new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
    }

    void EndPage()
    {
        GUILayout.EndArea();
        /*if (currentPage == Page.Options) {
	        ShowBackButton();
		}*/
    }

    bool IsBeginning()
    {
        return (Time.time < startTime);
    }

    /*void MainStartMenu() {
	    BeginPage(200,200);
	    if (GUILayout.Button (IsBeginning() ? "Start Game" : "Continue")) {
	        UnPauseGame();
 
	    }
	    if (GUILayout.Button ("Options")) {
	        currentPage = Page.Options;
	    }
	    if (GUILayout.Button ("Credits")) {
	        currentPage = Page.Credits;
	    }
	    EndPage();
	}*/

    void MainPauseMenu()
    {
        GUI.Label(new Rect((Screen.width - 340) / 2, (Screen.height - 440) / 2, 340, 440), backSheet);
        //BeginPage(200,600);
        GUI.BeginGroup(new Rect((Screen.width - 134) / 2, (Screen.height - 250) / 2, 300, 750));

        if (GUILayout.Button("Continue"))
        {
            UnPauseGame();
        }
        if (GUILayout.Button("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (GUILayout.Button("Audio"))
        {
            currentPage = Page.Options;
        }

        if (GUILayout.Button("Tutorial"))
        {
            currentPage = Page.Tutorial;
        }

        if (GUILayout.Button("Credits"))
        {
            currentPage = Page.Credits;
        }
        /*if (IsBrowser() && !IsBeginning() && GUILayout.Button ("Restart")) {
	        Application.OpenURL(url);
	    }*/
        /*if (GUILayout.Button ("Scene")) {
			currentPage = Page.Scene;
		}*/


        /*if (GUILayout.Button (titleTexture)) {
			SceneManager.LoadScene("Intro");
		}
		if (GUILayout.Button (exitTexture)) {
			Application.Quit();
		}*/
        GUI.EndGroup();
        //EndPage();

        int size = GUI.skin.button.fontSize;
        GUI.skin.button.fontSize = 22;
        if (GUI.Button(new Rect(Screen.width / 2 - 23, Screen.height / 2 + 159, 58, 30), "Exit"))
        {
            SceneManager.LoadScene("Intro");
            GUI.skin.button.fontSize = size;
        }
        GUI.skin.button.fontSize = size;

    }

    void GetObjectStats()
    {
        verts = 0;
        tris = 0;
        GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in ob)
        {
            GetObjectStats(obj);
        }
    }

    void GetObjectStats(GameObject obj)
    {
        Component[] filters;
        filters = obj.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter f in filters)
        {
            tris += f.sharedMesh.triangles.Length / 3;
            verts += f.sharedMesh.vertexCount;
        }
    }

    void PauseGame()
    {

        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
        AudioListener.pause = true;
        currentPage = Page.Main;
    }

    void UnPauseGame()
    {

        Time.timeScale = savedTimeScale;
        AudioListener.pause = false;
        currentPage = Page.None;
        if (IsBeginning() && start != null)
        {
            start.active = true;
        }
    }

    bool IsGamePaused()
    {
        return (Time.timeScale == 0);
    }

    void OnApplicationPause(bool pause)
    {
        if (IsGamePaused())
        {
            AudioListener.pause = true;
        }
    }
}
