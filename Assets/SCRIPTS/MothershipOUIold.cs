using UnityEngine;
using System.Collections;

public class MothershipOUIold : MonoBehaviour
{
    PublicData data;
    GameObject mothership;
    GameObject rightArm;
    GameObject leftArm;
    GameObject rightOUI;
    GameObject leftOUI;
    GameObject coreOUI;

    public bool right;
    public bool core;
    public bool left;


    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        mothership = GameObject.Find("MOTHERSHIP");

        leftArm = mothership.transform.Find("leftarm").gameObject;
        rightArm = mothership.transform.Find("rightarm").gameObject;

        leftOUI = transform.Find("leftOUI").gameObject;
        coreOUI = transform.Find("midOUI").gameObject;
        rightOUI = transform.Find("rightOUI").gameObject;

        right = data.right;
        core = data.core;
        left = data.left;

        if (left) enableLeft();
        if (core) enableCore();
        if (right) enableRight();
    }

    public void enableLeft()
    {
        leftOUI.GetComponent<GUIleft>().enabled = true;
    }

    public void enableCore()
    {
        coreOUI.GetComponent<GUIcore>().enabled = true;
    }

    public void enableRight()
    {
        rightOUI.GetComponent<GUIright>().enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = mothership.transform.position;
        transform.rotation = mothership.transform.rotation;
        /*leftOUI.transform.position = leftArm.transform.position;
		leftOUI.transform.rotation = leftArm.transform.rotation;
		rightOUI.transform.position = rightArm.transform.position;
		rightOUI.transform.rotation = rightArm.transform.rotation;*/
    }
}
