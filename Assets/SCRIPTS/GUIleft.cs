using UnityEngine;
using System.Collections;

public class GUIleft : MonoBehaviour
{
    public GameObject target;
    public GameObject laser;
    GameObject lilShip;
    GameObject currTarget;
    PublicData data;
    GameObject shotLaser;

    Vector3 targetPosition;

    bool isTargeting;
    bool isCharged;

    float damage;
    float delay;
    float mouseAngle;
    float shipAngle;
    float finalAngle;

    public float charge;
    public float maxCharge;
    float recharge;
    public float degreeLimit = 130;

    bool ranOut;

    Transform locationBase;
    float scaleMod;
    Vector3 localPos;


    Transform parent;

    // Use this for initialization
    void Start()
    {
        locationBase = transform.parent.parent.Find("leftarm");
        scaleMod = transform.parent.localScale.x;
        localPos = new Vector3(0, 0, -2);

        GetComponent<SpriteRenderer>().enabled = false;
        lilShip = GameObject.Find("LILSHIP");
        data = GameObject.Find("DataHolder").GetComponent<PublicData>();
        damage = data.motherShipDamage;
        delay = data.motherShipLaserDelay;
        isCharged = true;

        maxCharge = data.shipBurstMaxCharge;
        charge = maxCharge;
        recharge = data.shipBurstRecharge;
        data.shipBurstChargePercent = charge / maxCharge;


    }

