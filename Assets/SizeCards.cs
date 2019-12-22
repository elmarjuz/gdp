using UnityEngine;
using System.Collections;

public class SizeCards : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(Screen.height / 250, Screen.height / 250, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
