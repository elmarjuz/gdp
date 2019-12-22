using UnityEngine;
using System.Collections;

public class OUImothership : MonoBehaviour
{
    PublicData data;
    GameObject mothership;
    GameObject rightArm;
    GameObject leftArm;
    public GameObject rightOUI;
    public GameObject leftOUI;
    public GameObject coreOUI;

    GameObject theCore;
    GameObject theLeft;
    GameObject theRight;

    Vector3 modifier;

    float scaleMod;

    bool right;
    bool core;
    bool left;


    // Use this for initialization
    void Start()
    {
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        mothership = GameObject.Find("MOTHERSHIP");
        scaleMod = transform.parent.localScale.x;

        leftArm = mothership.transform.Find("leftarm").gameObject;
        rightArm = mothership.transform.Find("rightarm").gameObject;
        /*
		leftOUI = transform.FindChild("leftOUI").gameObject;
		coreOUI = transform.FindChild("midOUI").gameObject;
		rightOUI = transform.FindChild("rightOUI").gameObject;
*/
        right = data.right;
        core = data.core;
        left = data.left;

        if (left) enableLeft();
        if (core) enableCore();
        if (right) enableRight();
    }

    public void enableLeft()
    {
        modifier = new Vector3(-2.5f * scaleMod, -0.2f, 0);
        theLeft = Instantiate(leftOUI, transform.position + modifier, transform.rotation) as GameObject;
        theLeft.transform.parent = transform;
        theLeft.transform.localScale *= scaleMod;
        //theLeft.transform.localScale = transform.parent.localScale;
        //leftOUI.GetComponent<LeftGUI>().enabled = true;
    }

    public void enableCore()
    {
        modifier = new Vector3(0, 0, 0);
        theCore = Instantiate(coreOUI, transform.position + modifier, transform.rotation) as GameObject;
        theCore.transform.parent = transform;
        theCore.transform.localScale *= scaleMod;
        //theCore.transform.localScale = transform.parent.localScale;

        //coreOUI.GetComponent<coreGUI>().enabled = true;
    }

    public void enableRight()
    {
        modifier = new Vector3(2.5f * scaleMod, -0.2f, 0);
        theRight = Instantiate(rightOUI, transform.position + modifier, transform.rotation) as GameObject;
        theRight.transform.parent = transform;
        theRight.transform.localScale *= scaleMod;
        //theRight.transform.localScale = transform.parent.localScale;

    }



    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = mothership.transform.position;
        newPos.z = -1;
        transform.position = newPos;


        transform.rotation = transform.parent.transform.rotation;
        /*leftOUI.transform.position = leftArm.transform.position;
		leftOUI.transform.rotation = leftArm.transform.rotation;
		rightOUI.transform.position = rightArm.transform.position;
		rightOUI.transform.rotation = rightArm.transform.rotation;*/
    }
}