    void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().enabled = false;

    }


    void OnMouseDown()
    {
        if (isCharged)
        {
            isTargeting = true;
            /*Plane playerPlane = new Plane (Vector3.forward, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float hitdist = 0;
			if (Input.GetKey (KeyCode.Mouse0)) {
				//moved this check under keydown to avoid the constant turning after the mouse.
				if (playerPlane.Raycast (ray, out hitdist)) {
					targetPosition = ray.GetPoint (hitdist);	
				}
			}
			currTarget = Instantiate(target, targetPosition, transform.rotation) as GameObject;*/
            FireLaser();
        }
        lilShip.GetComponent<LilShipControls>().enabled = false;

    }

    void OnMouseUp()
    {
        StartCoroutine(WaitAndKill());
        isTargeting = false;
        lilShip.GetComponent<LilShipControls>().enabled = true;

        /*if(isCharged && shotLaser==null){
			GetComponent<SpriteRenderer>().enabled = false;
			isTargeting = false;
			Destroy(currTarget);
			lilShip.GetComponent<LilShipControls>().enabled = true;

			FireLaser();
			isCharged = false;
			StartCoroutine(Wait ());
		} else if (shotLaser!=null){
			GetComponent<SpriteRenderer>().enabled = false;
			isTargeting = false;
			Destroy(currTarget);
			NewAngles();
			shotLaser.transform.eulerAngles = new Vector3 (0, 0, finalAngle);

		}*/
    }

    void FireLaser()
    {
        NewAngles();

        shotLaser = Instantiate(laser, transform.position, transform.rotation) as GameObject;
        shotLaser.name = "theMotherShot";
        shotLaser.transform.parent = transform.parent.parent;
        shotLaser.GetComponent<CommonBulletStuff>().setDamage(damage);

        shotLaser.transform.eulerAngles = new Vector3(0, 0, finalAngle);

    }

    void NewAngles()
    {
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
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        mouseAngle = Mathf.Atan2(relative.x, relative.y) * 180 / Mathf.PI;
        shipAngle = data.motherShipAngle;
        if (mouseAngle < 0 && mouseAngle > -degreeLimit) mouseAngle = -degreeLimit;
        if (mouseAngle > 0 && mouseAngle < degreeLimit) mouseAngle = degreeLimit;

        finalAngle = -mouseAngle + shipAngle - 90;
    }

    IEnumerator WaitAndKill()
    {
        if (shotLaser != null)
        {
            shotLaser.GetComponent<PolygonCollider2D>().enabled = false;
            shotLaser.transform.Find("burstBody").GetComponent<ParticleSystem>().enableEmission = false;
        }
        yield return new WaitForSeconds(1);
        Destroy(shotLaser);
    }
    // Update is called once per frame
    void Update()
    {

        transform.position = locationBase.position + localPos;
        transform.rotation = locationBase.rotation;

        if (charge != maxCharge && charge != 0)
        {
            data.shipBurstChargePercent = charge / maxCharge;
        }
        if (shotLaser == null)
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
        if (isTargeting)
        {
            if (charge > 0)
            {
                charge -= 0.3f * Time.deltaTime;
                NewAngles();
                if (shotLaser != null) shotLaser.transform.eulerAngles = new Vector3(0, 0, finalAngle);
                /*
				Plane playerPlane = new Plane (Vector3.forward, transform.position);
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				float hitdist = 0;
				if (Input.GetKey (KeyCode.Mouse0)) {
					//moved this check under keydown to avoid the constant turning after the mouse.
					if (playerPlane.Raycast (ray, out hitdist)) {
						targetPosition = ray.GetPoint (hitdist);	
					}

				}*/
            }
            else
            {
                isTargeting = false;
                ranOut = true;
                StartCoroutine(WaitAndKill());
            }
            //			currTarget.transform.position = targetPosition;
        }
    }
    /*


	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().enabled = false;
		lilShip = GameObject.Find ("LILSHIP");
		data = GameObject.Find("DataHolder").GetComponent<PublicData>();
		damage = data.motherShipDamage;
		delay = data.motherShipLaserDelay;
		parent = transform.parent;
		isCharged = true;


	}

	void OnMouseDown() {
		if(GetComponent<LeftGUI>().enabled && isCharged){
			print ("OH HOLY SHIT");
			GetComponent<SpriteRenderer>().enabled = true;
			isTargeting = true;
			Plane playerPlane = new Plane (Vector3.forward, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float hitdist = 0;
			if (Input.GetKey (KeyCode.Mouse0)) {
				//moved this check under keydown to avoid the constant turning after the mouse.
				if (playerPlane.Raycast (ray, out hitdist)) {
					targetPosition = ray.GetPoint (hitdist);	
				}
			}	
			lilShip.GetComponent<LilShipControls>().enabled = false;

			currTarget = Instantiate(target, targetPosition, transform.rotation) as GameObject;
		}

	}

	void OnMouseUp(){
		if(GetComponent<LeftGUI>().enabled && isCharged){
			GetComponent<SpriteRenderer>().enabled = false;
			isTargeting = false;
			Destroy(currTarget);
			lilShip.GetComponent<LilShipControls>().enabled = true;
			FireLaser();
			isCharged = false;
			StartCoroutine(Wait ());
		}
	}
	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (delay);
		isCharged = true;
		print ("GO!");

	}
	void FireLaser()
	{
		float angle = 180 + (parent.rotation.z * 180 / Mathf.PI);
		GameObject shotLaser;
		Quaternion rot = parent.rotation;
		rot = transform.rotation;
		shotLaser = Instantiate (laser, transform.position, rot) as GameObject;
		shotLaser.name = "theShot";
		shotLaser.transform.parent = transform;
		shotLaser.GetComponent<CommonBulletStuff> ().setDamage (damage);
		shotLaser.transform.eulerAngles = new Vector3 (0, 0, angle);
		
		
	}
	// Update is called once per frame
	void Update () {
		if(isTargeting){
			Plane playerPlane = new Plane (Vector3.forward, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float hitdist = 0;
			if (Input.GetKey (KeyCode.Mouse0)) {
				//moved this check under keydown to avoid the constant turning after the mouse.
				if (playerPlane.Raycast (ray, out hitdist)) {
					targetPosition = ray.GetPoint (hitdist);	
				}
			}	

			currTarget.transform.position = targetPosition;
		}


	
	}
	*/
}
