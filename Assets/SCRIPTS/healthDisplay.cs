using UnityEngine;
using System.Collections;

public class healthDisplay : MonoBehaviour
{

    //Transform healthBar = transform.Find("thebar");
    float theSize;
    float initialHealth;
    public bool isVert = false;
    GameObject aChild;
    bool isShowing;


    // Use this for initialization
    void Start()
    {
        //theSize = gameObject.transform.localScale.x;

        aChild = transform.Find("thebar").gameObject;
        theSize = aChild.transform.localScale.x;
        aChild.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        aChild.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        isShowing = false;

    }

    public void Heal(float health)
    {
        health = theSize * health;
        aChild.transform.localScale = new Vector3(health, 1, 0);
    }

    void DisplayDamage(float damage)
    {
        damage = theSize * damage;
        aChild.transform.localScale -= new Vector3(damage, 0, 0);

        if (!isShowing)
        {
            StartCoroutine(Wait());
        }
        GetComponent<SpriteRenderer>().enabled = true;
        aChild.GetComponent<SpriteRenderer>().enabled = true;
        isShowing = true;
    }

    void DisplayValue(float value)
    {
        aChild.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        if (!isVert)
        {
            aChild.transform.localScale = new Vector3(value, 1, 0);
        }
        else
        {
            aChild.transform.localScale = new Vector3(1, value, 0);
        }


    }

    public void Reset()
    {
        aChild.transform.localScale = new Vector3(1, 1, 1);
    }
    public void SetInitialHealth(float setter)
    {
        initialHealth = setter;
    }
}
