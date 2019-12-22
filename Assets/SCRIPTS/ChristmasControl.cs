using UnityEngine;
using System.Collections;

public class ChristmasControl : MonoBehaviour {
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
	private bool isGameStarted;
	public Texture2D credits;
	public Texture2D infoPage;
	public Texture2D controlsPage;
	public Texture2D btnStart;
	public Texture2D btnContinue;
	public Texture2D btnSettings;
	public Texture2D btnCredits;
	public Texture2D btnExit;
	public Texture2D btnNext;
	public Texture2D btnPrev;
	public Texture2D btnTutorial;
	public Texture2D btnRetry;

	
	
	private Color lowFPSColor = Color.red;
	private Color highFPSColor = Color.green;
	
	private int lowFPS = 30;
	private int highFPS = 50;
	
	private GameObject start;
	
	//private string url = "unity.html";
	
	private Color statColor = Color.white;
	/*
	string[] credits= {
		"Sex, Drugs and Game Development team:",
		"Anne Mirjam Kraav, Jana Levitina, Elmar Juzar",
		"Music by Swift Feed",
		"Copyright (c) 2013- thisgamesgonnabeawesome ltd."} ;*/
	public Texture[] crediticons;
	
	public enum Page {
		None, Main, Tutorial, Options, Credits, Scene
	}
	
	private Page currentPage;
	
	private float[] fpsarray;
	private float fps;
	
	private int toolbarInt = 0;
	private int tutorialInt = 0;
	private string[]  tutorialStrings =  {"WHAT DO I DO?","HOW DO I DO IT?"};
	private string[]  toolbarstrings =  {"Audio","Stats","System","Game"};
	
	
	void Start() {
		isGameStarted=true;

		
		fpsarray = new float[Screen.width];
		AudioListener.pause = false;
		Time.timeScale = 1;
		//PauseGame ();
		GetComponent<AudioSource>().clip = music[(int)Random.Range(0,2)];

		GetComponent<AudioSource>().Play();
	}
	
