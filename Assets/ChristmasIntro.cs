using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChristmasIntro : MonoBehaviour
{

    public GUISkin skin;
    public Texture startTexture;
    public Texture tutorialTexture;
    public Texture creditsTexture;
    public Texture nextTexture;
    public Texture preTexture;

    float centerX = Screen.width / 2 - 60;
    float centerY = Screen.height / 2;

    bool isStart = true;
    bool isTut1;
    bool isTut2;
    bool isCredits;


    void OnGUI()
    {
        if (skin != null)
        {
            GUI.skin = skin;
        }
        GUI.backgroundColor = Color.clear;

        if (isStart)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 10, 120, 60), startTexture))
                SceneManager.LoadScene("Intro");

            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 46, 120, 60), tutorialTexture))
            {
                transform.position = new Vector3(20, 0, -10);
                isStart = false;
                isTut1 = true;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 94, 120, 60), creditsTexture))
            {
                transform.position = new Vector3(-20, 0, -10);
                isStart = false;
                isCredits = true;
            }
        }

        if (isTut1)
        {
            if (GUI.Button(new Rect(Screen.width / 2 + 210, Screen.height / 2 + 173, 120, 60), nextTexture))
            {
                transform.position = new Vector3(40, 0, -10);
                isTut1 = false;
                isTut2 = true;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 330, Screen.height / 2 - 232, 120, 60), preTexture))
            {
                transform.position = new Vector3(0, 0, -10);
                isStart = true;
                isTut1 = false;
            }
        }

        if (isTut2)
        {
            if (GUI.Button(new Rect(Screen.width / 2 + 210, Screen.height / 2 + 168, 120, 60), startTexture))
            {
                SceneManager.LoadScene("Intro");
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 330, Screen.height / 2 - 212, 120, 60), preTexture))
            {
                transform.position = new Vector3(20, 0, -10);
                isTut1 = true;
                isTut2 = false;
            }
        }

        if (isCredits)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 330, Screen.height / 2 - 212, 120, 60), preTexture))
            {
                transform.position = new Vector3(0, 0, -10);
                isCredits = false;
                isStart = true;
            }
        }


        void moveToPoint(float beg, float end)
        {
            transform.position = new Vector3(transform.position.x + 5 * Time.deltaTime, 0, -10);
        }

        void LateUpdate()
        {
            if (Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }
        }


        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    