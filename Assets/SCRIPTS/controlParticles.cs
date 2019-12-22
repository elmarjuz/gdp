using UnityEngine;
using System.Collections;

public class controlParticles : MonoBehaviour
{

    public float killtime = 0;

    // Use this for initialization
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<ParticleSystem>().enableEmission = false;

        if (killtime > 0)
        {
            GetComponent<ParticleSystem>().enableEmission = true;
            StartCoroutine(Kill());

        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartParticles()
    {
        GetComponent<ParticleSystem>().enableEmission = true;
    }

    void StopParticles()
    {
        GetComponent<ParticleSystem>().enableEmission = false;
        GetComponent<ParticleSystem>().Clear();
    }


    IEnumerator Kill()
    {
        yield return new WaitForSeconds(killtime);
        GetComponent<ParticleSystem>().enableEmission = false;
    }
}
