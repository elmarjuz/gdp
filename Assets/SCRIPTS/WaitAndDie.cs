using UnityEngine;
using System.Collections;

public class WaitAndDie : MonoBehaviour
{

    public float delay = 0.3f;
    public int randomFactor;


    // Use this for inßitialization
    void Start()
    {
        if (randomFactor > 1)
        {
            delay = Random.Range(1, randomFactor) * delay;

        }
        StartCoroutine(Wait());

    }

    IEnumerator WaitParticle()
    {
        gameObject.GetComponent<ParticleSystem>().enableEmission = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(delay);
        if (gameObject.name == "CHRISTMAS POOF(Clone)")
        {
            StartCoroutine(WaitParticle());
        }
        else
        {
            Destroy(gameObject);
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
