using UnityEngine;
using System.Collections;

public class PlayRandomSound : MonoBehaviour
{

    public AudioClip[] sounds;
    public bool playOnStart;
    // Use this for initialization
    void Start()
    {
        if (playOnStart)
        {
            PlayASound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //intended for one-time spawned sound-playing gameobjects it plays once and then dies
        if (!GetComponent<AudioSource>().isPlaying && playOnStart)
        {
            Destroy(gameObject);
        }
    }

    void PlayASound()
    {
        GetComponent<AudioSource>().clip = sounds[(int)Random.Range(0, sounds.Length)];
        GetComponent<AudioSource>().Play();
    }
}
