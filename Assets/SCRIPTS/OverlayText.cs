using UnityEngine;
using System.Collections;

public class OverlayText : MonoBehaviour
{

    public bool follow;
    private GameObject msgWrapper;

    Vector3 screenPoint;
    Vector3 eventPosition;
    string displayMessage;
    bool isShowing;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            if (isShowing)
            {
                screenPoint = Camera.main.WorldToScreenPoint(eventPosition);
            }
        }

    }

    void DisplayOverlay(string message)
    {
        /*msgWrapper = new GameObject("Message Wrapper");
		GUIText overlayMsg = (GUIText)msgWrapper.AddComponent(typeof(GUIText));
		//Vector3 offset = new Vector3(0,0.3f);
		overlayMsg.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		overlayMsg.fontSize = 20;
		overlayMsg.text = message;*/
        displayMessage = message;
        eventPosition = transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(eventPosition);
        isShowing = true;
        StartCoroutine(KillMessage());


    }
    void OnGUI()
    {
        if (isShowing)
        {
            GUI.Label(new Rect(Screen.width - screenPoint.x - 50, Screen.height - screenPoint.y, 100, 20), displayMessage);
        }
    }

    IEnumerator KillMessage()
    {
        yield return new WaitForSeconds(3);
        isShowing = false;
    }
}
