using UnityEngine;
using System.Collections;

public class GUIright : MonoBehaviour
{
    public GameObject target;
    GameObject lilShip;
    GameObject theShield;
    GameObject mothership;
    PublicData data;
    Transform parent;
    Vector3 targetPosition;
    bool isTargeting;

    float shipAngle;
    float mouseAngle;
    float finalAngle;
    public float degreeLimit;
    public float charge;
    public float maxCharge;
    float recharge;
    /*
	float ocX;
	float ocY;
	float nX;
	float nY;*/

    bool ranOut;

    Transform locationBase;
    float scaleMod;
    Vector3 localPos;


    // Use this for initialization
    void Start()
    {
        locationBase = transform.parent.parent.Find("rightarm");
        scaleMod = transform.parent.parent.localScale.x;
        localPos = new Vector3(0, 0, -2);

        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        GetComponent<SpriteRenderer>().enabled = false;
        lilShip = GameObject.Find("LILSHIP");
        mothership = transform.parent.parent.gameObject;
        maxCharge = data.shipBurstMaxCharge;
        charge = maxCharge;
        recharge = data.shipBurstRecharge;
        data.shipShieldChargePercent = charge / maxCharge;

    }

    void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
    void OnMouseExit()
    {
        if (!isTargeting)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnMouseDown()
    {
        if (charge > 0 && !ranOut)
        {
            isTargeting = true;
            Plane playerPlane = new Plane(Vector3.forward, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //moved this check under keydown to avoid the constant turning after the mouse.
                if (playerPlane.Raycast(ray, out hitdist))
                {
                    targetPosition = ray.GetPoint(hitdist);
                }
            }
            lilShip.GetComponent<LilShipControls>().enabled = false;

            theShield = Instantiate(target, mothership.transform.position, transform.rotation) as GameObject;
            theShield.transform.parent = mothership.transform;
            theShield.name = "theShield";
        }
        else
        {


        }


    }

    void NewAngles()
    {

        //this was pretty wrong, the angle kept changing with movement
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        mouseAngle = Mathf.Atan2(relative.x, relative.y) * 180 / Mathf.PI;
        shipAngle = theShield.transform.parent.GetComponent<MotherShipRotation>().angle;
        /*if(mouseAngle<0 && mouseAngle>-degreeLimit)mouseAngle=-degreeLimit;
		if(mouseAngle>0 && mouseAngle<degreeLimit)mouseAngle=degreeLimit;
		*/
        finalAngle = -mouseAngle + shipAngle - 90;
    }

    void OperateAShield()
    {
        NewAngles();
        theShield.transform.eulerAngles = new Vector3(0, 0, finalAngle);

        float sizeMod = -Mathf.Cos(mouseAngle / 60);
        if (sizeMod < 0) sizeMod = -sizeMod;
        if (sizeMod < 0.5) sizeMod = 0.5f;
        theShield.transform.localScale = new Vector3(sizeMod, sizeMod, 1);
        float posModX = Mathf.Sin(mouseAngle / 60) * 5;
        float posModY = Mathf.Cos(mouseAngle / 60);
        theShield.transform.localPosition = new Vector3(posModX, posModY);


        /*
		if(angle >= 45 && angle < 135) {

			//theShield.transform.eulerAngles = new Vector3 (0, 0, 0);
			theShield.transform.localScale = new Vector3(2,2,1);

			
			theShield.transform.position = parent.transform.position;
		}
		
		if(angle >= 135 && angle <= 180 || angle >= -180 && angle < -135) {
			//theShield.transform.eulerAngles = new Vector3 (0, 0, 90);
			theShield.transform.localScale = new Vector3(0.5f,0.5f,1);
			
			Vector3 newPosition = transform.position;
		
			nX=ocX+3*Mathf.Sin(Mathf.PI * (270+shipAngle) / 180);
			nY=ocY;
			newPosition.x=nX;
			newPosition.y=nY;
			theShield.transform.localPosition=newPosition;
		}
		if(angle >= -135 && angle < -45) {
			//theShield.transform.eulerAngles = new Vector3 (0, 0, 180);
			theShield.transform.localScale = new Vector3(2,2,1);
			
			theShield.transform.position = parent.transform.position;
			
		}
		
		if(angle >= -45 && angle < 0 || angle >= 0 && angle < 45) {
			//theShield.transform.eulerAngles = new Vector3 (0, 0, 270);
			theShield.transform.localScale = new Vector3(0.5f,0.5f,1);
			
			Vector3 newPosition = transform.position;
			nX=ocX-3*Mathf.Sin(Mathf.PI * (270+shipAngle) / 180);
			nY=ocY;
			newPosition.x=nX;
			newPosition.y=nY;
			theShield.transform.localPosition=newPosition;
		}
	*/
    }

    void OnMouseUp()
    {
        if (isTargeting)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            isTargeting = false;
            Destroy(theShield);
            lilShip.GetComponent<LilShipControls>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = locationBase.position + localPos;
        transform.rotation = locationBase.rotation;


        if (charge != maxCharge && charge != 0)
        {
            data.shipShieldChargePercent = charge / maxCharge;
        }
        if (theShield == null)
        {
            if (charge < maxCharge)
            {
                charge += recharge * Time.deltaTime;
                if (charge >= maxCharge)
                {
                    charge = maxCharge;
                    ranOut = false;
                }
            }
        }
        if (theShield != null)
        {
            OperateAShield();
        }
        if (isTargeting)
        {
            if (charge > 0)
            {
                charge -= 0.3f * Time.deltaTime;
                Plane playerPlane = new Plane(Vector3.forward, transform.position);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float hitdist = 0;
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    //moved this check under keydown to avoid the constant turning after the mouse.
                    if (playerPlane.Raycast(ray, out hitdist))
                    {
                        targetPosition = ray.GetPoint(hitdist);
                    }
                }

            }
            else
            {
                isTargeting = false;
                ranOut = true;
                Destroy(theShield);
            }

        }
    }

}