	void Update() {
		
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
	
	void ScrollFPS() {
		for (int x = 1; x < fpsarray.Length; ++x) {
			fpsarray[x-1]=fpsarray[x];
		}
		if (fps < 1000) {
			fpsarray[fpsarray.Length - 1]=fps;
		}
	}
	
	
	void LateUpdate () {
		if (showfps || showfpsgraph) {
			FPSUpdate();
		}

		if(Time.timeScale!=0 && !GetComponent<AudioSource>().isPlaying){
			GetComponent<AudioSource>().clip = music[(int)Random.Range(0,2)];
			GetComponent<AudioSource>().Play();
		}
		
		if (Input.GetKeyDown("escape")) 
		{
			ActivateMenu();
		} else if (Input.GetKeyDown("m")){
			muteMusic=!muteMusic;
		} else if (Input.GetKeyDown("c")){
			cameraRotation=!cameraRotation;
		}
	}
	
	public void ActivateMenu(){
		switch (currentPage) 
		{
		case Page.None: 
			PauseGame(); 
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
	
	void OnGUI () {
		if (skin != null) {
			GUI.skin = skin;
		}
		ShowStatNums();
		/*ShowLegal();*/
		CameraControls cr = GetComponent<CameraControls>();
		cr.toggleRotation(cameraRotation);
		GetComponent<AudioSource>().mute = muteMusic;
		GUI.color = statColor;
		switch (currentPage) {
		case Page.Main: MainPauseMenu(); break;
		case Page.Tutorial: ShowTutorial(); break;
		case Page.Options: ShowToolbar(); break;
		case Page.Credits: ShowCredits(); break;
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

	void ShowTutorial() {
		int tutWidth = (int)Screen.width/2;
		if(tutWidth<600)tutWidth=600;
		int tutHeight = (int)tutWidth/3*2;
		BeginPage(tutWidth,tutHeight);
		tutorialInt = GUILayout.Toolbar (tutorialInt, tutorialStrings);
		switch (tutorialInt) {
		case 0: Info(); break;
		case 1: Controls(); break;

		}
		EndPage();
	}

	void Info() {
		int tutWidth = (int)Screen.width/2;
		if(tutWidth<600)tutWidth=600;
		int tutHeight = (int)tutWidth/3*2;

		GUI.DrawTexture(new Rect(0,0, tutWidth, tutHeight), controlsPage);


		if(GUI.Button(new Rect(tutWidth-280, tutHeight-50, 130, 50), btnPrev) || 
		   GUI.Button(new Rect(tutWidth-140, tutHeight-50, 130, 50), btnNext) ){
			tutorialInt=1;
		}
	}
	
	void Controls() {
		int tutWidth = (int)Screen.width/2;
		if(tutWidth<600)tutWidth=600;
		int tutHeight = (int)tutWidth/3*2;
		GUI.DrawTexture(new Rect(0,0, tutWidth, tutHeight), infoPage);

		if(GUI.Button(new Rect(tutWidth-280, tutHeight-50, 130, 50), btnPrev) || 
		   GUI.Button(new Rect(tutWidth-140, tutHeight-50, 130, 50), btnNext) ){
			tutorialInt=0;
		}
	}


	void ShowToolbar() {
		BeginPage(300,300);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
		switch (toolbarInt) {
		case 0: VolumeControl(); break;
		case 1: StatControl(); break;
		case 2: ShowDevice(); break;
		case 3: ShowGameOptions(); break;
			
		}
		EndPage();
	}
	

	void ShowCredits() {
		int tutWidth = (int)Screen.width/2;
		int tutHeight = (int)tutWidth+tutWidth/20;
		BeginPage(tutWidth,tutHeight);
		/*foreach(string credit in credits) {
			GUILayout.Label(credit);
		}
		foreach( Texture credit in crediticons) {
			GUILayout.Label(credit);
		}*/
		if(GUI.Button(new Rect(0,0, tutWidth, tutHeight), credits)){
			currentPage = Page.Main;
		}

		EndPage();
	}
	
	void ShowBackButton() {
		if (GUI.Button(new Rect(20, Screen.height - 50, 50, 130), btnPrev)) {
			currentPage = Page.Main;
		}
	}
	
	
	
	void ShowDevice() {
		GUILayout.Label("Unity player version "+Application.unityVersion);
		GUILayout.Label("Graphics: "+SystemInfo.graphicsDeviceName+" "+
		                SystemInfo.graphicsMemorySize+"MB\n"+
		                SystemInfo.graphicsDeviceVersion+"\n"+
		                SystemInfo.graphicsDeviceVendor);
		GUILayout.Label("Shadows: "+SystemInfo.supportsShadows);
		GUILayout.Label("Image Effects: "+SystemInfo.supportsImageEffects);
		GUILayout.Label("Render Textures: "+SystemInfo.supportsRenderTextures);
	}
	
	
	void VolumeControl() {
		GUILayout.Label("Volume");
		AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
		muteMusic = GUILayout.Toggle(muteMusic, "Mute Music");
	}
	
	void ShowGameOptions() {
		GUILayout.BeginVertical();
		cameraRotation = GUILayout.Toggle(cameraRotation, "Camera Rotation");
		GUILayout.EndVertical();
	}

	
	void StatControl() {
		GUILayout.BeginHorizontal();
		showfps = GUILayout.Toggle(showfps,"FPS");
		showtris = GUILayout.Toggle(showtris,"Triangles");
		showvtx = GUILayout.Toggle(showvtx,"Vertices");
		showfpsgraph = GUILayout.Toggle(showfpsgraph,"FPS Graph");
		GUILayout.EndHorizontal();
	}
	
	void FPSUpdate() {
		float delta = Time.smoothDeltaTime;
		if (!IsGamePaused() && delta !=0.0) {
			fps = 1 / delta;
		}
	}
	
	
	
	void ShowStatNums() {
		GUILayout.BeginArea( new Rect(Screen.width - 100, 10, 100, 200));
		if (showfps) {
			string fpsstring= fps.ToString ("#,##0 fps");
			GUI.color = Color.Lerp(lowFPSColor, highFPSColor,(fps-lowFPS)/(highFPS-lowFPS));
			GUILayout.Label (fpsstring);
		}
		if (showtris || showvtx) {
			GetObjectStats();
			GUI.color = statColor;
		}
		if (showtris) {
			GUILayout.Label (tris+"tri");
		}
		if (showvtx) {
			GUILayout.Label (verts+"vtx");
		}
		GUILayout.EndArea();
	}
	
	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
		if (currentPage != Page.Main) {
			ShowBackButton();
		}
	}
	
	bool IsBeginning() {
		return (Time.time < startTime);
	}
	
	void MainStartMenu() {
	    BeginPage(200,600);
	    if (GUILayout.Button (IsBeginning() ? btnStart: btnContinue)) {
	        UnPauseGame();
 
	    }
	    if (GUILayout.Button (btnSettings)) {
	        currentPage = Page.Options;
	    }
		if (GUILayout.Button (btnTutorial)) {
			currentPage = Page.Tutorial;
		}
	    if (GUILayout.Button (btnCredits)) {
	        currentPage = Page.Credits;
	    }

		if (GUILayout.Button (btnExit)) {
			Application.Quit();
		}
	    EndPage();
	}
	
	void MainPauseMenu() {
		BeginPage(200,600);
		if (GUILayout.Button (IsBeginning() ? btnStart: btnContinue)) {
			UnPauseGame();
		}
		if (GUILayout.Button (btnTutorial)) {
			currentPage = Page.Tutorial;
		}
		if (GUILayout.Button (btnSettings)) {
			currentPage = Page.Options;
		}
		if (GUILayout.Button (btnCredits)) {
			currentPage = Page.Credits;
		}
		/*if (IsBrowser() && !IsBeginning() && GUILayout.Button ("Restart")) {
	        Application.OpenURL(url);
	    }*/
		/*if (GUILayout.Button ("Title Menu")) {
			SceneManager.LoadScene("Intro");
		}*/
		if (GUILayout.Button (btnExit)) {
			Application.Quit();
		}
		EndPage();
	}
	
	void GetObjectStats() {
		verts = 0;
		tris = 0;
		GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach (GameObject obj in ob) {
			GetObjectStats(obj);
		}
	}
	
	void GetObjectStats(GameObject obj) {
		Component[] filters;
		filters = obj.GetComponentsInChildren<MeshFilter>();
		foreach( MeshFilter f  in filters )
		{
			tris += f.sharedMesh.triangles.Length/3;
			verts += f.sharedMesh.vertexCount;
		}
	}
	
	void PauseGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
		currentPage = Page.Main;

	}
	
	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		currentPage = Page.None;
		if (IsBeginning() && start != null) {
			start.active = true;
		}
	}
	
	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}
	
	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}
}
