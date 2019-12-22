using UnityEngine;
using System.Collections;

public class AnimationEventElfDude : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayIdle()
    {
        GetComponent<Animator>().Play("elfDudeIdle");

    }

}
